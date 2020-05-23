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
	/// 发送讨论组消息命令
	/// </summary>
	[FunctionBinding (Function = nameof (CQPExport.CQ_sendDiscussMsg))]
	public class SendDiscussMsgCommand : AbstractCommand
	{
		#region --属性--
		public long FromDiscuss { get; }
		public string Message { get; }
		#endregion

		#region --构造函数--
		public SendDiscussMsgCommand (CQPSimulator simulator, CQPSimulatorApp app, bool isAuth, long discussId, string msg)
			: base (simulator, app, isAuth)
		{
			FromDiscuss = discussId;
			Message = msg ?? throw new ArgumentNullException (nameof (msg));
		}
		#endregion

		#region --公开方法--
		public override object ExecuteHaveAuth ()
		{
			AppInfo appInfo = this.App.Library.AppInfo;


			QQ qq = this.Simulator.DataPool.RobotQQ;
			Discuss discuss = this.Simulator.DataPool.DiscussCollection.Where (p => p.Id == this.FromDiscuss).FirstOrDefault ();
			if (discuss == null)
			{
				LogCenter.Instance.Info (appInfo.Name, CQPSimulator.STR_APP_SENDING, $"无法向 [讨论组: {this.FromDiscuss}] 发送消息, 未查询到与该讨论组的关系");
				return CQPResult.CQP_SEND_NOT_RELATION;
			}
			else
			{
				LogCenter.Instance.InfoSending (appInfo.Name, CQPSimulator.STR_APP_SENDING, $"向 [讨论组: {this.FromDiscuss}] 发送消息: {this.Message}");

				Message msg = new Message (this.Message, discuss, qq);
				this.Simulator.DataPool.MessageCollection.Add (msg);
				return msg.Id;
			}
		}
		public override object ExecuteHaveNoAuth ()
		{
			AppInfo appInfo = this.App.Library.AppInfo;
			LogCenter.Instance.Info (appInfo.Name, CQPSimulator.STR_APP_PERMISSIONS, $"检测到调用 Api [{nameof (CQPExport.CQ_sendDiscussMsg)}] 未经授权, 请检查 app.json 是否赋予权限", null, null);
			return CQPResult.CQP_APP_NOT_AUTHORIZE;
		}
		#endregion
	}
}
