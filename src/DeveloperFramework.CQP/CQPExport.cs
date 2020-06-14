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
	public static class CQPExport
	{
		#region --字段--
		private static readonly Encoding _defaultEncoding = Encoding.GetEncoding ("GB18030");
		#endregion

		#region --属性--
		/// <summary>
		/// 获取或设置动态库导出类的处理函数
		/// </summary>
		public static IFuncProcess FuncProcess { get; set; }
		#endregion

		#region --公开方法--
		[CQPAuth (AppAuth.SendPrivateMsssage)]
		[DllExport (ExportName = nameof (CQ_sendPrivateMsg), CallingConvention = CallingConvention.StdCall)]
		public static int CQ_sendPrivateMsg (int authCode, long qqId, IntPtr msg)
		{
			if (FuncProcess != null)
			{
				return FuncProcess.FuncProcess (authCode, nameof (CQ_sendPrivateMsg), qqId, msg.PtrToString (_defaultEncoding));
			}

			return -1;
		}

		[CQPAuth (AppAuth.SendGroupMessage)]
		[DllExport (ExportName = nameof (CQ_sendGroupMsg), CallingConvention = CallingConvention.StdCall)]
		public static int CQ_sendGroupMsg (int authCode, long groupId, IntPtr msg)
		{
			if (FuncProcess != null)
			{
				return FuncProcess.FuncProcess (authCode, nameof (CQ_sendGroupMsg), groupId, msg.PtrToString (_defaultEncoding));
			}

			return -1;
		}

		[CQPAuth (AppAuth.SendDiscussMessage)]
		[DllExport (ExportName = nameof (CQ_sendDiscussMsg), CallingConvention = CallingConvention.StdCall)]
		public static int CQ_sendDiscussMsg (int authcode, long discussId, IntPtr msg)
		{
			if (FuncProcess != null)
			{
				return FuncProcess.FuncProcess (authcode, nameof (CQ_sendDiscussMsg), discussId, msg.PtrToString (_defaultEncoding));
			}

			return -1;
		}

		[CQPAuth (AppAuth.DeleteMsg)]
		[DllExport (ExportName = nameof (CQ_deleteMsg), CallingConvention = CallingConvention.StdCall)]
		public static int CQ_deleteMsg (int authCode, long msgId)
		{
			if (FuncProcess != null)
			{
				return FuncProcess.FuncProcess (authCode, nameof (CQ_deleteMsg), msgId);
			}

			return -1;
		}

		[CQPAuth (AppAuth.SendLike)]
		[DllExport (ExportName = nameof (CQ_sendLikeV2), CallingConvention = CallingConvention.StdCall)]
		public static int CQ_sendLikeV2 (int authCode, long qqId, int count)
		{
			if (FuncProcess != null)
			{
				return FuncProcess.FuncProcess (authCode, nameof (CQ_sendLikeV2), qqId, count);
			}

			return -1;
		}

		[CQPAuth (AppAuth.GetCookiesOrCsrfToken)]
		[DllExport (ExportName = nameof (CQ_getCookiesV2), CallingConvention = CallingConvention.StdCall)]
		public static IntPtr CQ_getCookiesV2 (int authCode, IntPtr domain)
		{
			if (FuncProcess != null)
			{
				string cookie = (string)FuncProcess.FuncProcess (authCode, nameof (CQ_getCookiesV2), domain.PtrToString (_defaultEncoding));
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

		[CQPAuth (AppAuth.GetCookiesOrCsrfToken)]
		[DllExport (ExportName = nameof (CQ_getCsrfToken), CallingConvention = CallingConvention.StdCall)]
		public static int CQ_getCsrfToken (int authCode)
		{
			if (FuncProcess != null)
			{
				return FuncProcess.FuncProcess (authCode, nameof (CQ_getCsrfToken));
			}

			return -1;
		}

		[CQPAuth (AppAuth.GetRecord)]
		[DllExport (ExportName = nameof (CQ_getRecordV2), CallingConvention = CallingConvention.StdCall)]
		public static IntPtr CQ_getRecordV2 (int authCode, IntPtr file, IntPtr format)
		{
			if (FuncProcess != null)
			{
				string record = (string)FuncProcess.FuncProcess (authCode, nameof (CQ_getRecordV2), file.PtrToString (_defaultEncoding), format.PtrToString (_defaultEncoding));
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
			if (FuncProcess != null)
			{
				string appDir = (string)FuncProcess.FuncProcess (authCode, nameof (CQ_getAppDirectory));
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
			if (FuncProcess != null)
			{
				return (long)FuncProcess.FuncProcess (authCode, nameof (CQ_getLoginQQ));
			}

			return -1;
		}

		[DllExport (ExportName = nameof (CQ_getLoginNick), CallingConvention = CallingConvention.StdCall)]
		public static IntPtr CQ_getLoginNick (int authCode)
		{
			if (FuncProcess != null)
			{
				string nick = (string)FuncProcess.FuncProcess (authCode, nameof (CQ_getLoginNick));
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

		[CQPAuth (AppAuth.SetGroupKick)]
		[DllExport (ExportName = nameof (CQ_setGroupKick), CallingConvention = CallingConvention.StdCall)]
		public static int CQ_setGroupKick (int authCode, long groupId, long qqId, bool refuses)
		{
			if (FuncProcess != null)
			{
				return FuncProcess.FuncProcess (authCode, nameof (CQ_setGroupKick), groupId, qqId, refuses);
			}
			return -1;
		}

		[CQPAuth (AppAuth.SetGroupBan)]
		[DllExport (ExportName = nameof (CQ_setGroupBan), CallingConvention = CallingConvention.StdCall)]
		public static int CQ_setGroupBan (int authCode, long groupId, long qqId, long time)
		{
			if (FuncProcess != null)
			{
				return FuncProcess.FuncProcess (authCode, nameof (CQ_setGroupBan), groupId, qqId, time);
			}
			return -1;
		}

		[CQPAuth (AppAuth.SetGroupAdmin)]
		[DllExport (ExportName = nameof (CQ_setGroupAdmin), CallingConvention = CallingConvention.StdCall)]
		public static int CQ_setGroupAdmin (int authCode, long groupId, long qqId, bool isSet)
		{
			if (FuncProcess != null)
			{
				return FuncProcess.FuncProcess (authCode, nameof (CQ_setGroupAdmin), groupId, qqId, isSet);
			}
			return -1;
		}

		[CQPAuth (AppAuth.SetGroupSpecialTitle)]
		[DllExport (ExportName = nameof (CQ_setGroupSpecialTitle), CallingConvention = CallingConvention.StdCall)]
		public static int CQ_setGroupSpecialTitle (int authCode, long groupId, long qqId, IntPtr title, long durationTime)
		{
			if (FuncProcess != null)
			{
				return FuncProcess.FuncProcess (authCode, nameof (CQ_setGroupSpecialTitle), groupId, qqId, title.PtrToString (_defaultEncoding), durationTime);
			}

			return -1;
		}

		[CQPAuth (AppAuth.SetGroupWholeBan)]
		[DllExport (ExportName = nameof (CQ_setGroupWholeBan), CallingConvention = CallingConvention.StdCall)]
		public static int CQ_setGroupWholeBan (int authCode, long groupId, bool isSet)
		{
			if (FuncProcess != null)
			{
				return FuncProcess.FuncProcess (authCode, nameof (CQ_setGroupWholeBan), groupId, isSet);
			}

			return -1;
		}

		[CQPAuth (AppAuth.SetGroupAnonymousBan)]
		[DllExport (ExportName = nameof (CQ_setGroupAnonymousBan), CallingConvention = CallingConvention.StdCall)]
		public static int CQ_setGroupAnonymousBan (int authCode, long groupId, IntPtr anonymous, long banTime)
		{
			if (FuncProcess != null)
			{
				return FuncProcess.FuncProcess (authCode, nameof (CQ_setGroupAnonymousBan), groupId, anonymous.PtrToString (_defaultEncoding), banTime);
			}

			return -1;
		}

		[CQPAuth (AppAuth.SetGroupAnonymous)]
		[DllExport (ExportName = nameof (CQ_setGroupAnonymous), CallingConvention = CallingConvention.StdCall)]
		public static int CQ_setGroupAnonymous (int authCode, long groupId, bool isSet)
		{
			if (FuncProcess != null)
			{
				return FuncProcess.FuncProcess (authCode, nameof (CQ_setGroupAnonymous), groupId, isSet);
			}

			return -1;
		}

		[CQPAuth (AppAuth.SetGroupCard)]
		[DllExport (ExportName = nameof (CQ_setGroupCard), CallingConvention = CallingConvention.StdCall)]
		public static int CQ_setGroupCard (int authCode, long groupId, long qqId, IntPtr newCard)
		{
			if (FuncProcess != null)
			{
				return FuncProcess.FuncProcess (authCode, nameof (CQ_setGroupCard), qqId, newCard.PtrToString (_defaultEncoding));
			}

			return -1;
		}

		[CQPAuth (AppAuth.SetGroupLeave)]
		[DllExport (ExportName = nameof (CQ_setGroupLeave), CallingConvention = CallingConvention.StdCall)]
		public static int CQ_setGroupLeave (int authCode, long groupId, bool isDisband)
		{
			if (FuncProcess != null)
			{
				return FuncProcess.FuncProcess (authCode, nameof (CQ_setGroupLeave), groupId, isDisband);
			}

			return -1;
		}

		[CQPAuth (AppAuth.SetDiscussLeave)]
		[DllExport (ExportName = nameof (CQ_setDiscussLeave), CallingConvention = CallingConvention.StdCall)]
		public static int CQ_setDiscussLeave (int authCode, long discussId)
		{
			if (FuncProcess != null)
			{
				return FuncProcess.FuncProcess (authCode, nameof (CQ_setDiscussLeave), discussId);
			}

			return -1;
		}

		[CQPAuth (AppAuth.SetFriendAddRequest)]
		[DllExport (ExportName = nameof (CQ_setFriendAddRequest), CallingConvention = CallingConvention.StdCall)]
		public static int CQ_setFriendAddRequest (int authCode, IntPtr identifying, int requestType, IntPtr appendMsg)
		{
			if (FuncProcess != null)
			{
				return FuncProcess.FuncProcess (authCode, nameof (CQ_setFriendAddRequest), identifying.PtrToString (_defaultEncoding), requestType, appendMsg.PtrToString (_defaultEncoding));
			}

			return -1;
		}

		[CQPAuth (AppAuth.SetGroupAddRequest)]
		[DllExport (ExportName = nameof (CQ_setGroupAddRequestV2), CallingConvention = CallingConvention.StdCall)]
		public static int CQ_setGroupAddRequestV2 (int authCode, IntPtr identifying, int requestType, int responseType, IntPtr appendMsg)
		{
			if (FuncProcess != null)
			{
				return FuncProcess.FuncProcess (authCode, nameof (CQ_setGroupAddRequestV2), identifying.PtrToString (_defaultEncoding), requestType, responseType, appendMsg.PtrToString (_defaultEncoding));
			}

			return -1;
		}

		[DllExport (ExportName = nameof (CQ_addLog), CallingConvention = CallingConvention.StdCall)]
		public static int CQ_addLog (int authCode, int priority, IntPtr type, IntPtr msg)
		{
			if (FuncProcess != null)
			{
				return FuncProcess.FuncProcess (authCode, nameof (CQ_addLog), priority, type.PtrToString (_defaultEncoding), msg.PtrToString (_defaultEncoding));
			}

			return -1;
		}

		[DllExport (ExportName = nameof (CQ_setFatal), CallingConvention = CallingConvention.StdCall)]
		public static int CQ_setFatal (int authCode, IntPtr errorMsg)
		{
			if (FuncProcess != null)
			{
				return FuncProcess.FuncProcess (authCode, nameof (CQ_setFatal), errorMsg.PtrToString (_defaultEncoding));
			}

			return -1;
		}

		[CQPAuth (AppAuth.GetGroupMemberInfo)]
		[DllExport (ExportName = nameof (CQ_getGroupMemberInfoV2), CallingConvention = CallingConvention.StdCall)]
		public static IntPtr CQ_getGroupMemberInfoV2 (int authCode, long groupId, long qqId, bool isCache)
		{
			if (FuncProcess != null)
			{
				string data = (string)FuncProcess.FuncProcess (authCode, nameof (CQ_getGroupMemberInfoV2), groupId, qqId, isCache);

				GCHandle dataHandle = data.GetGCHandle (_defaultEncoding);
				try
				{
					return dataHandle.AddrOfPinnedObject ();
				}
				finally
				{
					dataHandle.Free ();
				}
			}

			return IntPtr.Zero;
		}

		[CQPAuth (AppAuth.GetGroupMemberList)]
		[DllExport (ExportName = nameof (CQ_getGroupMemberList), CallingConvention = CallingConvention.StdCall)]
		public static IntPtr CQ_getGroupMemberList (int authCode, long groupId)
		{
			if (FuncProcess != null)
			{
				string data = (string)FuncProcess.FuncProcess (authCode, nameof (CQ_getGroupMemberList), groupId);
				GCHandle dataHandle = data.GetGCHandle (_defaultEncoding);
				try
				{
					return dataHandle.AddrOfPinnedObject ();
				}
				finally
				{
					dataHandle.Free ();
				}
			}

			return IntPtr.Zero;
		}

		[CQPAuth (AppAuth.GetGroupList)]
		[DllExport (ExportName = nameof (CQ_getGroupList), CallingConvention = CallingConvention.StdCall)]
		public static IntPtr CQ_getGroupList (int authCode)
		{
			if (FuncProcess != null)
			{
				string data = (string)FuncProcess.FuncProcess (authCode, nameof (CQ_getGroupList));

				GCHandle dataHandle = data.GetGCHandle (_defaultEncoding);
				try
				{
					return dataHandle.AddrOfPinnedObject ();
				}
				finally
				{
					dataHandle.Free ();
				}
			}

			return IntPtr.Zero;
		}

		[CQPAuth (AppAuth.GetStrangerInfo)]
		[DllExport (ExportName = nameof (CQ_getStrangerInfo), CallingConvention = CallingConvention.StdCall)]
		public static IntPtr CQ_getStrangerInfo (int authCode, long qqId, bool notCache)
		{
			if (FuncProcess != null)
			{
				string data = (string)FuncProcess.FuncProcess (authCode, nameof (CQ_getStrangerInfo), qqId, notCache);
				GCHandle dataHandle = data.GetGCHandle (_defaultEncoding);
				try
				{
					return dataHandle.AddrOfPinnedObject ();
				}
				finally
				{
					dataHandle.Free ();
				}
			}

			return IntPtr.Zero;
		}

		[DllExport (ExportName = nameof (CQ_canSendImage), CallingConvention = CallingConvention.StdCall)]
		public static int CQ_canSendImage (int authCode)
		{
			if (FuncProcess != null)
			{
				return FuncProcess.FuncProcess (authCode, nameof (CQ_canSendImage));
			}

			return -1;
		}

		[DllExport (ExportName = nameof (CQ_canSendRecord), CallingConvention = CallingConvention.StdCall)]
		public static int CQ_canSendRecord (int authCode)
		{
			if (FuncProcess != null)
			{
				return FuncProcess.FuncProcess (authCode, nameof (CQ_canSendRecord));
			}

			return -1;
		}

		[DllExport (ExportName = nameof (CQ_getImage), CallingConvention = CallingConvention.StdCall)]
		public static IntPtr CQ_getImage (int authCode, IntPtr file)
		{
			if (FuncProcess != null)
			{
				string data = (string)FuncProcess.FuncProcess (authCode, nameof (CQ_getImage), file.PtrToString (_defaultEncoding));

				GCHandle dataHandle = data.GetGCHandle (_defaultEncoding);
				try
				{
					return dataHandle.AddrOfPinnedObject ();
				}
				finally
				{
					dataHandle.Free ();
				}
			}

			return IntPtr.Zero;
		}

		[CQPAuth (AppAuth.GetGroupInfo)]
		[DllExport (ExportName = nameof (CQ_getGroupInfo), CallingConvention = CallingConvention.StdCall)]
		public static IntPtr CQ_getGroupInfo (int authCode, long groupId, bool notCache)
		{
			if (FuncProcess != null)
			{
				string data = (string)FuncProcess.FuncProcess (authCode, nameof (CQ_getGroupInfo), groupId, notCache);

				GCHandle dataHandle = data.GetGCHandle (_defaultEncoding);
				try
				{
					return dataHandle.AddrOfPinnedObject ();
				}
				finally
				{
					dataHandle.Free ();
				}
			}

			return IntPtr.Zero;
		}

		[CQPAuth (AppAuth.GetFriendList)]
		[DllExport (ExportName = nameof (CQ_getFriendList), CallingConvention = CallingConvention.StdCall)]
		public static IntPtr CQ_getFriendList (int authCode, bool reserved)
		{
			if (FuncProcess != null)
			{
				string data = (string)FuncProcess.FuncProcess (authCode, nameof (CQ_getFriendList), reserved);

				GCHandle dataHandle = data.GetGCHandle (_defaultEncoding);
				try
				{
					return dataHandle.AddrOfPinnedObject ();
				}
				finally
				{
					dataHandle.Free ();
				}
			}

			return IntPtr.Zero;
		}
		#endregion
	}
}
