using System;

namespace DeveloperFramework.SimulatorModel.CQP
{
	/// <summary>
	/// 描述讨论组成员列表的类
	/// </summary>
	public class DiscussMemberCollection : FixedCollection<DiscussMember>
	{
		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="DiscussMemberCollection"/> 类的新实例, 该实例为空并且具有固定的容量
		/// </summary>
		/// <param name="capacity">新列表最初可以存储的元素数</param>
		/// <exception cref="ArgumentOutOfRangeException">capacity 小于 0</exception>
		public DiscussMemberCollection (int capacity)
			: base (capacity)
		{
		}
		#endregion

		#region --公开方法--
		/// <summary>
		/// 返回当前实例的 Base64 字符串
		/// </summary>
		/// <returns>当前实例的 Base64 字符串</returns>
		public override string ToBase64 ()
		{
			return string.Empty;
		}
		#endregion
	}
}
