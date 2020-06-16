using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.Simulator.CQP
{
	/// <summary>
	/// 表示应用加载失败时引发的异常
	/// </summary>
	public class CQLoadLibraryException : CQException
	{
		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="CQLoadLibraryException"/> 类的新实例
		/// </summary>
		/// <param name="message">解释异常原因的错误消息</param>
		/// <param name="innerException">导致当前异常的异常；如果未指定内部异常，则是一个 <see langword="null"/> 引用（在 Visual Basic 中为 <see langword="Nothing"/>）</param>
		public CQLoadLibraryException (string message, Exception innerException)
			: base (CQErrorCode.LoadLibraryError, message, innerException)
		{
		}
		#endregion
	}
}
