using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.Simulator.CQP
{
	/// <summary>
	/// 表示应用 Json 加载失败时引发的异常
	/// </summary>
	public class CQJsonLoadException : CQException
	{
		#region --属性--
		/// <summary>
		/// 获取导致当前异常的应用 AppID
		/// </summary>
		public string AppID { get; }
		#endregion

		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="CQJsonLoadException"/> 类的新实例
		/// </summary>
		/// <param name="appId">应用 AppID</param>
		public CQJsonLoadException (string appId)
			: base (CQErrorCode.JsonLoadError)
		{
			this.AppID = appId;
		}
		#endregion

		#region --私有方法--
		protected override string GetMessage ()
		{
			return string.Format (CultureInfo.CurrentCulture, "{0}\\app.json文件读取失败", this.AppID);
		}
		#endregion
	}
}
