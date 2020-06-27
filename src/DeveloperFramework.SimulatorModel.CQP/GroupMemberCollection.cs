using System;
using System.IO;
using System.Linq;

using DeveloperFramework.Extension;

namespace DeveloperFramework.SimulatorModel.CQP
{
	/// <summary>
	/// 描述群成员列表的类
	/// </summary>
	public class GroupMemberCollection : FixedCollection<GroupMember>
	{
		#region --属性--
		/// <summary>
		/// 获取当前实例绑定的群组实例
		/// </summary>
		public Group Group { get; }
		#endregion

		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="GroupMemberCollection"/> 类的新实例, 该实例为空并且具有固定的容量
		/// </summary>
		/// <param name="group">新列表绑定的 <see cref="CQP.Group"/> 实例</param>
		/// <param name="capacity">新列表最初可以存储的元素数</param>
		/// <exception cref="ArgumentOutOfRangeException">capacity 小于 0</exception>
		public GroupMemberCollection (Group group, int capacity)
			: base (capacity)
		{
			this.Group = group;
		}
		#endregion

		#region --公开方法--
		/// <summary>
		/// 返回当前实例的 Base64 字符串
		/// </summary>
		/// <returns>当前实例的 Base64 字符串</returns>
		public override string ToBase64 ()
		{
			using (BinaryWriter writer = new BinaryWriter (new MemoryStream ()))
			{
				writer.Write_Ex (this.Count);
				foreach (GroupMember item in this)
				{
					writer.Write_Ex (item.ToByteArray (this.Group));
				}

				return Convert.ToBase64String (writer.ToArray ());
			}
		}
		#endregion
	}
}
