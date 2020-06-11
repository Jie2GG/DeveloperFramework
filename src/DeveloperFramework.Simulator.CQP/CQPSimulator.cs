using DeveloperFramework.CQP;
using DeveloperFramework.Extension;
using DeveloperFramework.Library.CQP;
using DeveloperFramework.LibraryModel.CQP;
using DeveloperFramework.Log.CQP;
using DeveloperFramework.Simulator.CQP.Domain;
using DeveloperFramework.Simulator.CQP.Domain.Context;
using DeveloperFramework.Simulator.CQP.Domain.Expositor;
using DeveloperFramework.Simulator.CQP.Domain.Expression;
using DeveloperFramework.SimulatorModel.CQP;
using DeveloperFramework.Utility;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace DeveloperFramework.Simulator.CQP
{
	/// <summary>
	/// 表示 CQP 应用模拟器
	/// </summary>
	public class CQPSimulator : IFuncProcess
	{
		#region --字段--
		private static readonly Regex AppIdRegex = new Regex (@"(?:[a-z]*)\.(?:[a-z\-_]*)\.(?:[a-zA-Z0-9\.\-_]*)", RegexOptions.Compiled);
		private static readonly Regex AppInfoRegex = new Regex (@"([0-9]*),((?:[a-zA-Z0-9\.\-_]*))", RegexOptions.Compiled);
		private static readonly List<TaskExpression> TaskExpressions = new List<TaskExpression> ();
		private Task _taskProcess;
		private CancellationTokenSource _taskSource;
		private ConcurrentQueue<TaskContext> _taskContexts;
		private bool _isStart = false;
		#endregion

		#region --属性--
		/// <summary>
		/// 获取当前实例加载的 <see cref="CQPDynamicLibrary"/> 集合
		/// </summary>
		public List<CQPSimulatorApp> CQPApps { get; }
		/// <summary>
		/// 获取当前实例的数据池 <see cref="CQPSimulatorDataPool"/>
		/// </summary>
		public CQPSimulatorDataPool DataPool { get; }
		/// <summary>
		/// 获取当前实例的应用路径
		/// </summary>
		public string AppDirectory { get; }
		/// <summary>
		/// 获取当前实例的开发应用路径
		/// </summary>
		public string DevDirectory { get; }
		/// <summary>
		/// 获取当前实例的应用数据路径
		/// </summary>
		public string DataDirectory { get; }
		/// <summary>
		/// 获取当前实例的设置项路径
		/// </summary>
		public string ConfDirectory { get; }
		/// <summary>
		/// 获取当前实例的组件路径
		/// </summary>
		public string BinDirectory { get; }
		/// <summary>
		/// 获取当前实例的应用数据路径
		/// </summary>
		public string DataAppDirectory { get; }
		/// <summary>
		/// 获取当前实例的大表情目录
		/// </summary>
		public string DataBfaceDirectory { get; }
		/// <summary>
		/// 获取当前实例的进场特效目录
		/// </summary>
		public string DataShowDirectory { get; }
		/// <summary>
		/// 获取当前实例的图片目录
		/// </summary>
		public string DataImageDirectory { get; }
		/// <summary>
		/// 获取当前实例的语音目录
		/// </summary>
		public string DataRecordDirectory { get; }
		/// <summary>
		/// 获取当前实例的日志目录
		/// </summary>
		public string DataLogDirectory { get; }
		/// <summary>
		/// 获取当前实例的临时目录
		/// </summary>
		public string DataTmpDirectory { get; }
		/// <summary>
		/// 获取当前实例的临时应用存储路径
		/// </summary>
		public string AppRunTimeDirectory { get; }
		/// <summary>
		/// 获取当前实例的模拟器类型
		/// </summary>
		public CQPType CQPType { get; }
		/// <summary>
		/// 获取当前实例的 Api 版本
		/// </summary>
		public ApiType ApiType { get; }
		#endregion

		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="CQPSimulator"/> 类的新实例
		/// </summary>
		/// <param name="cqpType">初始化新模拟器的类型</param>
		public CQPSimulator (CQPType cqpType, ApiType apiType)
		{
			#region 初始化数据池
			this.CQPApps = new List<CQPSimulatorApp> ();
			this.DataPool = new CQPSimulatorDataPool ().Generate ();
			Logger.Instance.InfoSuccess (CQPErrorCode.TYPE_INIT, $"已加载 {this.DataPool.QQCollection.Count} 个QQ、{this.DataPool.FriendCollection.Count} 个好友、{this.DataPool.GroupCollection.Count} 个群、{this.DataPool.DiscussCollection.Count} 个讨论组");
			#endregion

			#region 初始化目录
			string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
			this.AppDirectory = Path.Combine (baseDirectory, "app");    // app 目录
			this.DevDirectory = Path.Combine (baseDirectory, "dev");    // dev 目录
			this.DataDirectory = Path.Combine (baseDirectory, "data");  // data 目录
			this.ConfDirectory = Path.Combine (baseDirectory, "conf");  // conf 目录
			this.BinDirectory = Path.Combine (baseDirectory, "bin");    // bin 目录
			Directory.CreateDirectory (this.AppDirectory);
			Directory.CreateDirectory (this.DevDirectory);
			Directory.CreateDirectory (this.DataDirectory);
			Directory.CreateDirectory (this.ConfDirectory);
			Directory.CreateDirectory (this.BinDirectory);

			this.DataAppDirectory = Path.Combine (this.DataDirectory, "app");       // data/app 目录
			this.DataBfaceDirectory = Path.Combine (this.DataDirectory, "bface");     // data/bface 目录
			this.DataShowDirectory = Path.Combine (this.DataDirectory, "show");      // data/show 目录
			this.DataImageDirectory = Path.Combine (this.DataDirectory, "image");     // data/image 目录
			this.DataRecordDirectory = Path.Combine (this.DataDirectory, "record");    // data/record 目录
			this.DataLogDirectory = Path.Combine (this.DataDirectory, "log");       // data/log 目录
			this.DataTmpDirectory = Path.Combine (this.DataDirectory, "tmp");       // data/tmp 目录
			Directory.CreateDirectory (this.DataAppDirectory);
			Directory.CreateDirectory (this.DataBfaceDirectory);
			Directory.CreateDirectory (this.DataImageDirectory);
			Directory.CreateDirectory (this.DataRecordDirectory);
			Directory.CreateDirectory (this.DataLogDirectory);
			Directory.CreateDirectory (this.DataTmpDirectory);

			this.AppRunTimeDirectory = Path.Combine (this.DataTmpDirectory, "capp");    // data/tmp/capp 目录
			Directory.CreateDirectory (this.AppRunTimeDirectory);
			Logger.Instance.InfoSuccess (CQPErrorCode.TYPE_INIT, "已创建数据目录");
			#endregion

			#region 初始化版本
			this.CQPType = cqpType;
			this.ApiType = apiType;
			Logger.Instance.InfoSuccess (CQPErrorCode.TYPE_INIT, $"{cqpType.GetDescription ()}, {apiType.GetDescription ()}");
			#endregion

			#region 初始化任务队列
			this._taskSource = new CancellationTokenSource ();
			this._taskProcess = new Task (this.ProcessTask, this._taskSource.Token);
			this._taskContexts = new ConcurrentQueue<TaskContext> ();
			// 任务表达式
			TaskExpressions.Add (new GroupMessageExpression (this));
			Logger.Instance.InfoSuccess (CQPErrorCode.TYPE_INIT, $"已创建任务队列");
			#endregion

			#region 设置服务
			// 设置 CQExport 服务
			CQPExport.Instance.FuncProcess = this;
			#endregion
		}
		#endregion

		#region --公开方法--
		/// <summary>
		/// 启动 <see cref="CQPSimulator"/>
		/// </summary>
		public void Start ()
		{
			if (!this._isStart)
			{
				this._isStart = true;

				Logger.Instance.Info ("正在启动任务...");
				this._taskProcess.Start ();

				Logger.Instance.Info ("已启动模拟器");
				// 加载应用
				this.LoadApps ();

				foreach (CQPSimulatorApp app in this.CQPApps)
				{
					// 读取应用设置, 判断是否需要继续启用应用
					bool startApp = true;

					if (startApp)
					{
						EnableApp (app);
					}
				}
			}
		}
		/// <summary>
		/// 停止 <see cref="CQPSimulator"/>
		/// </summary>
		public void Stop ()
		{
			if (this._isStart)
			{
				this._isStart = false;

				Logger.Instance.Info ("正在准备退出模拟器...");
				UnloadApps ();
				Logger.Instance.Info ("已退出模拟器");
			}
		}
		/// <summary>
		/// 启用指定的应用
		/// </summary>
		/// <param name="app">要启用的应用</param>
		public void EnableApp (CQPSimulatorApp app)
		{
			CQPDynamicLibrary library = app.Library;
			if (!library.IsEnable)
			{
				if (!library.IsInitialized)
				{
					InitializeApp (app);
				}

				IEnumerable<AppEvent> events = GetEvents (app, AppEventType.CQAppEnable);
				foreach (AppEvent appEvent in events)
				{
					try
					{
						library.InvokeCQAppEnable (appEvent);   // 忽略返回值
					}
					catch (Exception ex)
					{
						Logger.Instance.Error (library.AppInfo.Name, $"{ex.Message}");
					}
				}
			}
		}
		/// <summary>
		/// 禁用指定的应用
		/// </summary>
		/// <param name="app">要禁用的应用</param>
		public void DisableApp (CQPSimulatorApp app)
		{
			CQPDynamicLibrary library = app.Library;
			if (library.IsEnable)
			{
				IEnumerable<AppEvent> events = GetEvents (app, AppEventType.CQAppDisable);
				foreach (AppEvent appEvent in events)
				{
					try
					{
						library.InvokeCQAppDisable (appEvent);  // 忽略返回值
					}
					catch (Exception ex)
					{
						Logger.Instance.Error (library.AppInfo.Name, $"{ex.Message}");
					}
				}
			}
		}
		/// <summary>
		/// 推送群消息
		/// </summary>
		/// <param name="subType">消息子类型</param>
		/// <param name="msgId">消息Id</param>
		/// <param name="fromGroup">来源群号</param>
		/// <param name="fromQQ">来源QQ</param>
		/// <param name="fromAnonymous">来源匿名者</param>
		/// <param name="msg">消息内容</param>
		/// <param name="font">字体指针</param>
		public void PushGroupMessage (GroupMessageType subType, int msgId, long fromGroup, long fromQQ, string fromAnonymous, string msg, IntPtr font)
		{
			int id = Logger.Instance.BeginInfoReceive (CQPErrorCode.TYPE_MESSAGE_GROUP, $"群: {fromGroup} 账号: {fromQQ} {msg}");

			foreach (CQPSimulatorApp app in this.CQPApps)
			{
				CQPDynamicLibrary library = app.Library;
				if (library.IsEnable)
				{
					IEnumerable<AppEvent> events = GetEvents (app, AppEventType.GroupMessage);
					foreach (AppEvent appEvent in events)
					{
						try
						{
							if (library.InvokeCQGroupMessage (appEvent, subType, msgId, fromGroup, fromQQ, fromAnonymous, msg, font) == HandleType.Intercept)
							{
								break;  // 返回拦截消息, 跳出循环
							}
						}
						catch (Exception ex)
						{
							Logger.Instance.Error (library.AppInfo.Name, $"{ex.Message}");
						}
					}
				}
			}

			Logger.Instance.EndLog (id, LogState.Success);
		}
		/// <summary>
		/// 向 <see cref="CQPSimulator"/> 投递一个任务
		/// </summary>
		/// <param name="context">描述任务的上下文</param>
		public void AddTask (TaskContext context)
		{
			this._taskContexts.Enqueue (context);
		}
		/// <summary>
		/// 获取函数处理过程
		/// </summary>
		/// <param name="authCode">应用授权码</param>
		/// <param name="funcName">函数名称</param>
		/// <param name="objs">函数参数列表</param>
		/// <returns>返回值</returns>
		public object GetProcess (int authCode, [CallerMemberName] string funcName = null, params object[] objs)
		{
			// 获取方法
			MethodInfo method = typeof (CQPExport).GetMethod (funcName, BindingFlags.Static | BindingFlags.Public);
			if (method == null)
			{
				throw new MissingMethodException (nameof (CQPExport), funcName);
			}

			// 获取应用实例
			CQPSimulatorApp app = this.CQPApps.Where (temp => temp.AuthCode == authCode).FirstOrDefault ();
			bool isAuth = false;

			if (app == null)
			{
				Logger.Instance.Error ($"检测到非法的 Api 调用, 已阻止. 请确保调用的 Api 使用了 Initialize 下发的授权码");
			}
			else
			{
				isAuth = true;  // 默认允许调用

				// 获取方法是否标记了应用权限校验
				CQPAuthAttribute auth = method.GetCustomAttribute<CQPAuthAttribute> ();
				if (auth != null)
				{
					// 校验权限
					isAuth = app.Library.AppInfo.Auth.Contains (auth.AppAuth);
				}
			}

			return new CommandRouteInvoker (this, app, isAuth).GetCommandHandle (funcName, objs).Execute ();
		}
		#endregion

		#region --私有方法--
		/// <summary>
		/// 加载应用列表
		/// </summary>
		private void LoadApps ()
		{
			Logger.Instance.Info (CQPErrorCode.TYPE_APP, "正在加载应用模块...");

			// 读取所有文件夹
			foreach (string path in Directory.GetDirectories (this.DevDirectory))
			{
				string appId = Path.GetFileName (path); // 获取最后一段字符串
				if (!AppIdRegex.IsMatch (appId))   // 不匹配的 AppID
				{
					Logger.Instance.Warning (CQPErrorCode.TYPE_APP_LOAD_FAIL, CQPErrorCode.ERROR_APPID_IRREGULAR, "{0} 加载失败！错误：AppID({0})不符合AppID格式，请阅读开发文档进行修改", appId);
					continue;
				}

				string dllName = Path.Combine (path, "app.dll");
				string jsonName = Path.Combine (path, "app.json");

				try
				{
					// 加载动态库
					CQPDynamicLibrary library = new CQPDynamicLibrary (dllName, jsonName);

					// 检查返回码
					if (library.AppInfo.ResultCode != 1)
					{
						library.Dispose ();
						Logger.Instance.Warning (CQPErrorCode.TYPE_APP_LOAD_FAIL, CQPErrorCode.ERROR_JSON_PARSE_FAIL, "{0} 加载失败! 错误: 应用信息Json串解析失败，请检查Json串是否正确", appId);
						continue;
					}
					// 检查 Api 版本
					if (library.AppInfo.ApiVersion != (int)this.ApiType)
					{
						library.Dispose ();
						Logger.Instance.Warning (CQPErrorCode.TYPE_APP_LOAD_FAIL, CQPErrorCode.ERROR_API_OLD, "{0} 加载失败！错误：Api版本过旧，请获取新版应用或联系作者更新", appId);
						continue;
					}

					this.CQPApps.Add (new CQPSimulatorApp (appId, library));
					Logger.Instance.InfoSuccess (CQPErrorCode.TYPE_APP, $"应用 {library.AppInfo.Name}({appId}) 加载成功!");
				}
				catch (FileNotFoundException)
				{
					Logger.Instance.Warning (CQPErrorCode.TYPE_APP_LOAD_FAIL, CQPErrorCode.ERROR_JSON_READ_FAIL, "{0} 加载失败! 错误: {0}\\app.json文件读取失败", appId);
				}
			}

			Logger.Instance.Info (CQPErrorCode.TYPE_APP, $"成功加载 {this.CQPApps.Count} 个应用");
		}
		/// <summary>
		/// 卸载应用列表
		/// </summary>
		private void UnloadApps ()
		{
			Logger.Instance.Info (CQPErrorCode.TYPE_APP, "正在卸载应用...");
			foreach (CQPSimulatorApp app in this.CQPApps)
			{
				UninitializeApp (app);
				app.Library.Dispose (); // 销毁应用
			}
			Logger.Instance.Info (CQPErrorCode.TYPE_APP, "应用卸载完成");
		}
		/// <summary>
		/// 初始化指定的应用
		/// </summary>
		/// <param name="app">要初始化的应用</param>
		private void InitializeApp (CQPSimulatorApp app)
		{
			// 获取动态库实例
			CQPDynamicLibrary library = app.Library;

			if (!library.IsInitialized)
			{
				string appId = app.AppId;
				// 检查返回码
				if (library.AppInfo.ResultCode != 1)
				{
					library.Dispose ();

					Logger.Instance.Warning (CQPErrorCode.TYPE_APP_LOAD_FAIL, CQPErrorCode.ERROR_JSON_PARSE_FAIL, "{0} 加载失败! 错误: 应用信息Json串解析失败，请检查Json串是否正确", appId);
				}

				// 检查 Api 版本
				if (library.AppInfo.ApiVersion != (int)this.ApiType)
				{
					library.Dispose ();

					Logger.Instance.Warning (CQPErrorCode.TYPE_APP_LOAD_FAIL, CQPErrorCode.ERROR_API_OLD, "{0} 加载失败！错误：Api版本过旧，请获取新版应用或联系作者更新", appId);
				}

				// 检查应用返回信息
				try
				{
					string appInfo = library.InvokeAppInfo () ?? string.Empty;
					// 解析返回信息
					Match match = AppInfoRegex.Match (appInfo);
					if (!match.Success)
					{
						library.Dispose ();

						Logger.Instance.Warning (CQPErrorCode.TYPE_APP_LOAD_FAIL, CQPErrorCode.ERROR_APPINFO_PARSE_FAIL, "{0} 加载失败！错误：AppInfo返回信息解析错误", appId);
					}

					// 解析 Api版本
					if (int.Parse (match.Groups[1].Value) != (int)this.ApiType)
					{
						library.Dispose ();

						Logger.Instance.Warning (CQPErrorCode.TYPE_APP_LOAD_FAIL, CQPErrorCode.ERROR_APPINFO_NOT_LOAD, "{0} 加载失败！错误：AppInfo返回的Api版本不支持直接加载，仅支持Api版本为9(及以上)的应用直接加载", appId);
					}

					// 解析 AppID
					if (!appId.Equals (match.Groups[2].Value))
					{
						library.Dispose ();

						Logger.Instance.Warning (CQPErrorCode.TYPE_APP_LOAD_FAIL, CQPErrorCode.ERROR_APPINFO_DIFFERENT, "{0} 加载失败！错误：{0}目录名的AppID与AppInfo返回的AppID不同", appId);
					}
				}
				catch (Exception)
				{
					library.Dispose ();

					Logger.Instance.Warning (CQPErrorCode.TYPE_APP_LOAD_FAIL, CQPErrorCode.ERROR_APPINFO_NOT_FOUNT, "{0} 加载失败！错误：AppInfo函数不存在或错误", appId);
				}

				// 传递 AuthCode
				try
				{
					if (library.InvokeInitialize (app.AuthCode) != 0)
					{
						Logger.Instance.Warning (CQPErrorCode.TYPE_APP_LOAD_FAIL, CQPErrorCode.ERROR_AUTHCODE_FAIL, "{0} 加载失败！错误：Api授权接收函数(Initialize)返回值非0", appId);
					}
				}
				catch (Exception ex)
				{
					Logger.Instance.Error (library.AppInfo.Name, $"{ex.Message}");
				}
			}

			// 初始化应用
			if (!library.IsStartup)
			{
				IEnumerable<AppEvent> events = GetEvents (app, AppEventType.CQStartup);
				foreach (AppEvent appEvent in events)
				{
					try
					{
						app.Library.InvokeCQStartup (appEvent); // 忽略返回值
					}
					catch (Exception ex)
					{
						Logger.Instance.Error (library.AppInfo.Name, $"{ex.Message}");
					}
				}
			}
		}
		/// <summary>
		/// 反初始化指定的应用
		/// </summary>
		/// <param name="app">要反初始化的应用</param>
		private void UninitializeApp (CQPSimulatorApp app)
		{
			// 获取动态库实例
			CQPDynamicLibrary library = app.Library;

			if (library.IsInitialized && library.IsExit == false)
			{
				if (library.IsEnable)
				{
					DisableApp (app);
				}

				IEnumerable<AppEvent> events = GetEvents (app, AppEventType.CQExit);
				foreach (AppEvent appEvent in events)
				{
					try
					{
						library.InvokeCQExit (appEvent);    // 忽略返回值
					}
					catch (Exception ex)
					{
						Logger.Instance.Error (library.AppInfo.Name, $"{ex.Message}");
					}
				}
			}
		}
		/// <summary>
		/// 处理任务回调
		/// </summary>
		private void ProcessTask ()
		{
			// 初始化任务工厂, 并设置任务以顺序运行
			TaskFactory<bool> factory = new TaskFactory<bool> (TaskCreationOptions.PreferFairness, TaskContinuationOptions.PreferFairness);

			while (this._isStart)
			{
				// 获取任务
				if (this._taskContexts.TryDequeue (out TaskContext context))
				{
					foreach (TaskExpression expression in CQPSimulator.TaskExpressions)
					{
						factory.StartNew (() =>
						{
							return expression.Interpret (context);
						});
					}
				}
			}
		}
		/// <summary>
		/// 获取指定事件按照 ID 和优先级排序后的结果
		/// </summary>
		/// <param name="events">要获取的事件源的应用</param>
		/// <param name="type">事件类型</param>
		/// <returns>事件列表</returns>
		private static IEnumerable<AppEvent> GetEvents (CQPSimulatorApp app, AppEventType type)
		{
			return from temp in app.Library.AppInfo.Events
				   where temp.Type == type
				   orderby temp.Id
				   orderby temp.Priority
				   select temp;
		}
		#endregion
	}
}
