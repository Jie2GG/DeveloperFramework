using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.LibraryModel.CQP
{
	/// <summary>
	/// 描述 CQP 应用动态库的事件类型
	/// </summary>
	public enum AppEventType
	{
		/// <summary>
		/// 群消息事件
		/// </summary>
		GroupMessage = 2,
		/// <summary>
		/// 讨论组消息事件
		/// </summary>
		DiscussMessage = 4,
		/// <summary>
		/// 群文件上传事件
		/// </summary>
		GroupFileUpload = 11,
		/// <summary>
		/// 私聊消息事件
		/// </summary>
		PrivateMessage = 21,
		/// <summary>
		/// 群管理员变动事件
		/// </summary>
		GroupManagerChange = 101,
		/// <summary>
		/// 群成员减少事件
		/// </summary>
		GroupMemberDecrease = 102,
		/// <summary>
		/// 群成员增加事件
		/// </summary>
		GroupMemberIncrease = 103,
		/// <summary>
		/// 群禁言事件
		/// </summary>
		GroupMemberBanSpeak = 104,
		/// <summary>
		/// 好友已添加事件
		/// </summary>
		FriendAdd = 201,
		/// <summary>
		/// 好友添加请求
		/// </summary>
		FriendAddRequest = 301,
		/// <summary>
		/// 群添加请求
		/// </summary>
		GroupAddRequest = 302,
		/// <summary>
		/// 酷Q启动事件
		/// </summary>
		CQStartup = 1001,
		/// <summary>
		/// 酷Q退出事件
		/// </summary>
		CQExit = 1002,
		/// <summary>
		/// 应用启用事件
		/// </summary>
		CQAppEnable = 1003,
		/// <summary>
		/// 应用停用事件
		/// </summary>
		CQAppDisable = 1004
	}
}
