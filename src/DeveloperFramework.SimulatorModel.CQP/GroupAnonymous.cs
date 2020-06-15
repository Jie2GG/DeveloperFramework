using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeveloperFramework.Extension;

namespace DeveloperFramework.SimulatorModel.CQP
{
	/// <summary>
	/// 描述 CQP 应用的 Anonymous 类型
	/// </summary>
	public class GroupAnonymous
	{
		#region --属性--
		/// <summary>
		/// 获取当前实例的匿名标识
		/// </summary>
		public long Id { get; }
		/// <summary>
		/// 获取当前实例的匿名称号
		/// </summary>
		public string Name { get; }
		/// <summary>
		/// 获取当前实例的执行令牌
		/// </summary>
		public byte[] Token { get; }
		#endregion

		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="GroupAnonymous"/> 类的新实例
		/// </summary>
		/// <param name="id">匿名标识</param>
		/// <param name="name">匿名称号</param>
		/// <param name="token">匿名令牌</param>
		public GroupAnonymous (long id, string name, byte[] token)
		{
			Id = id;
			Name = name ?? throw new ArgumentNullException (nameof (name));
			Token = token ?? throw new ArgumentNullException (nameof (token));
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
				writer.Write_Ex (this.Id);
				writer.Write_Ex (this.Name);
				writer.Write_Ex (this.Token);
				return Convert.ToBase64String (writer.ToArray ());
			}
		}
		#endregion
	}
}
