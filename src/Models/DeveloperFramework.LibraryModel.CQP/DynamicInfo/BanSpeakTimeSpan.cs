using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.LibraryModel.CQP.Dynamic
{
	/// <summary>
	/// 描述 CQP 应用的 BanSpeakTimeSpan 类型
	/// </summary>
	public struct BanSpeakTimeSpan
	{
		#region --字段--
		private readonly TimeSpan _span;
		/// <summary>
		/// 获取 <see cref="BanSpeakTimeSpan"/> 的最小值, 当前值表示为 0秒 (当前值可用于解除禁言)
		/// </summary>
		public static readonly BanSpeakTimeSpan MinValue = 0;
		/// <summary>
		/// 获取 <see cref="BanSpeakTimeSpan"/> 的最大值, 当前值表示为 30天
		/// </summary>
		public static readonly BanSpeakTimeSpan MaxValue = 2592000;
		#endregion

		#region --属性--
		/// <summary>
		/// 获取原始的 <see cref="System.TimeSpan"/>
		/// </summary>
		public TimeSpan TimeSpan => this._span;
		#endregion

		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="BanSpeakTimeSpan"/> 结构的新实例, 并指定秒数
		/// </summary>
		/// <param name="second">指定的秒数</param>
		/// <exception cref="ArgumentOutOfRangeException">second 超过 <see cref="MinValue"/> 或 <see cref="MaxValue"/></exception>
		public BanSpeakTimeSpan (long second)
		{
			if (second < 0 || second > 2592000)
			{
				throw new ArgumentOutOfRangeException (nameof (second));
			}
			this._span = new TimeSpan (second * 10000000L);
		}
		/// <summary>
		/// 初始化 <see cref="BanSpeakTimeSpan"/> 结构的新实例, 并指定 天、时、分和秒
		/// </summary>
		/// <param name="days">指定的 "天" 数值, 范围在 0~30 之间</param>
		/// <param name="hours">指定的 "时" 数值, 范围在 0~23 之间, 并且当 days 为 30 时, 此值必须为 0</param>
		/// <param name="minutes">指定 "分" 数值, 范围在 0~59 之间, 并且当 days 为 30 时, 此值必须为 0</param>
		/// <param name="seconds">指定 "秒" 数值, 范围在 0~59 之间, 并且当 dyas 为 30 时, 此值必须为 0</param>
		/// <exception cref="ArgumentOutOfRangeException">days、hours、minutes 或 seconds 超出范围</exception>
		public BanSpeakTimeSpan (int days, int hours, int minutes, int seconds)
		{
			if (days == 30)
			{
				if (hours != 0)
				{
					throw new ArgumentOutOfRangeException (nameof (hours), "无法表示超过 30天 的时间");
				}

				if (minutes != 0)
				{
					throw new ArgumentOutOfRangeException (nameof (minutes), "无法表示超过 30天 的时间");
				}

				if (seconds != 0)
				{
					throw new ArgumentOutOfRangeException (nameof (seconds), "无法表示超过 30天 的时间");
				}
			}

			if (hours < 0 || hours > 23)
			{
				throw new ArgumentOutOfRangeException (nameof (hours), "范围必须处于 0~23 之间");
			}

			if (minutes < 0 || minutes > 59)
			{
				throw new ArgumentOutOfRangeException (nameof (minutes), "范围必须处于 0~59 之间");
			}

			if (seconds < 0 || seconds > 59)
			{
				throw new ArgumentOutOfRangeException (nameof (seconds), "范围必须处于 0~59 之间");
			}

			this._span = new TimeSpan (days, hours, minutes, seconds);
		}
		#endregion

		#region --转换方法--
		/// <summary>
		/// 定义将 <see cref="long"/> 转换为 <see cref="BanSpeakTimeSpan"/>
		/// </summary>
		/// <param name="value">转换为 <see cref="BanSpeakTimeSpan"/> 的值 (单位: 秒)</param>
		public static implicit operator BanSpeakTimeSpan (long value)
		{
			return new BanSpeakTimeSpan (value);
		}
		/// <summary>
		/// 定义将 <see cref="BanSpeakTimeSpan"/> 转换为 <see cref="long"/>
		/// </summary>
		/// <param name="value">转换为 <see cref="long"/> 的结构</param>
		public static implicit operator long (BanSpeakTimeSpan value)
		{
			return (long)value._span.TotalSeconds;
		}
		/// <summary>
		/// 定义将 <see cref="BanSpeakTimeSpan"/> 转换为 <see cref="System.TimeSpan"/>
		/// </summary>
		/// <param name="value">转换为 <see cref="System.TimeSpan"/> 的结构</param>
		public static implicit operator TimeSpan (BanSpeakTimeSpan value)
		{
			return value._span;
		}
		#endregion
	}
}
