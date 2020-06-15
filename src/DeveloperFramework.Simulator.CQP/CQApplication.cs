using DeveloperFramework.Library.CQP;
using DeveloperFramework.SimulatorModel.CQP;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.Simulator.CQP
{
	/// <summary>
	/// 表示酷Q应用的类
	/// </summary>
	public class CQApplication
	{
		/// <summary>
		/// 获取当前应用的应用信息
		/// </summary>
		public AppInfo AppInfo { get; }

		/// <summary>
		/// 获取当前应用的 (C/C++) 动态链接库实例
		/// </summary>
		public CQPDynamicLibrary Library { get; }

		public CQApplication (string libName, string jsonName)
		{
			this.Library = new CQPDynamicLibrary (libName);

			if (!File.Exists (jsonName))
			{

			}
		}
	}
}
