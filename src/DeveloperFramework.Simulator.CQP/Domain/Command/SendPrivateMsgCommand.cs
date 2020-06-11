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
	/// 发送私聊消息命令
	/// </summary>
	[FunctionBinding (Function = nameof (CQPExport.CQ_sendPrivateMsg))]
	public class SendPrivateMsgCommand : AbstractCommand
	{
		#region --常量--
		public const string TYPE_SEND_MSG = "发送 (↑)";
		/// <summary>
		/// 未建立关系
		/// </summary>
		public const int RESULT_NO_RELATIONSHIP = -100;
		#endregion

		#region --属性--
		public long FromQQ { get; }
		public string Message { get; }
		#endregion

		#region --构造函数--
		public SendPrivateMsgCommand (CQPSimulator simulator, CQPSimulatorApp app, bool isAuth, long qqId, string msg)
			: base (simulator, app, isAuth)
		{
			this.FromQQ = qqId;
			this.Message = msg;
		}
		#endregion

		#region --公开方法--
		public override object ExecuteHaveAuth ()
		{
			AppInfo appInfo = this.App.Library.AppInfo;

			// 在 QQ 列表中查询是否存在目标号码
			QQ qq = base.Simulator.DataPool.QQCollection.Where (p => p.Id == this.FromQQ).FirstOrDefault ();

			if (qq == null)
			{
				Logger.Instance.Info (appInfo.Name, TYPE_SEND_MSG, $"无法向 [QQ: {this.FromQQ}] 发送消息, 未查询到与该QQ的关系");
				return RESULT_NO_RELATIONSHIP;
			}
			else
			{
				Logger.Instance.InfoSending (appInfo.Name, TYPE_SEND_MSG, $"向 [QQ: {this.FromQQ}] 发送消息: {this.Message}");

				// 构建可撤回消息
				Message msg = new Message (this.Message, qq);
				this.Simulator.DataPool.MessageCollection.Add (msg);
				return msg.Id;
			}
		}

		public override object ExecuteHaveNoAuth ()
		{
			AppInfo appInfo = this.App.Library.AppInfo;
			Logger.Instance.Info (appInfo.Name, TYPE_CHECK_AUTHORIZATION, $"检测到调用 Api [{nameof (CQPExport.CQ_sendPrivateMsg)}] 未经授权, 请检查 app.json 是否赋予权限");
			return RESULT_API_UNAUTHORIZED;
		}
		#endregion
	}
}
