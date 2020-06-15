using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.SimulatorModel.CQP
{
	/// <summary>
	/// 描述 CQP 应用动态库的群成员减少事件类型
	/// </summary>
	public enum GroupMemberDecreaseType
	{
		/// <summary>
		/// 成员退出
		/// </summary>
		MemberExit = 1,
		/// <summary>
		/// 成员移除
		/// </summary>
		MemberRemove = 2
	}
}
