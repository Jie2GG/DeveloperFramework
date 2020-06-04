using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.LibraryModel.CQP
{
	/// <summary>
	/// 描述 CQP 应用动态库的群禁言事件类型
	/// </summary>
	public enum GroupBanSpeakType
	{
		/// <summary>
		/// 解除禁言
		/// </summary>
		RemoveBanSpeak = 1,
		/// <summary>
		/// 禁言
		/// </summary>
		BanSpeak = 2
	}
}
