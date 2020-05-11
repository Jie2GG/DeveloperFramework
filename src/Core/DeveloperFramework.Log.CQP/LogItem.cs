using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.Log.CQP
{
	/// <summary>
	/// 描述日志信息
	/// </summary>
	public class LogItem
	{
		/// <summary>
		/// 获取当前实例产生的时间
		/// </summary>
		public DateTime Time { get; }
		/// <summary>
		/// 获取当前实例的来源
		/// </summary>
		public string From { get; }
		/// <summary>
		/// 获取当前实例的类型
		/// </summary>
		public string Type { get; }
		/// <summary>
		/// 获取当前实例的内容
		/// </summary>
		public string Content { get; }
		/// <summary>
		/// 获取当前实例的执行状态
		/// </summary>
		public bool? State { get; }
		/// <summary>
		/// 获取当前实例描述耗费时长
		/// </summary>
		public TimeSpan? TimeConsuming { get; }
		/// <summary>
		/// 获取当前实例的等级
		/// </summary>
		public LogLevel Level { get; }

		public LogItem (LogLevel level, string from, string type, object content, bool? state, TimeSpan? timeConsuming)
		{
			this.Level = level;
			this.From = from;
			this.Type = type;
			this.State = state;
			this.TimeConsuming = timeConsuming;
			this.Time = DateTime.Now;


			if (content is string)
			{
				this.Content = (string)content;
			}
			else if (content is Exception)
			{
				this.Content = ((Exception)content).ToString ();
			}
			else
			{
				this.Content = content.ToString ();
			}
		}
	}
}
