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
	public class SendPrivateMsgCommand : Command
	{
		/// <summary>
		/// 获取当前实例的来源QQ
		/// </summary>
		public long FromQQ { get; }
		/// <summary>
		/// 获取当前实例的消息内容
		/// </summary>
		public string Message { get; }

		/// <summary>
		/// 初始化 <see cref="SendPrivateMsgCommand"/> 类的新实例
		/// </summary>
		/// <param name="app">相关联的应用 <see cref="CQPSimulatorApp"/></param>
		/// <param name="qqId">相关联的 QQ号</param>
		/// <param name="msg">相关联的消息内容</param>
		public SendPrivateMsgCommand (CQPSimulatorApp app, long qqId, string msg)
			: base (app)
		{
			this.FromQQ = qqId;
			this.Message = msg;
		}

		/// <summary>
		/// 处理命令的详细过程
		/// </summary>
		/// <returns>返回处理结果</returns>
		public override object Execute ()
		{
			AppInfo appInfo = this.App.Library.AppInfo;

			LogCenter.Instance.InfoSending (appInfo.Name, CQPSimulator.STR_APPSENDING, $"向 [QQ: {this.FromQQ}] 发送消息: {this.Message}", null, null);
			
			return 0;
		}
	}
}
