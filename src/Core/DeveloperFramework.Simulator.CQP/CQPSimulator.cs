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
		public const string STR_SIMULATOR_INIT = "初始化";
		public const string STR_APP_LOAD = "应用加载";
		public const string STR_APP_UNLOAD = "应用卸载";
		public const string STR_APP_PERMISSIONS = "权限检查";
		public const string STR_APP_SENDING = "发送";
		public const string STR_APP_DEL_MSG = "撤回消息";
		#endregion

		#region --字段--
		private static readonly Regex _appIdRegex = new Regex (@"(?:[a-z]*)\.(?:[a-z\-_]*)\.(?:[a-zA-Z0-9\.\-_]*)", RegexOptions.Compiled);
		private bool _isLogin = false;
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
		public string AddDirectory { get; }
		/// <summary>
		/// 获取或设置当前实例的登录QQ
		/// </summary>
		public QQ LoginQQ { get; set; }
		#endregion

		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="CQPSimulator"/> 类的新实例
		/// </summary>
		/// <param name="appDirectory">应用路径</param>
		public CQPSimulator (string appDirectory)
		{
			if (!Directory.Exists (appDirectory))
			{
				Directory.CreateDirectory (appDirectory);
				LogCenter.Instance.InfoSuccess (STR_SIMULATOR_INIT, $"已创建应用目录: {appDirectory}");
			}

			this.AddDirectory = appDirectory;
			this.CQPApps = new List<CQPSimulatorApp> ();
			this.DataPool = new CQPSimulatorDataPool ().Generate ();
			LogCenter.Instance.InfoSuccess (STR_SIMULATOR_INIT, $"已加载 {this.DataPool.QQCollection.Count} 个QQ、{this.DataPool.FriendCollection.Count} 个好友、{this.DataPool.GroupCollection.Count} 个群");

			// 设置 CQExport 服务
			CQPExport.Instance.FuncProcess = this;
		}
		#endregion

		#region --公开方法--
		/// <summary>
		/// 登录 <see cref="CQPSimulator"/> 实例
		/// </summary>
		/// <param name="qq">用于登陆到模拟器的 <see cref="QQ"/> 实例</param>
		public void Login (QQ qq)
		{
			if (!this._isLogin)
			{
				this.LoginQQ = qq;
				this._isLogin = true;
			}
		}
		/// <summary>
		/// 登出 <see cref="CQPSimulator"/> 实例
		/// </summary>
		public void Logout ()
		{
			if (this._isLogin)
			{
				this.LoginQQ = null;
				this._isLogin = false;
			}
		}
		/// <summary>
		/// 启动 <see cref="CQPSimulator"/>
		/// </summary>
		public void Start ()
		{
			if (!this._isLogin)
			{
				throw new InvalidOperationException ($"无法继续运行, 因为当前实例还未完成初始化");
			}

			if (this._isStart)
			{
				return;
			}

			LogCenter.Instance.InfoSuccess (STR_APP_LOAD, "应用加载开始");
			string[] pathes = Directory.GetDirectories (this.AddDirectory);
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
						LogCenter.Instance.Warning (STR_APP_LOAD, $"应用: {appId} 返回的 AppID 错误.");
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
						LogCenter.Instance.Error (STR_APP_LOAD, $"应用 {appId} 的 Initialize 方法未返回 0");
						failCount++;
						continue;
					}

					// 存入实例列表
					this.CQPApps.Add (new CQPSimulatorApp (authCode, appId, library));

					LogCenter.Instance.InfoSuccess (STR_APP_LOAD, $"应用: {appId} 加载成功");
				}
				catch (Exception ex)
				{
					LogCenter.Instance.Warning (STR_APP_LOAD, $"应用: {appId} 加载失败, 原因: {ex.Message}");
				}
			}

			LogCenter.Instance.InfoSuccess (STR_APP_LOAD, $"应用加载结束. 加载成功: {this.CQPApps.Count} 个, 失败: {this.CQPApps.Count - pathes.Length} 个");
			this._isStart = true;
		}
		/// <summary>
		/// 停止 <see cref="CQPSimulator"/>
		/// </summary>
		public void Stop ()
		{
			if (!this._isLogin)
			{
				throw new InvalidOperationException ($"无法继续运行, 因为当前实例还未完成初始化");
			}

			if (!this._isStart)
			{
				return;
			}

			LogCenter.Instance.InfoSuccess (STR_APP_UNLOAD, $"应用卸载开始");
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
						LogCenter.Instance.Error (STR_APP_UNLOAD, $"应用: {appId} 卸载失败, 原因: {ex.Message}");
					}
				}

				LogCenter.Instance.InfoSuccess (STR_APP_UNLOAD, $"应用: {appId} 卸载成功");

				// 销毁对象
				app.Library.Dispose ();
			}
			LogCenter.Instance.InfoSuccess (STR_APP_UNLOAD, $"应用卸载结束");
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
			if (!this._isLogin)
			{
				throw new InvalidOperationException ($"无法继续运行, 因为当前实例还未完成初始化");
			}

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
				LogCenter.Instance.Error (STR_APP_PERMISSIONS, $"检测到非法的 Api 调用, 已阻止. 请确保调用的 Api 使用了 Initialize 下发的授权码");
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

			return new CompositeInvoker (this, app, isAuth).GetCommandHandle (funcName, objs).Execute ();
		}
		#endregion
	}
}
