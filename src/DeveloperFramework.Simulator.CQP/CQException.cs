using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.Simulator.CQP
{
	/// <summary>
	/// 表示 CQ 在应用程序执行过程中发生的错误
	/// </summary>
	public abstract class CQException : Exception
	{
		#region --属性--
		/// <summary>
		/// 获取当前异常的错误代码
		/// </summary>
		public CQErrorCode ErrorCode { get; }
		/// <summary>
		/// 获取描述当前异常的消息
		/// </summary>
		public override string Message => $"{this.GetMessage ()}({(int)this.ErrorCode})";
		#endregion

		#region --构造函数--
		/// <summary>
		/// 用指定的错误代码初始化 <see cref="CQException"/> 类的新实例
		/// </summary>
		/// <param name="errorCode">异常错误代码</param>
		public CQException (CQErrorCode errorCode)
		{
			this.ErrorCode = errorCode;
		}
		/// <summary>
		/// 用指定的错误代码和错误消息初始化 <see cref="CQException"/> 类的新实例
		/// </summary>
		/// <param name="errorCode">错误代码</param>
		/// <param name="message">描述错误的消息</param>
		public CQException (CQErrorCode errorCode, string message)
			: base (message)
		{
			this.ErrorCode = errorCode;
		}
		/// <summary>
		/// 使用指定的错误代码和对作为此异常原因的内部异常的引用来初始化 <see cref="CQException"/> 类的新实例
		/// </summary>
		/// <param name="errorCode">错误代码</param>
		/// <param name="innerException">导致当前异常的异常；如果未指定内部异常，则是一个 <see langword="null"/> 引用（在 Visual Basic 中为 <see langword="Nothing"/>）</param>
		public CQException (CQErrorCode errorCode, Exception innerException)
			: this (errorCode, string.Empty, innerException)
		{ }
		/// <summary>
		/// 使用指定的错误代码、错误消息和对作为此异常原因的内部异常的引用来初始化 <see cref="CQException"/> 类的新实例
		/// </summary>
		/// <param name="errorCode">错误代码</param>
		/// <param name="message">解释异常原因的错误消息</param>
		/// <param name="innerException">导致当前异常的异常；如果未指定内部异常，则是一个 <see langword="null"/> 引用（在 Visual Basic 中为 <see langword="Nothing"/>）</param>
		public CQException (CQErrorCode errorCode, string message, Exception innerException)
			: base (message, innerException)
		{
			this.ErrorCode = errorCode;
		}
		#endregion

		#region --私有方法--
		/// <summary>
		/// 获取详细的错误信息
		/// </summary>
		/// <returns>详细的错误信息</returns>
		protected virtual string GetMessage ()
		{
			return base.Message;
		}
		#endregion
	}
}