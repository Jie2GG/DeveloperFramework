using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DeveloperFramework.LibraryModel.CQP.Dynamic
{
	/// <summary>
	/// 描述 CQP 应用动态库的基础信息
	/// </summary>
	public class AppInfo
	{
		/// <summary>
		/// 返回码
		/// </summary>
		[JsonProperty (PropertyName = "ret")]
		public int ResultCode { get; set; }
		/// <summary>
		/// Api版本
		/// </summary>
		[JsonProperty (PropertyName = "apiver")]
		public int ApiVersion { get; set; }
		/// <summary>
		/// 应用名称
		/// </summary>
		[JsonProperty (PropertyName = "name")]
		public string Name { get; set; }
		/// <summary>
		/// 应用版本
		/// </summary>
		[JsonProperty (PropertyName = "version")]
		public string Version { get; set; }
		/// <summary>
		/// 应用顺序版本
		/// </summary>
		[JsonProperty (PropertyName = "version_id")]
		public int VersionId { get; set; }
		/// <summary>
		/// 应用作者
		/// </summary>
		[JsonProperty (PropertyName = "author")]
		public string Author { get; set; }
		/// <summary>
		/// 应用详情
		/// </summary>
		[JsonProperty (PropertyName = "description")]
		public string Description { get; set; }
		/// <summary>
		/// 应用事件集合
		/// </summary>
		[JsonProperty (PropertyName = "event")]
		public List<AppEvent> Events { get; set; }
		/// <summary>
		/// 应用菜单集合
		/// </summary>
		[JsonProperty (PropertyName = "menu")]
		public List<AppMenu> Menus { get; set; }
		/// <summary>
		/// 应用悬浮窗集合
		/// </summary>
		[JsonProperty (PropertyName = "status")]
		public List<AppStatus> Statues { get; set; }
		/// <summary>
		/// 应用权限集合
		/// </summary>
		[JsonProperty (PropertyName = "auth")]
		public List<AppAuth> Auth { get; set; }
	}
}
