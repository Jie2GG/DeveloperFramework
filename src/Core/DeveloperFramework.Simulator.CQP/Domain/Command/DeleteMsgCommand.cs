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

			Message msg = this.Simulator.DataPool.MessageCollection.Where (p => p.Id == this.MsgId).FirstOrDefault ();
			if (msg == null)
			{
				LogCenter.Instance.Info (appInfo.Name, CQPSimulator.STR_APP_SENDING, $"无法撤回消息 [ID: {this.MsgId}], 可能消息已撤回或消息不存在");
				return CQPResult.CQP_MSG_NOT_FOUND;
			}
			else
			{
				if (msg.FromGroup != null)  // 如果是群消息
				{
					GroupMember member = msg.FromQQ as GroupMember;
					if (!msg.FromQQ.Equals (this.Simulator.DataPool.RobotQQ) && (member.MemberType == GroupMemberType.Creator || member.MemberType == GroupMemberType.Manager))
					{
						// 群管理员撤回群成员的消息, 移除对象
						while (!this.Simulator.DataPool.MessageCollection.TryTake (out Message outMsg)) ;

						LogCenter.Instance.InfoSuccess (appInfo.Name, CQPSimulator.STR_APP_DEL_MSG, $"撤回消息 [ID: {this.MsgId}]");
						return 0;
					}
				}

				TimeSpan span = DateTimeUtility.GetDateTimeInterval (DateTime.Now, msg.SendTime);
				if (span.TotalSeconds <= 120)
				{
					// 消息时间未超时, 移除对象
					while (!this.Simulator.DataPool.MessageCollection.TryTake (out Message outMsg)) ;
					LogCenter.Instance.InfoSuccess (appInfo.Name, CQPSimulator.STR_APP_DEL_MSG, $"撤回消息 [ID: {this.MsgId}]");
					return 0;
				}

			}

			LogCenter.Instance.Info (appInfo.Name, CQPSimulator.STR_APP_DEL_MSG, $"无法撤回消息 [ID: {this.MsgId}], 消息发送已超过 2分钟");

			return CQPResult.CQP_MSG_NOT_AUTHORIZE;
		}

		public override object ExecuteHaveNoAuth ()
		{
			AppInfo appInfo = this.App.Library.AppInfo;
			LogCenter.Instance.Info (appInfo.Name, CQPSimulator.STR_APP_PERMISSIONS, $"检测到调用 Api [{nameof (CQPExport.CQ_deleteMsg)}] 未经授权, 请检查 app.json 是否赋予权限", null, null);
			return CQPResult.CQP_APP_NOT_AUTHORIZE;
		} 
		#endregion
	}
}
