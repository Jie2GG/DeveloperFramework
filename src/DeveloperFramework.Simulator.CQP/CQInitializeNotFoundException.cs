using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.Simulator.CQP
{
	/// <summary>
	/// 表示应用 Initialize 函数不存在时引发的异常
	/// </summary>
	public class CQInitializeNotFoundException : CQException
	{
		#region --构造函数--
		/// <summary>
		/// 使用指定的对比为此异常原因的内部异常的引用来初始化 <see cref="CQInitializeNotFoundException"/> 类的新实例
		/// </summary>
		/// <param name="innerException"></param>
		public CQInitializeNotFoundException (Exception innerException)
			: base (CQErrorCode.AuthFuncNotFound, innerException)
		{ }
		#endregion

		#region --私有方法--
		protected override string GetMessage ()
		{
			return "无Api授权接收函数(Initialize)";
		}
		#endregion
	}
}
