using DeveloperFramework.Library.CQP;
using DeveloperFramework.LibraryModel.CQP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
		#endregion

		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="CQPSimulatorApp"/> 类的新实例
		/// </summary>
		/// <param name="authCode">和当前实例绑定的应用授权码</param>
		/// <param name="appId">当前实例的应用Id</param>
		/// <param name="library">和当前实例绑定的 <see cref="CQPDynamicLibrary"/></param>
		public CQPSimulatorApp (int authCode, string appId, CQPDynamicLibrary library)
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
		}
		#endregion
	}
}
