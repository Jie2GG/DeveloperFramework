using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DeveloperFramework.LibraryModel.CQP
{
	/// <summary>
	/// 描述 CQP 应用动态库的菜单信息
	/// </summary>
	public class AppMenu
	{
		/// <summary>
		/// 菜单名称
		/// </summary>
		[JsonPropertyName("name")]
		public string Name { get; set; }
		/// <summary>
		/// 菜单函数
		/// </summary>
		[JsonPropertyName("function")]
		public string Function { get; set; }
	}
}
