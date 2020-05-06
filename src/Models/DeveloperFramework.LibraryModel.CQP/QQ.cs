using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.LibraryModel.CQP
{
	/// <summary>
	/// 描述 CQP 应用的 QQ 类型
	/// </summary>
	public class QQ : IEquatable<QQ>
	{
		#region --常量--
		/// <summary>
		/// 表示当前实例 <see cref="QQ"/> 的最小值, 此字段为常数. 并且此值作为系统默认标识
		/// </summary>
		public const long MinValue = 10000;
		#endregion

		#region --属性--
		/// <summary>
		/// 获取当前实例的唯一标识 (QQ号)
		/// </summary>
		public long Id { get; private set; }
		#endregion

		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="QQ"/> 类的新实例
		/// </summary>
		/// <param name="id">当前实例指定的 Id</param>
		/// <exception cref="ArgumentOutOfRangeException">id 小于 <see cref="MinValue"/></exception>
		public QQ (long id)
		{
			if (id < QQ.MinValue)
			{
				throw new ArgumentOutOfRangeException (nameof (id));
			}

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
			if (other is null)
			{
				return false;
			}

			return this.Id == other.Id;
		}
		/// <summary>
		/// 指示当前对象是否等于同一类型的另一个对象
		/// </summary>
		/// <param name="obj">一个与此对象进行比较的对象</param>
		/// <returns>如果当前对象等于 other 参数，则为 <see langword="true"/>；否则为 <see langword="false"/></returns>
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
			return this.Id.GetHashCode ();
		}
		/// <summary>
		/// 返回表示当前对象的字符串
		/// </summary>
		/// <returns>表示当前对象的字符串</returns>
		public override string ToString ()
		{
			return this.Id.ToString ();
		}
		#endregion

		#region --转换方法--
		/// <summary>
		/// 定义将 <see cref="QQ"/> 对象转换为 <see cref="long"/>
		/// </summary>
		/// <param name="value">转换的 <see cref="QQ"/> 对象</param>
		public static implicit operator long (QQ value)
		{
			return value.Id;
		}
		/// <summary>
		/// 定义将 <see cref="QQ"/> 对象转换为 <see cref="string"/>
		/// </summary>
		/// <param name="value">转换的 <see cref="QQ"/> 对象</param>
		public static implicit operator string (QQ value)
		{
			return value.ToString ();
		}
		/// <summary>
		/// 确定两个指定的 <see cref="QQ"/> 实例是否具有相同的值
		/// </summary>
		/// <param name="a">要比较的第一个对象</param>
		/// <param name="b">要比较的第二个对象</param>
		/// <returns>如果 a 是与 b 相同的值，或两者均为 <see langword="null"/>，则为 <see langword="true"/>；否则为 <see langword="false"/></returns>
		public static bool operator == (QQ a, QQ b)
		{
			return a.Equals (b);
		}
		/// <summary>
		/// 确定两个指定的 <see cref="QQ"/> 实例是否具有不同的值
		/// </summary>
		/// <param name="a">要比较的第一个对象</param>
		/// <param name="b">要比较的第二个对象</param>
		/// <returns>如果 a 是与 b 相同的值，或两者均为 <see langword="null"/>，则为 <see langword="false"/>；否则为 <see langword="true"/></returns>
		public static bool operator != (QQ a, QQ b)
		{
			return !a.Equals (b);
		}
		/// <summary>
		/// 确定指定的 <see cref="QQ"/> 和 <see cref="string"/> 实例是否具有相同的值
		/// </summary>
		/// <param name="a">要比较的 <see cref="QQ"/> 对象</param>
		/// <param name="b">要比较的 <see cref="long"/> 对象</param>
		/// <returns>如果 a.Id 是与 b 相同的值，或两者均为 <see langword="null"/>，则为 <see langword="true"/>；否则为 <see langword="false"/></returns>
		public static bool operator == (QQ a, long b)
		{
			return a.Id.Equals (b);
		}
		/// <summary>
		/// 确定指定的 <see cref="QQ"/> 和 <see cref="string"/> 实例是否具有不同的值
		/// </summary>
		/// <param name="a">要比较的 <see cref="QQ"/> 对象</param>
		/// <param name="b">要比较的 <see cref="long"/> 对象</param>
		/// <returns>如果 a.Id 是与 b 相同的值，或两者均为 <see langword="null"/>，则为 <see langword="false"/>；否则为 <see langword="true"/></returns>
		public static bool operator != (QQ a, long b)
		{
			return a.Id.Equals (b);
		}
		/// <summary>
		/// 确定指定的 <see cref="QQ"/> 和 <see cref="string"/> 实例是否具有相同的值
		/// </summary>
		/// <param name="a">要比较的 <see cref="long"/> 对象</param>
		/// <param name="b">要比较的 <see cref="QQ"/> 对象</param>
		/// <returns>如果 a.Id 是与 b 相同的值，或两者均为 <see langword="null"/>，则为 <see langword="true"/>；否则为 <see langword="false"/></returns>
		public static bool operator == (long a, QQ b)
		{
			return a.Equals (b.Id);
		}
		/// <summary>
		/// 确定指定的 <see cref="QQ"/> 和 <see cref="string"/> 实例是否具有不同的值
		/// </summary>
		/// <param name="a">要比较的 <see cref="long"/> 对象</param>
		/// <param name="b">要比较的 <see cref="QQ"/> 对象</param>
		/// <returns>如果 a.Id 是与 b 相同的值，或两者均为 <see langword="null"/>，则为 <see langword="false"/>；否则为 <see langword="true"/></returns>
		public static bool operator != (long a, QQ b)
		{
			return a.Equals (b.Id);
		}
		/// <summary>
		/// 确定指定的 <see cref="QQ"/> 和 <see cref="string"/> 实例是否具有相同的值
		/// </summary>
		/// <param name="a">要比较的 <see cref="QQ"/> 对象</param>
		/// <param name="b">要比较的 <see cref="string"/> 对象</param>
		/// <returns>如果 a.Id 是与 b 相同的值，或两者均为 <see langword="null"/>，则为 <see langword="true"/>；否则为 <see langword="false"/></returns>
		public static bool operator == (QQ a, string b)
		{
			return ((string)a).Equals (b);
		}
		/// <summary>
		/// 确定指定的 <see cref="QQ"/> 和 <see cref="string"/> 实例是否具有不同的值
		/// </summary>
		/// <param name="a">要比较的 <see cref="QQ"/> 对象</param>
		/// <param name="b">要比较的 <see cref="string"/> 对象</param>
		/// <returns>如果 a.Id 是与 b 相同的值，或两者均为 <see langword="null"/>，则为 <see langword="false"/>；否则为 <see langword="true"/></returns>
		public static bool operator != (QQ a, string b)
		{
			return !((string)a).Equals (b);
		}
		/// <summary>
		/// 确定指定的 <see cref="QQ"/> 和 <see cref="string"/> 实例是否具有相同的值
		/// </summary>
		/// <param name="a">要比较的 <see cref="string"/> 对象</param>
		/// <param name="b">要比较的 <see cref="QQ"/> 对象</param>
		/// <returns>如果 a 是与 b.Id 相同的值，或两者均为 <see langword="null"/>，则为 <see langword="true"/>；否则为 <see langword="false"/></returns>
		public static bool operator == (string a, QQ b)
		{
			return a.Equals ((string)b);
		}
		/// <summary>
		/// 确定指定的 <see cref="QQ"/> 和 <see cref="string"/> 实例是否具有不同的值
		/// </summary>
		/// <param name="a">要比较的 <see cref="string"/> 对象</param>
		/// <param name="b">要比较的 <see cref="QQ"/> 对象</param>
		/// <returns>如果 a 是与 b.Id 相同的值，或两者均为 <see langword="null"/>，则为 <see langword="false"/>；否则为 <see langword="true"/></returns>
		public static bool operator != (string a, QQ b)
		{
			return !a.Equals ((string)b);
		}
		#endregion
	}
}
