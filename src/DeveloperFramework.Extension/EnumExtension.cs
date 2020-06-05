using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.Extension
{
	/// <summary>
	/// 表示 <see cref="Enum"/> 或其子枚举的扩展方法
	/// </summary>
	public static class EnumExtension
	{
		/// <summary>
		/// 获取当前枚举值的 <see cref="DescriptionAttribute"/> 特性的值
		/// </summary>
		/// <param name="value">要获取其特性的枚举</param>
		/// <returns>标记的特性枚举值, 若当前特性不存在则返回 <see cref="string.Empty"/></returns>
		public static string GetDescription (this Enum value)
		{
			if (value == null)
			{
				return string.Empty;
			}

			FieldInfo fieldInfo = value.GetType ().GetField (value.ToString ());
			DescriptionAttribute attribute = fieldInfo.GetCustomAttribute<DescriptionAttribute> (false);
			return attribute.Description;
		}
	}
}
