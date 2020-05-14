using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.LibraryModel.CQP
{
	/// <summary>
	/// 描述 CQP 应用动态库的悬浮窗状态信息
	/// </summary>
	public class AppStatus
	{
		/// <summary>
		/// 悬浮窗ID
		/// </summary>
		[JsonProperty (PropertyName = "id")]
		public int Id { get; set; }
		/// <summary>
		/// 悬浮窗名称
		/// </summary>
		[JsonProperty (PropertyName = "name")]
		public string Name { get; set; }
		/// <summary>
		/// 悬浮窗标题
		/// </summary>
		[JsonProperty (PropertyName = "title")]
		public string Title { get; set; }
		/// <summary>
		/// 悬浮窗函数
		/// </summary>
		[JsonProperty (PropertyName = "function")]
		public string Function { get; set; }
		/// <summary>
		/// 更新间隔时间 (单位ms (毫秒), 目前 CQP 仅支持 1000ms (1秒))
		/// </summary>
		[JsonProperty (PropertyName = "period")]
		public string Period { get; set; }
	}
}
