using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.LibraryModel.CQP
{
	/// <summary>
	/// 描述 CQP 应用的 GroupMember 枚举
	/// </summary>
	public enum GroupMemberType
	{
		/// <summary>
		/// 正常成员
		/// </summary>
		Normal = 1,
		/// <summary>
		/// 管理员
		/// </summary>
		Manager = 2,
		/// <summary>
		/// 创建者
		/// </summary>
		Creator = 3
	}
}
