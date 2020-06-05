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
	/// 获取语音命令
	/// </summary>
	[FunctionBinding(Function = nameof(CQPExport.CQ_getRecordV2))]
	public class GetRecordV2Command : AbstractCommand
	{
		#region --常量--
		public const string TYPE_GET_RECORD = "获取语音";
		#endregion

		#region --属性--
		public string File { get; }
		public string Format { get; }
        #endregion

        #region --构造函数--
        public GetRecordV2Command (CQPSimulator simulator, CQPSimulatorApp app, bool isAuth, string file, string format)
			: base (simulator, app, isAuth)
		{
			this.File = file;
			this.Format = format;
		}
        #endregion

        #region --公开方法--
        public override object ExecuteHaveAuth ()
		{
			//AppInfo appInfo = this.App.Library.AppInfo;
			//LogCenter.Instance.InfoSuccess(appInfo.Name, TYPE_GET_RECORD, "");
			throw new NotImplementedException ();
		}

		public override object ExecuteHaveNoAuth ()
		{
			AppInfo appInfo = this.App.Library.AppInfo;
			Logger.Instance.Info(appInfo.Name, TYPE_CHECK_AUTHORIZATION, $"检测到调用 Api [{nameof(CQPExport.CQ_getRecordV2)}] 未经授权, 请检查 app.json 是否赋予权限", null, null);
			return RESULT_API_UNAUTHORIZED;
		}
        #endregion
    }
}
