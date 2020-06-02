using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.Simulator.CQP.Domain
{
	/// <summary>
	/// 表示函数绑定的标记
	/// </summary>
	[AttributeUsage (AttributeTargets.Class)]
	public class FunctionBindingAttribute : Attribute
	{
		/// <summary>
		/// 获取或设置绑定的函数名称
		/// </summary>
		public string Function { get; set; }		
	}
}
