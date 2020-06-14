using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.CQP
{
	/// <summary>
	/// 表示定义CQP函数目标绑定的特性
	/// </summary>
	[AttributeUsage (AttributeTargets.Class)]
	public class CQPExportBindingAttribute : Attribute
	{
		/// <summary>
		/// 获取或设置函数的名称
		/// </summary>
		public string FunctionName { get; set; }
	}
}
