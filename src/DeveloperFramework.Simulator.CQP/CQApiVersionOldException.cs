using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.Simulator.CQP
{
	/// <summary>
	/// 表示应用 Api 版本过旧时引发的异常
	/// </summary>
	public class CQApiVersionOldException : CQException
	{
		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="CQApiVersionOldException"/> 类的新实例
		/// </summary>
		public CQApiVersionOldException ()
			: base (CQErrorCode.ApiVersionOld)
		{
		}
		#endregion

		#region --私有方法--
		protected override string GetMessage ()
		{
			return "Api版本过旧，请获取新版应用或联系作者更新";
		} 
		#endregion
	}
}
