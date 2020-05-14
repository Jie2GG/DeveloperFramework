using DeveloperFramework.CQP;
using DeveloperFramework.LibraryModel.CQP.Dynamic;
using DeveloperFramework.Log.CQP;
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

			// 将消息存入缓存池
			Message msg = this.Message;
			

			LogCenter.Instance.InfoSending (appInfo.Name, CQPSimulator.STR_APPSENDING, $"向 [QQ: {this.FromQQ}] 发送消息: {this.Message}", null, null);
			return msg.Id;	// 返回消息 Id 用于撤回
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
