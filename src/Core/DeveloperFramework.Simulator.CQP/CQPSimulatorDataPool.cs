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
		private int _qqCount = 100;
		private int _friendCount = 100;
		private int _groupCount = 10;
		private string[][] _groupLevels = { new string[] { "萌新", "黑马", "资深", "精英", "大佬", "怪物" } };
		#endregion

		#region --属性--
		/// <summary>
		/// 获取当前数据池的机器人QQ
		/// </summary>
		public QQ RobotQQ { get; }
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
			this.MessageCollection = new ConcurrentBag<Message> ();
		}
		#endregion

		#region --公开方法--
		/// <summary>
		/// 生成已指定条件的数据
		/// </summary>
		/// <returns>当前数据池实例</returns>
		public CQPSimulatorDataPool Generate ()
		{
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

					for (int j = 0; j < currentMemberCount; j++)
					{
						long qqId = RandomUtility.RandomInt64 (QQ.MinValue, 100000000000);
						QQ qq = this.QQCollection.Where (p => p.Id == qqId).FirstOrDefault ();


						if (qq != null)
						{
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
						}
						else
						{
							string nick = RandomUtility.RandomSymbol ();
							Sex sex = RandomUtility.RandomEnum<Sex> ();
							int age = RandomUtility.RandomInt32 (0, 131);

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


							GroupMember member = new GroupMember (qqId, nick, sex, age, group, card, area, joinTime, lastSpeakTime, level, memberType, exculsiveTitle, exculsiveTitleDateTime, isBadRecord, isAllowEditorCard);
							group.MemberCollection.Add (member);
							this.QQCollection.Add (member);
						}
					}
					#endregion
				}
			}
			#endregion

			#region QQ生成
			for (int i = 0; i < this._qqCount; i++)
			{
				long qqId = RandomUtility.RandomInt64 (QQ.MinValue, 100000000000);
				if (this.QQCollection.Count (p => p.Id == qqId) >= 1)
				{
					i--;
				}
				else
				{
					string nick = RandomUtility.RandomSymbol ();
					Sex sex = RandomUtility.RandomEnum<Sex> ();
					int age = RandomUtility.RandomInt32 (0, 131);
					this.QQCollection.Add (new QQ (qqId, nick, sex, age));
				}
			}
			#endregion

			return this;
		} 
		#endregion
	}
}
