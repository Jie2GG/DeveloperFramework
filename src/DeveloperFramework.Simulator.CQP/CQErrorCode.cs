using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.Simulator.CQP
{
	/// <summary>
	/// 表示CQ错误代码的枚举
	/// </summary>
	public enum CQErrorCode
	{
		/// <summary>
		/// LoadLibrary失败
		/// </summary>
		LoadLibraryError = -103,
		/// <summary>
		/// AppInfo 函数不存在
		/// </summary>
		AppInfoNotFound = -105,
		/// <summary>
		/// AppInfo 返回信息错误
		/// </summary>
		AppInfoResultError = -107,
		/// <summary>
		/// AppInfo 返回 Api 版本不支持
		/// </summary>
		AppInfoResultApiNoSupport = -108,
		/// <summary>
		/// 应用 Json 读取失败
		/// </summary>
		JsonLoadError = -110,
		/// <summary>
		/// AppInfo 返回的 AppID 错误
		/// </summary>
		AppInfoResultAppIDError = -111,
		/// <summary>
		/// 授权函数不存在
		/// </summary>
		AuthFuncNotFound = -115,
		/// <summary>
		/// 授权函数返回值是非0
		/// </summary>
		AuthFuncReturnNoZero = -121,
		/// <summary>
		/// 应用 Json 解析失败
		/// </summary>
		JsonParseError = -151,
		/// <summary>
		/// AppID不符合规范
		/// </summary>
		AppIDMismatch = -154,
		/// <summary>
		/// Api版本太旧
		/// </summary>
		ApiVersionOld = -158
	}
}
