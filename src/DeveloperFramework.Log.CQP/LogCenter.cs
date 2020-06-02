using System;
using System.Collections.ObjectModel;

namespace DeveloperFramework.Log.CQP
{
	/// <summary>
	/// 表示记录程序运行时结果的日志中心类
	/// </summary>
	public class LogCenter
	{
		#region --常量--
		private const string DefaultFrom = "框架";
		#endregion

		#region --字段--
		private static readonly LogCenter _instance = new Lazy<LogCenter> (() => new LogCenter ()).Value;
		private readonly Collection<LogItem> _cache;
		private readonly Collection<ILogObserver> _observers;
		#endregion

		#region --属性--
		/// <summary>
		/// 获取 <see cref="LogCenter"/> 类的唯一指示
		/// </summary>
		public static LogCenter Instance => _instance;
		#endregion

		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="this"/> 类的新实例
		/// </summary>
		private LogCenter ()
		{
			this._cache = new Collection<LogItem> ();
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

				// 加载缓存日志
				observer.Initialize (_cache);
			}
		}
		/// <summary>
		/// 移除观察者
		/// </summary>
		/// <param name="observer"></param>
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
		/// <param name="content">日志详细信息</param>
		public void Debug (object content)
		{
			this.Debug (content, null, null);
		}
		/// <summary>
		/// 写入一条 <see cref="LogLevel.Debug"/> 类型的日志
		/// </summary>
		/// <param name="type">日志的类型</param>
		/// <param name="content">日志详细信息</param>
		public void Debug (string type, object content)
		{
			this.Debug (type, content, null, null);
		}
		/// <summary>
		/// 写入一条 <see cref="LogLevel.Debug"/> 类型的日志
		/// </summary>
		/// <param name="content">日志详细信息</param>
		/// <param name="state">处理结果</param>
		/// <param name="timeConsuming">处理耗费时长</param>
		public void Debug (object content, bool? state, TimeSpan? timeConsuming)
		{
			this.Debug ("调试", content, state, timeConsuming);
		}
		/// <summary>
		/// 写入一条 <see cref="LogLevel.Debug"/> 类型的日志
		/// </summary>
		/// <param name="type">日志的类型</param>
		/// <param name="content">日志详细信息</param>
		/// <param name="state">处理结果</param>
		/// <param name="timeConsuming">处理耗费时长</param>
		public void Debug (string type, object content, bool? state, TimeSpan? timeConsuming)
		{
			this.Debug (DefaultFrom, type, content, state, timeConsuming);
		}
		/// <summary>
		/// 写入一条 <see cref="LogLevel.Debug"/> 类型的日志
		/// </summary>
		/// <param name="from">日志的来源模块名称</param>
		/// <param name="type">日志的类型</param>
		/// <param name="content">日志详细信息</param>
		/// <param name="state">处理结果</param>
		/// <param name="timeConsuming">处理耗费时长</param>
		public void Debug (string from, string type, object content, bool? state, TimeSpan? timeConsuming)
		{
			this.Write (LogLevel.Debug, from, type, content, state, timeConsuming);
		}

		/// <summary>
		/// 写入一条 <see cref="LogLevel.Info"/> 类型的日志
		/// </summary>
		/// <param name="content">日志详细信息</param>
		public void Info (object content)
		{
			this.Info (content, null, null);
		}
		/// <summary>
		/// 写入一条 <see cref="LogLevel.Info"/> 类型的日志
		/// </summary>
		/// <param name="type">日志的类型</param>
		/// <param name="content">日志详细信息</param>
		public void Info (string type, object content)
		{
			this.Info (type, content, null, null);
		}
		/// <summary>
		/// 写入一条 <see cref="LogLevel.Info"/> 类型的日志
		/// </summary>
		/// <param name="content">日志详细信息</param>
		/// <param name="state">处理结果</param>
		/// <param name="timeConsuming">处理耗费时长</param>
		public void Info (object content, bool? state, TimeSpan? timeConsuming)
		{
			this.Info ("信息", content, state, timeConsuming);
		}
		/// <summary>
		/// 写入一条 <see cref="LogLevel.Info"/> 类型的日志
		/// </summary>
		/// <param name="type">日志的类型</param>
		/// <param name="content">日志详细信息</param>
		/// <param name="state">处理结果</param>
		/// <param name="timeConsuming">处理耗费时长</param>
		public void Info (string type, object content, bool? state, TimeSpan? timeConsuming)
		{
			this.Info (DefaultFrom, type, content, state, timeConsuming);
		}
		/// <summary>
		/// 写入一条 <see cref="LogLevel.Info"/> 类型的日志
		/// </summary>
		/// <param name="from">日志的来源模块名称</param>
		/// <param name="type">日志的类型</param>
		/// <param name="content">日志详细信息</param>
		/// <param name="state">处理结果</param>
		/// <param name="timeConsuming">处理耗费时长</param>
		public void Info (string from, string type, object content, bool? state = null, TimeSpan? timeConsuming = null)
		{
			this.Write (LogLevel.Info, from, type, content, state, timeConsuming);
		}

		/// <summary>
		/// 写入一条 <see cref="LogLevel.Info_Success"/> 类型的日志
		/// </summary>
		/// <param name="content">日志详细信息</param>
		public void InfoSuccess(object content)
		{
			this.InfoSuccess(content, null, null);
		}
		/// <summary>
		/// 写入一条 <see cref="LogLevel.Info_Success"/> 类型的日志
		/// </summary>
		/// <param name="type">日志的类型</param>
		/// <param name="content">日志详细信息</param>
		public void InfoSuccess (string type, object content)
		{
			this.InfoSuccess (type, content, null, null);
		}
		/// <summary>
		/// 写入一条 <see cref="LogLevel.Info_Success"/> 类型的日志
		/// </summary>
		/// <param name="content">日志详细信息</param>
		/// <param name="state">处理结果</param>
		/// <param name="timeConsuming">处理耗费时长</param>
		public void InfoSuccess(object content, bool? state, TimeSpan? timeConsuming)
		{
			this.Info("成功", content, state, timeConsuming);
		}
		/// <summary>
		/// 写入一条 <see cref="LogLevel.Info_Success"/> 类型的日志
		/// </summary>
		/// <param name="type">日志的类型</param>
		/// <param name="content">日志详细信息</param>
		/// <param name="state">处理结果</param>
		/// <param name="timeConsuming">处理耗费时长</param>
		public void InfoSuccess (string type, object content, bool? state, TimeSpan? timeConsuming)
		{
			this.InfoSuccess (DefaultFrom, type, content, state, timeConsuming);
		}
		/// <summary>
		/// 写入一条 <see cref="LogLevel.Info_Success"/> 类型的日志
		/// </summary>
		/// <param name="from">日志的来源模块名称</param>
		/// <param name="type">日志的类型</param>
		/// <param name="content">日志详细信息</param>
		/// <param name="state">处理结果</param>
		/// <param name="timeConsuming">处理耗费时长</param>
		public void InfoSuccess (string from, string type, object content, bool? state = null, TimeSpan? timeConsuming = null)
		{
			this.Write (LogLevel.Info_Success, from, type, content, state, timeConsuming);
		}

		/// <summary>
		/// 写入一条 <see cref="LogLevel.Info_Receive"/> 类型的日志
		/// </summary>
		/// <param name="content">日志详细信息</param>
		public void InfoReceive (object content)
		{
			this.InfoReceive (content, null, null);
		}
		/// <summary>
		/// 写入一条 <see cref="LogLevel.Info_Receive"/> 类型的日志
		/// </summary>
		/// <param name="type">日志的类型</param>
		/// <param name="content">日志详细信息</param>
		public void InfoReceive (string type, object content)
		{
			this.InfoReceive (type, content, null, null);
		}
		/// <summary>
		/// 写入一条 <see cref="LogLevel.Info_Receive"/> 类型的日志
		/// </summary>
		/// <param name="content">日志详细信息</param>
		/// <param name="state">处理结果</param>
		/// <param name="timeConsuming">处理耗费时长</param>
		public void InfoReceive (object content, bool? state, TimeSpan? timeConsuming)
		{
			this.InfoReceive ("接收", content, state, timeConsuming);
		}
		/// <summary>
		/// 写入一条 <see cref="LogLevel.Info_Receive"/> 类型的日志
		/// </summary>
		/// <param name="type">日志的类型</param>
		/// <param name="content">日志详细信息</param>
		/// <param name="state">处理结果</param>
		/// <param name="timeConsuming">处理耗费时长</param>
		public void InfoReceive (string type, object content, bool? state, TimeSpan? timeConsuming)
		{
			this.InfoReceive (DefaultFrom, type, content, state, timeConsuming);
		}
		/// <summary>
		/// 写入一条 <see cref="LogLevel.Info_Receive"/> 类型的日志
		/// </summary>
		/// <param name="from">日志的来源模块名称</param>
		/// <param name="type">日志的类型</param>
		/// <param name="content">日志详细信息</param>
		/// <param name="state">处理结果</param>
		/// <param name="timeConsuming">处理耗费时长</param>
		public void InfoReceive (string from, string type, object content, bool? state, TimeSpan? timeConsuming)
		{
			this.Write (LogLevel.Info_Receive, from, type, content, state, timeConsuming);
		}

		/// <summary>
		/// 写入一条 <see cref="LogLevel.Info_Sending"/> 类型的日志
		/// </summary>
		/// <param name="content">日志详细信息</param>
		public void InfoSending (object content)
		{
			this.InfoSending (content, null, null);
		}
		/// <summary>
		/// 写入一条 <see cref="LogLevel.Info_Sending"/> 类型的日志
		/// </summary>
		/// <param name="type">日志的类型</param>
		/// <param name="content">日志详细信息</param>
		public void InfoSending (string type, object content)
		{
			this.InfoSending (type, content, null, null);
		}
		/// <summary>
		/// 写入一条 <see cref="LogLevel.Info_Sending"/> 类型的日志
		/// </summary>
		/// <param name="content">日志详细信息</param>
		/// <param name="state">处理结果</param>
		/// <param name="timeConsuming">处理耗费时长</param>
		public void InfoSending (object content, bool? state, TimeSpan? timeConsuming)
		{
			this.InfoSending ("发送", content, state, timeConsuming);
		}
		/// <summary>
		/// 写入一条 <see cref="LogLevel.Info_Sending"/> 类型的日志
		/// </summary>
		/// <param name="type">日志的类型</param>
		/// <param name="content">日志详细信息</param>
		/// <param name="state">处理结果</param>
		/// <param name="timeConsuming">处理耗费时长</param>
		public void InfoSending (string type, object content, bool? state, TimeSpan? timeConsuming)
		{
			this.InfoSending (DefaultFrom, type, content, state, timeConsuming);
		}
		/// <summary>
		/// 写入一条 <see cref="LogLevel.Info_Sending"/> 类型的日志
		/// </summary>
		/// <param name="from">日志的来源模块名称</param>
		/// <param name="type">日志的类型</param>
		/// <param name="content">日志详细信息</param>
		/// <param name="state">处理结果</param>
		/// <param name="timeConsuming">处理耗费时长</param>
		public void InfoSending (string from, string type, object content, bool? state = null, TimeSpan? timeConsuming = null)
		{
			this.Write (LogLevel.Info_Sending, from, type, content, state, timeConsuming);
		}

		/// <summary>
		/// 写入一条 <see cref="LogLevel.Warning"/> 类型的日志
		/// </summary>
		/// <param name="content">日志详细信息</param>
		public void Warning (object content)
		{
			this.Warning (content, null, null);
		}
		/// <summary>
		/// 写入一条 <see cref="LogLevel.Warning"/> 类型的日志
		/// </summary>
		/// <param name="type">日志的类型</param>
		/// <param name="content">日志详细信息</param>
		public void Warning (string type, object content)
		{
			this.Warning (type, content, null, null);
		}
		/// <summary>
		/// 写入一条 <see cref="LogLevel.Warning"/> 类型的日志
		/// </summary>
		/// <param name="content">日志详细信息</param>
		/// <param name="state">处理结果</param>
		/// <param name="timeConsuming">处理耗费时长</param>
		public void Warning (object content, bool? state, TimeSpan? timeConsuming)
		{
			this.Warning ("警告", content, state, timeConsuming);
		}
		/// <summary>
		/// 写入一条 <see cref="LogLevel.Warning"/> 类型的日志
		/// </summary>
		/// <param name="type">日志的类型</param>
		/// <param name="content">日志详细信息</param>
		/// <param name="state">处理结果</param>
		/// <param name="timeConsuming">处理耗费时长</param>
		public void Warning (string type, object content, bool? state, TimeSpan? timeConsuming)
		{
			this.Warning (DefaultFrom, type, content, state, timeConsuming);
		}
		/// <summary>
		/// 写入一条 <see cref="LogLevel.Warning"/> 类型的日志
		/// </summary>
		/// <param name="from">日志的来源模块名称</param>
		/// <param name="type">日志的类型</param>
		/// <param name="content">日志详细信息</param>
		/// <param name="state">处理结果</param>
		/// <param name="timeConsuming">处理耗费时长</param>
		public void Warning (string from, string type, object content, bool? state, TimeSpan? timeConsuming)
		{
			this.Write (LogLevel.Warning, from, type, content, state, timeConsuming);
		}

		/// <summary>
		/// 写入一条 <see cref="LogLevel.Error"/> 类型的日志
		/// </summary>
		/// <param name="content">日志详细信息</param>
		public void Error (object content)
		{
			this.Error (content, null, null);
		}
		/// <summary>
		/// 写入一条 <see cref="LogLevel.Error"/> 类型的日志
		/// </summary>
		/// <param name="type">日志的类型</param>
		/// <param name="content">日志详细信息</param>
		public void Error (string type, object content)
		{
			this.Error (type, content, null, null);
		}
		/// <summary>
		/// 写入一条 <see cref="LogLevel.Error"/> 类型的日志
		/// </summary>
		/// <param name="content">日志详细信息</param>
		/// <param name="state">处理结果</param>
		/// <param name="timeConsuming">处理耗费时长</param>
		public void Error (object content, bool? state, TimeSpan? timeConsuming)
		{
			this.Error ("错误", content, state, timeConsuming);
		}
		/// <summary>
		/// 写入一条 <see cref="LogLevel.Error"/> 类型的日志
		/// </summary>
		/// <param name="type">日志的类型</param>
		/// <param name="content">日志详细信息</param>
		/// <param name="state">处理结果</param>
		/// <param name="timeConsuming">处理耗费时长</param>
		public void Error (string type, object content, bool? state, TimeSpan? timeConsuming)
		{
			this.Error (DefaultFrom, type, content, state, timeConsuming);
		}
		/// <summary>
		/// 写入一条 <see cref="LogLevel.Error"/> 类型的日志
		/// </summary>
		/// <param name="from">日志的来源模块名称</param>
		/// <param name="type">日志的类型</param>
		/// <param name="content">日志详细信息</param>
		/// <param name="state">处理结果</param>
		/// <param name="timeConsuming">处理耗费时长</param>
		public void Error (string from, string type, object content, bool? state = null, TimeSpan? timeConsuming = null)
		{
			this.Write (LogLevel.Error, from, type, content, state, timeConsuming);
		}

		/// <summary>
		/// 写入一条 <see cref="LogLevel.FatalError"/> 类型的日志
		/// </summary>
		/// <param name="content">日志详细信息</param>
		public void FatalError (object content)
		{
			this.FatalError ("严重错误", content);
		}
		/// <summary>
		/// 写入一条 <see cref="LogLevel.FatalError"/> 类型的日志
		/// </summary>
		/// <param name="type">日志的类型</param>
		/// <param name="content">日志详细信息</param>
		public void FatalError (string type, object content)
		{
			this.FatalError (DefaultFrom, type, content);
		}
		/// <summary>
		/// 写入一条 <see cref="LogLevel.FatalError"/> 类型的日志
		/// </summary>
		/// <param name="from">日志的来源模块名称</param>
		/// <param name="type">日志的类型</param>
		/// <param name="content">日志详细信息</param>
		public void FatalError (string from, string type, object content)
		{
			this.Write (LogLevel.FatalError, from, type, content, null, null);
		}
		#endregion

		#region --私有方法--
		private void Write (LogLevel level, string from, string type, object content, bool? state, TimeSpan? timeConsuming)
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
				throw new ArgumentNullException (nameof (content));
			}

			// 处理缓存池
			while (this._cache.Count >= 10000)
			{
				this._cache.RemoveAt (0);
			}

			LogItem item = new LogItem (level, from, type, content, state, timeConsuming);
			this._cache.Add (item);

			// 通知观察者
			foreach (ILogObserver obs in this._observers)
			{
				obs.ReceiveLog (item);
			}
		}
		#endregion
	}
}
