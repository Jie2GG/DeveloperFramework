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
    /// 置群全体禁言命令
    /// </summary>
    [FunctionBinding(Function = nameof(CQPExport.CQ_setGroupWholeBan))]
    public class SetGroupWholeBanCommand : AbstractCommand
    {
        #region --常量--
        public const string TYPE_GROUP_WHOLE_BAN = "变更全体禁言状态";
        #endregion

        #region --属性--
        public long GroupId { get; }
        public bool IsSet { get; }
        #endregion

        #region --构造函数--
        public SetGroupWholeBanCommand(CQPSimulator simulator, CQPSimulatorApp app, bool isAuth, long groupId, bool isSet)
            : base(simulator, app, isAuth)
        {
            this.GroupId = groupId;
            this.IsSet = isSet;
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
            LogCenter.Instance.Info(appInfo.Name, TYPE_CHECK_AUTHORIZATION, $"检测到调用 Api [{nameof(CQPExport.CQ_setGroupWholeBan)}] 未经授权, 请检查 app.json 是否赋予权限", null, null);
            return RESULT_API_UNAUTHORIZED;
        }
        #endregion
    }
}
