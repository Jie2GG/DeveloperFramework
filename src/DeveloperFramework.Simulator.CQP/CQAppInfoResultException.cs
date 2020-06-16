using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.Simulator.CQP
{
	/// <summary>
	/// 表示应用 AppInfo 函数返回信息错误时引发的异常
	/// </summary>
	public class CQAppInfoResultException : CQException
	{
		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="CQAppInfoResultException"/> 类的新实例
		/// </summary>
		public CQAppInfoResultException ()
			: base (CQErrorCode.AppInfoResultError)
		{
		}
		#endregion

		#region --私有方法--
		protected override string GetMessage ()
		{
			return "AppInfo返回信息解析错误";
		} 
		#endregion
	}
}
