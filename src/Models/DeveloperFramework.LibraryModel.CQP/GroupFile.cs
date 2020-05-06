using DeveloperFramework.Extension;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.LibraryModel.CQP
{
	/// <summary>
	/// 描述 CQP 应用的 File 类型
	/// </summary>
	public class GroupFile : IEquatable<GroupFile>
	{
		#region --字段--
		private static int _privateId;
		#endregion

		#region --属性--
		/// <summary>
		/// 获取当前实例的全局唯一标识
		/// </summary>
		public int Id { get; }
		/// <summary>
		/// 获取当前实例的名称
		/// </summary>
		public string Name { get; }
		/// <summary>
		/// 获取当前实例的 GUID
		/// </summary>
		public string Guid { get; }
		/// <summary>
		/// 获取当前实例的字节数
		/// </summary>
		public long Size { get; }
		#endregion

		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="GroupFile"/> 类的新实例
		/// </summary>
		/// <param name="name">文件名</param>
		/// <param name="guid">文件 GUID</param>
		/// <param name="size">文件大小</param>
		public GroupFile (string name, string guid, long size)
		{
			this.Id = GetFileId ();
			this.Name = name ?? throw new ArgumentNullException (nameof (name));
			this.Guid = guid ?? throw new ArgumentNullException (nameof (guid));
			this.Size = size;
		}
		#endregion

		#region --公开方法--
		/// <summary>
		/// 返回 Base64 字符串
		/// </summary>
		/// <returns>Base64 字符串</returns>
		public string ToBase64String ()
		{
			using (BinaryWriter writer = new BinaryWriter (new MemoryStream ()))
			{
				writer.Write_Ex (this.Guid);
				writer.Write_Ex (this.Name);
				writer.Write_Ex (this.Size);
				writer.Write_Ex (this.Id);
				return Convert.ToBase64String (writer.ToArray ());
			}
		}
		/// <summary>
		/// 指示当前对象是否等于同一类型的另一个对象
		/// </summary>
		/// <param name="other">一个与此对象进行比较的对象</param>
		/// <returns>如果当前对象等于 other 参数，则为 <see langword="true"/>；否则为 <see langword="false"/></returns>
		public bool Equals (GroupFile other)
		{
			if (other is null)
			{
				return false;
			}

			return this.Id == other.Id && this.Name.Equals (other.Name) && this.Guid.Equals (other.Guid) && this.Size == other.Size;
		}
		/// <summary>
		/// 指示当前对象是否等于同一类型的另一个对象
		/// </summary>
		/// <param name="obj">一个与此对象进行比较的对象</param>
		/// <returns>如果当前对象等于 other 参数，则为 <see langword="true"/>；否则为 <see langword="false"/></returns>
		public override bool Equals (object obj)
		{
			return this.Equals (obj as GroupFile);
		}
		/// <summary>
		/// 返回此实例的哈希代码
		/// </summary>
		/// <returns>32 位有符号整数哈希代码</returns>
		public override int GetHashCode ()
		{
			return this.Id.GetHashCode () & this.Name.GetHashCode () & this.Guid.GetHashCode () & this.Size.GetHashCode ();
		}
		/// <summary>
		/// 返回表示当前对象的字符串
		/// </summary>
		/// <returns>表示当前对象的字符串</returns>
		public override string ToString ()
		{
			StringBuilder builder = new StringBuilder ();
			builder.AppendLine ($"ID: {this.Id}");
			builder.AppendLine ($"名称: {this.Name}");
			builder.AppendLine ($"标识 (GUID): {this.Guid}");
			builder.AppendLine ($"大小: {this.Size}");
			return builder.ToString ();
		}
		#endregion

		#region --私有方法--
		private static int GetFileId ()
		{
			lock (typeof (GroupFile))
			{
				if (_privateId == int.MaxValue)
				{
					_privateId = 0;
				}
				return ++_privateId;
			}
		}
		#endregion
	}
}
