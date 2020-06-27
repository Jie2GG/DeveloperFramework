using System;

namespace DeveloperFramework.SimulatorModel.CQP
{
	/// <summary>
	/// 描述消息的类
	/// </summary>
	public class Message
	{
		#region --字段--
		private static int _id;
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
		/// 初始化 <see cref="Message"/> 类的新实例
		/// </summary>
		/// <param name="text">新消息的字符串</param>
		public Message (string text)
		{
			this.Id = NextId ();
			this.Text = text;
			this.SendTime = DateTime.Now;
		}
		#endregion

		#region --私有方法--
		private static int NextId ()
		{
			lock (typeof (Message))
			{
				if (_id == int.MaxValue)
				{
					_id = 0;
				}

				return _id++;
			}
		}
		#endregion

		#region --运算符--
		/// <summary>
		/// 将 <see cref="string"/> 对象转换为 <see cref="Message"/>
		/// </summary>
		/// <param name="value">要转换的字符串</param>
		public static explicit operator Message (string value)
		{
			return new Message (value);
		}
		#endregion
	}
}
