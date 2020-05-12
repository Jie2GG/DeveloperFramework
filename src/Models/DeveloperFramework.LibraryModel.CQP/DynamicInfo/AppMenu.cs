using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.LibraryModel.CQP.Dynamic
{
	/// <summary>
	/// 描述 CQP 应用动态库的菜单信息
	/// </summary>
	public class AppMenu
	{
		/// <summary>
		/// 菜单名称
		/// </summary>
		[JsonProperty (PropertyName = "name")]
		public string Name { get; set; }
		/// <summary>
		/// 菜单函数
		/// </summary>
		[JsonProperty (PropertyName = "function")]
		public string Function { get; set; }
	}
}
