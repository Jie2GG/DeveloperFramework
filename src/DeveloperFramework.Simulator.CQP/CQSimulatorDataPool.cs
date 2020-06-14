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
		#region -属性---
		/// <summary>
		/// 获取当前数据池存储的机器人QQ实例
		/// </summary>
		public QQ RobotQQ { get; set; }
		/// <summary>
		/// 获取当前数据池的QQ数据列表
		/// </summary>
		public ObservableCollection<QQ> QQCollection { get; }
		/// <summary>
		/// 获取当前数据池的好友列表数据
		/// </summary>
		public FriendCollection FriendCollection { get; }
		/// <summary>
		/// 获取当前数据池的群列表
		/// </summary>
		public GroupCollection GroupCollection { get; }
		/// <summary>
		/// 获取当前数据池的讨论组列表
		/// </summary>
		public DiscussCollection DiscussCollection { get; }
		/// <summary>
		/// 获取当前数据池用于存放 "发送" 或 "接收" 撤回或未撤回的消息
		/// </summary>
		public ConcurrentBag<Message> MessageCollection { get; }
		#endregion

		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="CQSimulatorDataPool"/> 类的新实例
		/// </summary>
		public CQSimulatorDataPool ()
		{
			this.QQCollection = new ObservableCollection<QQ> ();
			this.FriendCollection = new FriendCollection ();
			this.GroupCollection = new GroupCollection ();
			this.DiscussCollection = new DiscussCollection ();
			this.MessageCollection = new ConcurrentBag<Message> ();
		}
		#endregion
	}
}
