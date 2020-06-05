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
	/// 获取图片命令
	/// </summary>
	[FunctionBinding(Function = nameof(CQPExport.CQ_getImage))]
	public class GetImageCommand : AbstractCommand
	{
		#region --常量--
		public const string TYPE_GET_IMAGE = "获取图片";
		#endregion

		#region --属性--
		public string File { get; }
        #endregion

        #region --构造函数--
        public GetImageCommand(CQPSimulator simulator, CQPSimulatorApp app, bool isAuth, string file)
			: base (simulator, app, isAuth)
		{
			this.File = file;
		}
        #endregion

        #region --公开方法--
        public override object ExecuteHaveAuth ()
		{
			//AppInfo appInfo = this.App.Library.AppInfo;
			//LogCenter.Instance.InfoSuccess(appInfo.Name, TYPE_GET_IMAGE, "");
			throw new NotImplementedException ();
		}

		public override object ExecuteHaveNoAuth ()
		{
			AppInfo appInfo = this.App.Library.AppInfo;
			Logger.Instance.Info(appInfo.Name, TYPE_CHECK_AUTHORIZATION, $"检测到调用 Api [{nameof(CQPExport.CQ_getImage)}] 未经授权, 请检查 app.json 是否赋予权限", null, null);
			return RESULT_API_UNAUTHORIZED;
		}
        #endregion
    }
}
