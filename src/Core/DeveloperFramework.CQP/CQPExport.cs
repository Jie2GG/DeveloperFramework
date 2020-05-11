using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.CQP
{
	internal static class CQPExport
	{
		#region --事件--
		public static EventHandler<>
		#endregion

		[DllExport (ExportName = nameof (CQ_sendPrivateMsg), CallingConvention = CallingConvention.StdCall)]
		private static extern int CQ_sendPrivateMsg (int authCode, long qqId, IntPtr msg)
		{

		}
	}
}
