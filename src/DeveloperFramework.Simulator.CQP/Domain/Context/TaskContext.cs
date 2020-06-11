using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace DeveloperFramework.Simulator.CQP.Domain.Context
{
	/// <summary>
	/// 描述任务上下文的类
	/// </summary>
	public class TaskContext
	{
		#region --字段--
		private readonly Dictionary<string, dynamic> _dict;
		#endregion

		#region --属性--
		/// <summary>
		/// 获取当前任务的类型
		/// </summary>
		public int Type { get; }
		#endregion

		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="TaskContext"/> 类的新实例
		/// </summary>
		/// <param name="type">任务类型Id</param>
		public TaskContext (int type)
		{
			this._dict = new Dictionary<string, dynamic> ();
			this.Type = type;
		}
		#endregion

		#region --公开方法--
		/// <summary>
		/// 向上下文中设置一个相关联的键值对
		/// </summary>
		/// <param name="key">设置到上下文中的键</param>
		/// <param name="value">设置到上下文中的值</param>
		public void SetValue (string key, dynamic value)
		{
			this._dict.Add (key, value);
		}
		/// <summary>
		/// 获取一个和上下文关联的键值对
		/// </summary>
		/// <param name="key">要获取值的键</param>
		/// <returns>与键关联的值, 该值自动转换为目标类型</returns>
		public dynamic GetValue (string key)
		{
			return this._dict[key];
		}
		#endregion
	}
}
