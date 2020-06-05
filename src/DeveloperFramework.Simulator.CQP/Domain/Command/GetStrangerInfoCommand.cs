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
	/// 获取QQ陌生人信息命令
	/// </summary>
	[FunctionBinding(Function = nameof(CQPExport.CQ_getStrangerInfo))]
	public class GetStrangerInfoCommand : AbstractCommand
	{
		#region --常量--
		public const string TYPE_GET_STRANGER_INFO = "获取QQ陌生人信息";
		#endregion

		#region --属性--
		public long QqId { get; }
		public bool NoCache { get; }
		#endregion

		#region --构造函数--
		public GetStrangerInfoCommand(CQPSimulator simulator, CQPSimulatorApp app, bool isAuth, long qqId, bool notCache)
			: base (simulator, app, isAuth)
		{
			this.QqId = QqId;
			this.NoCache = notCache;
		}
        #endregion

        #region --公开方法--
        public override object ExecuteHaveAuth ()
		{
			//AppInfo appInfo = this.App.Library.AppInfo;
			//LogCenter.Instance.InfoSuccess(appInfo.Name, TYPE_GET_STRANGER_INFO, "");
			return this.Simulator.DataPool.QQCollection.FirstOrDefault(w => w.Id == this.QqId).ToBase64String();
		}

		public override object ExecuteHaveNoAuth ()
		{
			AppInfo appInfo = this.App.Library.AppInfo;
			Logger.Instance.Info(appInfo.Name, TYPE_CHECK_AUTHORIZATION, $"检测到调用 Api [{nameof(CQPExport.CQ_getStrangerInfo)}] 未经授权, 请检查 app.json 是否赋予权限", null, null);
			return RESULT_API_UNAUTHORIZED;
		}
        #endregion
    }
}
