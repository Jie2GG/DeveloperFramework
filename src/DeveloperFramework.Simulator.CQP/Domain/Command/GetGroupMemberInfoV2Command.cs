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
	/// 获取群成员信息命令
	/// </summary>
	[FunctionBinding(Function = nameof(CQPExport.CQ_getGroupMemberInfoV2))]
	public class GetGroupMemberInfoV2Command : AbstractCommand
	{
		#region --常量--
		public const string TYPE_GET_GROUPMEMBER_INFO = "获取群成员信息";
		#endregion

		#region --属性--
		public long GroupId { get; }
		public long QqId { get; }
		public bool IsCache { get; }
		#endregion

		#region --构造函数--
		public GetGroupMemberInfoV2Command(CQPSimulator simulator, CQPSimulatorApp app, bool isAuth, long groupId, long qqId, bool isCache)
			: base (simulator, app, isAuth)
		{
			this.GroupId = groupId;
			this.QqId = qqId;
			this.IsCache = isCache;
		}
        #endregion

        #region --公开方法--
        public override object ExecuteHaveAuth ()
		{
			//AppInfo appInfo = this.App.Library.AppInfo;
			//LogCenter.Instance.InfoSuccess(appInfo.Name, TYPE_GET_GROUPMEMBER_INFO, "");
			return this.Simulator.DataPool.GroupCollection.FirstOrDefault(f => f.Id == this.GroupId)?.MemberCollection.FirstOrDefault(f => f.Id == this.QqId);
		}

		public override object ExecuteHaveNoAuth ()
		{
			AppInfo appInfo = this.App.Library.AppInfo;
			LogCenter.Instance.Info(appInfo.Name, TYPE_CHECK_AUTHORIZATION, $"检测到调用 Api [{nameof(CQPExport.CQ_getGroupMemberInfoV2)}] 未经授权, 请检查 app.json 是否赋予权限", null, null);
			return RESULT_API_UNAUTHORIZED;
		}
        #endregion
    }
}
