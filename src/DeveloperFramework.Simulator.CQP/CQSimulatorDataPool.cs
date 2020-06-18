using DeveloperFramework.SimulatorModel.CQP;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.Simulator.CQP
{
	/// <summary>
	/// 表示酷Q应用模拟器数据存储池的类
	/// </summary>
	public class CQSimulatorDataPool
	{
		#region --属性--
		/// <summary>
		/// 获取当前实例的机器人QQ
		/// </summary>
		public QQ RobotQQ { get; }
		/// <summary>
		/// 获取当前实例的QQ列表
		/// </summary>
		public QQCollection QQCollection { get; }
		/// <summary>
		/// 获取当前实例的好友列表 (<see cref="QQCollection"/> 中也应包含此列表中的内容)
		/// </summary>
		public FriendCollection FriendCollection { get; }
		/// <summary>
		/// 获取当前实例的群组列表
		/// </summary>
		public GroupCollection GroupCollection { get; }
		/// <summary>
		/// 获取当前实例的讨论组列表
		/// </summary>
		public DiscussCollection DiscussCollection { get; }
		/// <summary>
		/// 获取当前实例的存放 "发送" 或 "接收" 的消息缓存
		/// </summary>
		public ConcurrentBag<Message> MessageCollection { get; }
		#endregion

		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="CQSimulatorDataPool"/> 类的新实例
		/// </summary>
		public CQSimulatorDataPool ()
		{
			this.QQCollection = new QQCollection ();
			this.FriendCollection = new FriendCollection ();
			this.GroupCollection = new GroupCollection ();
			this.DiscussCollection = new DiscussCollection ();
			this.MessageCollection = new ConcurrentBag<Message> ();
		}
		#endregion
	}
}
