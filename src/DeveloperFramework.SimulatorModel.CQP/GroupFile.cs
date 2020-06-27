using System;
using System.IO;
using System.Linq;

using DeveloperFramework.Extension;

namespace DeveloperFramework.SimulatorModel.CQP
{
	/// <summary>
	/// 描述群文件的类
	/// </summary>
	public class GroupFile : IToBase64
	{
		#region --属性--
		/// <summary>
		/// 获取当前实例的全局唯一标识
		/// </summary>
		public int Id { get; }
		/// <summary>
		/// 获取当前实例的文件名称
		/// </summary>
		public string Name { get; }
		/// <summary>
		/// 获取当前实例文件的唯一标识
		/// </summary>
		public Guid Guid { get; }
		/// <summary>
		/// 获取当前实例的文件大小 (单位: 字节)
		/// </summary>
		public long Size { get; }
		#endregion

		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="GroupFile"/> 类的新实例
		/// </summary>
		/// <param name="id">绑定于当前实例的唯一标识 (ID)</param>
		/// <param name="name">绑定于当前实例的名称</param>
		/// <param name="size">当前实例的大小</param>
		public GroupFile (int id, string name, long size)
		{
			this.Id = id;
			this.Name = name;
			this.Guid = Guid.NewGuid ();
			this.Size = size;
		}
		#endregion

		#region --公开方法--
		/// <summary>
		/// 返回当前实例的 Base64 字符串
		/// </summary>
		/// <returns>当前实例的 Base64 字符串</returns>
		public string ToBase64 ()
		{
			using (BinaryWriter writer = new BinaryWriter (new MemoryStream ()))
			{
				writer.Write_Ex (this.Guid.ToString ());
				writer.Write_Ex (this.Name);
				writer.Write_Ex (this.Size);
				writer.Write_Ex (this.Id);
				return Convert.ToBase64String (writer.ToArray ());
			}
		}
		#endregion
	}
}
