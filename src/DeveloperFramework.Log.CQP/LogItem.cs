using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.Log.CQP
{
	/// <summary>
	/// 表示用于记录程序运行期间结果的详细信息
	/// </summary>
	public class LogItem
	{
		#region --属性--
		/// <summary>
		/// 获取一个值, 指示当日志的产生时间
		/// </summary>
		public DateTime DateTime { get; }
		/// <summary>
		/// 获取一个值, 指示当前日志的等级
		/// </summary>
		public LogLevel Level { get; }
		/// <summary>
		/// 获取一个值, 指示当前日志的源头
		/// </summary>
		public string From { get; }
		/// <summary>
		/// 获取一个值, 指示当前日志的类型
		/// </summary>
		public string Type { get; }
		/// <summary>
		/// 获取当前日志的详细信息
		/// </summary>
		public string Content { get; }
		/// <summary>
		/// 获取一个值, 指示当前日志描述程序的运行状态
		/// </summary>
		public LogState State { get; set; }
		/// <summary>
		/// 获取一个值, 指示当前日志从开始到结束所耗费的时长. 当 <see cref="State"/> 为 <see cref="LogState.None"/> 时, 该值为 <see langword="null"/>
		/// </summary>
		public TimeSpan? TimeSpan { get; set; }
		#endregion

		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="LogItem"/> 类的新实例
		/// </summary>
		/// <param name="level">日志等级</param>
		/// <param name="from">日志来源</param>
		/// <param name="type">日志类型</param>
		/// <param name="content">详细信息</param>
		public LogItem (LogLevel level, string from, string type, object content)
		{
			this.DateTime = DateTime.Now;
			this.Level = level;
			this.From = from;
			this.Type = type;

			if (content is string)
			{
				this.Content = (string)content;
			}
			if (content is Exception)
			{
				this.Content = content.ToString ();
			}
			if (content is object)
			{
				this.Content = content.ToString ();
			}
		}
		#endregion

		#region --公开方法--
		/// <summary>
		/// 获取当前实例格式化后的字符串
		/// </summary>
		/// <returns>格式化后的字符串</returns>
		public override string ToString ()
		{
			StringBuilder builder = new StringBuilder ();
			builder.AppendFormat ("时间: {0:HH:mm:ss}", this.DateTime);
			builder.AppendLine ();
			builder.AppendFormat ("等级: {0}\t来源: {1}\t类型: {2}\t", this.Level, this.From.Length, this.Type);
			if (this.TimeSpan != null)
			{
				builder.AppendFormat ("状态: {0}\t耗时: {1:0.00}s", this.State, this.TimeSpan.Value.TotalSeconds);
			}
			builder.AppendLine ();
			builder.AppendLine ("详情:");
			builder.AppendLine (this.Content.ToString ());
			return builder.ToString ();
		}
		#endregion
	}
}
