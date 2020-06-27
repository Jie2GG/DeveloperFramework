using System;
using System.Collections;
using System.Collections.Generic;

namespace DeveloperFramework.SimulatorModel.CQP
{
	/// <summary>
	/// 表示固定长度的集合
	/// </summary>
	/// <typeparam name="T">集合中的元素类型</typeparam>
	public abstract class FixedCollection<T> : ICollection<T>, IReadOnlyCollection<T>, IEquatable<FixedCollection<T>>, IToBase64
	{
		#region --字段--
		private readonly List<T> _items;
		#endregion

		#region --属性--
		/// <summary>
		/// 获取 <see cref="FixedCollection{T}"/> 包含的元素数
		/// </summary>
		public int Count => this._items.Count;
		/// <summary>
		/// 获取该内部数据结构最大能够容纳的元素总数
		/// </summary>
		public int Capacity => this._items.Capacity;
		/// <summary>
		/// 获取一个值，该值指示 <see cref="FixedCollection{T}"/> 是否为只读
		/// </summary>
		public bool IsReadOnly => false;
		#endregion

		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="FixedCollection{T}"/> 类的新实例，该实例为空并且具有固定的容量
		/// </summary>
		/// <param name="capacity">新列表最初可以存储的元素数</param>
		/// <exception cref="ArgumentOutOfRangeException">capacity 小于 0</exception>
		public FixedCollection (int capacity)
		{
			this._items = new List<T> (capacity);
		}
		#endregion

		#region --公开方法--
		/// <summary>
		/// 将对象添加到 <see cref="FixedCollection{T}"/> 的结尾处
		/// </summary>
		/// <param name="item">要添加到 <see cref="FixedCollection{T}"/> 末尾的对象。 对于引用类型，该值可以为 <see langword="null"/></param>
		/// <exception cref="IndexOutOfRangeException">集合已达最大数量</exception>
		public void Add (T item)
		{
			if (this.Count == this.Capacity)
			{
				throw new IndexOutOfRangeException ($"无法继续向集合内插入元素, 已达到集合最大数量");
			}

			this._items.Add (item);
		}
		/// <summary>
		/// 从 <see cref="FixedCollection{T}"/> 中移除所有元素
		/// </summary>
		public void Clear ()
		{
			this._items.Clear ();
		}
		/// <summary>
		/// 确定某元素是否在 <see cref="FixedCollection{T}"/> 中
		/// </summary>
		/// <param name="item">要在 <see cref="FixedCollection{T}"/> 中定位的对象。 对于引用类型，该值可以为 <see langword="null"/></param>
		/// <returns>如果在 <see cref="FixedCollection{T}"/> 中找到 item，则为 <see langword="true"/>；否则为 <see langword="false"/></returns>
		public bool Contains (T item)
		{
			return this._items.Contains (item);
		}
		/// <summary>
		/// 从目标数组的指定索引处开始，将整个 <see cref="FixedCollection{T}"/> 复制到兼容的一维数组
		/// </summary>
		/// <param name="array">一维 <see cref="Array"/>，它是从 <see cref="FixedCollection{T}"/> 复制的元素的目标。 <see cref="Array"/> 必须具有从零开始的索引</param>
		/// <param name="arrayIndex">array 中从零开始的索引，从此处开始复制</param>
		/// <exception cref="ArgumentNullException">array 为 <see langword="null"/></exception>
		/// <exception cref="ArgumentOutOfRangeException">arrayIndex 小于 0</exception>
		/// <exception cref="ArgumentException">源 <see cref="FixedCollection{T}"/> 中的元素个数大于从 arrayIndex 到目标 array 末尾之间的可用空间</exception>
		public void CopyTo (T[] array, int arrayIndex)
		{
			this._items.CopyTo (array, arrayIndex);
		}
		/// <summary>
		/// 从 <see cref="FixedCollection{T}"/> 中移除特定对象的第一个匹配项
		/// </summary>
		/// <param name="item">要从 <see cref="FixedCollection{T}"/> 中删除的对象。 对于引用类型，该值可以为 <see langword="null"/></param>
		/// <returns>如果成功移除了 item，则为 <see langword="true"/>；否则为 <see langword="false"/>。 如果在 <see cref="FixedCollection{T}"/> 中没有找到 item，则此方法也会返回 <see langword="false"/></returns>
		public bool Remove (T item)
		{
			return this._items.Remove (item);
		}
		/// <summary>
		/// 返回当前实例的 Base64 字符串
		/// </summary>
		/// <returns>当前实例的 Base64 字符串</returns>
		public abstract string ToBase64 ();
		/// <summary>
		/// 返回循环访问 <see cref="FixedCollection{T}"/> 的枚举数
		/// </summary>
		/// <returns>用于 <see cref="FixedCollection{T}"/> 的 <see cref="FixedCollection{T}.GetEnumerator"/></returns>
		public IEnumerator<T> GetEnumerator ()
		{
			return this._items.GetEnumerator ();
		}
		/// <summary>
		/// 返回循环访问 <see cref="FixedCollection{T}"/> 的枚举数
		/// </summary>
		/// <returns>用于 <see cref="FixedCollection{T}"/> 的 <see cref="FixedCollection{T}.GetEnumerator"/></returns>
		IEnumerator IEnumerable.GetEnumerator ()
		{
			return this._items.GetEnumerator ();
		}
		/// <summary>
		/// 指示当前对象是否等同于另一个对象
		/// </summary>
		/// <param name="other">一个与此对象进行比较的对象</param>
		/// <returns>如果当前对象等于 other 参数，则为 <see langword="true"/>；否则为 <see langword="false"/></returns>
		public bool Equals (FixedCollection<T> other)
		{
			if (other is null)
			{
				return false;
			}

			if (this.Count != other.Count)
			{
				return false;
			}

			using (IEnumerator<T> enumerator1 = this.GetEnumerator ())
			{
				using (IEnumerator<T> enumerator2 = other.GetEnumerator ())
				{
					while (enumerator1.MoveNext () && enumerator2.MoveNext ())
					{
						T item1 = enumerator1.Current;
						T item2 = enumerator2.Current;
						if (!item1.Equals (item2))
						{
							return false;
						}
					}
				}
			}

			return true;
		}
		/// <summary>
		/// 指示当前对象是否等同于另一个对象
		/// </summary>
		/// <param name="obj">一个与此对象进行比较的对象</param>
		/// <returns>如果当前对象等于 obj 参数，则为 <see langword="true"/>；否则为 <see langword="false"/></returns>
		public override bool Equals (object obj)
		{
			return this.Equals (obj as FixedCollection<T>);
		}
		/// <summary>
		/// 作为默认的哈希函数
		/// </summary>
		/// <returns>返回当前对象的哈希代码</returns>
		public override int GetHashCode ()
		{
			return this._items.GetHashCode ();
		}
		#endregion
	}
}
