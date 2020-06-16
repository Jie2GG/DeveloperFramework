using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.Simulator.CQP
{
	/// <summary>
	/// 表示 AppInfo 函数返回的 Api 版本不受支持时引发的异常
	/// </summary>
	public class CQAppInfoResultApiException : CQException
	{
		#region --属性--
		/// <summary>
		/// 获取导致当前异常的 Api 版本
		/// </summary>
		public int ApiVersion { get; }
		#endregion

		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="CQAppInfoResultApiException"/> 类的新实例
		/// </summary>
		public CQAppInfoResultApiException (int apiVer)
			: base (CQErrorCode.AppInfoResultApiNoSupport)
		{
			this.ApiVersion = apiVer;
		}
		#endregion

		#region --私有方法--
		protected override string GetMessage ()
		{
			return $"AppInfo返回的Api版本不支持直接加载，仅支持Api版本为{this.ApiVersion}(及以上)的应用直接加载";
		}
		#endregion
	}
}
