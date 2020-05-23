using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.CQP
{
	/// <summary>
	/// 描述 CQP.dll 动态库的回复值
	/// </summary>
	public static class CQPResult
	{
		/// <summary>
		/// 操作成功完成
		/// </summary>
		public const int CQP_SUCCESS = 0;
		/// <summary>
		/// 操作未注册
		/// </summary>
		public const int CQP_PROCESS_NOT_REISTER = -1;
	}
}
