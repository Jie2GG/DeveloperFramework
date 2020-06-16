using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.Simulator.CQP
{
	/// <summary>
	/// 表示 AppInfo 函数不存在或错误时引发的异常
	/// </summary>
	public class CQAppInfoNotFoundException : CQException
	{
		#region --构造函数--
		/// <summary>
		/// 使用指定的对比为此异常原因的内部异常的引用来初始化 <see cref="CQAppInfoNotFoundException"/> 类的新实例
		/// </summary>
		/// <param name="innerException"></param>
		public CQAppInfoNotFoundException (Exception innerException)
			: base (CQErrorCode.AppInfoNotFound, innerException)
		{
		} 
		#endregion

		#region --私有方法--
		protected override string GetMessage ()
		{
			return "AppInfo函数不存在或错误";
		} 
		#endregion
	}
}
