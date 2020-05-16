﻿using DeveloperFramework.Extension;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.SimulatorModel.CQP
{
	/// <summary>
	/// 描述 群 类型
	/// </summary>
	public class Group
	{
		#region --常量--
		private const int _minValue = 10000;
		#endregion

		#region --属性--
		/// <summary>
		/// 获取或设置当前实例的唯一标识 (群号)
		/// </summary>
		public long Id { get; set; }
		/// <summary>
		/// 获取或设置当前实例的群名称
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 获取或设置当前实例的群当前人数
		/// </summary>
		public int CurrentMemberCount { get; set; }
		/// <summary>
		/// 获取或设置当前实例的最大群人数
		/// </summary>
		public int MaxMemberCount { get; set; }
		/// <summary>
		/// 获取或设置当前实例的群成员列表
		/// </summary>
		public GroupMemberCollection MemberCollection { get; set; }
		#endregion

		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="Group"/> 类的新实例
		/// </summary>
		/// <param name="id">当前实例指定的 Id</param>
		/// <exception cref="ArgumentOutOfRangeException">id 小于 <see cref="MinValue"/></exception>
		public Group (long id, string name, int currentCount, int maxCount)
		{
			if (id < _minValue)
			{
				throw new ArgumentOutOfRangeException (nameof (id));
			}

			this.Id = id;
			this.Name = name;
			this.CurrentMemberCount = currentCount;
			this.MaxMemberCount = maxCount;
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
				writer.Write_Ex (this.Name);
				writer.Write_Ex (this.CurrentMemberCount);
				writer.Write_Ex (this.MaxMemberCount);
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
		#endregion

		#region --运算符--
		/// <summary>
		/// 定义将 <see cref="Group"/> 转换为 <see cref="long"/>
		/// </summary>
		/// <param name="value">要转换的 <see cref="Group"/> 实例</param>
		public static implicit operator long (Group value)
		{
			return value.Id;
		}
		#endregion
	}
}
