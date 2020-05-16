using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.SimulatorModel.CQP
{
	/// <summary>
	/// 描述 消息 类型
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
		/// <summary>
		/// 获取当前实例的发送时间
		/// </summary>
		public DateTime SendTime { get; }
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
			this.SendTime = DateTime.Now;
		}
		#endregion

		#region --私有方法--
		private static int GetMessageId ()
		{
			lock (typeof (Message))
			{
				if (_privateId == int.MaxValue)
				{
					_privateId = 0;
				}
				return ++_privateId;
			}
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
		#endregion
	}
}
