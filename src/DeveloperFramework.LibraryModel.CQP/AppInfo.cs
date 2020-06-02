using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DeveloperFramework.LibraryModel.CQP
{
	/// <summary>
	/// 描述 CQP 应用动态库的基础信息
	/// </summary>
	public class AppInfo
	{
		/// <summary>
		/// 返回码
		/// </summary>
		[JsonPropertyName("ret")]
		public int ResultCode { get; set; }
		/// <summary>
		/// Api版本
		/// </summary>
		[JsonPropertyName("apiver")]
		public int ApiVersion { get; set; }
		/// <summary>
		/// 应用名称
		/// </summary>
		[JsonPropertyName("name")]
		public string Name { get; set; }
		/// <summary>
		/// 应用版本
		/// </summary>
		[JsonPropertyName("version")]
		public string Version { get; set; }
		/// <summary>
		/// 应用顺序版本
		/// </summary>
		[JsonPropertyName("version_id")]
		public int VersionId { get; set; }
		/// <summary>
		/// 应用作者
		/// </summary>
		[JsonPropertyName("author")]
		public string Author { get; set; }
		/// <summary>
		/// 应用详情
		/// </summary>
		[JsonPropertyName("description")]
		public string Description { get; set; }
		/// <summary>
		/// 应用事件集合
		/// </summary>
		[JsonPropertyName("event")]
		public List<AppEvent> Events { get; set; }
		/// <summary>
		/// 应用菜单集合
		/// </summary>
		[JsonPropertyName("menu")]
		public List<AppMenu> Menus { get; set; }
		/// <summary>
		/// 应用悬浮窗集合
		/// </summary>
		[JsonPropertyName("status")]
		public List<AppStatus> Statues { get; set; }
		/// <summary>
		/// 应用权限集合
		/// </summary>
		[JsonPropertyName("auth")]
		public List<AppAuth> Auth { get; set; }
	}
}
