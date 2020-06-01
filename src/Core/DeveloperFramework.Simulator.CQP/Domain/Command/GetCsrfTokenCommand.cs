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
	/// 获取CsrfToken命令
	/// </summary>
	[FunctionBinding (Function = nameof (CQPExport.CQ_getCsrfToken))]
	public class GetCsrfTokenCommand : AbstractCommand
	{
		#region --常量--
		public const string TYPE_GET_CSRF_TOKEN = "获取CsrfToken";
		#endregion

		#region --构造函数--
		public GetCsrfTokenCommand (CQPSimulator simulator, CQPSimulatorApp app, bool isAuth)
			: base (simulator, app, isAuth)
		{ }
		#endregion

		public override object ExecuteHaveAuth ()
		{
			AppInfo appInfo = this.App.Library.AppInfo;

			LogCenter.Instance.InfoSuccess (appInfo.Name, TYPE_GET_CSRF_TOKEN, $"请求 CsrfToken (由于不考虑登录状况, 仅返回随机值)");

			return RandomUtility.RandomInt32 ();
		}

		public override object ExecuteHaveNoAuth ()
		{
			AppInfo appInfo = this.App.Library.AppInfo;
			LogCenter.Instance.Info (appInfo.Name, TYPE_CHECK_AUTHORIZATION, $"检测到调用 Api [{nameof (CQPExport.CQ_getCsrfToken)}] 未经授权, 请检查 app.json 是否赋予权限", null, null);
			return RESULT_API_UNAUTHORIZED;
		}
	}
}
