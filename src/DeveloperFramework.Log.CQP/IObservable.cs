using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.Log.CQP
{
	/// <summary>
	/// 定义基于推送通知的实用程序的接口
	/// </summary>
	/// <typeparam name="T">推送通知的实际类型</typeparam>
	public interface IObservable<T>
	{
		/// <summary>
		/// 当在派生类中重写时, 当成功注册为监听对象时调用
		/// </summary>
		/// <param name="list"></param>
		void Initialize (IEnumerable<T> list);
		/// <summary>
		/// 当在派生类中重写时, 当添加新通知时调用
		/// </summary>
		/// <param name="item">要添加的新通知项</param>
		void OnAdd (T item);
		/// <summary>
		/// 当在派生类中重写时, 当移除一个通知时调用
		/// </summary>
		/// <param name="item">被移除的通知项</param>
		void OnRemove (T item);
		/// <summary>
		/// 当在派生类中重写时, 当通知内容被修改时调用
		/// </summary>
		/// <param name="item">被修改的通知项</param>
		void OnReplace (T item);
	}
}
