using DeveloperFramework.CQP;
using DeveloperFramework.LibraryModel.CQP;
using DeveloperFramework.Log.CQP;
using DeveloperFramework.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.Simulator.CQP.Domain.Command
{
	[FunctionBinding (Function = nameof (CQPExport.CQ_getCookiesV2))]
	public class GetCookiesV2Command : AbstractCommand
	{
		#region --常量--
		public const string TYPE_GET_COOKIES = "获取Cookies";
		#endregion

		#region --属性--
		public string Domain;
		#endregion

		#region --构造函数--
		public GetCookiesV2Command (CQPSimulator simulator, CQPSimulatorApp app, bool isAuth, string domain)
			: base (simulator, app, isAuth)
		{
			this.Domain = domain;
		}
		#endregion

		#region --公开方法--
		public override object ExecuteHaveAuth ()
		{
			AppInfo appInfo = this.App.Library.AppInfo;

			LogCenter.Instance.InfoSuccess (appInfo.Name, TYPE_GET_COOKIES, $"请求 Cookies (由于不考虑登录状况, 仅返回空字符串)");

			return string.Empty;
		}

		public override object ExecuteHaveNoAuth ()
		{
			AppInfo appInfo = this.App.Library.AppInfo;
			LogCenter.Instance.Info (appInfo.Name, TYPE_CHECK_AUTHORIZATION, $"检测到调用 Api [{nameof (CQPExport.CQ_deleteMsg)}] 未经授权, 请检查 app.json 是否赋予权限", null, null);
			return RESULT_API_UNAUTHORIZED;
		}
		#endregion
	}
}
