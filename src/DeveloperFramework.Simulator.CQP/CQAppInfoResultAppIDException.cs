using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.Simulator.CQP
{
	/// <summary>
	/// 表示应用 AppInfo 函数返回的 AppID 错误时引发的异常
	/// </summary>
	public class CQAppInfoResultAppIDException : CQException
	{
		#region --属性--
		/// <summary>
		/// 获取导致当前异常的 AppID
		/// </summary>
		public string AppId { get; }
		#endregion

		#region --构造函数--
		/// <summary>
		/// 使用错误的 AppID 初始化 <see cref="CQAppInfoResultAppIDException"/> 类的新实例
		/// </summary>
		/// <param name="appId"></param>
		public CQAppInfoResultAppIDException (string appId)
			: base (CQErrorCode.AppInfoResultAppIDError)
		{
			this.AppId = appId;
		}
		#endregion

		#region --私有方法--
		protected override string GetMessage ()
		{
			return $"{this.AppId}目录名的AppID与AppInfo返回的AppID不同";
		}
		#endregion
	}
}
