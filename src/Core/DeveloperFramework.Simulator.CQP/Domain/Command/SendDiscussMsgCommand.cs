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
		#region --常量--
		public const string TYPE_SEND_MSG = "发送 (↑)";

		/// <summary>
		/// 未建立关系
		/// </summary>
		public const int RESULT_NO_RELATIONSHIP = -120;
		#endregion

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

			// 获取当前发送消息的机器人QQ
			QQ qq = this.Simulator.DataPool.RobotQQ;
			// 查询目标讨论组是否存在
			Discuss discuss = this.Simulator.DataPool.DiscussCollection.Where (p => p.Id == this.FromDiscuss).FirstOrDefault ();

			if (discuss == null)
			{
				LogCenter.Instance.Error (appInfo.Name, TYPE_SEND_MSG, $"无法向 [讨论组: {this.FromDiscuss}] 发送消息, 未查询到与该讨论组的关系");
				return RESULT_NO_RELATIONSHIP;
			}
			else
			{
				LogCenter.Instance.InfoSending (appInfo.Name, TYPE_SEND_MSG, $"向 [讨论组: {this.FromDiscuss}] 发送消息: {this.Message}");

				// 构建可撤回的消息
				Message msg = new Message (this.Message, discuss, qq);
				this.Simulator.DataPool.MessageCollection.Add (msg);
				return msg.Id;
			}
		}
		public override object ExecuteHaveNoAuth ()
		{
			AppInfo appInfo = this.App.Library.AppInfo;
			LogCenter.Instance.Info (appInfo.Name, TYPE_CHECK_AUTHORIZATION, $"检测到调用 Api [{nameof (CQPExport.CQ_sendDiscussMsg)}] 未经授权, 请检查 app.json 是否赋予权限", null, null);
			return RESULT_API_UNAUTHORIZED;
		}
		#endregion
	}
}
