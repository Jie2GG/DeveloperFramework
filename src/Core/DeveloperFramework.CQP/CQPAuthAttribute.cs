using DeveloperFramework.LibraryModel.CQP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.CQP
{
	/// <summary>
	/// 表示 CQP 应用函数权限的定义
	/// </summary>
	[AttributeUsage (AttributeTargets.Method)]
	public class CQPAuthAttribute : Attribute
	{
		/// <summary>
		/// 获取或设置函数的应用权限
		/// </summary>
		public AppAuth AppAuth { get; set; }


	}
}
