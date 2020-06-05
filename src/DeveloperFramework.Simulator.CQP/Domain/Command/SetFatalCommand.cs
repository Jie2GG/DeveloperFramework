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
        public const string TYPE_SET_FATAL = "引发致命错误";
        #endregion

        #region --属性--
        public string ErrorMsg { get; }
        #endregion

        #region --构造函数--
        public SetFatalCommand(CQPSimulator simulator, CQPSimulatorApp app, bool isAuth, string errorMsg)
            : base(simulator, app, isAuth)
        {
            this.ErrorMsg = errorMsg;
        }
        #endregion

        #region --公开方法--
        public override object ExecuteHaveAuth()
        {
            //todo : let CQPSimulatorApp call error message dialog
            AppInfo appInfo = this.App.Library.AppInfo;
            Logger.Instance.Fatal(appInfo.Name, "致命错误", this.ErrorMsg);
			return RESULT_SUCCESS;
        }

        public override object ExecuteHaveNoAuth()
        {
            AppInfo appInfo = this.App.Library.AppInfo;
            Logger.Instance.Info(appInfo.Name, TYPE_CHECK_AUTHORIZATION, $"检测到调用 Api [{nameof(CQPExport.CQ_setFatal)}] 未经授权, 请检查 app.json 是否赋予权限", null, null);
            return RESULT_API_UNAUTHORIZED;
        }
        #endregion
    }
}
