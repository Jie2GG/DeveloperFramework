using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.SimulatorModel.CQP
{
	/// <summary>
	/// 描述 CQP应用模拟器类型 枚举
	/// </summary>
	public enum CQType
	{
		/// <summary>
		/// 酷Q Air
		/// </summary>
		[Description ("酷Q Api")]
		Air = 1,
		/// <summary>
		/// 酷Q Pro
		/// </summary>
		[Description ("酷Q Pro")]
		Pro = 2
	}
}
