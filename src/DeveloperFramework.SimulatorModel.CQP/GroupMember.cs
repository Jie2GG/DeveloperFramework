using System;
using System.IO;
using System.Linq;

using DeveloperFramework.Extension;

namespace DeveloperFramework.SimulatorModel.CQP
{
	/// <summary>
	/// 描述群成员的类
	/// </summary>
	public class GroupMember : QQ, IEquatable<GroupMember>
	{
		#region --字段--
		private GroupAnonymous _anonymous;
		#endregion

		#region --属性--
		/// <summary>
		/// 获取当前实例的匿名对象
		/// </summary>
		public GroupAnonymous Anonymous
		{
			get
			{
				if (this._anonymous is null)
				{
					this._anonymous = new GroupAnonymous (this.Id);
				}

				return this._anonymous;
			}
		}
		/// <summary>
		/// 获取或设置当前实例的名片
		/// </summary>
		public string VisitingCard { get; set; }
		/// <summary>
		/// 获取或设置当前实例的所在地 (地区)
		/// </summary>
		public string Locality { get; set; }
		/// <summary>
		/// 获取或设置当前实例的加入时间
		/// </summary>
		public DateTime JoinTime { get; set; }
		/// <summary>
		/// 获取或设置当前实例的最后发言时间
		/// </summary>
		public DateTime LastSpeakTime { get; set; }
		/// <summary>
		/// 获取或设置当前实例的成员等级
		/// </summary>
		public GroupMemberLevel Level { get; set; }
		/// <summary>
		/// 获取或设置当前实例的成员类型
		/// </summary>
		public GroupMemberType MemberType { get; set; }
		/// <summary>
		/// 获取或设置当前实例的成员头衔
		/// </summary>
		public string Title { get; set; }
		/// <summary>
		/// 获取或设置当前实例的成员头衔过期时间
		/// </summary>
		public DateTime? TitleExpiredTime { get; set; }
		/// <summary>
		/// 获取或设置当前实例是否为不良记录成员
		/// </summary>
		public bool IsBadRecordMember { get; set; }
		/// <summary>
		/// 获取或设置当前实例是否允许编辑名片
		/// </summary>
		public bool IsAllowEditVisitingCard { get; set; }
		#endregion

		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="GroupMember"/> 类的新实例
		/// </summary>
		/// <param name="id">绑定于当前实例的唯一标识 (ID)</param>
		public GroupMember (long id)
			: base (id)
		{

		}
		#endregion

		#region --公开方法--
		/// <summary>
		/// 指示当前对象是否等于同一类型的另一个对象
		/// </summary>
		/// <param name="other">一个与此对象进行比较的对象</param>
		/// <returns>如果当前对象等于 other 参数，则为 <see langword="true"/>；否则为 <see langword="false"/></returns>
		public bool Equals (GroupMember other)
		{
			return base.Equals (other);
		}
		/// <summary>
		/// 指示当前对象是否等于同一类型的另一个对象
		/// </summary>
		/// <param name="other">一个与此对象进行比较的对象</param>
		/// <returns>如果当前对象等于 other 参数，则为 <see langword="true"/>；否则为 <see langword="false"/></returns>
		public override bool Equals (object obj)
		{
			return this.Equals (obj as GroupMember);
		}
		/// <summary>
		/// 作为默认哈希函数
		/// </summary>
		/// <returns>当前对象的哈希代码</returns>
		public override int GetHashCode ()
		{
			return base.GetHashCode ();
		}
		/// <summary>
		/// 返回当前实例的字节数组
		/// </summary>
		/// <param name="group">绑定字节数组信息中的群</param>
		/// <returns>当前实例的字节数组</returns>
		public byte[] ToByteArray (Group group)
		{
			using (BinaryWriter writer = new BinaryWriter (new MemoryStream ()))
			{
				writer.Write_Ex (group.Id);
				writer.Write_Ex (this.Id);
				writer.Write_Ex (this.Nickname);
				writer.Write_Ex (this.VisitingCard);
				writer.Write_Ex ((int)this.Gender);
				writer.Write_Ex (this.Age);
				writer.Write_Ex (this.Locality);
				writer.Write_Ex (this.JoinTime.ToTimeStamp ());
				writer.Write_Ex (this.LastSpeakTime.ToTimeStamp ());
				writer.Write_Ex (group.GetLevelName (this.Level));
				writer.Write_Ex ((int)this.MemberType);
				writer.Write_Ex (this.IsBadRecordMember ? 1 : 0);
				writer.Write_Ex (this.Title);
				writer.Write_Ex (this.TitleExpiredTime == null ? -1 : this.TitleExpiredTime.Value.ToTimeStamp ());
				writer.Write_Ex (this.IsAllowEditVisitingCard ? 1 : 0);
				return writer.ToArray ();
			}
		}
		/// <summary>
		/// 返回表示当前对象的 Base64 字符串
		/// </summary>
		/// <param name="group">绑定字符串信息中的群</param>
		/// <returns>表示当前对象的字符串</returns>
		public string ToBase64 (Group group)
		{
			return Convert.ToBase64String (this.ToByteArray (group));
		}
		#endregion
	}
}
