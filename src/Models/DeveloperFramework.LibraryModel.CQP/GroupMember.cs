using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.LibraryModel.CQP
{
	/// <summary>
	/// 描述 CQP 应用的 GroupMember 类型
	/// </summary>
	public class GroupMember 
	{
		#region --属性--
		/// <summary>
		/// 获取当前群成员所在的群
		/// </summary>
		public Group Group { get; }
		/// <summary>
		/// 获取当前群成员的 QQ 号
		/// </summary>
		public QQ QQ { get; }
		/// <summary>
		/// 获取当前群成员的群名片
		/// </summary>
		public string Card { get; }
		/// <summary>
		/// 获取当前群成员加入的时间
		/// </summary>
		public DateTime JoinTime { get; }
		/// <summary>
		/// 获取当前群成员最后发言时间
		/// </summary>
		public DateTime LastSpeakTime { get; }
		/// <summary>
		/// 获取当前群成员的群等级
		/// </summary>
		public string Level { get; }
		/// <summary>
		/// 获取当前群成员的类型
		/// </summary>
		public GroupMemberType MemberType { get; }
		/// <summary>
		/// 获取当前群成员的专属头衔
		/// </summary>
		public string ExclusiveTitle { get; }
		/// <summary>
		/// 获取当前群成员的专属头衔过期时间
		/// </summary>
		public DateTime ExclusiveTitleExpirationnTime { get; }
		/// <summary>
		/// 获取当前群成员是否为不良记录成员
		/// </summary>
		public bool IsBadRecord { get; }
		/// <summary>
		/// 获取当前群成员是否允许修改群名片
		/// </summary>
		public bool IsAllowEditorCard { get; }
		#endregion

		
	}
}
