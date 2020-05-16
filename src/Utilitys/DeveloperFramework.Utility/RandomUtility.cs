using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.Utility
{
	/// <summary>
	/// 伪随机数使用工具
	/// </summary>
	public static class RandomUtility
	{
		#region --字段--
		private static readonly Faker _faker = new Faker ("zh_CN");

		/// <summary>
		/// 获取指定范围内的随机整数值
		/// </summary>
		/// <param name="minValue">最小值</param>
		/// <param name="maxValue">最大值</param>
		/// <returns>范围内随机值</returns>
		public static int RandomInt32 (int minValue, int maxValue)
		{
			return _faker.Random.Int (minValue, maxValue);
		}
		/// <summary>
		/// 获取随机年龄
		/// </summary>
		/// <returns>随机年龄</returns>
		public static int RandomAge ()
		{
			return RandomInt32 (0, 150);
		}
		/// <summary>
		/// 获取随机姓名
		/// </summary>
		/// <returns>随机姓名</returns>
		public static string RandomName ()
		{
			return _faker.Name.FullName ().Replace (" ", string.Empty);
		}
		/// <summary>
		/// 获取随机单词
		/// </summary>
		/// <returns>随机单词</returns>
		public static string RandomWord ()
		{
			return _faker.Lorem.Word ();
		}
		/// <summary>
		/// 获取随机完整的地址
		/// </summary>
		/// <returns>随机地址</returns>
		public static string RandomArea ()
		{
			return _faker.Address.FullAddress ();
		}
		/// <summary>
		/// 获取随机的指定枚举
		/// </summary>
		/// <typeparam name="T">获取结果的枚举类型</typeparam>
		/// <returns>随机的枚举值</returns>
		public static T RandomEnum<T> ()
			where T : Enum
		{
			return _faker.PickRandom<T> ();
		}
		/// <summary>
		/// 获取随机时间
		/// </summary>
		/// <param name="yearToBack">从当前时间往前提早具体的年份作为最小值</param>
		/// <returns>随机时间</returns>
		public static DateTime RandomPastDateTime (int yearToBack)
		{
			return _faker.Date.Past (yearToBack);
		}
		/// <summary>
		/// 获取随机时间
		/// </summary>
		/// <param name="yearToForward">从起始时间往后推迟具体的年份作为最大值</param>
		/// <param name="startTime">起始时间, 默认为 <see cref="DateTime.Now"/></param>
		/// <returns>随机时间</returns>
		public static DateTime RandomFutureDateTime (int yearToForward, DateTime? startTime = null)
		{
			return _faker.Date.Future (yearToForward, startTime);
		}
		/// <summary>
		/// 获取数组中的随机一个元素
		/// </summary>
		/// <typeparam name="T">要获取数组的元素类型</typeparam>
		/// <param name="array">随机抽取的源数组</param>
		/// <returns>数组中的某一个元素</returns>
		public static T RandomElement<T> (T[] array)
		{
			return _faker.Random.ArrayElement<T> (array);
		}
		/// <summary>
		/// 获取指定类型的深度定义随机对象
		/// </summary>
		/// <typeparam name="T">需要随机的对象类型</typeparam>
		/// <returns>返回自定义随机对象</returns>
		public static Faker<T> GetFaker<T> ()
			where T : class
		{
			return new Faker<T> ("zh_CN");
		}
		#endregion

	}
}
