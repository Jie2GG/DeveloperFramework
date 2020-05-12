using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.Extension
{
	/// <summary>
	/// 表示 <see cref="IntPtr"/> 类的扩展方法
	/// </summary>
	public static class IntPtrExtension
	{
		/// <summary>
		/// 将当前指针实例转换为指定编码的字符串
		/// </summary>
		/// <param name="ptr">转换为字符串的指针</param>
		/// <param name="encoding">转换的目标编码</param>
		/// <returns>指定编码的字符串</returns>
		public static string PtrToString (this IntPtr ptr, Encoding encoding)
		{
			if (ptr.ToInt64() == 0)
			{
				throw new ArgumentException ("源指针指向的地址为 0", nameof (ptr));
			}

			if (encoding is null)
			{
				throw new ArgumentNullException (nameof (encoding));
			}

			int len = Kernel32.LstrlenA (ptr);   //获取指针中数据的长度
			if (len == 0)
			{
				return string.Empty;
			}
			byte[] buffer = new byte[len];
			Marshal.Copy (ptr, buffer, 0, len);
			return encoding.GetString (buffer);
		}
	}
}
