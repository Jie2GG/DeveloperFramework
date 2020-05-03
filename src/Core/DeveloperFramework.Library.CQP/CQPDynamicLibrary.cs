using DeveloperFramework.Utility;
using DeveloperFramework.Win32.LibraryCLR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.Library.CQP
{
	/// <summary>
	/// 提供用于操作 酷Q(C/C++) 动态链接库的操作类
	/// </summary>
	public sealed class CQPDynamicLibrary : DynamicLibrary
	{
		#region --字段--
		private readonly string _jsonPath;
		private readonly CQPAppInfo _appInfo;
		#endregion

		#region --属性--
		/// <summary>
		/// 获取当前加载 Json 的路径
		/// </summary>
		public string JsonPath => this._jsonPath;
		#endregion

		#region --委托--
		private delegate string CQ_AppInfo ();
		private delegate int CQ_Initialize (int authCode);
		private delegate int CQ_Startup ();
		private delegate int CQ_Exit ();
		private delegate int CQ_AppEnable ();
		private delegate int CQ_AppDisable ();
		#endregion

		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="CQPDynamicLibrary"/> 类的新实例, 并加载指定的动态链接库 (DLL) 和对应的 Json
		/// </summary>
		/// <param name="libFileName">要加载的动态链接库 (DLL) 的路径</param>
		/// <param name="jsonFileName">要加载的 Json 的路径</param>
		public CQPDynamicLibrary (string libFileName, string jsonFileName)
			: base (libFileName)
		{
			this._jsonPath = OtherUtility.GetAbsolutePath (this.LibraryDirectory, jsonFileName);
			if (!File.Exists (this._jsonPath))
			{
				throw new FileNotFoundException ("未找到指定的 Json 文件", jsonFileName);
			}

			using (StreamReader reader = new StreamReader (this._jsonPath, Encoding.UTF8))
			{
				this._appInfo = JsonConvert.DeserializeObject<CQPAppInfo> (reader.ReadToEnd ());
			}
		}
		#endregion

		#region --公开方法--
		/// <summary>
		/// 调用 <see cref="CQ_AppInfo"/> 方法
		/// </summary>
		/// <returns>返回描述 App 的信息</returns>
		public string InvokeAppInfo ()
		{
			return (string)this.InvokeFunction<CQ_AppInfo> ("AppInfo");
		}
		/// <summary>
		/// 调用 <see cref="CQ_Initialize"/> 方法
		/// </summary>
		/// <param name="authCode">验证码, 随后调用 Api 都要以此码为依据</param>
		/// <returns>操作成功返回 0</returns>
		public int InvokeInitialize (int authCode)
		{
			return (int)this.InvokeFunction<CQ_Initialize> ("Initialize", authCode);
		}
		/// <summary>
		/// 调用 <see cref="CQ_Startup"/> 方法
		/// </summary>
		/// <param name="appEvent">目标事件信息</param>
		/// <returns>操作成功返回 0</returns>
		public int InvokeCQStartup (CQPAppEvent appEvent)
		{
			if (appEvent is null)
			{
				throw new ArgumentNullException (nameof (appEvent));
			}

			return (int)this.InvokeFunction<CQ_Startup> (appEvent.Function);
		}
		/// <summary>
		/// 调用 <see cref="CQ_Exit"/> 方法
		/// </summary>
		/// <param name="appEvent">目标事件信息</param>
		/// <returns>操作成功返回 0</returns>
		public int InvokeCQExit (CQPAppEvent appEvent)
		{
			if (appEvent is null)
			{
				throw new ArgumentNullException (nameof (appEvent));
			}

			return (int)this.InvokeFunction<CQ_Exit> (appEvent.Function);
		}
		/// <summary>
		/// 调用 <see cref="CQ_AppEnable"/> 方法
		/// </summary>
		/// <param name="appEvent">目标事件信息</param>
		/// <returns>操作成功返回 0</returns>
		public int InvokeCQAppEnable (CQPAppEvent appEvent)
		{
			if (appEvent is null)
			{
				throw new ArgumentNullException (nameof (appEvent));
			}

			return (int)this.InvokeFunction<CQ_AppEnable> (appEvent.Function);
		}
		/// <summary>
		/// 调用 <see cref="CQ_AppDisable"/> 方法
		/// </summary>
		/// <param name="appEvent">目标事件信息</param>
		/// <returns>操作成功返回 0</returns>
		public int InvokeCQAppDisable (CQPAppEvent appEvent)
		{
			if (appEvent is null)
			{
				throw new ArgumentNullException (nameof (appEvent));
			}

			return (int)this.InvokeFunction<CQ_AppDisable> (appEvent.Function);
		}
		#endregion
	}
}
