using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.Library.CQP
{
	/// <summary>
	/// 描述 CQP 应用动态库的返回类型
	/// </summary>
	public enum CQPAppEventHandleType
	{
		/// <summary>
		/// 忽略消息
		/// </summary>
		Discard = 0,
		/// <summary>
		/// 拦截消息
		/// </summary>
		Intercept = 1
	}
}
