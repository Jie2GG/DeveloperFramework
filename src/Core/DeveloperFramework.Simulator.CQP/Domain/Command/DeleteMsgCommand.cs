using DeveloperFramework.CQP;
using DeveloperFramework.LibraryModel.CQP;
using DeveloperFramework.Log.CQP;
using DeveloperFramework.SimulatorModel.CQP;
using DeveloperFramework.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.Simulator.CQP.Domain.Command
{
	/// <summary>
	/// 撤回消息命令
	/// </summary>
	[FunctionBinding (Function = nameof (CQPExport.CQ_deleteMsg))]
	public class DeleteMsgCommand : AbstractCommand
	{
		#region --常量--
		public const string TYPE_MSG_UNDO = "撤回消息";
		/// <summary>
		/// 消息未找到
		/// </summary>
		public const int RESULT_MSG_NOT_FOUND = -130;
		/// <summary>
		/// 消息未撤回
		/// </summary>
		public const int RESULT_MSG_NOT_AUTHORIZE = -131;
		/// <summary>
		/// 消息已过期
		/// </summary>
		public const int RESULT_MSG_OVERDUE = -132;
		#endregion

		#region --属性--
		public long MsgId { get; }
		#endregion

		#region --构造函数--
		public DeleteMsgCommand (CQPSimulator simulator, CQPSimulatorApp app, bool isAuth, long msgId)
			: base (simulator, app, isAuth)
		{
			MsgId = msgId;
		}
		#endregion

		#region --公开方法--
		public override object ExecuteHaveAuth ()
		{
			AppInfo appInfo = this.App.Library.AppInfo;

			// 获取用来撤回消息的机器人QQ
			QQ qq = this.Simulator.DataPool.RobotQQ;
			// 根据消息 Id 查询是否存在目标消息
			Message msg = this.Simulator.DataPool.MessageCollection.Where (p => p.Id == this.MsgId).FirstOrDefault ();

			if (msg == null)
			{
				LogCenter.Instance.Info (appInfo.Name, TYPE_MSG_UNDO, $"无法撤回消息 [ID: {this.MsgId}], 指定的消息不存在");
				return RESULT_MSG_NOT_FOUND;
			}
			else
			{
				bool result = false;

				// 如果是群消息
				if (msg.FromGroup != null && msg.FromGroup == null)
				{
					// 获取当前机器人QQ在这个群的群成员信息
					GroupMember robotMember = msg.FromGroup.MemberCollection.Where (p => p.Id == qq.Id).FirstOrDefault ();
					// 获取当前消息发送者QQ的群成员信息
					GroupMember fromMember = msg.FromQQ as GroupMember;

					if (!robotMember.Equals (fromMember))
					{
						// 如果 机器人QQ是管理并且消息来源QQ是群主
						if (robotMember.MemberType == GroupMemberType.Manager && fromMember.MemberType == GroupMemberType.Creator)
						{
							LogCenter.Instance.Info (appInfo.Name, TYPE_MSG_UNDO, $"无法撤回消息");
							return RESULT_MSG_NOT_AUTHORIZE;
						}

						// 如果 机器人QQ是群主并且消息来源QQ是管理或群成员 或者 机器人QQ是管理并且消息来源QQ是群成员
						if ((robotMember.MemberType == GroupMemberType.Creator && (fromMember.MemberType == GroupMemberType.Normal || fromMember.MemberType == GroupMemberType.Manager)) || (robotMember.MemberType == GroupMemberType.Manager && (fromMember.MemberType == GroupMemberType.Normal)))
						{
							// 强制撤回消息
							result = msg.Revocation (true);
						}
					}
				}
				else
				{
					result = msg.Revocation (false);
				}

				if (result)
				{
					LogCenter.Instance.InfoSuccess (appInfo.Name, TYPE_MSG_UNDO, $"已撤回消息 [ID: {msg.Id}]");
					return RESULT_SUCCESS;
				}
				else
				{
					LogCenter.Instance.Info (appInfo.Name, TYPE_MSG_UNDO, $"撤回消息 [ID: {msg.Id}] 失败, 消息已发送超过 2分钟");
					return RESULT_MSG_OVERDUE;
				}

			}
		}

		public override object ExecuteHaveNoAuth ()
		{
			AppInfo appInfo = this.App.Library.AppInfo;
			LogCenter.Instance.Info (appInfo.Name, TYPE_CHECK_AUTHORIZATION, $"检测到调用 Api [{nameof (CQPExport.CQ_deleteMsg)}] 未经授权, 请检查 app.json 是否赋予权限", null, null);
			return RESULT_API_UNAUTHORIZED;
		}
		#endregion
	}
}
