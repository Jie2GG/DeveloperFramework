using DeveloperFramework.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.Utility
{
	/// <summary>
	/// 日期时间实用程序
	/// </summary>
	public static class DateTimeUtility
	{
		/// <summary>
		/// 获取当前时间戳
		/// </summary>
		/// <returns>返回当前时间戳</returns>
		public static int GetTimeStamp ()
		{
			return DateTime.Now.ToTimeStamp ();
		}
		/// <summary>
		/// 获取两个时间的间隔
		/// </summary>
		/// <param name="dt1">间隔时间的起点</param>
		/// <param name="dt2">间隔时间的终点</param>
		/// <returns>返回两个时间的差值</returns>
		public static TimeSpan GetDateTimeInterval (DateTime dt1, DateTime dt2)
		{
			long stamp1 = dt1.ToTimeStamp ();
			long stamp2 = dt2.ToTimeStamp ();

			if (stamp1 > stamp2)
			{
				return dt1 - dt2;
			}
			else if (stamp2 > stamp1)
			{
				return dt2 - dt1;
			}

			return TimeSpan.Zero;
		}
	}
}
