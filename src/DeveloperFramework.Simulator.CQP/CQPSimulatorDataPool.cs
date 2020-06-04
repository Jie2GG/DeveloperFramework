using DeveloperFramework.LibraryModel.CQP;
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
	/// 表示 CQP 应用模拟器的数据池
	/// </summary>
	public class CQPSimulatorDataPool
	{
		#region --字段--
		private static readonly DateTime _minValue = new DateTime (2000, 1, 1, 0, 0, 0);
		private int _friendCount = 20;
		private int _groupCount = 5;
		private int _discussCount = 5;
		private string[][] _groupLevels = { new string[] { "萌新", "黑马", "资深", "精英", "大佬", "怪物" } };
		#endregion

		#region --属性--
		/// <summary>
		/// 获取当前数据池的机器人QQ
		/// </summary>
		public QQ RobotQQ { get; private set; }
		/// <summary>
		/// 获取当前数据池的 QQ 数据列表
		/// </summary>
		public Collection<QQ> QQCollection { get; }
		/// <summary>
		/// 获取当前数据池的好友数据列表
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
		/// 获取当前数据池用于存放已发送或接收到的未撤回的消息
		/// </summary>
		public ConcurrentBag<Message> MessageCollection { get; }
		#endregion

		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="CQPSimulatorDataPool"/> 类的新实例
		/// </summary>
		public CQPSimulatorDataPool ()
		{
			this.QQCollection = new Collection<QQ> ();
			this.FriendCollection = new FriendCollection ();
			this.GroupCollection = new GroupCollection ();
			this.DiscussCollection = new DiscussCollection ();
			this.MessageCollection = new ConcurrentBag<Message> ();
		}
		#endregion

		#region --公开方法--
		/// <summary>
		/// 设置当前数据池生成好友的条件参数
		/// </summary>
		/// <param name="friendCount">要生成好友的个数</param>
		/// <returns>当前数据池实例</returns>
		public CQPSimulatorDataPool SetFriend (int friendCount)
		{
			this._friendCount = friendCount;
			return this;
		}
		/// <summary>
		/// 设置当前数据池生成群的参数条件
		/// </summary>
		/// <param name="groupCount">要生成群的个数</param>
		/// <param name="groupLevels">要生成成员等级的组</param>
		/// <returns>当前数据池实例</returns>
		/// <exception cref="ArgumentNullException">groupLevels 是 null</exception>
		/// <exception cref="ArgumentException">groupLevels 的二维长度为 0 或者任意一维长度不是 6</exception>
		public CQPSimulatorDataPool SetGroup (int groupCount, string[][] groupLevels)
		{
			if (groupLevels == null)
			{
				throw new ArgumentNullException (nameof (groupLevels));
			}

			if (groupLevels.Length == 0)
			{
				throw new ArgumentException ("二维数组长度为 0", nameof (groupLevels));
			}

			for (int i = 0; i < groupLevels.Length; i++)
			{
				if (groupLevels[i].Length != 6)
				{
					throw new ArgumentException ($"二维数组第 {i} 组的长度不是6", nameof (groupLevels));
				}
			}

			this._groupCount = groupCount;
			this._groupLevels = groupLevels;
			return this;
		}
		/// <summary>
		/// 设置当前数据池生成讨论组的条件参数
		/// </summary>
		/// <param name="discussCount">要生成讨论组的个数</param>
		/// <returns>当前数据池实例</returns>
		public CQPSimulatorDataPool SetDiscuss (int discussCount)
		{
			this._discussCount = discussCount;
			return this;
		}
		/// <summary>
		/// 生成已指定条件的数据
		/// </summary>
		/// <returns>当前数据池实例</returns>
		public CQPSimulatorDataPool Generate ()
		{
			#region --机器人QQ--
			if (this.RobotQQ == null)
			{
				long qqId = RandomUtility.RandomInt64 (QQ.MinValue, 100000000000);
				string nick = RandomUtility.RandomSymbol ();
				Sex sex = RandomUtility.RandomEnum<Sex> ();
				int age = RandomUtility.RandomInt32 (0, 131);

				this.RobotQQ = new QQ (qqId, nick, sex, age);
			}
			#endregion

			#region 好友生成
			for (int i = 0; i < this._friendCount; i++)
			{
				long qqId = RandomUtility.RandomInt64 (Friend.MinValue, 100000000000);

				if (this.GroupCollection.Count (p => p.Id == qqId) >= 1)
				{
					i--;
				}
				else
				{
					string nick = RandomUtility.RandomSymbol ();
					Sex sex = RandomUtility.RandomEnum<Sex> ();
					int age = RandomUtility.RandomInt32 (0, 131);
					string postscript = RandomUtility.RandomElement (RandomUtility.RandomName (), string.Empty);

					Friend friend = new Friend (qqId, nick, sex, age, postscript);
					this.QQCollection.Add (friend);
					this.FriendCollection.Add (friend);
				}
			}
			#endregion

			#region 群生成
			for (int i = 0; i < this._groupCount; i++)
			{
				long groupId = RandomUtility.RandomInt64 (Group.MinValue, 10000000000);
				if (this.GroupCollection.Count (p => p.Id == groupId) >= 1)
				{
					i--;
				}
				else
				{
					string name = RandomUtility.RandomGroupName ();
					int maxMemberCount = RandomUtility.RandomElement (200, 500, 1000, 2000);    // 随机获取一个群人数
					int currentMemberCount = RandomUtility.RandomInt32 (1, maxMemberCount);     // 随机群人数, 最少1人

					Group group = new Group (groupId, name, currentMemberCount, maxMemberCount);
					this.GroupCollection.Add (group);

					#region 群成员生成
					string[] levelGroup = RandomUtility.RandomElement (this._groupLevels);  // 获取一个成员等级名称组

					for (int j = 0; j <= currentMemberCount; j++)
					{
						QQ qq = null;
						bool newQQ = false;

						if (j == currentMemberCount)
						{
							qq = this.RobotQQ;
						}
						else
						{
							long qqId = RandomUtility.RandomInt64 (QQ.MinValue, 100000000000);
							qq = this.QQCollection.Where (p => p.Id == qqId).FirstOrDefault ();
							if (qq == null)
							{
								string nick = RandomUtility.RandomSymbol ();
								Sex sex = RandomUtility.RandomEnum<Sex> ();
								int age = RandomUtility.RandomInt32 (0, 131);

								qq = new QQ (qqId, nick, sex, age);
								newQQ = true;
							}
						}

						string card = RandomUtility.RandomSymbol ();
						string area = RandomUtility.RandomArea ();
						DateTime joinTime = RandomUtility.RandomDateTime (_minValue, DateTime.Now);
						DateTime lastSpeakTime = RandomUtility.RandomDateTime (joinTime, DateTime.Now);
						string level = RandomUtility.RandomElement (levelGroup);
						GroupMemberType memberType = RandomUtility.RandomEnum<GroupMemberType> ();
						string exculsiveTitle = string.Empty;
						DateTime? exculsiveTitleDateTime = null;
						bool isBadRecord = RandomUtility.RandomBoolean ();
						bool isAllowEditorCard = RandomUtility.RandomBoolean ();

						GroupMember member = new GroupMember (qq.Id, qq.Nick, qq.Sex, qq.Age, group, card, area, joinTime, lastSpeakTime, level, memberType, exculsiveTitle, exculsiveTitleDateTime, isBadRecord, isAllowEditorCard);
						group.MemberCollection.Add (member);
						if (newQQ)
						{
							this.QQCollection.Add (member);
						}
					}
					#endregion
				}
			}
			#endregion

			#region 讨论组生成
			for (int i = 0; i < this._discussCount; i++)
			{
				long discussId = RandomUtility.RandomInt64 (Discuss.MinValue, 10000000000);
				if (this.DiscussCollection.Count (p => p.Id == discussId) >= 1)
				{
					i--;
				}
				else
				{
					Discuss discuss = new Discuss (discussId);
					this.DiscussCollection.Add (discuss);

					#region 讨论组成员生成
					int memberCount = RandomUtility.RandomInt32 (10, 200);

					for (int j = 0; j <= memberCount; j++)
					{
						QQ qq = null;
						bool newQQ = false;

						if (j == memberCount)
						{
							qq = this.RobotQQ;
						}
						else
						{
							long qqId = RandomUtility.RandomInt64 (QQ.MinValue, 100000000000);
							qq = this.QQCollection.Where (p => p.Id == qqId).FirstOrDefault ();
							if (qq == null)
							{
								string nick = RandomUtility.RandomSymbol ();
								Sex sex = RandomUtility.RandomEnum<Sex> ();
								int age = RandomUtility.RandomInt32 (0, 131);

								qq = new QQ (qqId, nick, sex, age);
								newQQ = true;
							}
						}

						DiscussMember member = new DiscussMember (qq.Id, qq.Nick, qq.Sex, qq.Age);
						discuss.MemberCollection.Add (member);
						if (newQQ)
						{
							this.QQCollection.Add (member);
						}
					};
					#endregion
				}
			}
			#endregion

			return this;
		}
		#endregion
	}
}
