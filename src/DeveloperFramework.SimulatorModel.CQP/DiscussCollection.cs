using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.SimulatorModel.CQP
{
	/// <summary>
	/// 描述讨论组列表的类
	/// </summary>
	public class DiscussCollection : Collection<Discuss>, IToBase64
	{
		#region --构造函数--
		/// <summary>
		/// 初始化为空的 <see cref="DiscussCollection"/> 类的新实例
		/// </summary>
		public DiscussCollection ()
			: base ()
		{
		}
		#endregion

		#region --公开方法--
		/// <summary>
		/// 返回当前实例的 Base64 字符串
		/// </summary>
		/// <returns>当前实例的 Base64 字符串</returns>
		public string ToBase64 ()
		{
			return string.Empty;
		}
		#endregion
	}
}
