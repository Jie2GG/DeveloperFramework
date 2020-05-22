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
	/// 描述 QQ 类型
	/// </summary>
	public class QQ : IEquatable<QQ>
	{
		#region --常量--
		private const int _minValue = 10000;
		#endregion

		#region --属性--
		/// <summary>
		/// 表示当前实例 <see cref="QQ"/> 的最小值, 此字段为常数. 并且此值作为系统默认标识
		/// </summary>
		public static readonly long MinValue = 10000;
		/// <summary>
		/// 获取或设置当前实例的唯一标识 (QQ号)
		/// </summary>
		public long Id { get; set; }
		/// <summary>
		/// 获取或设置当前实例的昵称
		/// </summary>
		public string Nick { get; set; }
		/// <summary>
		/// 获取或设置当前实例的性别
		/// </summary>
		public Sex Sex { get; set; }
		/// <summary>
		/// 获取或设置当前实例的年龄
		/// </summary>
		public int Age { get; set; }
		#endregion

		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="QQ"/> 的新实例
		/// </summary>
		/// <param name="id">QQ号</param>
		/// <param name="nick">昵称</param>
		/// <param name="sex">性别</param>
		/// <param name="age">年龄</param>
		public QQ (long id, string nick, Sex sex, int age)
		{
			if (id < _minValue)
			{
				throw new ArgumentOutOfRangeException (nameof (id));
			}
			Id = id;
			Nick = nick ?? throw new ArgumentNullException (nameof (nick));
			Sex = sex;
			Age = age;
		}
		#endregion

		#region --公开方法--
		/// <summary>
		/// 获取当前实例的 <see cref="byte"/> 数组
		/// </summary>
		/// <returns>当前实例的 <see cref="byte"/> 数组</returns>
		public virtual byte[] ToByteArray ()
		{
			using (BinaryWriter writer = new BinaryWriter (new MemoryStream ()))
			{
				writer.Write_Ex (this.Id);
				writer.Write_Ex (this.Nick);
				writer.Write_Ex ((int)this.Sex);
				writer.Write_Ex (this.Age);
				return writer.ToArray ();
			}
		}
		/// <summary>
		/// 获取当前实例的 Base64 字符串
		/// </summary>
		/// <returns>当前实例的 Base64 字符串</returns>
		public virtual string ToBase64String ()
		{
			return Convert.ToBase64String (this.ToByteArray ());
		}
		/// <summary>
		/// 指示当前对象是否等于同一类型的另一个对象
		/// </summary>
		/// <param name="obj">一个与此对象进行比较的对象</param>
		/// <returns>如果当前对象等于 obj 参数，则为 <see langword="true"/>；否则为 <see langword="false"/></returns>
		public bool Equals (QQ obj)
		{
			if (obj == null)
			{
				return false;
			}

			return this.Id.Equals (obj.Id) && this.Nick.Equals (obj.Nick) && ((int)this.Sex).Equals ((int)obj.Sex) && this.Age.Equals (obj.Age);
		}
		/// <summary>
		/// 指示当前对象是否等于同一类型的另一个对象
		/// </summary>
		/// <param name="obj">一个与此对象进行比较的对象</param>
		/// <returns>如果当前对象等于 obj 参数，则为 <see langword="true"/>；否则为 <see langword="false"/></returns>
		public override bool Equals (object obj)
		{
			return this.Equals (obj as QQ);
		}
		/// <summary>
		/// 返回此实例的哈希代码
		/// </summary>
		/// <returns>32 位有符号整数哈希代码</returns>
		public override int GetHashCode ()
		{
			return this.Id.GetHashCode () & this.Nick.GetHashCode () & this.Sex.GetHashCode () & this.Age.GetHashCode ();
		}
		#endregion

		#region --运算符--
		/// <summary>
		/// 定义将 <see cref="QQ"/> 转换为 <see cref="long"/>
		/// </summary>
		/// <param name="value">要转换的原始 <see cref="QQ"/> 实例</param>
		public static implicit operator long (QQ value)
		{
			return value.Id;
		}
		#endregion
	}
}
