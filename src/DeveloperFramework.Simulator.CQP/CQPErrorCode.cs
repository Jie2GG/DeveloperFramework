using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.Simulator.CQP
{
	/// <summary>
	/// 表示 CQP 错误代码的类
	/// </summary>
	public static class CQPErrorCode
	{
		public const string TYPE_INIT = "初始化";
		public const string TYPE_APP = "应用";
		public const string TYPE_APP_LOAD_FAIL = "应用加载失败";

		public const int ERROR_APPINFO_NOT_FOUNT = -105;
		public const int ERROR_APPINFO_PARSE_FAIL = -107;
		public const int ERROR_APPINFO_NOT_LOAD = -108;
		public const int ERROR_APPINFO_DIFFERENT = -111;

		public const int ERROR_JSON_READ_FAIL = -110;
		public const int ERROR_AUTHCODE_FAIL = -121;
		public const int ERROR_JSON_PARSE_FAIL = -151;
		public const int ERROR_APPID_IRREGULAR = -154;
		public const int ERROR_API_OLD = 158;
	}
}
