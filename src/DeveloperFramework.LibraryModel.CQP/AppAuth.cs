using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.LibraryModel.CQP
{
	/// <summary>
	/// 描述 CQP 应用动态库的权限枚举
	/// </summary>
	public enum AppAuth
	{
		/// <summary>
		/// [敏感]取Cookies
		/// </summary>
		[Description ("[敏感]取Cookies")]
		GetCookiesOrCsrfToken = 20,
		/// <summary>
		/// 接收语音
		/// </summary>
		[Description ("接收语音")]
		GetRecord = 30,
		/// <summary>
		/// 发送群消息
		/// </summary>
		[Description ("发送群消息")]
		SendGroupMessage = 101,
		/// <summary>
		/// 发送讨论组消息
		/// </summary>
		[Description ("发送讨论组消息")]
		SendDiscussMessage = 103,
		/// <summary>
		/// 发送私聊消息
		/// </summary>
		[Description ("发送私聊消息")]
		SendPrivateMsssage = 106,
		/// <summary>
		/// [敏感]发送赞
		/// </summary>
		[Description ("[敏感]发送赞")]
		SendLike = 110,
		/// <summary>
		/// 置群员移除
		/// </summary>
		[Description ("置群员移除")]
		SetGroupKick = 120,
		/// <summary>
		/// 置群员禁言
		/// </summary>
		[Description ("置群员禁言")]
		SetGroupBan = 121,
		/// <summary>
		/// 置群管理员
		/// </summary>
		[Description ("置群管理员")]
		SetGroupAdmin = 122,
		/// <summary>
		/// 置全群禁言
		/// </summary>
		[Description ("置全群禁言")]
		SetGroupWholeBan = 123,
		/// <summary>
		/// 置匿名群员禁言
		/// </summary>
		[Description ("置匿名群员禁言")]
		SetGroupAnonymousBan = 124,
		/// <summary>
		/// 置群匿名设置
		/// </summary>
		[Description ("置群匿名设置")]
		SetGroupAnonymous = 125,
		/// <summary>
		/// 置群成员名片
		/// </summary>
		[Description ("置群成员名片")]
		SetGroupCard = 126,
		/// <summary>
		/// [敏感]置群退出
		/// </summary>
		[Description ("[敏感]置群退出")]
		SetGroupLeave = 127,
		/// <summary>
		/// 置群成员专属头衔
		/// </summary>
		[Description ("置群成员专属头衔")]
		SetGroupSpecialTitle = 128,
		/// <summary>
		/// 取群成员信息
		/// </summary>
		[Description ("取群成员信息")]
		GetGroupMemberInfo = 130,
		/// <summary>
		/// 取陌生人信息
		/// </summary>
		[Description ("取陌生人信息")]
		GetStrangerInfo = 131,
		/// <summary>
		/// 取群信息
		/// </summary>
		[Description ("取群信息")]
		GetGroupInfo = 132,
		/// <summary>
		/// 置讨论组退出
		/// </summary>
		[Description ("置讨论组退出")]
		SetDiscussLeave = 140,
		/// <summary>
		/// 置好友添加请求
		/// </summary>
		[Description ("置好友添加请求")]
		SetFriendAddRequest = 150,
		/// <summary>
		/// 置群添加请求
		/// </summary>
		[Description ("置群添加请求")]
		SetGroupAddRequest = 151,
		/// <summary>
		/// 取群成员列表
		/// </summary>
		[Description ("取群成员列表")]
		GetGroupMemberList = 160,
		/// <summary>
		/// 取群列表
		/// </summary>
		[Description ("取群列表")]
		GetGroupList = 161,
		/// <summary>
		/// 取好友列表
		/// </summary>
		[Description ("取好友列表")]
		GetFriendList = 162,
		/// <summary>
		/// 撤回消息
		/// </summary>
		[Description ("撤回消息")]
		DeleteMsg = 180
	}
}
