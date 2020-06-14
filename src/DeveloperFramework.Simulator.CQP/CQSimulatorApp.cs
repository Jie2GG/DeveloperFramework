using DeveloperFramework.Library.CQP;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.Simulator.CQP
{
	/// <summary>
	/// 表示酷Q模拟器应用类
	/// </summary>
	public class CQSimulatorApp
	{
		/// <summary>
		/// 获取当前实例 (C/C++) 动态链接库的实例
		/// </summary>
		public CQPDynamicLibrary Library { get; }
	}
}
