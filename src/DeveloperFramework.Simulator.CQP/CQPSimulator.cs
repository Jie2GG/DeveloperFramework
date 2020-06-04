using DeveloperFramework.CQP;
using DeveloperFramework.Library.CQP;
using DeveloperFramework.LibraryModel.CQP;
using DeveloperFramework.Log.CQP;
using DeveloperFramework.Simulator.CQP.Domain;
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
using System.Threading.Tasks;
using System.Timers;

namespace DeveloperFramework.Simulator.CQP
{
	/// <summary>
	/// 表示 CQP 应用模拟器
	/// </summary>
	public class CQPSimulator : IFuncProcess
	{
		#region --常量--
		public const string TYPE_INIT = "初始化";
		public const string TYPE_GROUP_MESSAGE = "[↓]群组消息";
		public const string TYPE_PRIVATE_MESSAGE_STATUS = "[↓]私聊消息(在线状态)";
		public const string TYPE_PRIVATE_MESSAGE_FRIENDS = "[↓]私聊消息(好友)";
		public const string TYPE_APP_LOAD = "应用加载";
		public const string TYPE_APP_UNLOAD = "应用卸载";
		#endregion

		#region --字段--
		private static readonly Regex _appIdRegex = new Regex (@"(?:[a-z]*)\.(?:[a-z\-_]*)\.(?:[a-zA-Z0-9\.\-_]*)", RegexOptions.Compiled);
		private readonly ConcurrentQueue<TaskContext> _taskContexts;
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
		public string DevAppDirectory { get; }
		/// <summary>
		/// 获取当前实例的已加载的应用路径
		/// </summary>
		public string RunningAppDirectory { get; }
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
		/// 获取当前实例是否许可发送图片
		/// </summary>
		public bool CanSendImage { get; }
		/// <summary>
		/// 获取当前实例是否许可发送语音
		/// </summary>
		public bool CanSendRecord { get; }
		/// <summary>
		/// 获取当前实例的唯一码
		/// </summary>
		public string InstanceHash { get; }
		#endregion

		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="CQPSimulator"/> 类的新实例
		/// </summary>
		/// <param name="appDirectory">应用路径</param>
		public CQPSimulator (string coolqVersion = "v9pro")
		{
			this.InstanceHash = DateTime.Now.Ticks.ToString ("X").ToLower ();
			this.AppDirectory = Path.Combine (AppDomain.CurrentDomain.BaseDirectory, "app");
			this.DevAppDirectory = Path.Combine (AppDomain.CurrentDomain.BaseDirectory, "dev");
			this.DataDirectory = Path.Combine (AppDomain.CurrentDomain.BaseDirectory, "data");
			this.ConfDirectory = Path.Combine (AppDomain.CurrentDomain.BaseDirectory, "conf");
			this.BinDirectory = Path.Combine (AppDomain.CurrentDomain.BaseDirectory, "bin");
			this.RunningAppDirectory = Path.Combine (this.DataDirectory, "tmp", "capp", InstanceHash);
			Directory.CreateDirectory (this.AppDirectory);
			Directory.CreateDirectory (this.DevAppDirectory);
			Directory.CreateDirectory (this.ConfDirectory);
			Directory.CreateDirectory (this.BinDirectory);
			Directory.CreateDirectory (this.DataDirectory);
			Directory.CreateDirectory (Path.Combine (this.DataDirectory, "app"));
			Directory.CreateDirectory (Path.Combine (this.DataDirectory, "bface"));
			Directory.CreateDirectory (Path.Combine (this.DataDirectory, "image"));
			Directory.CreateDirectory (Path.Combine (this.DataDirectory, "log"));
			Directory.CreateDirectory (Path.Combine (this.DataDirectory, "record"));
			Directory.CreateDirectory (Path.Combine (this.DataDirectory, "show"));
			Directory.CreateDirectory (Path.Combine (this.DataDirectory, "tmp"));
			Directory.CreateDirectory (Path.Combine (this.DataDirectory, "tmp", "capp"));
			Directory.CreateDirectory (this.RunningAppDirectory);
			Directory.CreateDirectory (Path.Combine (this.DataDirectory, $"{this.DataPool.RobotQQ.Id}"));
			LogCenter.Instance.InfoSuccess (TYPE_INIT, "已生成目录结构");

			switch (coolqVersion)
			{
				case "v9pro":
					CanSendImage = true;
					CanSendRecord = true;
					break;
				case "v9air":
					CanSendImage = false;
					CanSendRecord = false;
					break;
				case "lite":
					CanSendImage = false;
					CanSendRecord = false;
					break;
			}
			LogCenter.Instance.InfoSuccess (TYPE_INIT, $"已根据版本:{coolqVersion} 定义模拟器特性");

			// 初始化数据池
			this.CQPApps = new List<CQPSimulatorApp> ();
			this.DataPool = new CQPSimulatorDataPool ().Generate ();
			LogCenter.Instance.InfoSuccess (TYPE_INIT, $"已加载 {this.DataPool.QQCollection.Count} 个QQ、{this.DataPool.FriendCollection.Count} 个好友、{this.DataPool.GroupCollection.Count} 个群、{this.DataPool.DiscussCollection.Count} 个讨论组");

			// 初始化任务队列
			this._taskContexts = new ConcurrentQueue<TaskContext> ();
			LogCenter.Instance.InfoSuccess (TYPE_INIT, $"已创建任务队列");

			// 设置 CQExport 服务
			CQPExport.Instance.FuncProcess = this;
		}
		
		/// <summary>
		/// 初始化 <see cref="CQPSimulator"/> 类的新实例
		/// </summary>
		/// <param name="simulatorType">初始化新模拟器的类型</param>
		public CQPSimulator (CQPSimulatorType simulatorType)
		{

		}
		#endregion

		#region --公开方法--
		/// <summary>
		/// 启动 <see cref="CQPSimulator"/>
		/// </summary>
		public void Start ()
		{
			if (this._isStart)
			{
				return;
			}

			LogCenter.Instance.InfoSuccess (TYPE_APP_LOAD, "应用加载开始");
			string[] pathes = Directory.GetDirectories (this.DevAppDirectory);
			int failCount = 0;
			foreach (string path in pathes)
			{
				string appId = Path.GetFileName (path);     // 获取最后一段字符串
				if (!_appIdRegex.IsMatch (appId))
				{
					continue;   // 跳过加载
				}

				string dllName = Path.Combine (path, "app.dll");
				string jsonName = Path.Combine (path, "app.json");

				try
				{
					CQPDynamicLibrary library = new CQPDynamicLibrary (dllName, jsonName);
					string appInfo = library.InvokeAppInfo ();
					if (!appInfo.Equals ($"{library.AppInfo.ApiVersion},{appId}"))
					{
						// 卸载 Library
						library.Dispose ();
						// 写入日志
						LogCenter.Instance.Warning (TYPE_APP_LOAD, $"应用: {appId} 返回的 AppID 错误.");
						failCount++;
						continue;
					}

					int authCode = RandomUtility.RandomInt32 (0);
					// 传递验证码
					int resCode = library.InvokeInitialize (authCode);
					if (resCode != 0)
					{
						// 卸载 Library
						library.Dispose ();
						// 写入日志
						LogCenter.Instance.Error (TYPE_APP_LOAD, $"应用 {appId} 的 Initialize 方法未返回 0");
						failCount++;
						continue;
					}

					// 存入实例列表
					this.CQPApps.Add (new CQPSimulatorApp (authCode, appId, library));

					LogCenter.Instance.InfoSuccess (TYPE_APP_LOAD, $"应用: {appId} 加载成功");
				}
				catch (Exception ex)
				{
					LogCenter.Instance.Warning (TYPE_APP_LOAD, $"应用: {appId} 加载失败, 原因: {ex.Message}");
				}
			}

			LogCenter.Instance.InfoSuccess (TYPE_APP_LOAD, $"应用加载结束. 加载成功: {this.CQPApps.Count} 个, 失败: {failCount} 个");
			this._isStart = true;
		}
		/// <summary>
		/// 停止 <see cref="CQPSimulator"/>
		/// </summary>
		public void Stop ()
		{
			if (!this._isStart)
			{
				return;
			}

			LogCenter.Instance.InfoSuccess (TYPE_APP_UNLOAD, $"应用卸载开始");
			for (int i = 0; i < this.CQPApps.Count; i++)
			{
				CQPSimulatorApp app = this.CQPApps[i];

				string appId = app.AppId;

				// 调用 CQExit 函数
				foreach (AppEvent appEvent in app.Library.AppInfo.Events.Where (temp => temp.Type == AppEventType.CQExit))
				{
					try
					{
						app.Library.InvokeCQExit (appEvent);
					}
					catch (Exception ex)
					{
						LogCenter.Instance.Error (TYPE_APP_UNLOAD, $"应用: {appId} 卸载失败, 原因: {ex.Message}");
					}
				}

				LogCenter.Instance.InfoSuccess (TYPE_APP_UNLOAD, $"应用: {appId} 卸载成功");

				// 销毁对象
				app.Library.Dispose ();
			}
			LogCenter.Instance.InfoSuccess (TYPE_APP_UNLOAD, $"应用卸载结束");
			this._isStart = false;
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
				LogCenter.Instance.Error ($"检测到非法的 Api 调用, 已阻止. 请确保调用的 Api 使用了 Initialize 下发的授权码");
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


		/// <summary>
		/// 向 <see cref="CQPSimulator"/> 投递一个任务
		/// </summary>
		/// <param name="context">描述任务的上下文</param>
		public void AddTask (TaskContext context)
		{
			this._taskContexts.Enqueue (context);
		}

		/// <summary>
		/// 群组消息事件调用
		/// </summary>
		public void GroupMessage (int msgId, long fromGroup, long fromQQ, string fromAnonymous, string msg, IntPtr font)
		{
			LogCenter.Instance.InfoSuccess (TYPE_GROUP_MESSAGE, $"群: {fromGroup} 帐号: {fromQQ} {msg}");
			for (int i = 0; i < this.CQPApps.Count; i++)
			{
				CQPSimulatorApp app = this.CQPApps[i];

				string appId = app.AppId;

				// 调用 CQExit 函数
				foreach (AppEvent appEvent in app.Library.AppInfo.Events.Where (temp => temp.Type == AppEventType.GroupMessage))
				{
					try
					{
						app.Library.InvokeCQGroupMessage (appEvent, GroupMessageType.Group, msgId, fromGroup, fromQQ, fromAnonymous, msg, font);
					}
					catch (Exception ex)
					{
						LogCenter.Instance.Error (TYPE_APP_UNLOAD, $"应用: {appId} 事件调用失败, 原因: {ex.Message}");
					}
				}
			}
		}
		#endregion

		#region --私有方法--
		private string[] GetLibrarys ()
		{

		}
		#endregion
	}
}
