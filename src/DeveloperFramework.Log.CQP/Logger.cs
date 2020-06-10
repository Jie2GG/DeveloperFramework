using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.Log.CQP
{
	/// <summary>
	/// 表示记录程序运行时结果的类
	/// </summary>
	public class Logger : IObservable<LogItem>
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

		#region --属性--
		/// <summary>
		/// 获取当前 <see cref="Logger"/> 的唯一实例
		/// </summary>
		public static readonly Logger Instance = new Logger ();
		private readonly List<IObservable<LogItem>> _logObservers;
		private readonly List<LogItem> _logCaches;
		private readonly Dictionary<int, Stopwatch> _logDict;
		#endregion

		#region --构造函数--
		private Logger ()
		{
			this._logObservers = new List<IObservable<LogItem>> ();
			this._logCaches = new List<LogItem> ();
			this._logDict = new Dictionary<int, Stopwatch> ();
		}
		#endregion

		#region --公开方法--
		/// <summary>
		/// 添加日志观察成员
		/// </summary>
		/// <param name="item">要添加的日志观察成员</param>
		public void AddObserver (IObservable<LogItem> item)
		{
			if (item is null)
			{
				throw new ArgumentNullException (nameof (item));
			}

			this._logObservers.Add (item);
			Initialize (this._logCaches);
		}
		/// <summary>
		/// 移除日志观察成员
		/// </summary>
		/// <param name="item">要移除的日志观察成员</param>
		public void RemoveObserver (IObservable<LogItem> item)
		{
			if (item is null)
			{
				throw new ArgumentNullException (nameof (item));
			}

			this._logObservers.Remove (item);
		}
		/// <summary>
		/// 引发最后一个注册的监听对象获取现有缓存池中的日志
		/// </summary>
		/// <param name="list">日志列表</param>
		public void Initialize (IEnumerable<LogItem> list)
		{
			if (this._logObservers.Count > 0)
			{
				this._logObservers.Last ().Initialize (list);
			}
		}
		/// <summary>
		/// 引发所有已注册观察者添加的新日志
		/// </summary>
		/// <param name="item">用于通知观察者的 <see cref="LogItem"/> 实例</param>
		public void OnAdd (LogItem item)
		{
			foreach (IObservable<LogItem> observable in this._logObservers)
			{
				observable.OnAdd (item);
			}
		}
		/// <summary>
		/// 引发所有已注册观察者移除一项日志
		/// </summary>
		/// <param name="item">用于通知观察者的 <see cref="LogItem"/> 实例</param>
		public void OnRemove (LogItem item)
		{
			foreach (IObservable<LogItem> observable in this._logObservers)
			{
				observable.OnRemove (item);
			}
		}
		/// <summary>
		/// 引发所有已注册观察者替换一项日志
		/// </summary>
		/// <param name="item">用于通知观察者的 <see cref="LogItem"/> 实例</param>
		public void OnReplace (LogItem item)
		{
			foreach (IObservable<LogItem> observable in this._logObservers)
			{
				observable.OnReplace (item);
			}
		}
		/// <summary>
		/// 开启写入一个可计时的新日志
		/// </summary>
		/// <param name="level">日志等级</param>
		/// <param name="from">日志来源</param>
		/// <param name="type">日志类型</param>
		/// <param name="content">日志内容</param>
		/// <returns>新添加日志的唯一索引</returns>
		public int BeginLog (LogLevel level, string from, string type, object content)
		{
			int id = this.Writer (level, from, type, content, LogState.Running);
			this._logDict.Add (id, Stopwatch.StartNew ());
			return id;
		}
		/// <summary>
		/// 结束一个可计时的新日志, 并刷新需要表现的结果
		/// </summary>
		/// <param name="id">日志索引. 该值由 <see cref="BeginLog"/> 方法返回</param>
		/// <param name="state">运行结果.</param>
		public void EndLog (int id, LogState state)
		{
			if (!(this._logDict.ContainsKey (id) && (id >= 0 && id < this._logCaches.Count)))
			{
				throw new KeyNotFoundException ($"未找到指定ID的日志条目, ID: {id}");
			}

			if (state == LogState.None || state == LogState.Running)
			{
				throw new ArgumentException ("值不能是 None 或 Running", nameof (state));
			}

			Stopwatch stopwatch = this._logDict[id];
			stopwatch.Stop ();

			this._logCaches[id].State = state;
			this._logCaches[id].TimeSpan = stopwatch.Elapsed;
			this.OnReplace (this._logCaches[id]);
		}
		/// <summary>
		/// 写入一个新日志
		/// </summary>
		/// <param name="level">日志等级</param>
		/// <param name="from">日志来源</param>
		/// <param name="type">日志类型</param>
		/// <param name="content">日志内容</param>
		public void Log (LogLevel level, string from, string type, object content)
		{
			this.Writer (level, from, type, content, LogState.None);
		}
		/// <summary>
		/// 写入一个 <see cref="LogLevel.Debug"/> 类型的新日志
		/// </summary>
		/// <param name="content">日志内容</param>
		public void Debug (object content)
		{
			this.Debug (TYPE_DEFAULT_DEBUG, content);
		}
		/// <summary>
		/// 写入一个 <see cref="LogLevel.Debug"/> 类型的新日志
		/// </summary>
		/// <param name="type">日志类型</param>
		/// <param name="content">日志内容</param>
		public void Debug (string type, object content)
		{
			this.Debug (FROM_DEFAULT, type, content);
		}
		/// <summary>
		/// 写入一个 <see cref="LogLevel.Debug"/> 类型的新日志
		/// </summary>
		/// <param name="from">日志来源</param>
		/// <param name="type">日志类型</param>
		/// <param name="content">日志内容</param>
		public void Debug (string from, string type, object content)
		{
			this.Log (LogLevel.Debug, from, type, content);
		}
		/// <summary>
		/// 写入一个 <see cref="LogLevel.Info"/> 类型的新日志
		/// </summary>
		/// <param name="content">日志内容</param>
		public void Info (object content)
		{
			this.Info (TYPE_DEFAULT_INFO, content);
		}
		/// <summary>
		/// 写入一个 <see cref="LogLevel.Info"/> 类型的新日志
		/// </summary>
		/// <param name="type">日志类型</param>
		/// <param name="content">日志内容</param>
		public void Info (string type, object content)
		{
			this.Info (FROM_DEFAULT, type, content);
		}
		/// <summary>
		/// 写入一个 <see cref="LogLevel.Info"/> 类型的新日志
		/// </summary>
		/// <param name="from">日志来源</param>
		/// <param name="type">日志类型</param>
		/// <param name="content">日志内容</param>
		public void Info (string from, string type, object content)
		{
			this.Log (LogLevel.Info, from, type, content);
		}
		/// <summary>
		/// 写入一个 <see cref="LogLevel.Info_Success"/> 类型的新日志
		/// </summary>
		/// <param name="content">日志内容</param>
		public void InfoSuccess (object content)
		{
			this.InfoSuccess (TYPE_DEFAULT_INFO_SUCCESS, content);
		}
		/// <summary>
		/// 写入一个 <see cref="LogLevel.Info_Success"/> 类型的新日志
		/// </summary>
		/// <param name="type">日志类型</param>
		/// <param name="content">日志内容</param>
		public void InfoSuccess (string type, object content)
		{
			this.InfoSuccess (FROM_DEFAULT, type, content);
		}
		/// <summary>
		/// 写入一个 <see cref="LogLevel.Info_Success"/> 类型的新日志
		/// </summary>
		/// <param name="from">日志来源</param>
		/// <param name="type">日志类型</param>
		/// <param name="content">日志内容</param>
		public void InfoSuccess (string from, string type, object content)
		{
			this.Log (LogLevel.Info_Success, from, type, content);
		}
		/// <summary>
		/// 写入一个 <see cref="LogLevel.Info_Receive"/> 类型的新日志
		/// </summary>
		/// <param name="content">日志内容</param>
		public void InfoReceive (object content)
		{
			this.InfoReceive (TYPE_DEFAULT_INFO_RECEIVE, content);
		}
		/// <summary>
		/// 写入一个 <see cref="LogLevel.Info_Receive"/> 类型的新日志
		/// </summary>
		/// <param name="type">日志类型</param>
		/// <param name="content">日志内容</param>
		public void InfoReceive (string type, object content)
		{
			this.InfoReceive (FROM_DEFAULT, type, content);
		}
		/// <summary>
		/// 写入一个 <see cref="LogLevel.Info_Receive"/> 类型的新日志
		/// </summary>
		/// <param name="from">日志来源</param>
		/// <param name="type">日志类型</param>
		/// <param name="content">日志内容</param>
		public void InfoReceive (string from, string type, object content)
		{
			this.Log (LogLevel.Info_Receive, from, type, content);
		}
		/// <summary>
		/// 写入一个 <see cref="LogLevel.Info_Sending"/> 类型的新日志
		/// </summary>
		/// <param name="content">日志内容</param>
		public void InfoSending (object content)
		{
			this.InfoSending (TYPE_DEFAULT_INFO_SENDING, content);
		}
		/// <summary>
		/// 写入一个 <see cref="LogLevel.Info_Sending"/> 类型的新日志
		/// </summary>
		/// <param name="type">日志类型</param>
		/// <param name="content">日志内容</param>
		public void InfoSending (string type, object content)
		{
			this.InfoSending (FROM_DEFAULT, type, content);
		}
		/// <summary>
		/// 写入一个 <see cref="LogLevel.Info_Sending"/> 类型的新日志
		/// </summary>
		/// <param name="from">日志来源</param>
		/// <param name="type">日志类型</param>
		/// <param name="content">日志内容</param>
		public void InfoSending (string from, string type, object content)
		{
			this.Log (LogLevel.Info_Sending, from, type, content);
		}
		/// <summary>
		/// 写入一个 <see cref="LogLevel.Warning"/> 类型的新日志
		/// </summary>
		/// <param name="content">日志内容</param>
		public void Warning (object content)
		{
			this.Warning (TYPE_DEFAULT_WARNING, content);
		}
		/// <summary>
		/// 写入一个 <see cref="LogLevel.Warning"/> 类型的新日志
		/// </summary>
		/// <param name="type">日志类型</param>
		/// <param name="content">日志内容</param>
		public void Warning (string type, object content)
		{
			this.Warning (FROM_DEFAULT, type, content);
		}
		/// <summary>
		/// 写入一个 <see cref="LogLevel.Warning"/> 类型的新日志
		/// </summary>
		/// <param name="from">日志来源</param>
		/// <param name="type">日志类型</param>
		/// <param name="content">日志内容</param>
		public void Warning (string from, string type, object content)
		{
			this.Log (LogLevel.Warning, from, type, content);
		}
		/// <summary>
		/// 写入一个 <see cref="LogLevel.Error"/> 类型的新日志
		/// </summary>
		/// <param name="content">日志内容</param>
		public void Error (object content)
		{
			this.Error (TYPE_DEFAULT_ERROR, content);
		}
		/// <summary>
		/// 写入一个 <see cref="LogLevel.Error"/> 类型的新日志
		/// </summary>
		/// <param name="type">日志类型</param>
		/// <param name="content">日志内容</param>
		public void Error (string type, object content)
		{
			this.Error (FROM_DEFAULT, type, content);
		}
		/// <summary>
		/// 写入一个 <see cref="LogLevel.Error"/> 类型的新日志
		/// </summary>
		/// <param name="from">日志来源</param>
		/// <param name="type">日志类型</param>
		/// <param name="content">日志内容</param>
		public void Error (string from, string type, object content)
		{
			this.Log (LogLevel.Error, from, type, content);
		}
		/// <summary>
		/// 写入一个 <see cref="LogLevel.Fatal"/> 类型的新日志
		/// </summary>
		/// <param name="content">日志内容</param>
		public void Fatal (object content)
		{
			this.Fatal (TYPE_DEFAULT_FATAL, content);
		}
		/// <summary>
		/// 写入一个 <see cref="LogLevel.Fatal"/> 类型的新日志
		/// </summary>
		/// <param name="type">日志类型</param>
		/// <param name="content">日志内容</param>
		public void Fatal (string type, object content)
		{
			this.Fatal (FROM_DEFAULT, type, content);
		}
		/// <summary>
		/// 写入一个 <see cref="LogLevel.Error"/> 类型的新日志
		/// </summary>
		/// <param name="from">日志来源</param>
		/// <param name="type">日志类型</param>
		/// <param name="content">日志内容</param>
		public void Fatal (string from, string type, object content)
		{
			this.Log (LogLevel.Fatal, from, type, content);
		}
		/// <summary>
		/// 开启一个可计时的 <see cref="LogLevel.Debug"/> 类型的新日志
		/// </summary>
		/// <param name="content">日志内容</param>
		/// <returns>新添加日志的唯一索引</returns>
		public int BeginDebug (object content)
		{
			return this.BeginDebug (TYPE_DEFAULT_DEBUG, content);
		}
		/// <summary>
		/// 开启一个可计时的 <see cref="LogLevel.Debug"/> 类型的新日志
		/// </summary>
		/// <param name="type">日志类型</param>
		/// <param name="content">日志内容</param>
		/// <returns>新添加日志的唯一索引</returns>
		public int BeginDebug (string type, object content)
		{
			return this.BeginDebug (FROM_DEFAULT, type, content);
		}
		/// <summary>
		/// 开启一个可计时的 <see cref="LogLevel.Debug"/> 类型的新日志
		/// </summary>
		/// <param name="from">日志来源</param>
		/// <param name="type">日志类型</param>
		/// <param name="content">日志内容</param>
		/// <returns>新添加日志的唯一索引</returns>
		public int BeginDebug (string from, string type, object content)
		{
			return this.BeginLog (LogLevel.Debug, from, type, content);
		}
		/// <summary>
		/// 开启一个可计时的 <see cref="LogLevel.Info"/> 类型的新日志
		/// </summary>
		/// <param name="content">日志内容</param>
		/// <returns>新添加日志的唯一索引</returns>
		public int BeginInfo (object content)
		{
			return this.BeginInfo (TYPE_DEFAULT_INFO, content);
		}
		/// <summary>
		/// 开启一个可计时的 <see cref="LogLevel.Info"/> 类型的新日志
		/// </summary>
		/// <param name="type">日志类型</param>
		/// <param name="content">日志内容</param>
		/// <returns>新添加日志的唯一索引</returns>
		public int BeginInfo (string type, object content)
		{
			return this.BeginInfo (FROM_DEFAULT, type, content);
		}
		/// <summary>
		/// 开启一个可计时的 <see cref="LogLevel.Info"/> 类型的新日志
		/// </summary>
		/// <param name="from">日志来源</param>
		/// <param name="type">日志类型</param>
		/// <param name="content">日志内容</param>
		/// <returns>新添加日志的唯一索引</returns>
		public int BeginInfo (string from, string type, object content)
		{
			return this.BeginLog (LogLevel.Info, from, type, content);
		}
		/// <summary>
		/// 开启一个可计时的 <see cref="LogLevel.Info_Success"/> 类型的新日志
		/// </summary>
		/// <param name="content">日志内容</param>
		/// <returns>新添加日志的唯一索引</returns>
		public int BeginInfoSuccess (object content)
		{
			return this.BeginInfoSuccess (TYPE_DEFAULT_INFO_SUCCESS, content);
		}
		/// <summary>
		/// 开启一个可计时的 <see cref="LogLevel.Info_Success"/> 类型的新日志
		/// </summary>
		/// <param name="type">日志类型</param>
		/// <param name="content">日志内容</param>
		/// <returns>新添加日志的唯一索引</returns>
		public int BeginInfoSuccess (string type, object content)
		{
			return this.BeginInfoSuccess (FROM_DEFAULT, type, content);
		}
		/// <summary>
		/// 开启一个可计时的 <see cref="LogLevel.Info_Success"/> 类型的新日志
		/// </summary>
		/// <param name="from">日志来源</param>
		/// <param name="type">日志类型</param>
		/// <param name="content">日志内容</param>
		/// <returns>新添加日志的唯一索引</returns>
		public int BeginInfoSuccess (string from, string type, object content)
		{
			return this.BeginLog (LogLevel.Info_Success, from, type, content);
		}
		/// <summary>
		/// 开启一个可计时的 <see cref="LogLevel.Info_Receive"/> 类型的新日志
		/// </summary>
		/// <param name="content">日志内容</param>
		/// <returns>新添加日志的唯一索引</returns>
		public int BeginInfoReceive (object content)
		{
			return this.BeginInfoReceive (TYPE_DEFAULT_INFO_RECEIVE, content);
		}
		/// <summary>
		/// 开启一个可计时的 <see cref="LogLevel.Info_Receive"/> 类型的新日志
		/// </summary>
		/// <param name="type">日志类型</param>
		/// <param name="content">日志内容</param>
		/// <returns>新添加日志的唯一索引</returns>
		public int BeginInfoReceive (string type, object content)
		{
			return this.BeginInfoReceive (FROM_DEFAULT, type, content);
		}
		/// <summary>
		/// 开启一个可计时的 <see cref="LogLevel.Info_Receive"/> 类型的新日志
		/// </summary>
		/// <param name="from">日志来源</param>
		/// <param name="type">日志类型</param>
		/// <param name="content">日志内容</param>
		/// <returns>新添加日志的唯一索引</returns>
		public int BeginInfoReceive (string from, string type, object content)
		{
			return this.BeginLog (LogLevel.Info_Receive, from, type, content);
		}
		/// <summary>
		/// 开启一个可计时的 <see cref="LogLevel.Info_Sending"/> 类型的新日志
		/// </summary>
		/// <param name="content">日志内容</param>
		/// <returns>新添加日志的唯一索引</returns>
		public int BeginInfoSending (object content)
		{
			return this.BeginInfoSending (TYPE_DEFAULT_INFO_RECEIVE, content);
		}
		/// <summary>
		/// 开启一个可计时的 <see cref="LogLevel.Info_Sending"/> 类型的新日志
		/// </summary>
		/// <param name="type">日志类型</param>
		/// <param name="content">日志内容</param>
		/// <returns>新添加日志的唯一索引</returns>
		public int BeginInfoSending (string type, object content)
		{
			return this.BeginInfoSending (FROM_DEFAULT, type, content);
		}
		/// <summary>
		/// 开启一个可计时的 <see cref="LogLevel.Info_Sending"/> 类型的新日志
		/// </summary>
		/// <param name="from">日志来源</param>
		/// <param name="type">日志类型</param>
		/// <param name="content">日志内容</param>
		/// <returns>新添加日志的唯一索引</returns>
		public int BeginInfoSending (string from, string type, object content)
		{
			return this.BeginLog (LogLevel.Info_Sending, from, type, content);
		}
		/// <summary>
		/// 开启一个可计时的 <see cref="LogLevel.Warning"/> 类型的新日志
		/// </summary>
		/// <param name="content">日志内容</param>
		/// <returns>新添加日志的唯一索引</returns>
		public int BeginWarning (object content)
		{
			return this.BeginWarning (TYPE_DEFAULT_WARNING, content);
		}
		/// <summary>
		/// 开启一个可计时的 <see cref="LogLevel.Warning"/> 类型的新日志
		/// </summary>
		/// <param name="type">日志类型</param>
		/// <param name="content">日志内容</param>
		/// <returns>新添加日志的唯一索引</returns>
		public int BeginWarning (string type, object content)
		{
			return this.BeginWarning (FROM_DEFAULT, type, content);
		}
		/// <summary>
		/// 开启一个可计时的 <see cref="LogLevel.Warning"/> 类型的新日志
		/// </summary>
		/// <param name="from">日志来源</param>
		/// <param name="type">日志类型</param>
		/// <param name="content">日志内容</param>
		/// <returns>新添加日志的唯一索引</returns>
		public int BeginWarning (string from, string type, object content)
		{
			return this.BeginLog (LogLevel.Warning, from, type, content);
		}
		/// <summary>
		/// 开启一个可计时的 <see cref="LogLevel.Error"/> 类型的新日志
		/// </summary>
		/// <param name="content">日志内容</param>
		/// <returns>新添加日志的唯一索引</returns>
		public int BeginError (object content)
		{
			return this.BeginError (TYPE_DEFAULT_ERROR, content);
		}
		/// <summary>
		/// 开启一个可计时的 <see cref="LogLevel.Error"/> 类型的新日志
		/// </summary>
		/// <param name="type">日志类型</param>
		/// <param name="content">日志内容</param>
		/// <returns>新添加日志的唯一索引</returns>
		public int BeginError (string type, object content)
		{
			return this.BeginError (FROM_DEFAULT, type, content);
		}
		/// <summary>
		/// 开启一个可计时的 <see cref="LogLevel.Error"/> 类型的新日志
		/// </summary>
		/// <param name="from">日志来源</param>
		/// <param name="type">日志类型</param>
		/// <param name="content">日志内容</param>
		/// <returns>新添加日志的唯一索引</returns>
		public int BeginError (string from, string type, object content)
		{
			return this.BeginLog (LogLevel.Error, from, type, content);
		}
		/// <summary>
		/// 开启一个可计时的 <see cref="LogLevel.Fatal"/> 类型的新日志
		/// </summary>
		/// <param name="content">日志内容</param>
		/// <returns>新添加日志的唯一索引</returns>
		public int BeginFatal (object content)
		{
			return this.BeginFatal (TYPE_DEFAULT_FATAL, content);
		}
		/// <summary>
		/// 开启一个可计时的 <see cref="LogLevel.Fatal"/> 类型的新日志
		/// </summary>
		/// <param name="type">日志类型</param>
		/// <param name="content">日志内容</param>
		/// <returns>新添加日志的唯一索引</returns>
		public int BeginFatal (string type, object content)
		{
			return this.BeginFatal (FROM_DEFAULT, type, content);
		}
		/// <summary>
		/// 开启一个可计时的 <see cref="LogLevel.Fatal"/> 类型的新日志
		/// </summary>
		/// <param name="from">日志来源</param>
		/// <param name="type">日志类型</param>
		/// <param name="content">日志内容</param>
		/// <returns>新添加日志的唯一索引</returns>
		public int BeginFatal (string from, string type, object content)
		{
			return this.BeginLog (LogLevel.Fatal, from, type, content);
		}
		#endregion

		#region --私有方法--
		private int Writer (LogLevel level, string from, string type, object content, LogState state)
		{
			LogItem item = new LogItem (level, from, type, content)
			{
				State = state,
				TimeSpan = null
			};

			while (this._logCaches.Count > 10000)
			{
				LogItem temp = this._logCaches[0];
				if (temp.State == LogState.Running)
				{
					break;
				}

				OnRemove (temp);
			}

			this._logCaches.Add (item);
			OnAdd (item);
			return this._logCaches.LastIndexOf (item);
		}
		#endregion
	}
}
