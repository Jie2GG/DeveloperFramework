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
    /// 置群成员踢除命令
    /// </summary>
    [FunctionBinding(Function = nameof(CQPExport.CQ_setGroupKick))]
    public class SetGroupKickCommand : AbstractCommand
    {
        #region --常量--
        public const string TYPE_GROUP_KICK = "群成员踢除";
        #endregion

        #region --属性--
        public long GroupId { get; }
        public long QqId { get; }
        public bool Refuses { get; }
        #endregion

        #region --构造函数--
        public SetGroupKickCommand(CQPSimulator simulator, CQPSimulatorApp app, bool isAuth, long groupId, long qqId, bool refuses)
            : base(simulator, app, isAuth)
        {
            this.GroupId = groupId;
            this.QqId = qqId;
            this.Refuses = refuses;
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
            LogCenter.Instance.Info(appInfo.Name, TYPE_CHECK_AUTHORIZATION, $"检测到调用 Api [{nameof(CQPExport.CQ_setGroupKick)}] 未经授权, 请检查 app.json 是否赋予权限", null, null);
            return RESULT_API_UNAUTHORIZED;
        }
        #endregion
    }
}
