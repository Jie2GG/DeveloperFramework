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
		#region --属性--
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
		#endregion

		#region --构造函数--
		/// <summary>
		/// 写入一条 <see cref="LogLevel.Debug"/> 类型的日志
		/// </summary>
		/// <param name="level">日志等级</param>
		/// <param name="from">日志的来源模块名称</param>
		/// <param name="type">日志的类型</param>
		/// <param name="content">日志详细信息</param>
		/// <param name="state">处理结果</param>
		/// <param name="timeConsuming">处理耗费时长</param>
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
		#endregion

		#region --公开方法--
		/// <summary>
		/// 返回当前实例的字符串
		/// </summary>
		/// <returns>当前实例的字符串</returns>
		public override string ToString ()
		{
			StringBuilder builder = new StringBuilder ();
			builder.Append ($"等级: {this.Level}\t来源: {this.From}\t类型: {this.Type}");
			if (this.State != null)
			{
				builder.Append ($"\t状态: {(this.State.Value ? "√" : "×")}");
			}
			if (this.TimeConsuming != null)
			{
				builder.Append ($"/{this.TimeConsuming.Value}");
			}
			builder.AppendLine ();
			builder.AppendLine ("详细信息:");
			builder.AppendLine ($"\t{this.Content}");
			return builder.ToString ();
		}
		#endregion
	}
}
