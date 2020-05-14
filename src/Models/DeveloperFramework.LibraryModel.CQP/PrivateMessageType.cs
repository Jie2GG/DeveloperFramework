using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.LibraryModel.CQP
{
	/// <summary>
	/// 描述 CQP 应用动态库的私有消息事件类型
	/// </summary>
	public enum PrivateMessageType
	{
		/// <summary>
		/// 在线状态
		/// </summary>
		OnlineStatus = 1,
		/// <summary>
		/// 群
		/// </summary>
		Group = 2,
		/// <summary>
		/// 讨论组
		/// </summary>
		Discuss = 3,
		/// <summary>
		/// 好友
		/// </summary>
		Friend = 11
	}
}
