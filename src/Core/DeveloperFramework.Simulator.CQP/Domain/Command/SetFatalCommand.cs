using DeveloperFramework.CQP;
using DeveloperFramework.LibraryModel.CQP;
using DeveloperFramework.Log.CQP;
using DeveloperFramework.SimulatorModel.CQP;
using DeveloperFramework.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DeveloperFramework.Simulator.CQP.Domain.Command
{   /// <summary>
    /// 置致命错误命令
    /// </summary>
    [FunctionBinding(Function = nameof(CQPExport.CQ_setFatal))]
    public class SetFatalCommand : AbstractCommand
    {
        #region --常量--
        public const string TYPE_SET_FATAL = "置致命错误";
        #endregion

        #region --构造函数--
        public SetFatalCommand(CQPSimulator simulator, CQPSimulatorApp app, bool isAuth)
            : base(simulator, app, isAuth)
        { }
        #endregion

        #region --公开方法--
        public override object ExecuteHaveAuth()
        {
            //AppInfo appInfo = this.App.Library.AppInfo;
            //LogCenter.Instance.InfoSuccess(appInfo.Name, TYPE_SET_FATAL, "");
            throw new NotImplementedException();
        }

        public override object ExecuteHaveNoAuth()
        {
            AppInfo appInfo = this.App.Library.AppInfo;
            LogCenter.Instance.Info(appInfo.Name, TYPE_CHECK_AUTHORIZATION, $"检测到调用 Api [{nameof(CQPExport.CQ_setFatal)}] 未经授权, 请检查 app.json 是否赋予权限", null, null);
            return RESULT_API_UNAUTHORIZED;
        }
        #endregion
    }
}
