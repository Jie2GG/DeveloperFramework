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
		#region --属性--
		/// <summary>
		/// 获取当前实例模拟的酷Q类型
		/// </summary>
		public CQType CQType { get; }
		/// <summary>
		/// 获取当前实例模拟的Api类型
		/// </summary>
		public ApiType ApiType { get; }
		/// <summary>
		/// 获取当前实例加载的应用程序
		/// </summary>
		public List<CQApplication> Applications { get; }
		#endregion

		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="CQSimulator"/> 类的新实例, 同时指定模拟方式和 Api 版本
		/// </summary>
		/// <param name="cqType">CQ应用模拟器类型</param>
		/// <param name="apiType">Api类型</param>
		public CQSimulator (CQType cqType, ApiType apiType)
		{
			this.CQType = cqType;
			this.ApiType = apiType;

			#region 初始化属性
			this.Applications = new List<CQApplication> ();
			#endregion

			#region 加载应用
			this.Load ();
			#endregion

			#region 加载数据
			// TOOD: 加载好友, 群, 讨论组
			#endregion
		}
		#endregion

		#region --公开方法--
		public void Load ()
		{

		}
		#endregion
	}
}
