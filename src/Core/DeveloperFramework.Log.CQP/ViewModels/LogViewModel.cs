using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace DeveloperFramework.Log.CQP.ViewModels
{
	public class LogViewModel : BindableBase
	{
		/// <summary>
		/// 获取日志项目集合
		/// </summary>
		public ObservableCollection<LogItem> LogItems { get; } = new ObservableCollection<LogItem>
		{
			new LogItem(LogLevel.Debug, "测试", "框架", "一条 Debug 消息", true, TimeSpan.FromSeconds(10)),
			new LogItem(LogLevel.Info, "测试", "框架", "一条 Info 消息", true, TimeSpan.FromSeconds(10)),
			new LogItem(LogLevel.Info_Success, "测试", "框架", "一条 Info_Success 消息", true, TimeSpan.FromSeconds(10)),
			new LogItem(LogLevel.Info_Receive, "测试", "框架", "一条 Info_Receive 消息", true, TimeSpan.FromSeconds(10)),
			new LogItem(LogLevel.Info_Sending, "测试", "框架", "一条 Info_Sending 消息", true, TimeSpan.FromSeconds(10)),
			new LogItem(LogLevel.Warning, "测试", "框架", "一条 Warning 消息", true, TimeSpan.FromSeconds(10)),
			new LogItem(LogLevel.Error, "测试", "框架", "一条 Error 消息", true, TimeSpan.FromSeconds(10)),
			new LogItem(LogLevel.FatalError, "测试", "框架", "一条 FatalError 消息", true, TimeSpan.FromSeconds(10)),
			new LogItem(LogLevel.Warning, "测试", "异常", new ArgumentNullException("aabbcc123").ToString(), true, TimeSpan.Zero)
		};
	}
}
