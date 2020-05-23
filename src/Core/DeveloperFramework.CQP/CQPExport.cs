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

			return CQPErrorCode.CQP_PROCESS_NOT_REISTER;
		}

		[CQPAuth (AppAuth = AppAuth.SendGroupMessage)]
		[DllExport (ExportName = nameof (CQ_sendGroupMsg), CallingConvention = CallingConvention.StdCall)]
		public static int CQ_sendGroupMsg (int authCode, long groupId, IntPtr msg)
		{
			if (Instance.FuncProcess != null)
			{
				return (int)Instance.FuncProcess.GetProcess (authCode, nameof (CQ_sendGroupMsg), groupId, msg.PtrToString (_defaultEncoding));
			}

			return CQPErrorCode.CQP_PROCESS_NOT_REISTER;
		}

		[CQPAuth (AppAuth = AppAuth.sendDiscussMessage)]
		[DllExport (ExportName = nameof (CQ_sendDiscussMsg), CallingConvention = CallingConvention.StdCall)]
		public static int CQ_sendDiscussMsg (int authcode, long discussId, IntPtr msg)
		{
			if (Instance.FuncProcess != null)
			{
				return (int)Instance.FuncProcess.GetProcess (authcode, nameof (CQ_sendDiscussMsg), discussId, msg.PtrToString (_defaultEncoding));
			}

			return CQPErrorCode.CQP_PROCESS_NOT_REISTER;
		}

		[CQPAuth (AppAuth = AppAuth.deleteMsg)]
		[DllExport (ExportName = nameof (CQ_deleteMsg), CallingConvention = CallingConvention.StdCall)]
		public static int CQ_deleteMsg (int authCode, long msgId)
		{
			if (Instance.FuncProcess != null)
			{
				return (int)Instance.FuncProcess.GetProcess (authCode, nameof (CQ_deleteMsg), msgId);
			}

			return CQPErrorCode.CQP_PROCESS_NOT_REISTER;
		}

		[CQPAuth (AppAuth = AppAuth.sendLike)]
		[DllExport (ExportName = nameof (CQ_sendLikeV2), CallingConvention = CallingConvention.StdCall)]
		public static int CQ_sendLikeV2 (int authCode, long qqId, int count)
		{
			if (Instance.FuncProcess != null)
			{
				return (int)Instance.FuncProcess.GetProcess (authCode, nameof (CQ_sendLikeV2), qqId, count);
			}

			return CQPErrorCode.CQP_PROCESS_NOT_REISTER;
		}

		[CQPAuth (AppAuth = AppAuth.GetCookiesOrCsrfToken)]
		[DllExport (ExportName = nameof (CQ_getCookiesV2), CallingConvention = CallingConvention.StdCall)]
		public static IntPtr CQ_getCookiesV2 (int authCode, IntPtr domain)
		{
			if (Instance.FuncProcess != null)
			{
				string cookie = (string)Instance.FuncProcess.GetProcess (authCode, nameof (CQ_getCookiesV2), domain.PtrToString (_defaultEncoding));
				GCHandle cookieHandle = cookie.GetGCHandle (_defaultEncoding);
				try
				{
					return cookieHandle.AddrOfPinnedObject ();
				}
				finally
				{
					cookieHandle.Free ();
				}
			}

			return IntPtr.Zero;
		}

		[CQPAuth (AppAuth = AppAuth.GetCookiesOrCsrfToken)]
		[DllExport (ExportName = nameof (CQ_getCsrfToken), CallingConvention = CallingConvention.StdCall)]
		public static int CQ_getCsrfToken (int authCode)
		{
			if (Instance.FuncProcess != null)
			{
				return (int)Instance.FuncProcess.GetProcess (authCode, nameof (CQ_getCsrfToken));
			}

			return CQPErrorCode.CQP_PROCESS_NOT_REISTER;
		}

		[CQPAuth (AppAuth = AppAuth.GetRecord)]
		[DllExport (ExportName = nameof (CQ_getRecordV2), CallingConvention = CallingConvention.StdCall)]
		public static IntPtr CQ_getRecordV2 (int authCode, IntPtr file, IntPtr format)
		{
			if (Instance.FuncProcess != null)
			{
				string record = (string)Instance.FuncProcess.GetProcess (authCode, nameof (CQ_getRecordV2), file.PtrToString (_defaultEncoding), format.PtrToString (_defaultEncoding));
				GCHandle recordHandle = record.GetGCHandle (_defaultEncoding);
				try
				{
					return recordHandle.AddrOfPinnedObject ();
				}
				finally
				{
					recordHandle.Free ();
				}
			}

			return IntPtr.Zero;
		}

		[DllExport (ExportName = nameof (CQ_getAppDirectory), CallingConvention = CallingConvention.StdCall)]
		public static IntPtr CQ_getAppDirectory (int authCode)
		{
			if (Instance.FuncProcess != null)
			{
				string appDir = (string)Instance.FuncProcess.GetProcess (authCode, nameof (CQ_getAppDirectory));
				GCHandle appDirHandle = appDir.GetGCHandle (_defaultEncoding);
				try
				{
					return appDirHandle.AddrOfPinnedObject ();
				}
				finally
				{
					appDirHandle.Free ();
				}
			}

			return IntPtr.Zero;
		}

		[DllExport (ExportName = nameof (CQ_getLoginQQ), CallingConvention = CallingConvention.StdCall)]
		public static long CQ_getLoginQQ (int authCode)
		{
			if (Instance.FuncProcess != null)
			{
				return (long)Instance.FuncProcess.GetProcess (authCode, nameof (CQ_getLoginQQ));
			}

			return CQPErrorCode.CQP_PROCESS_NOT_REISTER;
		}

		[DllExport (ExportName = nameof (CQ_getLoginNick), CallingConvention = CallingConvention.StdCall)]
		public static IntPtr CQ_getLoginNick (int authCode)
		{
			if (Instance.FuncProcess != null)
			{
				string nick = (string)Instance.FuncProcess.GetProcess (authCode, nameof (CQ_getLoginNick));
				GCHandle nickHandle = nick.GetGCHandle (_defaultEncoding);
				try
				{
					return nickHandle.AddrOfPinnedObject ();
				}
				finally
				{
					nickHandle.Free ();
				}
			}

			return IntPtr.Zero;
		}

		[CQPAuth (AppAuth = AppAuth.setGroupKick)]
		[DllExport (ExportName = nameof (CQ_setGroupKick), CallingConvention = CallingConvention.StdCall)]
		public static int CQ_setGroupKick (int authCode, long groupId, long qqId, bool refuses)
		{
			if (Instance.FuncProcess != null)
			{
				return (int)Instance.FuncProcess.GetProcess (authCode, nameof (CQ_setGroupKick), groupId, qqId, refuses);
			}
			return CQPErrorCode.CQP_PROCESS_NOT_REISTER;
		}

		[CQPAuth (AppAuth = AppAuth.setGroupBan)]
		[DllExport (ExportName = nameof (CQ_setGroupBan), CallingConvention = CallingConvention.StdCall)]
		public static int CQ_setGroupBan (int authCode, long groupId, long qqId, long time)
		{
			if (Instance.FuncProcess != null)
			{
				return (int)Instance.FuncProcess.GetProcess (authCode, nameof (CQ_setGroupBan), groupId, qqId, time);
			}
			return CQPErrorCode.CQP_PROCESS_NOT_REISTER;
		}

		[CQPAuth (AppAuth = AppAuth.setGroupAdmin)]
		[DllExport (ExportName = nameof (CQ_setGroupAdmin), CallingConvention = CallingConvention.StdCall)]
		public static int CQ_setGroupAdmin (int authCode, long groupId, long qqId, bool isSet)
		{
			if (Instance.FuncProcess != null)
			{
				return (int)Instance.FuncProcess.GetProcess (authCode, nameof (CQ_setGroupAdmin), groupId, qqId, isSet);
			}
			return CQPErrorCode.CQP_PROCESS_NOT_REISTER;
		}

		[CQPAuth (AppAuth = AppAuth.setGroupSpecialTitle)]
		[DllExport (ExportName = nameof (CQ_setGroupSpecialTitle), CallingConvention = CallingConvention.StdCall)]
		public static int CQ_setGroupSpecialTitle (int authCode, long groupId, long qqId, IntPtr title, long durationTime)
		{
			if (Instance.FuncProcess != null)
			{
				return (int)Instance.FuncProcess.GetProcess (authCode, nameof (CQ_setGroupSpecialTitle), groupId, qqId, title.PtrToString (_defaultEncoding), durationTime);
			}

			return CQPErrorCode.CQP_PROCESS_NOT_REISTER;
		}

		[CQPAuth (AppAuth = AppAuth.setGroupWholeBan)]
		[DllExport (ExportName = nameof (CQ_setGroupWholeBan), CallingConvention = CallingConvention.StdCall)]
		public static int CQ_setGroupWholeBan (int authCode, long groupId, bool isSet)
		{
			if (Instance.FuncProcess != null)
			{
				return (int)Instance.FuncProcess.GetProcess (authCode, nameof (CQ_setGroupWholeBan), groupId, isSet);
			}

			return CQPErrorCode.CQP_PROCESS_NOT_REISTER;
		}

		[CQPAuth (AppAuth = AppAuth.setGroupAnonymousBan)]
		[DllExport (ExportName = nameof (CQ_setGroupAnonymousBan), CallingConvention = CallingConvention.StdCall)]
		public static int CQ_setGroupAnonymousBan (int authCode, long groupId, IntPtr anonymous, long banTime)
		{
			if (Instance.FuncProcess != null)
			{
				return (int)Instance.FuncProcess.GetProcess (authCode, nameof (CQ_setGroupAnonymousBan), groupId, anonymous.PtrToString (_defaultEncoding), banTime);
			}

			return CQPErrorCode.CQP_PROCESS_NOT_REISTER;
		}

		[CQPAuth (AppAuth = AppAuth.setGroupAnonymous)]
		[DllExport (ExportName = nameof (CQ_setGroupAnonymous), CallingConvention = CallingConvention.StdCall)]
		public static int CQ_setGroupAnonymous (int authCode, long groupId, bool isSet)
		{
			if (Instance.FuncProcess != null)
			{
				return (int)Instance.FuncProcess.GetProcess (authCode, nameof (CQ_setGroupAnonymous), groupId, isSet);
			}

			return CQPErrorCode.CQP_PROCESS_NOT_REISTER;
		}

		[CQPAuth (AppAuth = AppAuth.setGroupCard)]
		[DllExport (ExportName = nameof (CQ_setGroupCard), CallingConvention = CallingConvention.StdCall)]
		public static int CQ_setGroupCard (int authCode, long groupId, long qqId, IntPtr newCard)
		{
			if (Instance.FuncProcess != null)
			{
				return (int)Instance.FuncProcess.GetProcess (authCode, nameof (CQ_setGroupCard), qqId, newCard.PtrToString (_defaultEncoding));
			}

			return CQPErrorCode.CQP_PROCESS_NOT_REISTER;
		}

		[CQPAuth (AppAuth = AppAuth.setGroupLeave)]
		[DllExport (ExportName = nameof (CQ_setGroupLeave), CallingConvention = CallingConvention.StdCall)]
		public static int CQ_setGroupLeave (int authCode, long groupId, bool isDisband)
		{
			if (Instance.FuncProcess != null)
			{
				return (int)Instance.FuncProcess.GetProcess (authCode, nameof (CQ_setGroupLeave), groupId, isDisband);
			}

			return CQPErrorCode.CQP_PROCESS_NOT_REISTER;
		}

		[CQPAuth (AppAuth = AppAuth.setDiscussLeave)]
		[DllExport (ExportName = nameof (CQ_setDiscussLeave), CallingConvention = CallingConvention.StdCall)]
		public static extern int CQ_setDiscussLeave (int authCode, long disscussId);

		[CQPAuth (AppAuth = AppAuth.setFriendAddRequest)]
		[DllExport (ExportName = nameof (CQ_setFriendAddRequest), CallingConvention = CallingConvention.StdCall)]
		public static extern int CQ_setFriendAddRequest (int authCode, IntPtr identifying, int requestType, IntPtr appendMsg);

		[CQPAuth (AppAuth = AppAuth.setGroupAddRequest)]
		[DllExport (ExportName = nameof (CQ_setGroupAddRequestV2), CallingConvention = CallingConvention.StdCall)]
		public static extern int CQ_setGroupAddRequestV2 (int authCode, IntPtr identifying, int requestType, int responseType, IntPtr appendMsg);

		[DllExport (ExportName = nameof (CQ_addLog), CallingConvention = CallingConvention.StdCall)]
		public static extern int CQ_addLog (int authCode, int priority, IntPtr type, IntPtr msg);

		[DllExport (ExportName = nameof (CQ_setFatal), CallingConvention = CallingConvention.StdCall)]
		public static extern int CQ_setFatal (int authCode, IntPtr errorMsg);

		[CQPAuth (AppAuth = AppAuth.getGroupMemberInfo)]
		[DllExport (ExportName = nameof (CQ_getGroupMemberInfoV2), CallingConvention = CallingConvention.StdCall)]
		public static extern IntPtr CQ_getGroupMemberInfoV2 (int authCode, long groudId, long qqId, bool isCache);

		[CQPAuth (AppAuth = AppAuth.getGroupMemberList)]
		[DllExport (ExportName = nameof (CQ_getGroupMemberList), CallingConvention = CallingConvention.StdCall)]
		public static extern IntPtr CQ_getGroupMemberList (int authCode, long groupId);

		[CQPAuth (AppAuth = AppAuth.getGroupList)]
		[DllExport (ExportName = nameof (CQ_getGroupList), CallingConvention = CallingConvention.StdCall)]
		public static extern IntPtr CQ_getGroupList (int authCode);

		[CQPAuth (AppAuth = AppAuth.getStrangerInfo)]
		[DllExport (ExportName = nameof (CQ_getStrangerInfo), CallingConvention = CallingConvention.StdCall)]
		public static extern IntPtr CQ_getStrangerInfo (int authCode, long qqId, bool notCache);

		[DllExport (ExportName = nameof (CQ_canSendImage), CallingConvention = CallingConvention.StdCall)]
		public static extern int CQ_canSendImage (int authCode);

		[DllExport (ExportName = nameof (CQ_canSendRecord), CallingConvention = CallingConvention.StdCall)]
		public static extern int CQ_canSendRecord (int authCode);

		[DllExport (ExportName = nameof (CQ_getImage), CallingConvention = CallingConvention.StdCall)]
		public static extern IntPtr CQ_getImage (int authCode, IntPtr file);

		[CQPAuth (AppAuth = AppAuth.getGroupInfo)]
		[DllExport (ExportName = nameof (CQ_getGroupInfo), CallingConvention = CallingConvention.StdCall)]
		public static extern IntPtr CQ_getGroupInfo (int authCode, long groupId, bool notCache);

		[CQPAuth (AppAuth = AppAuth.getFriendList)]
		[DllExport (ExportName = nameof (CQ_getFriendList), CallingConvention = CallingConvention.StdCall)]
		public static extern IntPtr CQ_getFriendList (int authCode, bool reserved);
		#endregion
	}
}
