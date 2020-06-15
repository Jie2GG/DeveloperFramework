using DeveloperFramework.Log.CQP;
using DeveloperFramework.SimulatorModel.CQP;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.Simulator.CQP
{
	/// <summary>
	/// 表示酷Q应用模拟器类
	/// </summary>
	public class CQSimulator
	{
		public CQSimulatorDirectory Directorys { get; }
		public CQSimulatorDataPool DataPool { get; }
		public CQType SimulatorType { get; }
		public ApiType ApiType { get; }

		/// <summary>
		/// 初始化 <see cref="CQSimulator"/> 类的新实例, 同时指定模拟方式和 Api 版本
		/// </summary>
		/// <param name="simulatorType">CQ应用模拟器类型</param>
		/// <param name="apiType"></param>
		public CQSimulator (CQType simulatorType, ApiType apiType)
		{
			this.SimulatorType = simulatorType;
			this.ApiType = apiType;

			this.DataPool = new CQSimulatorDataPool ();
			this.Directorys = new CQSimulatorDirectory (AppDomain.CurrentDomain.BaseDirectory);
		}

		#region --公开方法--

		#endregion
	}
}
