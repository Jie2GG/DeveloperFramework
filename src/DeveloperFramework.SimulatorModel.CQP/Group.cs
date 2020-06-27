using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

using DeveloperFramework.Extension;

namespace DeveloperFramework.SimulatorModel.CQP
{
	/// <summary>
	/// 描述群组的类
	/// </summary>
	public class Group : IToBase64
	{
		#region --字段--
		private string[] _memberLevels;
		#endregion

		#region --属性--
		/// <summary>
		/// 获取当前实例的唯一标识 (ID)
		/// </summary>
		public long Id { get; }
		/// <summary>
		/// 获取或设置当前实例的名字
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 获取当前实例的成员列表
		/// </summary>
		public GroupMemberCollection Members { get; }
		/// <summary>
		/// 获取当前实例的文件列表
		/// </summary>
		public Collection<GroupFile> Files { get; }
		/// <summary>
		/// 获取或设置当前实例的成员等级列表
		/// </summary>
		public string[] MemberLevels
		{
			get => this._memberLevels;
			set
			{
				int length = Enum.GetValues (typeof (GroupMemberType)).Length;

				if (value.Length != length)
				{
					throw new ArgumentException ($"数组长度必须为: {length}");
				}

				this._memberLevels = value;
			}
		}
		#endregion

		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="Group"/> 类的新实例
		/// </summary>
		/// <param name="id">绑定于当前实例的唯一标识 (ID)</param>
		/// <param name="maxCount">设置当前实例的最大成员人数</param>
		public Group (long id, int maxCount)
		{
			this.Id = id;
			this.Members = new GroupMemberCollection (this, maxCount);
			this.Files = new Collection<GroupFile> ();
		}
		#endregion

		#region --公开方法--
		/// <summary>
		/// 根据群成员等级获取对应的等级名称
		/// </summary>
		/// <param name="level">群成员等级枚举</param>
		/// <returns>等级枚举对应的等级字符串</returns>
		public string GetLevelName (GroupMemberLevel level)
		{
			if (this.MemberLevels is null)
			{
				return string.Empty;
			}
			return this.MemberLevels[(int)level];
		}
		/// <summary>
		/// 返回当前实例的 Base64 字符串
		/// </summary>
		/// <returns>Base64 字符串</returns>
		public string ToBase64 ()
		{
			using (BinaryWriter writer = new BinaryWriter (new MemoryStream ()))
			{
				writer.Write_Ex (this.Id);
				writer.Write_Ex (this.Name);
				writer.Write_Ex (this.Members.Count);
				writer.Write_Ex (this.Members.Capacity);

				return Convert.ToBase64String (writer.ToArray ());
			}
		}
		#endregion
	}
}
