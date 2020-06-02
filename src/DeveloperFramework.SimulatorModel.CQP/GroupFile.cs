using DeveloperFramework.Extension;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.SimulatorModel.CQP
{
	/// <summary>
	/// 描述 CQP 应用的 File 类型
	/// </summary>
	public class GroupFile
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
