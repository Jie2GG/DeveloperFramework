using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.CQP
{
	/// <summary>
	/// 表示 CQP.dll 动态库的导出类
	/// </summary>
	public class CQPExport
	{
		#region --字段--
		private static readonly CQPExport _instace = new Lazy<CQPExport> (() => new CQPExport ()).Value;
		#endregion

		#region --属性--
		/// <summary>
		/// 获取当前 <see cref="CQPExport"/> 类的唯一实例
		/// </summary>
		public static CQPExport Instance => CQPExport._instace;
		#endregion

		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="CQPExport"/> 类的新实例
		/// </summary>
		private CQPExport () { }
		#endregion

		#region --导出方法--
		[DllExport (ExportName = nameof (CQ_sendPrivateMsg), CallingConvention = CallingConvention.StdCall)]
		private static int CQ_sendPrivateMsg (int authCode, long qqId, IntPtr msg)
		{
			return 0;
		}
		#endregion
	}
}
