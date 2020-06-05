using DeveloperFramework.Log.CQP;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.Extension
{
	/// <summary>
	/// 表示 <see cref="Logger"/> 类的扩展方法
	/// </summary>
	public static class LoggerExtension
	{
		/// <summary>
		/// 写入一条 <see cref="LogLevel.Warning"/> 类型的日志
		/// </summary>
		/// <param name="logger">要写入日志的目标日志实例</param>
		/// <param name="type">日志类型</param>
		/// <param name="content">日志详细信息</param>
		/// <param name="errorCode">错误代码</param>
		/// <param name="message">详细信息</param>
		/// <param name="args">参数列表</param>
		public static void Warning (this Logger logger, string type, int errorCode, string message, params object[] args)
		{
			logger.Warning (type, string.Format ($"{message}({errorCode})", args), null, null);
		}
	}
}
