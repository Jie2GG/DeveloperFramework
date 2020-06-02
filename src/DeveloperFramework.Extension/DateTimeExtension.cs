using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.Extension
{
	/// <summary>
	/// 表示 <see cref="DateTime"/> 类的扩展方法
	/// </summary>
	public static class DateTimeExtension
	{
		/// <summary>
		/// 将当前时间转化为单位是 "秒" 的时间戳
		/// </summary>
		/// <param name="dateTime">要转换的时间</param>
		/// <returns>单位是 "秒" 的时间戳</returns>
		public static int ToTimeStamp (this DateTime dateTime)
		{
			return Convert.ToInt32 (new TimeSpan (dateTime.ToUniversalTime ().Ticks - 621355968000000000).TotalSeconds);
		}
	}
}
