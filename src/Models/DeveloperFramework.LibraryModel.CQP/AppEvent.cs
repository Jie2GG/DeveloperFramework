using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.LibraryModel.CQP
{
	/// <summary>
	/// 描述 CQP 应用动态库的事件信息
	/// </summary>
	public class AppEvent
	{
		/// <summary>
		/// 事件ID
		/// </summary>
		[JsonProperty (PropertyName = "id")]
		public int Id { get; set; }
		/// <summary>
		/// 事件类型
		/// </summary>
		[JsonProperty (PropertyName = "type")]
		public AppEventType Type { get; set; }
		/// <summary>
		/// 事件名称
		/// </summary>
		[JsonProperty (PropertyName = "name")]
		public string Name { get; set; }
		/// <summary>
		/// 事件函数
		/// </summary>
		[JsonProperty (PropertyName = "function")]
		public string Function { get; set; }
		/// <summary>
		/// 事件优先级
		/// </summary>
		[JsonProperty (PropertyName = "priority")]
		public int Priority { get; set; }
	}
}
