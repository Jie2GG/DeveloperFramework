using DeveloperFramework.Library.CQP;
using DeveloperFramework.SimulatorModel.CQP;
using DeveloperFramework.Utility;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DeveloperFramework.Simulator.CQP
{
	/// <summary>
	/// 表示酷Q应用的类
	/// </summary>
	public class CQApplication
	{
		#region --字段--
		private static readonly Encoding _defaultEncoding = Encoding.UTF8;
		private static readonly Regex AppIdRegex = new Regex (@"(?:[a-z]*)\.(?:[a-z\-_]*)\.(?:[a-zA-Z0-9\.\-_]*)", RegexOptions.Compiled);
		private static readonly Regex AppInfoRegex = new Regex (@"([0-9]*),((?:[a-zA-Z0-9\.\-_]*))", RegexOptions.Compiled);

		private readonly string _appDir;
		private readonly ApiType _apiType;
		#endregion

		#region --属性--
		/// <summary>
		/// 获取当前应用的 ID
		/// </summary>
		public string AppID { get; private set; }
		/// <summary>
		/// 获取当前应用的授权码
		/// </summary>
		public int AuthCode { get; private set; }
		/// <summary>
		/// 获取当前应用的应用信息
		/// </summary>
		public AppInfo AppInfo { get; private set; }
		/// <summary>
		/// 获取当前应用的 (C/C++) 动态链接库实例
		/// </summary>
		public CQPDynamicLibrary Library { get; private set; }
		/// <summary>
		///  获取当前应用是否已经初始化完毕
		/// </summary>
		public bool IsInitialize { get; private set; }
		/// <summary>
		/// 获取当前应用是否启动
		/// </summary>
		public bool IsStartup { get; private set; }
		/// <summary>
		/// 获取当前应用是否启用
		/// </summary>
		public bool IsEnabled { get; private set; }
		#endregion

		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="CQApplication"/> 类的新实例
		/// </summary>
		/// <param name="appDirectory">应用程序所在的路径</param>
		/// <param name="apiType">指定应用的执行模式</param>
		public CQApplication (string appDirectory, ApiType apiType)
		{
			this._appDir = appDirectory;
			this._apiType = apiType;

			this.IsInitialize = false;
		}
		#endregion

		#region --公开方法--
		/// <summary>
		/// 加载应用程序
		/// </summary>
		/// <param name="appId">要加载的应用 AppID</param>	
		/// <exception cref="CQAppIDMismatchException">AppID 不符合AppID格式</exception>
		/// <exception cref="CQJsonLoadException">app.json 加载失败</exception>
		/// <exception cref="CQJsonParseException">app.json 解析失败</exception>
		/// <exception cref="CQApiVersionOldException">app.json 定义的 Api 版本不符合 apiType</exception>\
		/// <exception cref="CQLoadLibraryException">LoadLibrary 失败或应用不合法</exception>
		public void Loading (string appId)
		{
			#region 检查AppID
			if (!AppIdRegex.IsMatch (appId))
			{
				throw new CQAppIDMismatchException (appId);
			}
			#endregion

			this.AppID = appId;
			string basePath = Path.Combine (this._appDir, appId);
			string dllPath = Path.Combine (basePath, "app.dll");
			string jsonPath = Path.Combine (basePath, "app.json");

			#region 加载 app.json
			try
			{
				using (StreamReader reader = new StreamReader (jsonPath, _defaultEncoding))
				{
					this.AppInfo = JsonSerializer.Deserialize<AppInfo> (reader.ReadToEnd (), new JsonSerializerOptions () { ReadCommentHandling = JsonCommentHandling.Skip });
				}
			}
			catch (Exception)
			{
				throw new CQJsonLoadException (appId);
			}
			#endregion

			#region 检查 app.json
			if (this.AppInfo.ResultCode != 1)
			{
				throw new CQJsonParseException ();
			}

			if (this.AppInfo.ApiVersion != (int)this._apiType)
			{
				throw new CQApiVersionOldException ();
			}
			#endregion

			#region 加载 app.dll
			try
			{
				this.Library = new CQPDynamicLibrary (dllPath);
			}
			catch (DllNotFoundException ex)
			{
				throw new CQLoadLibraryException ("LoadLibrary 失败", ex);
			}
			catch (BadImageFormatException ex)
			{
				throw new CQLoadLibraryException ("不是合法应用(LoadLibrary 失败)", ex);
			}
			#endregion

			this.IsInitialize = true;
		}
		/// <summary>
		/// 启动应用程序
		/// </summary>
		/// <exception cref="CQAppInfoNotFoundException">AppInfo 函数不存在</exception>
		/// <exception cref="CQAppInfoResultException">AppInfo 返回的信息无效</exception>
		/// <exception cref="CQAppInfoResultApiException">AppInfo 返回的 Api 版本不受支持</exception>
		/// <exception cref="CQAppInfoResultAppIDException">AppInfo 返回的 AppID 异常</exception>
		/// <exception cref="CQInitializeNotFoundException">Initialize 函数不存在</exception>
		/// <exception cref="CQInitializeResultException">Initialize 函数返回的值不是 0</exception>
		/// <exception cref="EntryPointNotFoundException">Startup 函数不存在</exception>
		public void Startup ()
		{
			if (!this.IsStartup)
			{
				#region 调用 AppInfo 函数
				try
				{
					// 调用 AppInfo 函数
					string appinfo = this.Library.InvokeAppInfo () ?? string.Empty;

					Match match = AppInfoRegex.Match (appinfo);
					// 判断返回信息是否有效
					if (!match.Success)
					{
						throw new CQAppInfoResultException ();
					}

					// 判断 Api 版本是否受支持
					int apiVer = int.Parse (match.Groups[1].Value);
					if (apiVer != (int)this._apiType)
					{
						throw new CQAppInfoResultApiException (apiVer);
					}

					// 判断 AppID
					if (!this.AppID.Equals (match.Groups[2].Value))
					{
						throw new CQAppInfoResultAppIDException (this.AppID);
					}
				}
				catch (EntryPointNotFoundException ex)
				{
					throw new CQAppInfoNotFoundException (ex);
				}
				#endregion

				#region 调用 Initialize 函数
				try
				{
					// 生成全局唯一授权码
					do
					{
						byte[] buffer = Guid.NewGuid ().ToByteArray ();
						this.AuthCode = BitConverter.ToInt32 (buffer, RandomUtility.RandomInt32 (0, buffer.Length));
					} while (this.AuthCode <= 0);

					// 判断返回值
					int result = this.Library.InvokeInitialize (this.AuthCode);
					if (result != 0)
					{
						throw new CQInitializeResultException ();
					}
				}
				catch (EntryPointNotFoundException ex)
				{
					throw new CQInitializeNotFoundException (ex);
				}
				#endregion

				#region 调用 Startup 函数
				foreach (AppEvent appEvent in GetAppEvents (AppEventType.CQStartup))
				{
					try
					{
						this.Library.InvokeStartup (appEvent.Function); // 忽略返回值
					}
					catch
					{
						throw;
					}
				}
				#endregion

				this.IsStartup = true;
			}
		}
		/// <summary>
		/// 退出应用程序
		/// </summary>
		/// <exception cref="EntryPointNotFoundException">Exit 函数不存在</exception>
		public void Exit ()
		{
			if (this.IsStartup && !this.IsEnabled)  // 需要已启动并且处于禁用模式
			{
				foreach (AppEvent appEvent in GetAppEvents (AppEventType.CQExit))
				{
					try
					{
						this.Library.InvokeExit (appEvent.Function);    // 忽略返回值
					}
					catch
					{
						throw;
					}
				}

				this.IsStartup = false;
			}
		}
		/// <summary>
		/// 启用应用程序
		/// </summary>
		/// <exception cref="EntryPointNotFoundException">找不到 Enable 函数的入口</exception>
		public void Enable ()
		{
			if (!this.IsEnabled && this.IsStartup)  // 需要处于禁用模式并且已经完成了启动
			{
				foreach (AppEvent appEvent in GetAppEvents (AppEventType.CQAppEnable))
				{
					try
					{
						this.Library.InvokeAppEnable (appEvent.Function);   // 忽略返回值
					}
					catch
					{
						throw;
					}
				}

				this.IsEnabled = true;
			}
		}
		/// <summary>
		/// 禁用应用程序
		/// </summary>
		/// <exception cref="EntryPointNotFoundException">Disable 函数不存在</exception>
		public void Disable ()
		{
			if (this.IsEnabled)
			{
				foreach (AppEvent appEvent in GetAppEvents (AppEventType.CQAppDisable))
				{
					try
					{
						this.Library.InvokeAppDisable (appEvent.Function);  // 忽略返回值
					}
					catch
					{
						throw;
					}
				}

				this.IsEnabled = false;
			}
		}
		/// <summary>
		/// 推送私聊消息
		/// </summary>
		/// <param name="subType">子类型</param>
		/// <param name="msgId">消息ID</param>
		/// <param name="fromQQ">来源QQ</param>
		/// <param name="msg">消息内容</param>
		/// <param name="font">字体指针</param>
		/// <returns>指示事件是否正常结束, <see langword="true"/> 表示正常结束, <see langword="false"/> 表示事件被拦截</returns>
		/// <exception cref="EntryPointNotFoundException">PrivateMessage 函数不存在</exception>
		public bool PushPrivateMessage (PrivateMessageType subType, int msgId, long fromQQ, string msg, IntPtr font)
		{
			if (this.IsEnabled) // 应用处于启用状态才允许推送
			{
				foreach (AppEvent appEvent in this.GetAppEvents (AppEventType.PrivateMessage))
				{
					try
					{
						HandleType handleType = (HandleType)this.Library.IncokePrivateMessage (appEvent.Function, (int)subType, msgId, fromQQ, msg, font);
						if (handleType == HandleType.Intercept)
						{
							return false;
						}
					}
					catch
					{
						throw;
					}
				}
			}

			return true;
		}
		/// <summary>
		/// 推送群消息
		/// </summary>
		/// <param name="subType">子类型</param>
		/// <param name="msgId">消息ID</param>
		/// <param name="fromGroup">来源群号</param>
		/// <param name="fromQQ">来源QQ</param>
		/// <param name="fromAnonymous">来源匿名者</param>
		/// <param name="msg">消息内容</param>
		/// <param name="font">字体指针</param>
		/// <returns>指示事件是否正常结束, <see langword="true"/> 表示正常结束, <see langword="false"/> 表示事件被拦截</returns>
		/// <exception cref="EntryPointNotFoundException">GroupMessage 函数不存在</exception>
		public bool PushGroupMessage (GroupMessageType subType, int msgId, long fromGroup, long fromQQ, string fromAnonymous, string msg, IntPtr font)
		{
			if (this.IsEnabled) // 应用处于启用状态才允许推送
			{
				foreach (AppEvent appEvent in this.GetAppEvents (AppEventType.GroupMessage))
				{
					try
					{
						HandleType handleType = (HandleType)this.Library.InvokeGroupMessage (appEvent.Function, (int)subType, msgId, fromGroup, fromQQ, fromAnonymous, msg, font);
						if (handleType == HandleType.Intercept)
						{
							return false;
						}
					}
					catch
					{
						throw;
					}
				}
			}

			return true;
		}
		/// <summary>
		/// 推送讨论组消息
		/// </summary>
		/// <param name="subType">子类型</param>
		/// <param name="msgId">消息ID</param>
		/// <param name="fromDiscuss">来源讨论组</param>
		/// <param name="fromQQ">来源QQ</param>
		/// <param name="msg">消息内容</param>
		/// <param name="font">字体指针</param>
		/// <returns>指示事件是否正常结束, <see langword="true"/> 表示正常结束, <see langword="false"/> 表示事件被拦截</returns>
		/// <exception cref="EntryPointNotFoundException">DiscussMessage 函数不存在</exception>
		public bool PushDiscussMessage (DiscussMessageType subType, int msgId, long fromDiscuss, long fromQQ, string msg, IntPtr font)
		{
			if (this.IsEnabled)
			{
				foreach (AppEvent appEvent in this.GetAppEvents (AppEventType.DiscussMessage))
				{
					try
					{
						HandleType handleType = (HandleType)this.Library.InvokeDiscussMessage (appEvent.Function, (int)subType, msgId, fromDiscuss, fromQQ, msg, font);
						if (handleType == HandleType.Intercept)
						{
							return false;
						}
					}
					catch
					{
						throw;
					}
				}
			}

			return true;
		}
		/// <summary>
		/// 推送群文件上传事件
		/// </summary>
		/// <param name="subType">子类型</param>
		/// <param name="sendTime">发送时间(时间戳)</param>
		/// <param name="fromGroup">来源群号</param>
		/// <param name="fromQQ">来源QQ</param>
		/// <param name="file">上传文件信息</param>
		/// <returns>指示事件是否正常结束, <see langword="true"/> 表示正常结束, <see langword="false"/> 表示事件被拦截</returns>
		/// <exception cref="EntryPointNotFoundException">GroupUpload 函数不存在</exception>
		public bool PushGroupUpload (GroupUploadType subType, int sendTime, long fromGroup, long fromQQ, string file)
		{
			if (this.IsEnabled)
			{
				foreach (AppEvent appEvent in this.GetAppEvents (AppEventType.GroupUpload))
				{
					try
					{
						HandleType handleType = (HandleType)this.Library.InvokeGroupUpload (appEvent.Function, (int)subType, sendTime, fromGroup, fromQQ, file);
						if (handleType == HandleType.Intercept)
						{
							return false;
						}
					}
					catch
					{
						throw;
					}
				}
			}

			return true;
		}
		/// <summary>
		/// 推送群管理员变动事件
		/// </summary>
		/// <param name="subType">子类型</param>
		/// <param name="sendTime">发送时间(时间戳)</param>
		/// <param name="fromGroup">来源群号</param>
		/// <param name="operatedQQ">被操作QQ</param>
		/// <returns>指示事件是否正常结束, <see langword="true"/> 表示正常结束, <see langword="false"/> 表示事件被拦截</returns>
		/// <exception cref="EntryPointNotFoundException">GroupManagerChanged 函数不存在</exception>
		public bool PushGroupManagerChanged (GroupManagerChangedType subType, int sendTime, long fromGroup, long operatedQQ)
		{
			if (this.IsEnabled)
			{
				foreach (AppEvent appEvent in this.GetAppEvents (AppEventType.GroupManagerChanged))
				{
					try
					{
						HandleType handleType = (HandleType)this.Library.InvokeGroupManagerChanged (appEvent.Function, (int)subType, sendTime, fromGroup, operatedQQ);
						if (handleType == HandleType.Intercept)
						{
							return false;
						}
					}
					catch
					{
						throw;
					}
				}
			}

			return true;
		}
		/// <summary>
		/// 推送群成员减少事件
		/// </summary>
		/// <param name="subType">子类型</param>
		/// <param name="sendTime">发送时间(时间戳)</param>
		/// <param name="fromGroup">来源群号</param>
		/// <param name="fromQQ">操作者QQ</param>
		/// <param name="operatedQQ">被操作者QQ</param>
		/// <returns>指示事件是否正常结束, <see langword="true"/> 表示正常结束, <see langword="false"/> 表示事件被拦截</returns>
		/// <exception cref="EntryPointNotFoundException">GroupMemberDecrease 函数不存在</exception>
		public bool PushGroupMemberDecrease (GroupMemberDecreaseType subType, int sendTime, long fromGroup, long fromQQ, long operatedQQ)
		{
			if (this.IsEnabled)
			{
				foreach (AppEvent appEvent in this.GetAppEvents (AppEventType.GroupMemberDecrease))
				{
					try
					{
						HandleType handleType = (HandleType)this.Library.InvokeGroupMemberDecrease (appEvent.Function, (int)subType, sendTime, fromGroup, fromQQ, operatedQQ);
						if (handleType == HandleType.Intercept)
						{
							return false;
						}
					}
					catch
					{
						throw;
					}
				}
			}

			return true;
		}
		/// <summary>
		/// 推送群成员增加事件
		/// </summary>
		/// <param name="subType">子类型</param>
		/// <param name="sendTime">发送时间(时间戳)</param>
		/// <param name="fromGroup">来源群号</param>
		/// <param name="fromQQ">操作者QQ</param>
		/// <param name="operatedQQ">被操作者QQ</param>
		/// <returns>指示事件是否正常结束, <see langword="true"/> 表示正常结束, <see langword="false"/> 表示事件被拦截</returns>
		/// <exception cref="EntryPointNotFoundException">GroupMemberIncrease 函数不存在</exception>
		public bool PushGroupMemberIncrease (GroupMemberIncreaseType subType, int sendTime, long fromGroup, long fromQQ, long operatedQQ)
		{
			if (this.IsEnabled)
			{
				foreach (AppEvent appEvent in this.GetAppEvents (AppEventType.GroupMemberIncrease))
				{
					try
					{
						HandleType handleType = (HandleType)this.Library.InvokeGroupMemberIncrease (appEvent.Function, (int)subType, sendTime, fromGroup, fromQQ, operatedQQ);
						if (handleType == HandleType.Intercept)
						{
							return false;
						}
					}
					catch
					{
						throw;
					}
				}
			}

			return true;
		}
		/// <summary>
		/// 推送群禁言事件
		/// </summary>
		/// <param name="subType">子类型</param>
		/// <param name="sendTime">发送时间(时间戳)</param>
		/// <param name="fromGroup">来源群号</param>
		/// <param name="fromQQ">操作者QQ</param>
		/// <param name="operatedQQ">被操作QQ</param>
		/// <param name="duration">禁用时长</param>
		/// <returns>指示事件是否正常结束, <see langword="true"/> 表示正常结束, <see langword="false"/> 表示事件被拦截</returns>
		/// <exception cref="EntryPointNotFoundException">GroupBanSpeak 函数不存在</exception>
		public bool PushGroupBanSpeak (GroupBanSpeakType subType, int sendTime, long fromGroup, long fromQQ, long operatedQQ, long duration)
		{
			if (this.IsEnabled)
			{
				foreach (AppEvent appEvent in this.GetAppEvents (AppEventType.GroupBanSpeak))
				{
					try
					{
						HandleType handleType = (HandleType)this.Library.InvokeGroupBanSpeak (appEvent.Function, (int)subType, sendTime, fromGroup, fromQQ, operatedQQ, duration);
						if (handleType == HandleType.Intercept)
						{
							return false;
						}
					}
					catch
					{
						throw;
					}
				}
			}

			return true;
		}
		/// <summary>
		/// 推送好友已添加事件
		/// </summary>
		/// <param name="subType">子类型</param>
		/// <param name="sendTime">发送时间(时间戳)</param>
		/// <param name="fromQQ">来源QQ</param>
		/// <returns>指示事件是否正常结束, <see langword="true"/> 表示正常结束, <see langword="false"/> 表示事件被拦截</returns>
		/// <exception cref="EntryPointNotFoundException">FriendAdd 函数不存在</exception>
		public bool PushFriendAdd (FriendAddType subType, int sendTime, long fromQQ)
		{
			if (this.IsEnabled)
			{
				foreach (AppEvent appEvent in this.GetAppEvents (AppEventType.FriendAdd))
				{
					try
					{
						HandleType handleType = (HandleType)this.Library.InvokeFriendAdd (appEvent.Function, (int)subType, sendTime, fromQQ);
						if (handleType == HandleType.Intercept)
						{
							return false;
						}
					}
					catch
					{
						throw;
					}
				}
			}

			return true;
		}
		/// <summary>
		/// 推送好友添加请求事件
		/// </summary>
		/// <param name="subType">子类型</param>
		/// <param name="sendTime">发送时间</param>
		/// <param name="fromQQ">来源QQ</param>
		/// <param name="appendMsg">附加消息</param>
		/// <param name="responseFlag">反馈标识</param>
		/// <returns>指示事件是否正常结束, <see langword="true"/> 表示正常结束, <see langword="false"/> 表示事件被拦截</returns>
		/// <exception cref="EntryPointNotFoundException">FriendAddRequest 函数不存在</exception>
		public bool PushFriendAddRequest (FriendAddRequestType subType, int sendTime, long fromQQ, string appendMsg, string responseFlag)
		{
			if (this.IsEnabled)
			{
				foreach (AppEvent appEvent in this.GetAppEvents (AppEventType.FriendAddRequest))
				{
					try
					{
						HandleType handleType = (HandleType)this.Library.InvokeFriendAddRequest (appEvent.Function, (int)subType, sendTime, fromQQ, appendMsg, responseFlag);
						if (handleType == HandleType.Intercept)
						{
							return false;
						}
					}
					catch
					{
						throw;
					}
				}
			}

			return true;
		}
		/// <summary>
		/// 推送好友添加请求事件
		/// </summary>
		/// <param name="subType">子类型</param>
		/// <param name="sendTime">发送时间</param>
		/// <param name="fromGroup">来源群号</param>
		/// <param name="fromQQ">来源QQ</param>
		/// <param name="appendMsg">附加消息</param>
		/// <param name="responseFlag">反馈标识</param>
		/// <returns>指示事件是否正常结束, <see langword="true"/> 表示正常结束, <see langword="false"/> 表示事件被拦截</returns>
		/// <exception cref="EntryPointNotFoundException">GroupAddRequest 函数不存在</exception>
		public bool PushGroupAddRequest (GroupAddRequestType subType, int sendTime, long fromGroup, long fromQQ, string appendMsg, string responseFlag)
		{
			if (this.IsEnabled)
			{
				foreach (AppEvent appEvent in this.GetAppEvents (AppEventType.FriendAddRequest))
				{
					try
					{
						HandleType handleType = (HandleType)this.Library.InvokeGroupAddRequest (appEvent.Function, (int)subType, sendTime, fromGroup, fromQQ, appendMsg, responseFlag);
						if (handleType == HandleType.Intercept)
						{
							return false;
						}
					}
					catch
					{
						throw;
					}
				}
			}

			return true;
		}
		#endregion

		#region --私有方法--
		/// <summary>
		/// 获取指定事件按照 ID 和优先级排序后的结果
		/// </summary>
		/// <param name="type">事件类型</param>
		/// <returns>事件列表</returns>
		private IEnumerable<AppEvent> GetAppEvents (AppEventType type)
		{
			return from temp in this.AppInfo.Events
				   where temp.Type == type
				   orderby temp.Id
				   orderby temp.Priority
				   select temp;
		}
		#endregion
	}
}
