using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.LibraryModel.CQP
{
	/// <summary>
	/// 描述 CQP 应用动态库的群成员增加事件类型
	/// </summary>
	public enum GroupMemberIncreaseType
	{
		/// <summary>
		/// 管理员通过
		/// </summary>
		ManagerPass = 1,
		/// <summary>
		/// 管理员邀请
		/// </summary>
		ManagerInvite = 2
	}
}
