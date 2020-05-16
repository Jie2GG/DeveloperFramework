using DeveloperFramework.Extension;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.SimulatorModel.CQP
{
	/// <summary>
	/// 描述 群成员 类型
	/// </summary>
	public class GroupMember : QQ
	{
		#region --属性--
		/// <summary>
		/// 获取或设置当前实例成员的群
		/// </summary>
		public Group Group { get; set; }
		/// <summary>
		/// 获取或设置当前实例的群名片
		/// </summary>
		public string Card { get; set; }
		/// <summary>
		/// 获取或设置当前实例成员的所在区域
		/// </summary>
		public string Area { get; set; }
		/// <summary>
		/// 获取或设置当前实例成员的加入时间
		/// </summary>
		public DateTime JoinTime { get; set; }
		/// <summary>
		/// 获取或设置当前实例成员的最后发言时间
		/// </summary>
		public DateTime LastSpeakTime { get; set; }
		/// <summary>
		/// 获取或设置当前实例成员的成员等级
		/// </summary>
		public string Level { get; set; }
		/// <summary>
		/// 获取或设置当前实例成员的成员类型
		/// </summary>
		public GroupMemberType MemberType { get; set; }
		/// <summary>
		/// 获取或设置当前实例成员的专属头衔
		/// </summary>
		public string ExclusiveTitle { get; set; }
		/// <summary>
		/// 获取或设置当前实例成员专属头衔的过期时间
		/// </summary>
		public DateTime? ExclusiveTitleExpirationTime { get; set; }
		/// <summary>
		/// 获取或设置当前实例成员是否为不良记录群成员
		/// </summary>
		public bool IsBadRecord { get; set; }
		/// <summary>
		/// 获取或设置当前实例成员是否允许其修改群名片
		/// </summary>
		public bool IsAllowEditorCard { get; set; }
		#endregion

		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="GroupMember"/> 类的新实例
		/// </summary>
		/// <param name="id">QQ号</param>
		/// <param name="nick">昵称</param>
		/// <param name="sex">性别</param>
		/// <param name="age">年龄</param>
		/// <param name="card">群名片</param>
		/// <param name="area">所在区域</param>
		/// <param name="joinTime">加入时间</param>
		/// <param name="lastSpeakTime">最后发言时间</param>
		/// <param name="level">成员等级</param>
		/// <param name="memberType">成员类型</param>
		/// <param name="exclusiveTitle">专属头衔</param>
		/// <param name="exclusiveTitleExpirationTime">专属头衔过期时间</param>
		/// <param name="isBadRecord">是否为不良群成员</param>
		/// <param name="isAllowEditorCard">是否允许修改群名片</param>
		public GroupMember (long id, string nick, Sex sex, int age, Group group, string card, string area, DateTime joinTime, DateTime lastSpeakTime, string level, GroupMemberType memberType, string exclusiveTitle, DateTime? exclusiveTitleExpirationTime, bool isBadRecord, bool isAllowEditorCard)
			: base (id, nick, sex, age)
		{
			Group = group ?? throw new ArgumentNullException (nameof (group));
			Card = card ?? throw new ArgumentNullException (nameof (card));
			Area = area ?? throw new ArgumentNullException (nameof (area));
			JoinTime = joinTime;
			LastSpeakTime = lastSpeakTime;
			Level = level ?? throw new ArgumentNullException (nameof (level));
			MemberType = memberType;
			ExclusiveTitle = exclusiveTitle ?? throw new ArgumentNullException (nameof (exclusiveTitle));
			ExclusiveTitleExpirationTime = exclusiveTitleExpirationTime;
			IsBadRecord = isBadRecord;
			IsAllowEditorCard = isAllowEditorCard;
		}
		#endregion

		#region --公开方法--
		/// <summary>
		/// 获取当前实例的 <see cref="byte"/> 数组
		/// </summary>
		/// <returns>当前实例的 <see cref="byte"/> 数组</returns>
		public override byte[] ToByteArray ()
		{
			using (BinaryWriter writer = new BinaryWriter (new MemoryStream ()))
			{
				writer.Write_Ex (this.Group.Id);
				writer.Write_Ex (this.Id);
				writer.Write_Ex (this.Nick);
				writer.Write_Ex (this.Card);
				writer.Write_Ex ((int)this.Sex);
				writer.Write_Ex (this.Age);
				writer.Write_Ex (this.Area);
				writer.Write_Ex (this.JoinTime.ToTimeStamp ());
				writer.Write_Ex (this.LastSpeakTime.ToTimeStamp ());
				writer.Write_Ex (this.Level);
				writer.Write_Ex ((int)this.MemberType);
				writer.Write_Ex (this.IsBadRecord ? 1 : 0);
				writer.Write_Ex (this.ExclusiveTitle);
				writer.Write_Ex (this.ExclusiveTitleExpirationTime == null ? -1 : this.ExclusiveTitleExpirationTime.Value.ToTimeStamp ());
				writer.Write_Ex (this.IsAllowEditorCard ? 1 : 0);
				return writer.ToArray ();
			}
		}
		#endregion
	}
}
