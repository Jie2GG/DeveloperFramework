using DeveloperFramework.Utility;
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
		/// <summary>
		/// 获取当前实例的来源群
		/// </summary>
		public Group FromGroup { get; }
		/// <summary>
		/// 获取当前实例的来源讨论组
		/// </summary>
		public Discuss FromDiscuss { get; }
		/// <summary>
		/// 获取当前实例的来源QQ
		/// </summary>
		public QQ FromQQ { get; }
		/// <summary>
		/// 获取当前实例是否已撤回消息
		/// </summary>
		public bool IsRevocation { get; private set; }
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
		/// <param name="text">相关联的消息字符串</param>
		/// <param name="fromGroup">来源群</param>
		/// <param name="fromQQ">来源QQ</param>
		public Message (string text, Group fromGroup, QQ fromQQ)
			: this (text, fromQQ)
		{
			this.FromGroup = fromGroup;
		}
		/// <summary>
		/// 初始化 <see cref="Message"/> 类的新实例
		/// </summary>
		/// <param name="text">相关联的消息字符串</param>
		/// <param name="fromDiscuss">来源讨论组</param>
		/// /// <param name="fromQQ">来源QQ</param>
		public Message (string text, Discuss fromDiscuss, QQ fromQQ)
			: this (text, fromQQ)
		{
			this.FromDiscuss = fromDiscuss;
		}
		/// <summary>
		/// 初始化 <see cref="Message"/> 类的新实例
		/// </summary>
		/// <param name="text">相关联的消息字符串</param>
		/// <param name="fromQQ">来源QQ</param>
		public Message (string text, QQ fromQQ)
			: this (text)
		{
			this.FromQQ = fromQQ;
		}
		private Message (string text)
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

		#region --公开方法--
		/// <summary>
		/// 将当前实例做撤回处理
		/// </summary>
		/// <param name="coerce">是否强制撤回消息</param>
		/// <returns>如果当前时间减去 <see cref="SendTime"/> 小于 2 或 coerce 为 <see langword="true"/> 则返回 <see langword="true"/> 否则返回 <see langword="false"/></returns>
		public bool Revocation (bool coerce)
		{
			if (DateTimeUtility.GetDateTimeInterval (DateTime.Now, this.SendTime).TotalSeconds <= 120 || coerce)
			{
				this.IsRevocation = true;
				return true;
			}

			return false;
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
		#endregion
	}
}
