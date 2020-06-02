using DeveloperFramework.Library.CQP;
using DeveloperFramework.LibraryModel.CQP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DeveloperFramework.Simulator.CQP
{
	/// <summary>
	/// 描述 CQP 应用模拟器的应用
	/// </summary>
	public class CQPSimulatorApp
	{
		#region --属性--
		/// <summary>
		/// 获取当前实例的授权码, 该码由 <see cref="CQPSimulator"/> 对象进行授权
		/// </summary>
		public int AuthCode { get; }
		/// <summary>
		/// 获取当前实例的 AppId
		/// </summary>
		public string AppId { get; }
		/// <summary>
		/// 获取当前实例的 <see cref="CQPDynamicLibrary"/> 对象
		/// </summary>
		public CQPDynamicLibrary Library { get; }
		/// <summary>
		/// 对消息事件处理进行调用限制
		/// </summary>
		public SemaphoreSlim Throttler { get; }
		/// <summary>
		/// 事件队列限制数
		/// </summary>
		private int ThreadAllow { get; }
		/// <summary>
		/// 获取当前加载 酷Q(C/C++) 动态库是否已初始化
		/// </summary>
		public bool IsInitialized => Library.IsInitialized;
		/// <summary>
		/// 获取当前加载 酷Q(C/C++) 动态库是否已实行
		/// </summary>
		public bool IsStartup => Library.IsStartup;
		/// <summary>
		/// 获取当前加载 酷Q(C/C++) 动态库的启停状态
		/// </summary>
		public bool IsEnable => Library.IsEnable;
		/// <summary>
		/// 获取当前加载 酷Q(C/C++) 动态库是否事件队列已满
		/// </summary>
		public bool IsOverTask => Throttler.CurrentCount == 0;
		/// <summary>
		/// 获取当前加载 酷Q(C/C++) 动态库是否已退出
		/// </summary>
		public bool IsExit => Library.IsExit;
		#endregion

		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="CQPSimulatorApp"/> 类的新实例
		/// </summary>
		/// <param name="authCode">和当前实例绑定的应用授权码</param>
		/// <param name="appId">当前实例的应用Id</param>
		/// <param name="library">和当前实例绑定的 <see cref="CQPDynamicLibrary"/></param>
		public CQPSimulatorApp (int authCode, string appId, CQPDynamicLibrary library, int threadAllow = 30)
		{
			if (authCode <= 0)
			{
				throw new ArgumentException ("授权码无效", nameof (authCode));
			}

			if (appId is null)
			{
				throw new ArgumentNullException (nameof (appId));
			}

			if (library is null)
			{
				throw new ArgumentNullException (nameof (library));
			}

			this.AuthCode = authCode;
			this.AppId = appId;
			this.Library = library;
			this.ThreadAllow = threadAllow;
			this.Throttler = new SemaphoreSlim(threadAllow, threadAllow);
		}
		#endregion
	}
}
