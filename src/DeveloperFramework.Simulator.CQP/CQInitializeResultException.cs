using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.Simulator.CQP
{
	/// <summary>
	/// 表示应用 Initialize 函数返回值错误时引发的异常
	/// </summary>
	public class CQInitializeResultException : CQException
	{
		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="CQInitializeResultException"/> 类的新实例
		/// </summary>
		public CQInitializeResultException ()
			: base (CQErrorCode.AuthFuncReturnNoZero)
		{ }
		#endregion

		#region --私有方法--
		protected override string GetMessage ()
		{
			return "Api授权接收函数(Initialize)返回值非0";
		}
		#endregion
	}
}
