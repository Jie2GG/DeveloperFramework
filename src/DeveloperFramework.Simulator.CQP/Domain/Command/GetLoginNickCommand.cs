using DeveloperFramework.CQP;
using DeveloperFramework.LibraryModel.CQP;
using DeveloperFramework.Log.CQP;
using DeveloperFramework.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.Simulator.CQP.Domain.Command
{
	/// <summary>
	/// 获取当前的QQ昵称命令
	/// </summary>
	[FunctionBinding(Function = nameof(CQPExport.CQ_getLoginNick))]
	public class GetLoginNickCommand : AbstractCommand
	{
		#region --常量--
		public const string TYPE_GET_LOGIN_NICK= "获取图片";
		#endregion

		#region --构造函数--
		public GetLoginNickCommand(CQPSimulator simulator, CQPSimulatorApp app, bool isAuth)
			: base (simulator, app, isAuth)
		{
		}
        #endregion

        #region --公开方法--
        public override object ExecuteHaveAuth ()
		{
			//AppInfo appInfo = this.App.Library.AppInfo;
			//LogCenter.Instance.InfoSuccess(appInfo.Name, TYPE_GET_LOGIN_NICK, "");
			return this.Simulator.DataPool.RobotQQ.Nick;
		}

		public override object ExecuteHaveNoAuth ()
		{
			AppInfo appInfo = this.App.Library.AppInfo;
			Logger.Instance.Info(appInfo.Name, TYPE_CHECK_AUTHORIZATION, $"检测到调用 Api [{nameof(CQPExport.CQ_getLoginNick)}] 未经授权, 请检查 app.json 是否赋予权限", null, null);
			return RESULT_API_UNAUTHORIZED;
		}
        #endregion
    }
}
