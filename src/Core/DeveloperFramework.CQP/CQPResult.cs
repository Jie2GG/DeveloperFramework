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
		/// <summary>
		/// 应用未经授权
		/// </summary>
		public const int CQP_APP_NOT_AUTHORIZE = -10;
		/// <summary>
		/// 发送未找到关系
		/// </summary>
		public const int CQP_SEND_NOT_RELATION = -20;
		/// <summary>
		/// 未找到消息
		/// </summary>
		public const int CQP_MSG_NOT_FOUND = -30;
		/// <summary>
		/// 无法撤回, 权限不足
		/// </summary>
		public const int CQP_MSG_NOT_AUTHORIZE = -31;

	}
}
