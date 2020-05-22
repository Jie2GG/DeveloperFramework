using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.SimulatorModel.CQP
{
	/// <summary>
	/// 描述 处理类型 枚举
	/// </summary>
	public enum HandleType
	{
		/// <summary>
		/// 忽略消息
		/// </summary>
		Discard = 0,
		/// <summary>
		/// 拦截消息
		/// </summary>
		Intercept = 1
	}
}
