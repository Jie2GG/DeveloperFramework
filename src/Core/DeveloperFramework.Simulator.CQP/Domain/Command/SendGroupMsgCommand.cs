using DeveloperFramework.CQP;
using DeveloperFramework.LibraryModel.CQP;
using DeveloperFramework.Log.CQP;
using DeveloperFramework.SimulatorModel.CQP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.Simulator.CQP.Domain.Command
{
	/// <summary>
	/// 发送群聊消息命令
	/// </summary>
	[FunctionBinding (Function = nameof (CQPExport.CQ_sendGroupMsg))]
	public class SendGroupMsgCommand : AbstractCommand
	{
		#region --属性--
		public long FromGroup { get; }
		public string Message { get; }
		#endregion

		#region --构造函数--
		public SendGroupMsgCommand (CQPSimulator simulator, CQPSimulatorApp app, bool isAuth, long groupId, string msg)
			: base (simulator, app, isAuth)
		{
			FromGroup = groupId;
			Message = msg;
		}
		#endregion

		#region --公开方法--
		public override object ExecuteHaveAuth ()
		{
			AppInfo appInfo = this.App.Library.AppInfo;

			QQ qq = this.Simulator.DataPool.RobotQQ;
			Group group = this.Simulator.DataPool.GroupCollection.Where (p => p.Id == this.FromGroup).FirstOrDefault ();
			if (group == null)
			{
				LogCenter.Instance.Info (appInfo.Name, CQPSimulator.STR_APP_SENDING, $"无法向 [群: {this.FromGroup}] 发送消息, 未查询到与该群的关系");
				return CQPResult.CQP_SEND_NOT_RELATION;
			}
			else
			{
				LogCenter.Instance.InfoSending (appInfo.Name, CQPSimulator.STR_APP_SENDING, $"向 [群: {this.FromGroup}] 发送消息: {this.Message}");

				Message msg = new Message (this.Message, group, qq);
				this.Simulator.DataPool.MessageCollection.Add (msg);
				return msg.Id;
			}
		}

		public override object ExecuteHaveNoAuth ()
		{
			AppInfo appInfo = this.App.Library.AppInfo;
			LogCenter.Instance.Info (appInfo.Name, CQPSimulator.STR_APP_PERMISSIONS, $"检测到调用 Api [{nameof (CQPExport.CQ_sendGroupMsg)}] 未经授权, 请检查 app.json 是否赋予权限", null, null);
			return CQPResult.CQP_APP_NOT_AUTHORIZE;
		}
		#endregion
	}
}
