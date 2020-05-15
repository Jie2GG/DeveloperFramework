using DeveloperFramework.CQP;
using DeveloperFramework.LibraryModel.CQP;
using DeveloperFramework.Log.CQP;
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
			// 将消息存入缓存池
			Message msg = this.Message;
			

			LogCenter.Instance.InfoSending (appInfo.Name, CQPSimulator.STR_APPSENDING, $"向 [讨论组: {this.FromDiscuss}] 发送消息: {this.Message}", null, null);
			return msg.Id;
		}
		public override object ExecuteHaveNoAuth ()
		{
			AppInfo appInfo = this.App.Library.AppInfo;
			LogCenter.Instance.Info (appInfo.Name, CQPSimulator.STR_APPPERMISSIONS, $"检测到 Api 调用未经授权, 请检查 app.json 是否赋予权限", null, null);
			return -1;
		}
		#endregion
	}
}
