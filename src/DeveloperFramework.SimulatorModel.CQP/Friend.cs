using System;
using System.IO;
using System.Linq;

using DeveloperFramework.Extension;

namespace DeveloperFramework.SimulatorModel.CQP
{
	/// <summary>
	/// 描述好友的类
	/// </summary>
	public class Friend : QQ, IEquatable<Friend>, IToBase64
	{
		#region --属性--
		/// <summary>
		/// 获取或设置当前实例的好友备注
		/// </summary>
		public string Notes { get; set; }
		#endregion

		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="Friend"/> 类的新实例
		/// </summary>
		/// <param name="id">绑定于当前实例的唯一标识 (ID)</param>
		public Friend (long id)
			: base (id)
		{
		}
		#endregion

		#region --公开方法--
		/// <summary>
		/// 指示当前对象是否等于同一类型的另一个对象
		/// </summary>
		/// <param name="other">一个与此对象进行比较的对象</param>
		/// <returns>如果当前对象等于 other 参数，则为 <see langword="true"/>；否则为 <see langword="false"/></returns>
		public bool Equals (Friend other)
		{
			return base.Equals (other);
		}
		/// <summary>
		/// 指示当前对象是否等于同一类型的另一个对象
		/// </summary>
		/// <param name="other">一个与此对象进行比较的对象</param>
		/// <returns>如果当前对象等于 other 参数，则为 <see langword="true"/>；否则为 <see langword="false"/></returns>
		public override bool Equals (object obj)
		{
			return this.Equals (obj as Friend);
		}
		/// <summary>
		/// 作为默认哈希函数
		/// </summary>
		/// <returns>当前对象的哈希代码</returns>
		public override int GetHashCode ()
		{
			return base.GetHashCode ();
		}
		/// <summary>
		/// 返回当前实例的字节数组
		/// </summary>
		/// <param name="group">绑定字节数组信息中的群</param>
		/// <returns>当前实例的字节数组</returns>
		public byte[] ToByteArray ()
		{
			using (BinaryWriter writer = new BinaryWriter (new MemoryStream ()))
			{
				writer.Write_Ex (this.Id);
				writer.Write_Ex (this.Nickname);
				writer.Write_Ex (this.Notes);

				return writer.ToArray ();
			}
		}
		/// <summary>
		/// 返回表示当前对象的 Base64 字符串
		/// </summary>
		/// <returns>表示当前对象的字符串</returns>
		public override string ToBase64 ()
		{
			return Convert.ToBase64String (this.ToByteArray ());
		}
		#endregion
	}
}
