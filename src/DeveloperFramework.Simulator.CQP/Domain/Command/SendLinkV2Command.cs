using DeveloperFramework.CQP;
using DeveloperFramework.LibraryModel.CQP;
using DeveloperFramework.Log.CQP;
using DeveloperFramework.SimulatorModel.CQP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.Simulator.CQP.Domain.Command
{
	/// <summary>
	/// 发送赞命令
	/// </summary>
	[FunctionBinding (Function = nameof (CQPExport.CQ_sendLikeV2))]
	public class SendLinkV2Command : AbstractCommand
	{
		#region --常量--
		public const string TYPE_SEND_LINK = "发送赞";
		/// <summary>
		/// 未建立关系
		/// </summary>
		public const int RESULT_NO_RELATIONSHIP = -140;
		/// <summary>
		/// 赞数量溢出
		/// </summary>
		public const int RESULT_LINK_OVERFLOW = -141;
		#endregion

		#region --属性--
		public long FromQQ { get; }
		public int Count { get; }
		#endregion

		#region --构造函数--
		public SendLinkV2Command (CQPSimulator simulator, CQPSimulatorApp app, bool isAuth, long qqId, int count)
			: base (simulator, app, isAuth)
		{
			FromQQ = qqId;
			Count = count;
		}
		#endregion

		#region --公开方法--
		public override object ExecuteHaveAuth ()
		{
			AppInfo appInfo = this.App.Library.AppInfo;

			// 向数据池中查询QQ
			QQ qq = this.Simulator.DataPool.QQCollection.Where (p => p.Id == this.FromQQ).FirstOrDefault ();
			if (qq == null)
			{
				LogCenter.Instance.Error (appInfo.Name, TYPE_SEND_LINK, $"无法向 [QQ: {this.FromQQ}] 发送赞, 未查询到与该QQ的关系");
				return RESULT_NO_RELATIONSHIP;
			}
			else
			{
				if (qq.SetLink (this.Count))
				{
					LogCenter.Instance.InfoSuccess (appInfo.Name, TYPE_SEND_LINK, $"成功为 [QQ: {this.FromQQ}] 点了 {this.Count} 个赞");
					return RESULT_SUCCESS;
				}
				else
				{
					LogCenter.Instance.Info (appInfo.Name, TYPE_SEND_LINK, $"为 [QQ: {this.FromQQ}] 的点赞数已满");
					return RESULT_LINK_OVERFLOW;
				}
			}
		}

		public override object ExecuteHaveNoAuth ()
		{
			AppInfo appInfo = this.App.Library.AppInfo;
			LogCenter.Instance.Info (appInfo.Name, TYPE_CHECK_AUTHORIZATION, $"检测到调用 Api [{nameof (CQPExport.CQ_sendLikeV2)}] 未经授权, 请检查 app.json 是否赋予权限", null, null);
			return RESULT_API_UNAUTHORIZED;
		}
		#endregion
	}
}
