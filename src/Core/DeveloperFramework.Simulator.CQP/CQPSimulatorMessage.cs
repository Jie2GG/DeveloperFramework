using DeveloperFramework.LibraryModel.CQP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.Simulator.CQP
{
	/// <summary>
	/// 描述 CQP 应用模拟器的消息
	/// </summary>
	public class CQPSimulatorMessage
	{
		#region --属性--
		/// <summary>
		/// 获取当前实例的消息对象 <see cref="LibraryModel.CQP.Dynamic.Message"/>
		/// </summary>
		public Message Message { get; }
		/// <summary>
		/// 获取当前实例的发送时间
		/// </summary>
		public DateTime SendTime { get; }
		#endregion

		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="CQPSimulatorMessage"/> 类的新实例
		/// </summary>
		/// <param name="message">描述消息的 <see cref="LibraryModel.CQP.Dynamic.Message"/> 对象</param>
		public CQPSimulatorMessage (Message message)
		{
			this.Message = message ?? throw new ArgumentNullException (nameof (message));
			this.SendTime = DateTime.Now;
		}
		#endregion

		#region --公开方法--
		/// <summary>
		/// 判断是否已经过期
		/// </summary>
		/// <returns>已过期返回 <see langword="true"/> 否则返回 <see langword="false"/></returns>
		public bool IsOverdue ()
		{
			TimeSpan span = this.SendTime - DateTime.Now;
			return span.TotalMinutes >= 2;
		} 
		#endregion
	}
}
