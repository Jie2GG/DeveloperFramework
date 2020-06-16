using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.Simulator.CQP
{
	/// <summary>
	/// 表示应用 AppID 不符合要求时引发的异常
	/// </summary>
	public class CQAppIDMismatchException : CQException
	{
		#region --属性--
		/// <summary>
		/// 获取导致当前异常的应用 AppID
		/// </summary>
		public string AppId { get; }
		#endregion

		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="CQAppIDMismatchException"/> 类的新实例
		/// </summary>
		/// <param name="appId">应用 AppID</param>
		public CQAppIDMismatchException (string appId)
			: base (CQErrorCode.AppIDMismatch)
		{
			AppId = appId;
		}
		#endregion

		#region --私有方法--
		protected override string GetMessage ()
		{
			return string.Format (CultureInfo.CurrentCulture, "AppID({0})不符合AppID格式, 请阅读开发文档进行修改", this.AppId);
		}
		#endregion
	}
}
