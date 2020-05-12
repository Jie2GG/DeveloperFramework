using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.LibraryModel.CQP.Dynamic
{
	/// <summary>
	/// 描述 CQP 应用动态库的群添加请求事件类型
	/// </summary>
	public enum GroupAddRequestType
	{
		/// <summary>
		/// 其他人申请入群
		/// </summary>
		OtherApplicant = 1,
		/// <summary>
		/// 接受他人邀请入群
		/// </summary>
		AcceptInvitation = 2
	}
}
