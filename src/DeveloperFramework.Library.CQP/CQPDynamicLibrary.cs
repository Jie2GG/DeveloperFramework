using DeveloperFramework.Extension;
using DeveloperFramework.LibraryModel.CQP;
using DeveloperFramework.Win32.LibraryCLR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.Library.CQP
{
	/// <summary>
	/// 提供用于操作 酷Q (C/C++) 动态链接库的操作类
	/// </summary>
	public class CQPDynamicLibrary : DynamicLibrary
	{
		#region --字段--
		private static readonly Encoding _defaultEncoding = Encoding.GetEncoding ("GB18030");
		#endregion

		#region --委托--
		private delegate string CQ_AppInfo ();
		private delegate int CQ_Initialize (int authCode);
		private delegate int CQ_Startup ();
		private delegate int CQ_Exit ();
		private delegate int CQ_AppEnable ();
		private delegate int CQ_AppDisable ();
		private delegate int CQ_PrivateMessage (int subType, int msgId, long fromQQ, IntPtr msg, IntPtr font);
		private delegate int CQ_GroupMessage (int subType, int msgId, long fromGroup, long fromQQ, IntPtr fromAnonymous, IntPtr msg, IntPtr font);
		private delegate int CQ_DiscussMessage (int subType, int msgId, long fromDiscuss, long fromQQ, IntPtr msg, IntPtr font);
		private delegate int CQ_GroupUpload (int subType, int sendTime, long fromGroup, long fromQQ, IntPtr file);
		private delegate int CQ_GroupManagerChanged (int subType, int sendTime, long fromGroup, long beingOperateQQ);
		private delegate int CQ_GroupMemberDecrease (int subType, int sendTime, long fromGroup, long fromQQ, long beingOperateQQ);
		private delegate int CQ_GroupMemberIncrease (int subType, int sendTime, long fromGroup, long fromQQ, long beingOperateQQ);
		private delegate int CQ_GroupBanSpeak (int subType, int sendTime, long fromGroup, long fromQQ, long beingOperateQQ, long duration);
		private delegate int CQ_FriendAdd (int subType, int sendTime, long fromQQ);
		private delegate int CQ_FriendAddRequest (int subType, int sendTime, long fromQQ, IntPtr msg, IntPtr responseFlag);
		private delegate int CQ_GroupAddRequest (int subType, int sendTime, long fromGroup, long fromQQ, IntPtr msg, IntPtr responseFlag);
		private delegate int CQ_Menu ();
		private delegate string CQ_Status ();
		#endregion

		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="CQPDynamicLibrary"/> 类的新实例
		/// </summary>
		/// <param name="libFileName">要加载的动态链接库 (DLL) 的路径</param>
		/// <exception cref="BadImageFormatException">试图加载格式不正确的程序</exception>
		/// <exception cref="DllNotFoundException">找不到指定的模块</exception>
		public CQPDynamicLibrary (string libFileName)
			: base (libFileName)
		{
		}
		#endregion

		#region --公开方法--
		/// <summary>
		/// 调用 <see cref="CQ_AppInfo"/> 函数
		/// </summary>
		/// <returns>AppID 和 Api版本的信息</returns>
		/// <exception cref="ObjectDisposedException">当前对象已经被释放</exception>
		/// <exception cref="EntryPointNotFoundException">在 DLL 中找不到名为 funcName 的入口点</exception>
		public string InvokeAppInfo ()
		{
			return this.GetFunction<CQ_AppInfo> ("AppInfo") ();
		}
		/// <summary>
		/// 调用 <see cref="CQ_Initialize"/> 函数
		/// </summary>
		/// <param name="authCode">授权码, 作为此应用调用公开接口的凭据</param>
		/// <returns>授权结果, 成功返回零, 失败返回其它值</returns>
		/// <exception cref="ObjectDisposedException">当前对象已经被释放</exception>
		/// <exception cref="EntryPointNotFoundException">在 DLL 中找不到名为 funcName 的入口点</exception>
		public int InvokeInitialize (int authCode)
		{
			return this.GetFunction<CQ_Initialize> ("Initialize") (authCode);
		}
		/// <summary>
		/// 调用 <see cref="CQ_Startup"/> 函数
		/// </summary>
		/// <param name="funcName">实际函数的名称</param>
		/// <returns>保留值, 暂时不表示任何操作结果</returns>
		/// <exception cref="ObjectDisposedException">当前对象已经被释放</exception>
		/// <exception cref="EntryPointNotFoundException">在 DLL 中找不到名为 funcName 的入口点</exception>
		public int InvokeStartup (string funcName)
		{
			if (string.IsNullOrEmpty (funcName))
			{
				throw new ArgumentException ("调用的目标函数的名称不能为空", nameof (funcName));
			}

			return this.GetFunction<CQ_Startup> (funcName) ();
		}
		/// <summary>
		/// 调用<see cref="CQ_Exit"/> 函数
		/// </summary>
		/// <param name="funcName">实际函数的名称</param>
		/// <returns>保留值, 暂时不表示任何操作结果</returns>
		/// <exception cref="ObjectDisposedException">当前对象已经被释放</exception>
		/// <exception cref="EntryPointNotFoundException">在 DLL 中找不到名为 funcName 的入口点</exception>
		public int InvokeExit (string funcName)
		{
			if (string.IsNullOrEmpty (funcName))
			{
				throw new ArgumentException ("调用的目标函数的名称不能为空", nameof (funcName));
			}

			return this.GetFunction<CQ_Exit> (funcName) ();
		}
		/// <summary>
		/// 调用<see cref="CQ_AppEnable"/> 函数
		/// </summary>
		/// <param name="funcName">实际函数的名称</param>
		/// <returns>保留值, 暂时不表示任何操作结果</returns>
		/// <exception cref="ObjectDisposedException">当前对象已经被释放</exception>
		/// <exception cref="EntryPointNotFoundException">在 DLL 中找不到名为 funcName 的入口点</exception>
		public int InvokeAppEnable (string funcName)
		{
			if (string.IsNullOrEmpty (funcName))
			{
				throw new ArgumentException ("调用的目标函数的名称不能为空", nameof (funcName));
			}

			return this.GetFunction<CQ_AppEnable> (funcName) ();
		}
		/// <summary>
		/// 调用 <see cref="CQ_AppEnable"/> 函数
		/// </summary>
		/// <param name="funcName">实际函数的名称</param>
		/// <returns>保留值, 暂时不表示任何操作结果</returns>
		/// <exception cref="ObjectDisposedException">当前对象已经被释放</exception>
		/// <exception cref="EntryPointNotFoundException">在 DLL 中找不到名为 funcName 的入口点</exception>
		public int InvokeAppDisable (string funcName)
		{
			if (string.IsNullOrEmpty (funcName))
			{
				throw new ArgumentException ("调用的目标函数的名称不能为空", nameof (funcName));
			}

			return this.GetFunction<CQ_AppDisable> (funcName) ();
		}
		/// <summary>
		/// 调用 <see cref="CQ_PrivateMessage"/> 函数
		/// </summary>
		/// <param name="funcName">实际函数的名称</param>
		/// <param name="subType">子类型</param>
		/// <param name="msgId">消息ID</param>
		/// <param name="fromQQ">来源QQ</param>
		/// <param name="msg">消息内容</param>
		/// <param name="font">字体指针</param>
		/// <returns>0 表示该消息继续向下传递, 1 表示该消息被截断</returns>
		/// <exception cref="ObjectDisposedException">当前对象已经被释放</exception>
		/// <exception cref="EntryPointNotFoundException">在 DLL 中找不到名为 funcName 的入口点</exception>
		public int IncokePrivateMessage (string funcName, int subType, int msgId, long fromQQ, string msg, IntPtr font)
		{
			if (string.IsNullOrEmpty (funcName))
			{
				throw new ArgumentException ("调用的目标函数的名称不能为空", nameof (funcName));
			}

			if (msg is null)
			{
				msg = string.Empty;
			}

			GCHandle msgHandle = msg.GetGCHandle (_defaultEncoding);
			try
			{
				return this.GetFunction<CQ_PrivateMessage> (funcName) (subType, msgId, fromQQ, msgHandle.AddrOfPinnedObject (), font);
			}
			finally
			{
				msgHandle.Free ();
			}
		}
		/// <summary>
		/// 调用 <see cref="CQ_GroupMessage"/> 函数
		/// </summary>
		/// <param name="funcName">实际函数的名称</param>
		/// <param name="subType">子类型</param>
		/// <param name="msgId">消息ID</param>
		/// <param name="fromGroup">来源群号</param>
		/// <param name="fromQQ">来源QQ</param>
		/// <param name="fromAnonymous">来源匿名者信息</param>
		/// <param name="msg">消息内容</param>
		/// <param name="font">字体指针</param>
		/// <returns>0 表示该消息继续向下传递, 1 表示该消息被截断</returns>
		/// <exception cref="ObjectDisposedException">当前对象已经被释放</exception>
		/// <exception cref="EntryPointNotFoundException">在 DLL 中找不到名为 funcName 的入口点</exception>
		public int InvokeGroupMessage (string funcName, int subType, int msgId, long fromGroup, long fromQQ, string fromAnonymous, string msg, IntPtr font)
		{
			if (string.IsNullOrEmpty (funcName))
			{
				throw new ArgumentException ("调用的目标函数的名称不能为空", nameof (funcName));
			}

			if (fromAnonymous is null)
			{
				fromAnonymous = string.Empty;
			}

			if (msg is null)
			{
				msg = string.Empty;
			}

			GCHandle anonymousHandle = fromAnonymous.GetGCHandle (_defaultEncoding);
			GCHandle msgHandle = msg.GetGCHandle (_defaultEncoding);
			try
			{
				return this.GetFunction<CQ_GroupMessage> (funcName) (subType, msgId, fromGroup, fromQQ, anonymousHandle.AddrOfPinnedObject (), msgHandle.AddrOfPinnedObject (), font);
			}
			finally
			{
				anonymousHandle.Free ();
				msgHandle.Free ();
			}
		}
		/// <summary>
		/// 调用 <see cref="CQ_DiscussMessage"/> 函数
		/// </summary>
		/// <param name="funcName">实际函数的名称</param>
		/// <param name="subType">子类型</param>
		/// <param name="msgId">消息ID</param>
		/// <param name="fromDiscuss">来源讨论组号</param>
		/// <param name="fromQQ">来源QQ</param>
		/// <param name="msg">消息内容</param>
		/// <param name="font">字体指针</param>
		/// <returns>0 表示该消息继续向下传递, 1 表示该消息被截断</returns>
		/// <exception cref="ObjectDisposedException">当前对象已经被释放</exception>
		/// <exception cref="EntryPointNotFoundException">在 DLL 中找不到名为 funcName 的入口点</exception>
		public int InvokeDiscussMessage (string funcName, int subType, int msgId, long fromDiscuss, long fromQQ, string msg, IntPtr font)
		{
			if (string.IsNullOrEmpty (funcName))
			{
				throw new ArgumentException ("调用的目标函数的名称不能为空", nameof (funcName));
			}

			if (msg is null)
			{
				msg = string.Empty;
			}

			GCHandle msgHandle = msg.GetGCHandle (_defaultEncoding);

			try
			{
				return this.GetFunction<CQ_DiscussMessage> (funcName) (subType, msgId, fromDiscuss, fromQQ, msgHandle.AddrOfPinnedObject (), font);
			}
			finally
			{
				msgHandle.Free ();
			}
		}
		/// <summary>
		/// 调用 <see cref="CQ_GroupUpload"/> 函数
		/// </summary>
		/// <param name="funcName">实际函数的名称</param>
		/// <param name="subType">子类型</param>
		/// <param name="sendTime">发送时间</param>
		/// <param name="fromGroup">来源群号</param>
		/// <param name="fromQQ">来源QQ</param>
		/// <param name="file">文件信息</param>
		/// <returns>0 表示该消息继续向下传递, 1 表示该消息被截断</returns>
		/// <exception cref="ObjectDisposedException">当前对象已经被释放</exception>
		/// <exception cref="EntryPointNotFoundException">在 DLL 中找不到名为 funcName 的入口点</exception>
		public int InvokeGroupUpload (string funcName, int subType, int sendTime, long fromGroup, long fromQQ, string file)
		{
			if (string.IsNullOrEmpty (funcName))
			{
				throw new ArgumentException ("调用的目标函数的名称不能为空", nameof (funcName));
			}

			if (file is null)
			{
				file = string.Empty;
			}

			GCHandle fileHandle = file.GetGCHandle (_defaultEncoding);

			try
			{
				return this.GetFunction<CQ_GroupUpload> (funcName) (subType, sendTime, fromGroup, fromQQ, fileHandle.AddrOfPinnedObject ());
			}
			finally
			{
				fileHandle.Free ();
			}
		}
		/// <summary>
		/// 调用 <see cref="CQ_GroupManagerChanged"/> 函数
		/// </summary>
		/// <param name="funcName">实际函数的名称</param>
		/// <param name="subType">子类型</param>
		/// <param name="sendTime">发送时间</param>
		/// <param name="fromGroup">来源群号</param>
		/// <param name="operatedQQ">被操作QQ</param>
		/// <returns>0 表示该消息继续向下传递, 1 表示该消息被截断</returns>
		/// <exception cref="ObjectDisposedException">当前对象已经被释放</exception>
		/// <exception cref="EntryPointNotFoundException">在 DLL 中找不到名为 funcName 的入口点</exception>
		public int InvokeGroupManageChanged (string funcName, int subType, int sendTime, long fromGroup, long operatedQQ)
		{
			if (string.IsNullOrEmpty (funcName))
			{
				throw new ArgumentException ("调用的目标函数的名称不能为空", nameof (funcName));
			}

			return this.GetFunction<CQ_GroupManagerChanged> (funcName) (subType, sendTime, fromGroup, operatedQQ);
		}
		/// <summary>
		/// 调用 <see cref="CQ_GroupMemberDecrease"/> 函数
		/// </summary>
		/// <param name="funcName">实际函数的名称</param>
		/// <param name="subType">子类型</param>
		/// <param name="sendTime">发送时间</param>
		/// <param name="fromGroup">来源群号</param>
		/// <param name="fromQQ">来源QQ</param>
		/// <param name="operatedQQ">被操作QQ</param>
		/// <returns>0 表示该消息继续向下传递, 1 表示该消息被截断</returns>
		/// <exception cref="ObjectDisposedException">当前对象已经被释放</exception>
		/// <exception cref="EntryPointNotFoundException">在 DLL 中找不到名为 funcName 的入口点</exception>
		public int InvokeGroupMemberDecrease (string funcName, int subType, int sendTime, long fromGroup, long fromQQ, long operatedQQ)
		{
			if (string.IsNullOrEmpty (funcName))
			{
				throw new ArgumentException ("调用的目标函数的名称不能为空", nameof (funcName));
			}

			return this.GetFunction<CQ_GroupMemberDecrease> (funcName) (subType, sendTime, fromGroup, fromQQ, operatedQQ);
		}
		/// <summary>
		/// 调用 <see cref="CQ_GroupMemberIncrease"/> 函数
		/// </summary>
		/// <param name="funcName">实际函数的名称</param>
		/// <param name="subType">子类型</param>
		/// <param name="sendTime">发送时间</param>
		/// <param name="fromGroup">来源群号</param>
		/// <param name="fromQQ">来源QQ</param>
		/// <param name="operatedQQ">被操作QQ</param>
		/// <returns>0 表示该消息继续向下传递, 1 表示该消息被截断</returns>
		/// <exception cref="ObjectDisposedException">当前对象已经被释放</exception>
		/// <exception cref="EntryPointNotFoundException">在 DLL 中找不到名为 funcName 的入口点</exception>
		public int InvokeGroupMemberIncrease (string funcName, int subType, int sendTime, long fromGroup, long fromQQ, long operatedQQ)
		{
			if (string.IsNullOrEmpty (funcName))
			{
				throw new ArgumentException ("调用的目标函数的名称不能为空", nameof (funcName));
			}

			return this.GetFunction<CQ_GroupMemberIncrease> (funcName) (subType, sendTime, fromGroup, fromQQ, operatedQQ);
		}
		/// <summary>
		/// 调用 <see cref="CQ_GroupBanSpeak"/> 函数
		/// </summary>
		/// <param name="funcName">实际函数的名称</param>
		/// <param name="subType">子类型</param>
		/// <param name="sendTime">发送时间</param>
		/// <param name="fromGroup">来源群号</param>
		/// <param name="fromQQ">来源QQ</param>
		/// <param name="operatedQQ">被操作QQ</param>
		/// <param name="duration">时长</param>
		/// <returns>0 表示该消息继续向下传递, 1 表示该消息被截断</returns>
		/// <exception cref="ObjectDisposedException">当前对象已经被释放</exception>
		/// <exception cref="EntryPointNotFoundException">在 DLL 中找不到名为 funcName 的入口点</exception>
		public int InvokeGroupBanSpeak (string funcName, int subType, int sendTime, long fromGroup, long fromQQ, long operatedQQ, long duration)
		{
			if (string.IsNullOrEmpty (funcName))
			{
				throw new ArgumentException ("调用的目标函数的名称不能为空", nameof (funcName));
			}

			return this.GetFunction<CQ_GroupBanSpeak> (funcName) (subType, sendTime, fromGroup, fromQQ, operatedQQ, duration);
		}
		/// <summary>
		/// 调用 <see cref="CQ_FriendAdd"/> 函数
		/// </summary>
		/// <param name="funcName">实际函数的名称</param>
		/// <param name="subType">子类型</param>
		/// <param name="sendTime">发送时间</param>
		/// <param name="fromQQ">来源QQ</param>
		/// <returns>0 表示该消息继续向下传递, 1 表示该消息被截断</returns>
		/// <exception cref="ObjectDisposedException">当前对象已经被释放</exception>
		/// <exception cref="EntryPointNotFoundException">在 DLL 中找不到名为 funcName 的入口点</exception>
		public int InvokeFriendAdd (string funcName, int subType, int sendTime, long fromQQ)
		{
			if (string.IsNullOrEmpty (funcName))
			{
				throw new ArgumentException ("调用的目标函数的名称不能为空", nameof (funcName));
			}

			return this.GetFunction<CQ_FriendAdd> (funcName) (subType, sendTime, fromQQ);
		}
		/// <summary>
		/// 调用 <see cref="CQ_FriendAddRequest"/> 函数
		/// </summary>
		/// <param name="funcName">实际函数的名称</param>
		/// <param name="subType">子类型</param>
		/// <param name="sendTime">发送时间</param>
		/// <param name="fromQQ">来源QQ</param>
		/// <param name="appendMsg">附加消息</param>
		/// <param name="responseFlag">反馈标识</param>
		/// <returns>0 表示该消息继续向下传递, 1 表示该消息被截断</returns>
		/// <exception cref="ObjectDisposedException">当前对象已经被释放</exception>
		/// <exception cref="EntryPointNotFoundException">在 DLL 中找不到名为 funcName 的入口点</exception>
		public int InvokeFriendAddRequest (string funcName, int subType, int sendTime, long fromQQ, string appendMsg, string responseFlag)
		{
			if (string.IsNullOrEmpty (funcName))
			{
				throw new ArgumentException ("调用的目标函数的名称不能为空", nameof (funcName));
			}

			if (responseFlag is null)
			{
				throw new ArgumentNullException (nameof (responseFlag));
			}

			if (appendMsg is null)
			{
				appendMsg = string.Empty;
			}

			GCHandle appendMsgHandle = appendMsg.GetGCHandle (_defaultEncoding);
			GCHandle responseFlagHandle = responseFlag.GetGCHandle (_defaultEncoding);
			try
			{
				return this.GetFunction<CQ_FriendAddRequest> (funcName) (subType, sendTime, fromQQ, appendMsgHandle.AddrOfPinnedObject (), responseFlagHandle.AddrOfPinnedObject ());
			}
			finally
			{
				appendMsgHandle.Free ();
				responseFlagHandle.Free ();
			}
		}
		/// <summary>
		/// 调用 <see cref="CQ_GroupAddRequest"/> 函数
		/// </summary>
		/// <param name="funcName">实际函数的名称</param>
		/// <param name="subType">子类型</param>
		/// <param name="sendTime">发送时间</param>
		/// <param name="fromGroup">来源群号</param>
		/// <param name="fromQQ">来源QQ</param>
		/// <param name="appendMsg">附加消息</param>
		/// <param name="responseFlag">反馈标识</param>
		/// <returns>0 表示该消息继续向下传递, 1 表示该消息被截断</returns>
		/// <exception cref="ObjectDisposedException">当前对象已经被释放</exception>
		/// <exception cref="EntryPointNotFoundException">在 DLL 中找不到名为 funcName 的入口点</exception>
		public int InvokeGroupAddRequest (string funcName, int subType, int sendTime, long fromGroup, long fromQQ, string appendMsg, string responseFlag)
		{
			if (string.IsNullOrEmpty (funcName))
			{
				throw new ArgumentException ("调用的目标函数的名称不能为空", nameof (funcName));
			}

			if (responseFlag is null)
			{
				throw new ArgumentNullException (nameof (responseFlag));
			}

			if (appendMsg is null)
			{
				appendMsg = string.Empty;
			}

			GCHandle appendMsgHandle = appendMsg.GetGCHandle (_defaultEncoding);
			GCHandle responseFlagHandle = responseFlag.GetGCHandle (_defaultEncoding);
			try
			{
				return this.GetFunction<CQ_GroupAddRequest> (funcName) (subType, sendTime, fromGroup, fromQQ, appendMsgHandle.AddrOfPinnedObject (), responseFlagHandle.AddrOfPinnedObject ());
			}
			finally
			{
				appendMsgHandle.Free ();
				responseFlagHandle.Free ();
			}
		}
		/// <summary>
		/// 调用 <see cref="CQ_Menu"/> 函数
		/// </summary>
		/// <param name="funcName">实际函数的名称</param>
		/// <returns>保留值, 暂时不表示任何操作结果</returns>
		/// <exception cref="ObjectDisposedException">当前对象已经被释放</exception>
		/// <exception cref="EntryPointNotFoundException">在 DLL 中找不到名为 funcName 的入口点</exception>
		public int InvokeMenu (string funcName)
		{
			if (string.IsNullOrEmpty (funcName))
			{
				throw new ArgumentException ("调用的目标函数的名称不能为空", nameof (funcName));
			}

			return this.GetFunction<CQ_Menu> (funcName) ();
		}
		/// <summary>
		/// 调用 <see cref="CQ_Menu"/> 函数
		/// </summary>
		/// <param name="funcName">实际函数的名称</param>
		/// <returns>悬浮窗数据</returns>
		/// <exception cref="ObjectDisposedException">当前对象已经被释放</exception>
		/// <exception cref="EntryPointNotFoundException">在 DLL 中找不到名为 funcName 的入口点</exception>
		public string InvokeStatus (string funcName)
		{
			if (string.IsNullOrEmpty (funcName))
			{
				throw new ArgumentException ("调用的目标函数的名称不能为空", nameof (funcName));
			}

			return this.GetFunction<CQ_Status> (funcName) ();
		}
		#endregion
	}
}
