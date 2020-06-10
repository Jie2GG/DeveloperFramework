using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.Log.CQP
{
	/// <summary>
	/// 表示程序运行状态的枚举
	/// </summary>
	public enum LogState
	{
		/// <summary>
		/// 无状态
		/// </summary>
		None = 0,
		/// <summary>
		/// 程序执行成功
		/// </summary>
		Success = 1,
		/// <summary>
		/// 程序执行失败
		/// </summary>
		Failure = 2,
		/// <summary>
		/// 程序正在执行
		/// </summary>
		Running = 3,
	}
}
