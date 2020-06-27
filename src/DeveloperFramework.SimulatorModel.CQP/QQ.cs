using System;
using System.IO;
using System.Linq;

using DeveloperFramework.Extension;

namespace DeveloperFramework.SimulatorModel.CQP
{
	/// <summary>
	/// 描述QQ的类
	/// </summary>
	public class QQ : IEquatable<QQ>, IToBase64
	{
		#region --属性--
		/// <summary>
		/// 获取当前实例的唯一标识 (ID)
		/// </summary>
		public long Id { get; }
		/// <summary>
		/// 获取或设置当前实例表示的性别
		/// </summary>
		public Gender Gender { get; set; }
		/// <summary>
		/// 获取或设置当前实例表示的年龄
		/// </summary>
		public int Age { get; set; }
		/// <summary>
		/// 获取或设置当前实例表示的昵称
		/// </summary>
		public string Nickname { get; set; }
		#endregion

		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="QQ"/> 类的新实例
		/// </summary>
		/// <param name="id">绑定于当前实例的唯一标识 (ID)</param>
		public QQ (long id)
		{
			this.Id = id;
		}
		#endregion

		#region --公开方法--
		/// <summary>
		/// 指示当前对象是否等于同一类型的另一个对象
		/// </summary>
		/// <param name="other">一个与此对象进行比较的对象</param>
		/// <returns>如果当前对象等于 other 参数，则为 <see langword="true"/>；否则为 <see langword="false"/></returns>
		public bool Equals (QQ other)
		{
			if (other == null)
			{
				return false;
			}

			return this.Id == other.Id;
		}
		/// <summary>
		/// 指示当前对象是否等于同一类型的另一个对象
		/// </summary>
		/// <param name="other">一个与此对象进行比较的对象</param>
		/// <returns>如果当前对象等于 other 参数，则为 <see langword="true"/>；否则为 <see langword="false"/></returns>
		public override bool Equals (object obj)
		{
			return this.Equals (obj as QQ);
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
		/// 返回表示当前对象的 Base64 字符串
		/// </summary>
		/// <returns>表示当前对象的字符串</returns>
		public virtual string ToBase64 ()
		{
			using (BinaryWriter writer = new BinaryWriter (new MemoryStream ()))
			{
				writer.Write_Ex (this.Id);
				writer.Write_Ex (this.Nickname);
				writer.Write_Ex ((int)this.Gender);
				writer.Write_Ex (this.Age);

				return Convert.ToBase64String (writer.ToArray ());
			}
		}
		#endregion
	}
}
