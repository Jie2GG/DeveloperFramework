using System;
using System.IO;
using System.Linq;

using DeveloperFramework.Extension;

namespace DeveloperFramework.SimulatorModel.CQP
{
	/// <summary>
	/// 描述群匿名的类
	/// </summary>
	public class GroupAnonymous : IToBase64
	{
		#region --属性--
		/// <summary>
		/// 获取当前实例的匿名标识
		/// </summary>
		public long Id { get; }
		/// <summary>
		/// 获取当前实例的匿名称号
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 获取当前实例的执行令牌
		/// </summary>
		public byte[] Token { get; }
		#endregion

		#region --构造函数--
		/// <summary>
		/// 使用指定的 ID 初始化 <see cref="GroupAnonymous"/> 类的新实例
		/// </summary>
		/// <param name="id">绑定于匿名的 ID</param>
		public GroupAnonymous (long id)
		{
			this.Id = id;
			this.Token = Guid.NewGuid ().ToByteArray ();
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
				writer.Write_Ex (this.Id);
				writer.Write_Ex (this.Name);
				writer.Write_Ex (this.Token);

				return Convert.ToBase64String (writer.ToArray ());
			}
		}
		#endregion
	}
}
