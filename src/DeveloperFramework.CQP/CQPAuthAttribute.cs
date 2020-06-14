using DeveloperFramework.LibraryModel.CQP;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.CQP
{
	/// <summary>
	/// 表示定义函数CQP权限的特性
	/// </summary>
	[AttributeUsage (AttributeTargets.Method)]
	public class CQPAuthAttribute : Attribute
	{
		/// <summary>
		/// 获取或设置定义的应用权限
		/// </summary>
		public AppAuth FunctionAuth { get; set; }

		/// <summary>
		/// 初始化 <see cref="CQPAuthAttribute"/> 类的新实例
		/// </summary>
		/// <param name="functionAuth">指定函数权限</param>
		public CQPAuthAttribute (AppAuth functionAuth)
		{
			this.FunctionAuth = functionAuth;
		}
	}
}
