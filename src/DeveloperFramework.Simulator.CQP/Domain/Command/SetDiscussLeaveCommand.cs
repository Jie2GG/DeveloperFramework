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
    /// 退出讨论组命令
    /// </summary>
    [FunctionBinding(Function = nameof(CQPExport.CQ_setDiscussLeave))]
    public class SetDiscussLeaveCommand : AbstractCommand
    {
        #region --常量--
        public const string TYPE_DISCUSS_LEAVE= "退出讨论组";
        #endregion

        #region --属性--
        public long DiscussId { get; }
        #endregion

        #region --构造函数--
        public SetDiscussLeaveCommand(CQPSimulator simulator, CQPSimulatorApp app, bool isAuth, long discussId)
            : base(simulator, app, isAuth)
        {
            this.DiscussId = discussId;
        }
        #endregion

        #region --公开方法--
        public override object ExecuteHaveAuth()
        {
            throw new NotImplementedException();
        }

        public override object ExecuteHaveNoAuth()
        {
            AppInfo appInfo = this.App.Library.AppInfo;
            Logger.Instance.Info(appInfo.Name, TYPE_CHECK_AUTHORIZATION, $"检测到调用 Api [{nameof(CQPExport.CQ_setDiscussLeave)}] 未经授权, 请检查 app.json 是否赋予权限");
            return RESULT_API_UNAUTHORIZED;
        }
        #endregion
    }
}
