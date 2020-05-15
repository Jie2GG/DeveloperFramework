using DeveloperFramework.LibraryModel.CQP;
using DeveloperFramework.SimulatorModel.CQP;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.Simulator.CQP
{
	/// <summary>
	/// 表示 CQP 应用模拟器的数据池
	/// </summary>
	public class CQPSimulatorDataPool
	{
		/// <summary>
		/// 获取当前数据池中的缓存消息集合
		/// </summary>
		public ConcurrentBag<MessageCache> MessagesCollection { get; }
		/// <summary>
		/// 获取当前数据池中的缓存 <see cref="QQ"/> 集合
		/// </summary>
		public ConcurrentBag<QQ> QQCollection { get; }
		/// <summary>
		/// 获取当前数据池中的缓存 <see cref="Group"/> 集合
		/// </summary>
		public ConcurrentBag<Group> GroupCollection { get; }
		/// <summary>
		/// 获取当前数据池中的缓存 <see cref="Discuss"/> 集合
		/// </summary>
		public ConcurrentBag<Discuss> DiscussCollection { get; }


	}
}
