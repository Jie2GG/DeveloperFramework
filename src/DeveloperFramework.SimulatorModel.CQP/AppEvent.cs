using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DeveloperFramework.SimulatorModel.CQP
{
	/// <summary>
	/// 描述 CQP 应用动态库的事件信息
	/// </summary>
	public class AppEvent
	{
		/// <summary>
		/// 事件ID
		/// </summary>
		[JsonPropertyName ("id")]
		public int Id { get; set; }
		/// <summary>
		/// 事件类型
		/// </summary>
		[JsonPropertyName ("type")]
		public AppEventType Type { get; set; }
		/// <summary>
		/// 事件名称
		/// </summary>
		[JsonPropertyName ("name")]
		public string Name { get; set; }
		/// <summary>
		/// 事件函数
		/// </summary>
		[JsonPropertyName ("function")]
		public string Function { get; set; }
		/// <summary>
		/// 事件优先级
		/// </summary>
		[JsonPropertyName ("priority")]
		public int Priority { get; set; }
	}
}
