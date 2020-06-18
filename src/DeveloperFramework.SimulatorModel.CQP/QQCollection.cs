﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.SimulatorModel.CQP
{
	/// <summary>
	/// 描述 QQ列表 的类型
	/// </summary>
	public class QQCollection : Collection<QQ>, IEquatable<QQCollection>
	{
		#region --构造函数--
		/// <summary>
		/// 初始化为空的 <see cref="QQCollection"/> 类的新实例
		/// </summary>
		public QQCollection ()
			: base ()
		{

		}
		/// <summary>
		/// 新实例初始化 <see cref="QQCollection"/> 包装指定列表的类
		/// </summary>
		/// <param name="list">用于包装由新的集合的列表</param>
		public QQCollection (IList<QQ> list)
			: base (list)
		{

		}
		#endregion

		#region --公开方法--
		/// <summary>
		/// 指示当前对象是否等于同一类型的另一个对象
		/// </summary>
		/// <param name="obj">一个与此对象进行比较的对象</param>
		/// <returns>如果当前对象等于 obj 参数，则为 <see langword="true"/>；否则为 <see langword="false"/></returns>
		public bool Equals (QQCollection other)
		{
			if (other is null)
			{
				return false;
			}

			if (this.Count != other.Count)
			{
				return false;
			}

			for (int i = 0; i < this.Count; i++)
			{
				if (!this[i].Equals (other[i]))
				{
					return false;
				}
			}

			return true;
		}
		/// <summary>
		/// 指示当前对象是否等于同一类型的另一个对象
		/// </summary>
		/// <param name="obj">一个与此对象进行比较的对象</param>
		/// <returns>如果当前对象等于 obj 参数，则为 <see langword="true"/>；否则为 <see langword="false"/></returns>
		public override bool Equals (object obj)
		{
			return this.Equals (obj as QQCollection);
		}
		/// <summary>
		/// 返回此实例的哈希代码
		/// </summary>
		/// <returns>32 位有符号整数哈希代码</returns>
		public override int GetHashCode ()
		{
			return base.GetHashCode ();
		}
		#endregion
	}
}
