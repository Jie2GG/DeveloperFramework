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
	/// 写入日志命令
	/// </summary>
	[FunctionBinding(Function = nameof(CQPExport.CQ_addLog))]
	public class AddLogCommand : AbstractCommand
	{
		#region --属性--
		public int Level { get; }
		public string Type { get; }
		public string Contents { get; }
		#endregion

		#region --构造函数--
		public AddLogCommand(CQPSimulator simulator, CQPSimulatorApp app, bool isAuth, int level, string type, string contents)
			: base (simulator, app, isAuth)
		{
			this.Level = level;
			this.Type = type;
			this.Contents = contents;
		}
        #endregion

        #region --公开方法--
        public override object ExecuteHaveAuth ()
		{
			AppInfo appInfo = this.App.Library.AppInfo;

			switch ((LogLevel)this.Level)
            {
				case LogLevel.Info:
					LogCenter.Instance.Info(appInfo.Name, this.Type, this.Contents);
					break;
				case LogLevel.Info_Receive:
					LogCenter.Instance.InfoReceive(appInfo.Name, this.Type, this.Contents);
					break;
				case LogLevel.Info_Sending:
					LogCenter.Instance.InfoSending(appInfo.Name, this.Type, this.Contents);
					break;
				case LogLevel.Info_Success:
					LogCenter.Instance.InfoSuccess(appInfo.Name, this.Type, this.Contents);
					break;
				case LogLevel.Debug:
					LogCenter.Instance.Debug(appInfo.Name, this.Type, this.Contents);
					break;
				case LogLevel.Warning:
					LogCenter.Instance.Warning(appInfo.Name, this.Type, this.Contents);
					break;
				case LogLevel.Error:
					LogCenter.Instance.Error(appInfo.Name, this.Type, this.Contents);
					break;
				case LogLevel.FatalError:
					LogCenter.Instance.FatalError(appInfo.Name, this.Type, this.Contents);
					break;
			}
			return RESULT_SUCCESS;
		}

		public override object ExecuteHaveNoAuth ()
		{
			AppInfo appInfo = this.App.Library.AppInfo;
			LogCenter.Instance.Warning(appInfo.Name, TYPE_CHECK_AUTHORIZATION, $"检测到调用 Api [{nameof(CQPExport.CQ_addLog)}] 未经授权, 请检查 app.json 是否赋予权限", null, null);
			return RESULT_API_UNAUTHORIZED;
		}
        #endregion
    }
}
