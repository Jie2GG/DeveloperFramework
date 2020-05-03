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
		[DllExport (ExportName = nameof (CQ_sendPrivateMsg), CallingConvention = CallingConvention.StdCall)]
		public static extern int CQ_sendPrivateMsg (int authCode, long qqId, IntPtr msg)
		{

		}
	}
}
