using DeveloperFramework.Extension;
using DeveloperFramework.LibraryModel.CQP;
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
		private static readonly Encoding _defaultEncoding = Encoding.GetEncoding ("GB18030");
		#endregion

		#region --属性--
		/// <summary>
		/// 获取当前 <see cref="CQPExport"/> 类的唯一实例
		/// </summary>
		public static CQPExport Instance => CQPExport._instace;
		/// <summary>
		/// 获取或设置当前实例的函数处理过程
		/// </summary>
		public IFuncProcess FuncProcess { get; set; }
		#endregion

		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="CQPExport"/> 类的新实例
		/// </summary>
		private CQPExport () { }
		#endregion

		#region --导出方法--
		[CQPAuth (AppAuth = AppAuth.sendPrivateMsssage)]
		[DllExport (ExportName = nameof (CQ_sendPrivateMsg), CallingConvention = CallingConvention.StdCall)]
		public static int CQ_sendPrivateMsg (int authCode, long qqId, IntPtr msg)
		{
			if (Instance.FuncProcess != null)
			{
				return (int)Instance.FuncProcess.GetProcess (authCode, nameof (CQ_sendPrivateMsg), qqId, msg.PtrToString (_defaultEncoding));
			}

			return -1;
		}

		[CQPAuth (AppAuth = AppAuth.SendGroupMessage)]
		[DllExport (ExportName = nameof (CQ_sendGroupMsg), CallingConvention = CallingConvention.StdCall)]
		public static int CQ_sendGroupMsg (int authCode, long groupId, IntPtr msg)
		{
			if (Instance.FuncProcess != null)
			{
				return (int)Instance.FuncProcess.GetProcess (authCode, nameof (CQ_sendGroupMsg), groupId, msg.PtrToString (_defaultEncoding));
			}

			return -1;
		}

		[CQPAuth (AppAuth = AppAuth.sendDiscussMessage)]
		[DllExport (ExportName = nameof (CQ_sendDiscussMsg), CallingConvention = CallingConvention.StdCall)]
		public static int CQ_sendDiscussMsg (int authcode, long discussId, IntPtr msg)
		{
			if (Instance.FuncProcess != null)
			{
				return (int)Instance.FuncProcess.GetProcess (authcode, nameof (CQ_sendDiscussMsg), discussId, msg.PtrToString (_defaultEncoding));
			}

			return -1;
		}

		[CQPAuth (AppAuth = AppAuth.deleteMsg)]
		[DllExport (ExportName = nameof (CQ_deleteMsg), CallingConvention = CallingConvention.StdCall)]
		public static int CQ_deleteMsg (int authCode, long msgId)
		{
			if (Instance.FuncProcess != null)
			{
				return (int)Instance.FuncProcess.GetProcess (authCode, nameof (CQ_deleteMsg), msgId);
			}

			return -1;
		}
		#endregion
	}
}
