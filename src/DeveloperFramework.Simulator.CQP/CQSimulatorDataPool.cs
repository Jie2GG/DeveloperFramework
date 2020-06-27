using DeveloperFramework.SimulatorModel.CQP;
using DeveloperFramework.Utility;

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
		#region --字段--
		private readonly RandomUtility _randomUtility;
		private static readonly DateTime _minDateTime = new DateTime (2000, 1, 1, 0, 0, 0);

		private int _friendCount = 20;
		private int _groupCount = 3;
		private int _discussCount = 2;
		#endregion

		//#region --属性--
		///// <summary>
		///// 获取当前实例的机器人QQ
		///// </summary>
		//public QQ RobotQQ { get; }
		///// <summary>
		///// 获取当前实例的QQ列表
		///// </summary>
		//public QQCollection QQCollection { get; }
		///// <summary>
		///// 获取当前实例的好友列表 (<see cref="QQCollection"/> 中也应包含此列表中的内容)
		///// </summary>
		//public FriendCollection FriendCollection { get; }
		///// <summary>
		///// 获取当前实例的群组列表
		///// </summary>
		//public GroupCollection GroupCollection { get; }
		///// <summary>
		///// 获取当前实例的讨论组列表
		///// </summary>
		//public DiscussCollection DiscussCollection { get; }
		///// <summary>
		///// 获取当前实例的存放 "发送" 或 "接收" 的消息缓存
		///// </summary>
		//public ConcurrentBag<Message> MessageCollection { get; }
		//#endregion

		//#region --构造函数--
		///// <summary>
		///// 初始化 <see cref="CQSimulatorDataPool"/> 类的新实例
		///// </summary>
		///// <param name="robotQQ">设置作为随机数种子的机器人QQ</param>
		//public CQSimulatorDataPool (long robotQQ)
		//{
		//	// 初始化列表
		//	this.QQCollection = new QQCollection ();
		//	this.FriendCollection = new FriendCollection ();
		//	this.GroupCollection = new GroupCollection ();
		//	this.DiscussCollection = new DiscussCollection ();
		//	this.MessageCollection = new ConcurrentBag<Message> ();

		//	// 初始化数据随机工具
		//	this._randomUtility = new RandomUtility (robotQQ.GetHashCode ());

		//	// 随机机器人QQ数据
		//	this.RobotQQ = this.FindOrCreateQQ (robotQQ, true);
		//}
		//#endregion

		//#region --公开方法--
		//public void Generate ()
		//{
		//	#region 好友生成
		//	for (int i = 0; i < this._friendCount; i++)
		//	{
		//		long qqId = this._randomUtility.RandomInt64 (Friend.MinValue, 100000000000);

		//		if (this.FindOrCreateFriend (qqId) != null)
		//		{
		//			i--;    // 查找到存在相同的好友, 不算做生成
		//			continue;
		//		}

		//		this.FindOrCreateFriend (qqId, true);   // 创建新好友
		//	}
		//	#endregion

		//	#region 群生成
		//	for (int i = 0; i < this._groupCount; i++)
		//	{
		//		long groupId = this._randomUtility.RandomInt64 (Group.MinValue, 10000000000);

		//		if (this.FindOrCreateGroup (groupId) != null)
		//		{
		//			i--;
		//			continue;
		//		}

		//		Group group = this.FindOrCreateGroup (groupId, true);

		//		#region 群成员生成

		//		#endregion
		//	}
		//	#endregion
		//}
		//#endregion

		//#region --私有方法--
		///// <summary>
		///// 查找或创建新的 <see cref="QQ"/> 实例
		///// </summary>
		///// <param name="qqId">查找或创建实例的唯一标识 (QQID)</param>
		///// <param name="isCreate">是否在未查找到实例时创建新实例, 默认: <see langword="false"/></param>
		///// <returns>如果不需要创建新实例, 返回查找到的实例或 <see langword="null"/>. 否则将返回查找到的实例或创建的新实例</returns>
		//private QQ FindOrCreateQQ (long qqId, bool isCreate = false)
		//{
		//	QQ qq = this.QQCollection.FirstOrDefault (p => p.Id == qqId);
		//	if (!isCreate)  // 如果不创建新的 QQ 实例则返回旧实例或默认值 (null)
		//	{
		//		return qq;
		//	}

		//	if (qq == null) // 如果创建新的 QQ 实例并且查询到的实例为默认值 (null) 则允许创建
		//	{
		//		qq = new QQ (qqId, this._randomUtility.RandomSymbol (), this._randomUtility.RandomEnum<Sex> (), this._randomUtility.RandomInt32 (0, 131));
		//		this.QQCollection.Add (qq);
		//	}

		//	return qq;
		//}
		///// <summary>
		///// 查找或创建新的 <see cref="Friend"/> 实例
		///// </summary>
		///// <param name="qqId">查找或创建实例的唯一标识 (QQID)</param>
		///// <param name="isCreate">是否在未查找到实例时创建新实例, 默认: <see langword="false"/></param>
		///// <returns>如果不需要创建新实例, 返回查找到的实例或 <see langword="null"/>. 否则将返回查找到的实例或创建的新实例</returns>
		//private Friend FindOrCreateFriend (long qqId, bool isCreate = false)
		//{
		//	Friend friend = this.FriendCollection.FirstOrDefault (p => p.Id == qqId);

		//	if (!isCreate)
		//	{
		//		return friend;
		//	}

		//	if (friend == null)
		//	{
		//		QQ qq = FindOrCreateQQ (qqId, true);

		//		friend = new Friend (qq.Id, qq.Nick, qq.Sex, qq.Age, this._randomUtility.RandomElement (this._randomUtility.RandomName (), string.Empty));
		//		this.FriendCollection.Add (friend);
		//	}

		//	return friend;
		//}
		///// <summary>
		///// 查找或创建新的 <see cref="Group"/> 实例
		///// </summary>
		///// <param name="groupId">查找或创建实例的唯一标识 (群ID)</param>
		///// <param name="isCreate">是否在未查找到实例时创建新实例, 默认: <see langword="false"/></param>
		///// <returns>如果不需要创建新实例, 返回查找到的实例或 <see langword="null"/>. 否则将返回查找到的实例或创建的新实例</returns>
		//private Group FindOrCreateGroup (long groupId, bool isCreate = false)
		//{
		//	Group group = this.GroupCollection.FirstOrDefault (p => p.Id == groupId);

		//	if (!isCreate)
		//	{
		//		return group;
		//	}

		//	if (group == null)
		//	{
		//		int maxMemberCount = this._randomUtility.RandomElement (200, 500, 1000, 2000);
		//		int currentMemberCount = this._randomUtility.RandomInt32 (1, maxMemberCount);

		//		group = new Group (groupId, this._randomUtility.RandomGroupName (), currentMemberCount, maxMemberCount);
		//		this.GroupCollection.Add (group);
		//	}

		//	return group;
		//}

		//private GroupMember FindOrCreateGroupMember (long groupId, long qqId, bool isCreate = false)
		//{
		//	Group group = this.GroupCollection.FirstOrDefault (p => p.Id == groupId);

		//	if (group == null)
		//	{
		//		return null;
		//	}

		//	GroupMember member = group.MemberCollection.FirstOrDefault (p => p.Id == qqId);

		//	if (!isCreate)
		//	{
		//		return member;
		//	}

		//	if (member == null)
		//	{
		//		QQ qq = FindOrCreateQQ (qqId, true);

				
		//	}
		//}
		//#endregion
	}
}
