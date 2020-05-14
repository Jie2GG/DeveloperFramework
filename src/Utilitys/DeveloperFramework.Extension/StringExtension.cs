using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.Extension
{
	/// <summary>
	/// 表示 <see cref="string"/> 类的扩展方法
	/// </summary>
	public static class StringExtension
	{
		/// <summary>
		/// 获取字符串在非托管内存中实例化的托管对象 <see cref="GCHandle"/>
		/// </summary>
		/// <param name="str">要转换的托管的字符串</param>
		/// <param name="encoding">字符编码</param>
		/// <returns>操作非托管字符串的对象</returns>
		public static GCHandle GetGCHandle (this string str, Encoding encoding)
		{
			if (str is null)
			{
				throw new ArgumentNullException (nameof (str));
			}

			if (encoding is null)
			{
				throw new ArgumentNullException (nameof (encoding));
			}

			byte[] buffer = encoding.GetBytes (str);
			return GCHandle.Alloc (buffer, GCHandleType.Pinned);
		}
	}
}
