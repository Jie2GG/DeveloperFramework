using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.LibraryModel.CQP
{
	/// <summary>
	/// 描述 CQP 应用动态库的群管理员改变事件类型
	/// </summary>
	public enum GroupManagerChangeType
	{
		/// <summary>
		/// 取消管理员
		/// </summary>
		CancelManager = 1,
		/// <summary>
		/// 设置管理员
		/// </summary>
		SetingManager = 2
	}
}
