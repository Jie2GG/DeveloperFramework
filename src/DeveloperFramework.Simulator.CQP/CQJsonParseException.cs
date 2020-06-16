using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.Simulator.CQP
{
	/// <summary>
	/// 表示应用 Json 解析失败时引发的异常
	/// </summary>
	public class CQJsonParseException : CQException
	{
		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="CQJsonParseException"/> 类的新实例
		/// </summary>
		public CQJsonParseException ()
			: base (CQErrorCode.JsonParseError)
		{
		}
		#endregion

		#region --私有方法--
		protected override string GetMessage ()
		{
			return " 应用信息Json串解析失败，请检查Json串是否正确";
		} 
		#endregion
	}
}
