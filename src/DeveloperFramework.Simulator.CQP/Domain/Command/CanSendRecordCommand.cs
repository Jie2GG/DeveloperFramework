﻿using DeveloperFramework.CQP;
using DeveloperFramework.LibraryModel.CQP;
using DeveloperFramework.Log.CQP;
using DeveloperFramework.SimulatorModel.CQP;
using DeveloperFramework.Utility;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DeveloperFramework.Simulator.CQP.Domain.Command
{   /// <summary>
	/// 查询发送语音命令
	/// </summary>
	[FunctionBinding (Function = nameof (CQPExport.CQ_canSendRecord))]
	public class CanSendRecordCommand : AbstractCommand
	{
		#region --常量--
		public const string TYPE_CAN_SEND_RECORD = "查询发送语音";
		#endregion

		#region --构造函数--
		public CanSendRecordCommand (CQPSimulator simulator, CQPSimulatorApp app, bool isAuth)
			: base (simulator, app, isAuth)
		{ }
		#endregion

		#region --公开方法--
		public override object ExecuteHaveAuth ()
		{
			//AppInfo appInfo = this.App.Library.AppInfo;
			//LogCenter.Instance.InfoSuccess(appInfo.Name, TYPE_CAN_SEND_RECORD, this.Simulator.CanSendImage ? "可以发送" : "不可发送");
			//return this.Simulator.CanSendImage;

			bool isExists = true;
			// TODO: 语音组件的调用或者存在检查

			// 如果语音组件正常并且是 V9Pro 则允许接收
			return isExists && this.Simulator.CQPType == CQPType.Pro;
		}

		public override object ExecuteHaveNoAuth ()
		{
			AppInfo appInfo = this.App.Library.AppInfo;
			Logger.Instance.Warning (appInfo.Name, TYPE_CHECK_AUTHORIZATION, $"检测到调用 Api [{nameof (CQPExport.CQ_canSendRecord)}] 未经授权, 请检查 app.json 是否赋予权限", null, null);
			return RESULT_API_UNAUTHORIZED;
		}
		#endregion
	}
}
