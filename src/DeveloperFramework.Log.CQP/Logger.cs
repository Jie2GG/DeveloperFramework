using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.Log.CQP
{
	/// <summary>
	/// 表示记录程序运行时结果的类
	/// </summary>
	public class Logger
	{
		#region --常量--
		/// <summary>
		/// 获取一个值, 指示程序运行时记录的默认来源
		/// </summary>
		public const string FROM_DEFAULT = "框架";
		/// <summary>
		/// 获取一个值, 指示程序运行时 <see cref="LogLevel.Debug"/> 类型的默认描述
		/// </summary>
		public const string TYPE_DEFAULT_DEBUG = "调试";
		/// <summary>
		/// 获取一个值, 指示程序运行时 <see cref="LogLevel.Info"/> 类型的默认描述
		/// </summary>
		public const string TYPE_DEFAULT_INFO = "信息";
		/// <summary>
		/// 获取一个值, 指示程序运行时 <see cref="LogLevel.Info_Success"/> 类型的默认描述
		/// </summary>
		public const string TYPE_DEFAULT_INFO_SUCCESS = "成功";
		/// <summary>
		/// 获取一个值, 指示程序运行时 <see cref="LogLevel.Info_Receive"/> 类型的默认描述
		/// </summary>
		public const string TYPE_DEFAULT_INFO_RECEIVE = "接收(↓)";
		/// <summary>
		/// 获取一个值, 指示程序运行时 <see cref="LogLevel.Info_Sending"/> 类型的默认描述
		/// </summary>
		public const string TYPE_DEFAULT_INFO_SENDING = "发送(↑)";
		/// <summary>
		/// 获取一个值, 指示程序运行时 <see cref="LogLevel.Warning"/> 类型的默认描述
		/// </summary>
		public const string TYPE_DEFAULT_WARNING = "警告";
		/// <summary>
		/// 获取一个值, 指示程序运行时 <see cref="LogLevel.Error"/> 类型的默认描述
		/// </summary>
		public const string TYPE_DEFAULT_ERROR = "错误";
		/// <summary>
		/// 获取一个值, 指示程序运行时 <see cref="LogLevel.Fatal"/> 类型的默认描述
		/// </summary>
		public const string TYPE_DEFAULT_FATAL = "致命错误";
		#endregion

		#region --字段--
		private static readonly Logger _instance = new Logger ();
		private readonly Collection<LogItem> _caches;
		private readonly Collection<ILogObserver> _observers;
		#endregion

		#region --属性--
		/// <summary>
		/// 获取 <see cref="Logger"/> 类的唯一实例
		/// </summary>
		public static Logger Instance => _instance;
		#endregion

		#region --构造函数--
		private Logger ()
		{
			this._caches = new Collection<LogItem> ();
			this._observers = new Collection<ILogObserver> ();
		}
		#endregion

		#region --公开方法--
		/// <summary>
		/// 添加观察者
		/// </summary>
		/// <param name="observer">观察者对象</param>
		public void AddObserver (ILogObserver observer)
		{
			if (!this._observers.Contains (observer))
			{
				this._observers.Add (observer);

				observer.Initialize (_caches);
			}
		}
		/// <summary>
		/// 添加观察者
		/// </summary>
		/// <param name="observer">观察者对象</param>
		public void RemoveObserver (ILogObserver observer)
		{
			if (this._observers.Contains (observer))
			{
				this._observers.Remove (observer);
			}
		}
		/// <summary>
		/// 写入一条 <see cref="LogLevel.Debug"/> 类型的日志
		/// </summary>
		/// <param name="from">日志的来源模块名称</param>
		/// <param name="content">日志详细信息</param>
		public void Debug (string from, object content)
		{
			this.Debug (from, TYPE_DEFAULT_DEBUG, content, null, null);
		}
		/// <summary>
		/// 写入一条 <see cref="LogLevel.Debug"/> 类型的日志
		/// </summary>
		/// <param name="content">日志详细信息</param>
		/// <param name="state">处理结果</param>
		/// <param name="consuming">处理耗费时长</param>
		public void Debug (object content, bool? state = null, TimeSpan? consuming = null)
		{
			this.Debug (TYPE_DEFAULT_DEBUG, content, state, consuming);
		}
		/// <summary>
		/// 写入一条 <see cref="LogLevel.Debug"/> 类型的日志
		/// </summary>
		/// <param name="type">日志的类型</param>
		/// <param name="content">日志详细信息</param>
		/// <param name="state">处理结果</param>
		/// <param name="consuming">处理耗费时长</param>
		public void Debug (string type, object content, bool? state = null, TimeSpan? consuming = null)
		{
			this.Debug (FROM_DEFAULT, type, content, state, consuming);
		}
		/// <summary>
		/// 写入一条 <see cref="LogLevel.Debug"/> 类型的日志
		/// </summary>
		/// <param name="from">日志的来源模块名称</param>
		/// <param name="type">日志的类型</param>
		/// <param name="content">日志详细信息</param>
		/// <param name="state">处理结果</param>
		/// <param name="consuming">处理耗费时长</param>
		public void Debug (string from, string type, object content, bool? state = null, TimeSpan? consuming = null)
		{
			this.Write (LogLevel.Debug, from, type, content, state, consuming);
		}
		/// <summary>
		/// 写入一条 <see cref="LogLevel.Info"/> 类型的日志
		/// </summary>
		/// <param name="from">日志的来源模块名称</param>
		/// <param name="content">日志详细信息</param>
		public void Info (string from, object content)
		{
			this.Info (from, TYPE_DEFAULT_INFO, content, null, null);
		}
		/// <summary>
		/// 写入一条 <see cref="LogLevel.Info"/> 类型的日志
		/// </summary>
		/// <param name="content">日志详细信息</param>
		/// <param name="state">处理结果</param>
		/// <param name="consuming">处理耗费时长</param>
		public void Info (object content, bool? state = null, TimeSpan? consuming = null)
		{
			this.Info (TYPE_DEFAULT_INFO, content, state, consuming);
		}
		/// <summary>
		/// 写入一条 <see cref="LogLevel.Info"/> 类型的日志
		/// </summary>
		/// <param name="type">日志的类型</param>
		/// <param name="content">日志详细信息</param>
		/// <param name="state">处理结果</param>
		/// <param name="consuming">处理耗费时长</param>
		public void Info (string type, object content, bool? state = null, TimeSpan? consuming = null)
		{
			this.Info (FROM_DEFAULT, type, content, state, consuming);
		}
		/// <summary>
		/// 写入一条 <see cref="LogLevel.Info"/> 类型的日志
		/// </summary>
		/// <param name="from">日志的来源模块名称</param>
		/// <param name="type">日志的类型</param>
		/// <param name="content">日志详细信息</param>
		/// <param name="state">处理结果</param>
		/// <param name="consuming">处理耗费时长</param>
		public void Info (string from, string type, object content, bool? state = null, TimeSpan? consuming = null)
		{
			this.Write (LogLevel.Info, from, type, content, state, consuming);
		}
		/// <summary>
		/// 写入一条 <see cref="LogLevel.Info_Success"/> 类型的日志
		/// </summary>
		/// <param name="from">日志的来源模块名称</param>
		/// <param name="content">日志详细信息</param>
		public void InfoSuccess (string from, object content)
		{
			this.InfoSuccess (from, TYPE_DEFAULT_INFO_SUCCESS, content, null, null);
		}
		/// <summary>
		/// 写入一条 <see cref="LogLevel.Info_Success"/> 类型的日志
		/// </summary>
		/// <param name="content">日志详细信息</param>
		/// <param name="state">处理结果</param>
		/// <param name="consuming">处理耗费时长</param>
		public void InfoSuccess (object content, bool? state = null, TimeSpan? consuming = null)
		{
			this.InfoSuccess (TYPE_DEFAULT_INFO_SUCCESS, content, state, consuming);
		}
		/// <summary>
		/// 写入一条 <see cref="LogLevel.Info_Success"/> 类型的日志
		/// </summary>
		/// <param name="type">日志的类型</param>
		/// <param name="content">日志详细信息</param>
		/// <param name="state">处理结果</param>
		/// <param name="consuming">处理耗费时长</param>
		public void InfoSuccess (string type, object content, bool? state = null, TimeSpan? consuming = null)
		{
			this.InfoSuccess (FROM_DEFAULT, type, content, state, consuming);
		}
		/// <summary>
		/// 写入一条 <see cref="LogLevel.Info_Success"/> 类型的日志
		/// </summary>
		/// <param name="from">日志的来源模块名称</param>
		/// <param name="type">日志的类型</param>
		/// <param name="content">日志详细信息</param>
		/// <param name="state">处理结果</param>
		/// <param name="consuming">处理耗费时长</param>
		public void InfoSuccess (string from, string type, object content, bool? state = null, TimeSpan? consuming = null)
		{
			this.Write (LogLevel.Info_Success, from, type, content, state, consuming);
		}
		/// <summary>
		/// 写入一条 <see cref="LogLevel.Info_Receive"/> 类型的日志
		/// </summary>
		/// <param name="from">日志的来源模块名称</param>
		/// <param name="content">日志详细信息</param>
		public void InfoReceive (string from, object content)
		{
			this.InfoReceive (from, TYPE_DEFAULT_INFO_RECEIVE, content, null, null);
		}
		/// <summary>
		/// 写入一条 <see cref="LogLevel.Info_Receive"/> 类型的日志
		/// </summary>
		/// <param name="content">日志详细信息</param>
		/// <param name="state">处理结果</param>
		/// <param name="consuming">处理耗费时长</param>
		public void InfoReceive (object content, bool? state = null, TimeSpan? consuming = null)
		{
			this.InfoReceive (TYPE_DEFAULT_INFO_RECEIVE, content, state, consuming);
		}
		/// <summary>
		/// 写入一条 <see cref="LogLevel.Info_Receive"/> 类型的日志
		/// </summary>
		/// <param name="type">日志的类型</param>
		/// <param name="content">日志详细信息</param>
		/// <param name="state">处理结果</param>
		/// <param name="consuming">处理耗费时长</param>
		public void InfoReceive (string type, object content, bool? state = null, TimeSpan? consuming = null)
		{
			this.InfoReceive (FROM_DEFAULT, type, content, state, consuming);
		}
		/// <summary>
		/// 写入一条 <see cref="LogLevel.Info_Receive"/> 类型的日志
		/// </summary>
		/// <param name="from">日志的来源模块名称</param>
		/// <param name="type">日志的类型</param>
		/// <param name="content">日志详细信息</param>
		/// <param name="state">处理结果</param>
		/// <param name="consuming">处理耗费时长</param>
		public void InfoReceive (string from, string type, object content, bool? state = null, TimeSpan? consuming = null)
		{
			this.Write (LogLevel.Info_Receive, from, type, content, state, consuming);
		}
		/// <summary>
		/// 写入一条 <see cref="LogLevel.Info_Sending"/> 类型的日志
		/// </summary>
		/// <param name="from">日志的来源模块名称</param>
		/// <param name="content">日志详细信息</param>
		public void InfoSending (string from, object content)
		{
			this.InfoSending (from, TYPE_DEFAULT_INFO_SENDING, content, null, null);
		}
		/// <summary>
		/// 写入一条 <see cref="LogLevel.Info_Sending"/> 类型的日志
		/// </summary>
		/// <param name="content">日志详细信息</param>
		/// <param name="state">处理结果</param>
		/// <param name="consuming">处理耗费时长</param>
		public void InfoSending (object content, bool? state = null, TimeSpan? consuming = null)
		{
			this.InfoSending (TYPE_DEFAULT_INFO_SENDING, content, state, consuming);
		}
		/// <summary>
		/// 写入一条 <see cref="LogLevel.Info_Sending"/> 类型的日志
		/// </summary>
		/// <param name="type">日志的类型</param>
		/// <param name="content">日志详细信息</param>
		/// <param name="state">处理结果</param>
		/// <param name="consuming">处理耗费时长</param>
		public void InfoSending (string type, object content, bool? state = null, TimeSpan? consuming = null)
		{
			this.InfoSending (FROM_DEFAULT, type, content, state, consuming);
		}
		/// <summary>
		/// 写入一条 <see cref="LogLevel.Info_Sending"/> 类型的日志
		/// </summary>
		/// <param name="from">日志的来源模块名称</param>
		/// <param name="type">日志的类型</param>
		/// <param name="content">日志详细信息</param>
		/// <param name="state">处理结果</param>
		/// <param name="consuming">处理耗费时长</param>
		public void InfoSending (string from, string type, object content, bool? state = null, TimeSpan? consuming = null)
		{
			this.Write (LogLevel.Info_Sending, from, type, content, state, consuming);
		}
		/// <summary>
		/// 写入一条 <see cref="LogLevel.Warning"/> 类型的日志
		/// </summary>
		/// <param name="from">日志的来源模块名称</param>
		/// <param name="content">日志详细信息</param>
		public void Warning (string from, object content)
		{
			this.Warning (from, TYPE_DEFAULT_WARNING, content, null, null);
		}
		/// <summary>
		/// 写入一条 <see cref="LogLevel.Warning"/> 类型的日志
		/// </summary>
		/// <param name="content">日志详细信息</param>
		/// <param name="state">处理结果</param>
		/// <param name="consuming">处理耗费时长</param>
		public void Warning (object content, bool? state = null, TimeSpan? consuming = null)
		{
			this.Warning (TYPE_DEFAULT_WARNING, content, state, consuming);
		}
		/// <summary>
		/// 写入一条 <see cref="LogLevel.Warning"/> 类型的日志
		/// </summary>
		/// <param name="type">日志的类型</param>
		/// <param name="content">日志详细信息</param>
		/// <param name="state">处理结果</param>
		/// <param name="consuming">处理耗费时长</param>
		public void Warning (string type, object content, bool? state = null, TimeSpan? consuming = null)
		{
			this.Warning (FROM_DEFAULT, type, content, state, consuming);
		}
		/// <summary>
		/// 写入一条 <see cref="LogLevel.Warning"/> 类型的日志
		/// </summary>
		/// <param name="from">日志的来源模块名称</param>
		/// <param name="type">日志的类型</param>
		/// <param name="content">日志详细信息</param>
		/// <param name="state">处理结果</param>
		/// <param name="consuming">处理耗费时长</param>
		public void Warning (string from, string type, object content, bool? state = null, TimeSpan? consuming = null)
		{
			this.Write (LogLevel.Warning, from, type, content, state, consuming);
		}
		/// <summary>
		/// 写入一条 <see cref="LogLevel.Error"/> 类型的日志
		/// </summary>
		/// <param name="from">日志的来源模块名称</param>
		/// <param name="content">日志详细信息</param>
		public void Error (string from, object content)
		{
			this.Error (from, TYPE_DEFAULT_ERROR, content, null, null);
		}
		/// <summary>
		/// 写入一条 <see cref="LogLevel.Error"/> 类型的日志
		/// </summary>
		/// <param name="content">日志详细信息</param>
		/// <param name="state">处理结果</param>
		/// <param name="consuming">处理耗费时长</param>
		public void Error (object content, bool? state = null, TimeSpan? consuming = null)
		{
			this.Error (TYPE_DEFAULT_ERROR, content, state, consuming);
		}
		/// <summary>
		/// 写入一条 <see cref="LogLevel.Error"/> 类型的日志
		/// </summary>
		/// <param name="type">日志的类型</param>
		/// <param name="content">日志详细信息</param>
		/// <param name="state">处理结果</param>
		/// <param name="consuming">处理耗费时长</param>
		public void Error (string type, object content, bool? state = null, TimeSpan? consuming = null)
		{
			this.Error (FROM_DEFAULT, type, content, state, consuming);
		}
		/// <summary>
		/// 写入一条 <see cref="LogLevel.Error"/> 类型的日志
		/// </summary>
		/// <param name="from">日志的来源模块名称</param>
		/// <param name="type">日志的类型</param>
		/// <param name="content">日志详细信息</param>
		/// <param name="state">处理结果</param>
		/// <param name="consuming">处理耗费时长</param>
		public void Error (string from, string type, object content, bool? state = null, TimeSpan? consuming = null)
		{
			this.Write (LogLevel.Error, from, type, content, state, consuming);
		}
		/// <summary>
		/// 写入一条 <see cref="LogLevel.Error"/> 类型的日志
		/// </summary>
		/// <param name="from">日志的来源模块名称</param>
		/// <param name="content">日志详细信息</param>
		public void Fatal (string from, object content)
		{
			this.Fatal (from, TYPE_DEFAULT_FATAL, content, null, null);
		}
		/// <summary>
		/// 写入一条 <see cref="LogLevel.Fatal"/> 类型的日志
		/// </summary>
		/// <param name="content">日志详细信息</param>
		/// <param name="state">处理结果</param>
		/// <param name="consuming">处理耗费时长</param>
		public void Fatal (object content, bool? state = null, TimeSpan? consuming = null)
		{
			this.Fatal (TYPE_DEFAULT_FATAL, content, state, consuming);
		}
		/// <summary>
		/// 写入一条 <see cref="LogLevel.Fatal"/> 类型的日志
		/// </summary>
		/// <param name="type">日志的类型</param>
		/// <param name="content">日志详细信息</param>
		/// <param name="state">处理结果</param>
		/// <param name="consuming">处理耗费时长</param>
		public void Fatal (string type, object content, bool? state = null, TimeSpan? consuming = null)
		{
			this.Fatal (FROM_DEFAULT, type, content, state, consuming);
		}
		/// <summary>
		/// 写入一条 <see cref="LogLevel.Fatal"/> 类型的日志
		/// </summary>
		/// <param name="from">日志的来源模块名称</param>
		/// <param name="type">日志的类型</param>
		/// <param name="content">日志详细信息</param>
		/// <param name="state">处理结果</param>
		/// <param name="consuming">处理耗费时长</param>
		public void Fatal (string from, string type, object content, bool? state = null, TimeSpan? consuming = null)
		{
			this.Write (LogLevel.Fatal, from, type, content, state, consuming);
		}
		/// <summary>
		/// 写入一条日志, 并具有指定的级别、来源、类型、内容、状态和耗费时长
		/// </summary>
		/// <param name="level">日志的等级</param>
		/// <param name="from">日志的来源模块名称</param>
		/// <param name="type">日志的类型</param>
		/// <param name="content">日志详细信息</param>
		/// <param name="state">处理结果</param>
		/// <param name="consuming">处理耗费时长</param>
		public void Write (LogLevel level, string from, string type, object content, bool? state, TimeSpan? consuming = null)
		{
			if (from is null)
			{
				throw new ArgumentNullException (nameof (from));
			}

			if (type is null)
			{
				throw new ArgumentNullException (nameof (type));
			}

			if (content is null)
			{
				content = string.Empty;
			}

			// 处理缓存池
			while (this._caches.Count >= 10000)
			{
				this._caches.RemoveAt (0);
			}

			LogItem item = new LogItem (level, from, type, content, state, consuming);
			this._caches.Add (item);

			// 通知观察者
			foreach (ILogObserver obs in this._observers)
			{
				obs.ReceiveLog (item);
			}
		}
		#endregion
	}
}
