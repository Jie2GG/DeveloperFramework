using DeveloperFramework.CQP;
using DeveloperFramework.LibraryModel.CQP;
using DeveloperFramework.Log.CQP;
using DeveloperFramework.SimulatorModel.CQP;
using DeveloperFramework.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace DeveloperFramework.Simulator.CQP.Domain.Command
{   /// <summary>
    /// 置群成员头衔命令
    /// </summary>
    [FunctionBinding(Function = nameof(CQPExport.CQ_setGroupSpecialTitle))]
    public class SetGroupSpecialTitleCommand : AbstractCommand
    {
        #region --常量--
        public const string TYPE_GROUP_SPECIAL_TITLE = "群成员头衔变更";
        #endregion

        #region --属性--
        public long GroupId { get; }
        public long QqId { get; }
        public string Title { get; }
        public TimeSpan DurationTime { get; }
        #endregion

        #region --构造函数--
        public SetGroupSpecialTitleCommand(CQPSimulator simulator, CQPSimulatorApp app, bool isAuth, long groupId, long qqId, string title, long durationTime)
            : base(simulator, app, isAuth)
        {
            this.GroupId = groupId;
            this.QqId = qqId;
            this.Title = title;
            this.DurationTime = TimeSpan.FromSeconds(durationTime);
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
            Logger.Instance.Info(appInfo.Name, TYPE_CHECK_AUTHORIZATION, $"检测到调用 Api [{nameof(CQPExport.CQ_setGroupSpecialTitle)}] 未经授权, 请检查 app.json 是否赋予权限");
            return RESULT_API_UNAUTHORIZED;
        }
        #endregion
    }
}
