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
	/// 获取群成员列表命令
	/// </summary>
	[FunctionBinding(Function = nameof(CQPExport.CQ_getGroupMemberList))]
	public class GetGroupMemberListCommand : AbstractCommand
	{
		#region --常量--
		public const string TYPE_GET_GROUPMEMBER_LIST = "获取群成员列表";
		#endregion

		#region --属性--
		public long GroupId { get; }
		#endregion

		#region --构造函数--
		public GetGroupMemberListCommand(CQPSimulator simulator, CQPSimulatorApp app, bool isAuth, long groupId)
			: base (simulator, app, isAuth)
		{
			this.GroupId = groupId;
		}
        #endregion

        #region --公开方法--
        public override object ExecuteHaveAuth ()
		{
			//AppInfo appInfo = this.App.Library.AppInfo;
			//LogCenter.Instance.InfoSuccess(appInfo.Name, TYPE_GET_GROUPMEMBER_LIST, "");
			return this.Simulator.DataPool.GroupCollection.FirstOrDefault(f => f.Id == this.GroupId)?.MemberCollection.ToBase64String();
		}

		public override object ExecuteHaveNoAuth ()
		{
			AppInfo appInfo = this.App.Library.AppInfo;
			Logger.Instance.Info(appInfo.Name, TYPE_CHECK_AUTHORIZATION, $"检测到调用 Api [{nameof(CQPExport.CQ_getGroupMemberList)}] 未经授权, 请检查 app.json 是否赋予权限");
			return RESULT_API_UNAUTHORIZED;
		}
        #endregion
    }
}
