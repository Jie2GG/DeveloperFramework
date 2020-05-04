using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.LibraryModel.CQP
{
	/// <summary>
	/// 描述 CQP 应用的 Discuss 类型
	/// </summary>
	public class Message
	{
		#region --字段--
		private static int _privateId;
		#endregion

		#region --属性--
		/// <summary>
		/// 获取当前实例的唯一标识
		/// </summary>
		public int Id { get; }
		/// <summary>
		/// 获取当前实例的详细信息
		/// </summary>
		public string Text { get; }
		#endregion

		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="Message"/> 静态类的数据
		/// </summary>
		static Message ()
		{
			_privateId = 0;
		}
		/// <summary>
		/// 初始化 <see cref="Message"/> 类的新实例
		/// </summary>
		/// <param name="text">消息内容</param>
		public Message (string text)
		{
			if (text is null)
			{
				throw new ArgumentNullException (nameof (text));
			}

			this.Id = GetMessageId ();
			this.Text = text;
		}
		#endregion

		#region --公开方法--
		/// <summary>
		/// 指示当前对象是否等于同一类型的另一个对象
		/// </summary>
		/// <param name="other">一个与此对象进行比较的对象</param>
		/// <returns>如果当前对象等于 other 参数，则为 <see langword="true"/>；否则为 <see langword="false"/></returns>
		public bool Equals (Message other)
		{
			if (other is null)
			{
				return false;
			}

			return this.Id == other.Id && this.Text.Equals (other.Text);
		}
		/// <summary>
		/// 指示当前对象是否等于同一类型的另一个对象
		/// </summary>
		/// <param name="obj">一个与此对象进行比较的对象</param>
		/// <returns>如果当前对象等于 other 参数，则为 <see langword="true"/>；否则为 <see langword="false"/></returns>
		public override bool Equals (object obj)
		{
			return this.Equals (obj as Message);
		}
		/// <summary>
		/// 返回此实例的哈希代码
		/// </summary>
		/// <returns>32 位有符号整数哈希代码</returns>
		public override int GetHashCode ()
		{
			return this.Id.GetHashCode () & Text.GetHashCode ();
		}
		/// <summary>
		/// 返回表示当前对象的字符串
		/// </summary>
		/// <returns>表示当前对象的字符串</returns>
		public override string ToString ()
		{
			return this;
		}
		#endregion

		#region --私有方法--
		private static int GetMessageId ()
		{
			if (_privateId == int.MaxValue)
			{
				_privateId = 0;
			}
			return ++_privateId;
		}
		#endregion

		#region --运算符--
		/// <summary>
		/// 定义将 <see cref="Message"/> 对象转换为 <see cref="string"/>
		/// </summary>
		/// <param name="value">转换 <see cref="Message"/> 对象</param>
		public static implicit operator string (Message value)
		{
			return value.Text;
		}
		/// <summary>
		/// 定义将 <see cref="string"/> 对象转换为 <see cref="Message"/>
		/// </summary>
		/// <param name="value">转换 <see cref="string"/> 对象</param>
		public static implicit operator Message (string value)
		{
			return new Message (value);
		}
		/// <summary>
		/// 确定两个指定的 <see cref="Message"/> 实例是否具有相同的值
		/// </summary>
		/// <param name="a">要比较的第一个对象</param>
		/// <param name="b">要比较的第二个对象</param>
		/// <returns>如果 a 是与 b 相同的值，或两者均为 <see langword="null"/>，则为 <see langword="true"/>；否则为 <see langword="false"/></returns>
		public static bool operator == (Message a, Message b)
		{
			return a.Equals (b);
		}
		/// <summary>
		/// 确定两个指定的 <see cref="Message"/> 实例是否具有不同的值
		/// </summary>
		/// <param name="a">要比较的第一个对象</param>
		/// <param name="b">要比较的第二个对象</param>
		/// <returns>如果 a 是与 b 相同的值，或两者均为 <see langword="null"/>，则为 <see langword="false"/>；否则为 <see langword="true"/></returns>
		public static bool operator != (Message a, Message b)
		{
			return !a.Equals (b);
		}
		/// <summary>
		/// 确定指定的 <see cref="Message"/> 和 <see cref="string"/> 实例是否具有相同的值
		/// </summary>
		/// <param name="a">要比较的 <see cref="Message"/> 对象</param>
		/// <param name="b">要比较的 <see cref="string"/> 对象</param>
		/// <returns>如果 a.Text 是与 b 相同的值，或两者均为 <see langword="null"/>，则为 <see langword="true"/>；否则为 <see langword="false"/></returns>
		public static bool operator == (Message a, string b)
		{
			return a.Text.Equals (b);
		}
		/// <summary>
		/// 确定指定的 <see cref="Message"/> 和 <see cref="string"/> 实例是否具有不同的值
		/// </summary>
		/// <param name="a">要比较的 <see cref="Message"/> 对象</param>
		/// <param name="b">要比较的 <see cref="string"/> 对象</param>
		/// <returns>如果 a.Text 是与 b 相同的值，或两者均为 <see langword="null"/>，则为 <see langword="false"/>；否则为 <see langword="true"/></returns>
		public static bool operator != (Message a, string b)
		{
			return !a.Text.Equals (b);
		}
		/// <summary>
		/// 确定指定的 <see cref="Message"/> 和 <see cref="string"/> 实例是否具有相同的值
		/// </summary>
		/// <param name="a">要比较的 <see cref="string"/> 对象</param>
		/// <param name="b">要比较的 <see cref="Message"/> 对象</param>
		/// <returns>如果 a 是与 b.Text 相同的值，或两者均为 <see langword="null"/>，则为 <see langword="true"/>；否则为 <see langword="false"/></returns>
		public static bool operator == (string a, Message b)
		{
			return a.Equals (b.Text);
		}
		/// <summary>
		/// 确定指定的 <see cref="Message"/> 和 <see cref="string"/> 实例是否具有不同的值
		/// </summary>
		/// <param name="a">要比较的 <see cref="string"/> 对象</param>
		/// <param name="b">要比较的 <see cref="Message"/> 对象</param>
		/// <returns>如果 a 是与 b.Text 相同的值，或两者均为 <see langword="null"/>，则为 <see langword="false"/>；否则为 <see langword="true"/></returns>
		public static bool operator != (string a, Message b)
		{
			return !a.Equals (b.Text);
		}
		#endregion
	}
}
