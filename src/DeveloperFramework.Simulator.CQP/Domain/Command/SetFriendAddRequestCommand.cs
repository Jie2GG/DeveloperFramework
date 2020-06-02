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
    /// 置好友请求命令
    /// </summary>
    [FunctionBinding(Function = nameof(CQPExport.CQ_setFriendAddRequest))]
    public class SetFriendAddRequestCommand : AbstractCommand
    {
        #region --常量--
        public const string TYPE_FRIEND_ADD_REQUEST = "好友添加或请求";
        #endregion

        #region --属性--
        public string Identifying { get; }
        public int RequestType { get; }
        public int ResponseType { get; }
        public string AppendMsg { get; }
        #endregion

        #region --构造函数--
        public SetFriendAddRequestCommand(CQPSimulator simulator, CQPSimulatorApp app, bool isAuth, string identifying, int requestType, string appendMsg)
            : base(simulator, app, isAuth)
        {
            this.Identifying = identifying;
            this.RequestType = requestType;
            this.AppendMsg = appendMsg;
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
            LogCenter.Instance.Info(appInfo.Name, TYPE_CHECK_AUTHORIZATION, $"检测到调用 Api [{nameof(CQPExport.CQ_setFriendAddRequest)}] 未经授权, 请检查 app.json 是否赋予权限", null, null);
            return RESULT_API_UNAUTHORIZED;
        }
        #endregion
    }
}
