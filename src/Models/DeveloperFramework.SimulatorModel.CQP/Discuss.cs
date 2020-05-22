using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.SimulatorModel.CQP
{
	/// <summary>
	/// 描述 讨论组 类型
	/// </summary>
	public class Discuss
	{
		#region --常量--
		private const int _minValue = 10000;
		#endregion

		#region --属性--
		/// <summary>
		/// 表示当前实例 <see cref="Group"/> 的最小值.
		/// </summary>
		public static readonly long MinValue = 10000;
		/// <summary>
		/// 获取或设置当前实例的唯一标识 (群号)
		/// </summary>
		public long Id { get; set; }
		#endregion

		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="Discuss"/> 类的新实例
		/// </summary>
		/// <param name="id">当前实例指定的 Id</param>
		/// <exception cref="ArgumentOutOfRangeException">id 小于 <see cref="MinValue"/></exception>
		public Discuss (long id)
		{
			if (id < _minValue)
			{
				throw new ArgumentOutOfRangeException (nameof (id));
			}

			this.Id = id;
		}
		#endregion

		#region --运算符--
		/// <summary>
		/// 定义将 <see cref="long"/> 转换为 <see cref="Discuss"/>
		/// </summary>
		/// <param name="value">要转换的值</param>
		public static implicit operator Discuss (long value)
		{
			return new Discuss (value);
		}
		/// <summary>
		/// 定义将 <see cref="Discuss"/> 转换为 <see cref="long"/>
		/// </summary>
		/// <param name="value">要转换的 <see cref="Discuss"/> 实例</param>
		public static implicit operator long (Discuss value)
		{
			return value.Id;
		}
		#endregion
	}
}
