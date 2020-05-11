using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.Log.CQP
{
	/// <summary>
	/// 描述日志观察者的接口
	/// </summary>
	public interface ILogObserver
	{
		/// <summary>
		/// 当在派生类中重写时, 处理加载已缓存的日志信息
		/// </summary>
		/// <param name="logs">日志信息集合</param>
		void Initialize (ICollection<LogItem> logs);
		/// <summary>
		/// 当在派生类中重写时, 处理收到的新日志
		/// </summary>
		/// <param name="log">日志信息</param>
		void ReceiveLog (LogItem log);
	}
}
