using DeveloperFramework.Extension;
using DeveloperFramework.LibraryModel.CQP;
using DeveloperFramework.Utility;
using DeveloperFramework.Win32.LibraryCLR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;

namespace DeveloperFramework.Library.CQP
{
	/// <summary>
	/// 提供用于操作 酷Q(C/C++) 动态链接库的操作类
	/// </summary>
	public sealed class CQPDynamicLibrary : DynamicLibrary
	{
		#region --字段--
		private readonly string _jsonPath;
		private readonly AppInfo _appInfo;
		private static readonly Encoding _defaultEncoding = Encoding.GetEncoding ("GB18030");
		#endregion

		#region --属性--
		/// <summary>
		/// 获取当前加载 Json 的路径
		/// </summary>
		public string JsonPath => this._jsonPath;
		/// <summary>
		/// 获取当前加载 酷Q(C/C++) 动态库的定义信息
		/// </summary>
		public AppInfo AppInfo => this._appInfo;
		/// <summary>
		/// 获取当前加载 酷Q(C/C++) 动态库是否已初始化
		/// </summary>
		public bool IsInitialized { get; private set; } = false;
		/// <summary>
		/// 获取当前加载 酷Q(C/C++) 动态库是否已实行
		/// </summary>
		public bool IsStartup { get; private set; } = false;
		/// <summary>
		/// 获取当前加载 酷Q(C/C++) 动态库的启停状态
		/// </summary>
		public bool IsEnable { get; private set; } = false;
		/// <summary>
		/// 获取当前加载 酷Q(C/C++) 动态库是否已退出
		/// </summary>
		public bool IsExit { get; private set; } = false;
		#endregion

		#region --委托--
		private delegate string CQ_AppInfo ();
		private delegate int CQ_Initialize (int authCode);
		private delegate int CQ_Startup ();
		private delegate int CQ_Exit ();
		private delegate int CQ_AppEnable ();
		private delegate int CQ_AppDisable ();
		private delegate int CQ_PrivateMessage (int subType, int msgId, long fromQQ, IntPtr msg, int font);
		private delegate int CQ_GroupMessage (int subType, int msgId, long fromGroup, long fromQQ, IntPtr fromAnonymous, IntPtr msg, int font);
		private delegate int CQ_DiscussMessage (int subType, int msgId, long fromDiscuss, long fromQQ, IntPtr msg, int font);
		private delegate int CQ_GroupUpload (int subType, int sendTime, long fromGroup, long fromQQ, IntPtr file);
		private delegate int CQ_GroupManagerChange (int subType, int sendTime, long fromGroup, long beingOperateQQ);
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
		/// 初始化 <see cref="CQPDynamicLibrary"/> 类的新实例, 并加载指定的动态链接库 (DLL) 和对应的 Json
		/// </summary>
		/// <param name="libFileName">要加载的动态链接库 (DLL) 的路径</param>
		/// <param name="jsonFileName">要加载的 Json 的路径</param>
		public CQPDynamicLibrary (string libFileName, string jsonFileName)
			: base (libFileName)
		{
			this._jsonPath = OtherUtility.GetAbsolutePath (this.LibraryDirectory, jsonFileName);
			if (!File.Exists (this._jsonPath))
			{
				throw new FileNotFoundException ("未找到指定的 Json 文件", jsonFileName);
			}
			using (StreamReader reader = new StreamReader (this._jsonPath, Encoding.UTF8))
			{
				this._appInfo = JsonSerializer.Deserialize<AppInfo> (reader.ReadToEnd (), 
					new JsonSerializerOptions() {  ReadCommentHandling = JsonCommentHandling.Skip});
			}
		}
		#endregion

		#region --公开方法--
		/// <summary>
		/// 调用 <see cref="CQ_AppInfo"/> 方法
		/// </summary>
		/// <exception cref="ObjectDisposedException">当前对象已经被释放</exception>
		/// <exception cref="MissingMethodException">尝试访问未公开的函数</exception>
		/// <returns>返回描述 App 的信息</returns>
		public string InvokeAppInfo ()
		{
			return this.GetFunction<CQ_AppInfo> ("AppInfo") ();
		}
		/// <summary>
		/// 调用 <see cref="CQ_Initialize"/> 方法
		/// </summary>
		/// <param name="authCode">验证码, 随后调用 Api 都要以此码为依据</param>
		/// <exception cref="ObjectDisposedException">当前对象已经被释放</exception>
		/// <exception cref="MissingMethodException">尝试访问未公开的函数</exception>
		/// <returns>操作成功返回 0</returns>
		public int InvokeInitialize(int authCode)
		{
			int recode = this.GetFunction<CQ_Initialize>("Initialize")(authCode);
			if (recode == 0) this.IsInitialized = true;
			return recode;
		}
		/// <summary>
		/// 调用 <see cref="CQ_Startup"/> 方法
		/// </summary>
		/// <param name="appEvent">目标事件信息</param>
		/// <exception cref="ObjectDisposedException">当前对象已经被释放</exception>
		/// <exception cref="MissingMethodException">尝试访问未公开的函数</exception>
		/// <returns>操作成功返回 0</returns>
		public int InvokeCQStartup (AppEvent appEvent)
		{
            if (!this.IsInitialized)
            {
				throw new MethodAccessException($"未完成初始化，无法实行事件",new EntryPointNotFoundException());
			}
			if (appEvent is null)
			{
				throw new ArgumentNullException (nameof (appEvent));
			}

			if (appEvent.Type != AppEventType.CQStartup)
			{
				throw new ArgumentException ($"函数信息不是 {AppEventType.CQStartup} 类型", nameof (appEvent));
			}
			int recode = this.GetFunction<CQ_Startup>(appEvent.Function)();
			if (recode == 0) this.IsStartup = true;
			return recode;
		}
		/// <summary>
		/// 调用 <see cref="CQ_Exit"/> 方法
		/// </summary>
		/// <param name="appEvent">目标事件信息</param>
		/// <exception cref="ObjectDisposedException">当前对象已经被释放</exception>
		/// <exception cref="MissingMethodException">尝试访问未公开的函数</exception>
		/// <returns>操作成功返回 0</returns>
		public int InvokeCQExit (AppEvent appEvent)
		{
			if (!this.IsStartup)
			{
				throw new MethodAccessException(nameof(appEvent));
			}
			if (appEvent is null)
			{
				throw new ArgumentNullException (nameof (appEvent));
			}

			if (appEvent.Type != AppEventType.CQExit)
			{
				throw new ArgumentException ($"函数信息不是 {AppEventType.CQExit} 类型", nameof (appEvent));
			}
			int recode = this.GetFunction<CQ_Exit>(appEvent.Function)();
			if(recode==0) this.IsExit = true;
			return recode;
		}
		/// <summary>
		/// 调用 <see cref="CQ_AppEnable"/> 方法
		/// </summary>
		/// <param name="appEvent">目标事件信息</param>
		/// <exception cref="ObjectDisposedException">当前对象已经被释放</exception>
		/// <exception cref="MissingMethodException">尝试访问未公开的函数</exception>
		/// <returns>操作成功返回 0</returns>
		public int InvokeCQAppEnable (AppEvent appEvent)
		{
			if (appEvent is null)
			{
				throw new ArgumentNullException (nameof (appEvent));
			}

			if (appEvent.Type != AppEventType.CQAppEnable)
			{
				throw new ArgumentException ($"函数信息不是 {AppEventType.CQAppEnable} 类型", nameof (appEvent));
			}
			int recode = this.GetFunction<CQ_AppEnable>(appEvent.Function)();
			if (recode == 0) this.IsEnable = true;
			return recode;
		}
		/// <summary>
		/// 调用 <see cref="CQ_AppDisable"/> 方法
		/// </summary>
		/// <param name="appEvent">目标事件信息</param>
		/// <exception cref="ObjectDisposedException">当前对象已经被释放</exception>
		/// <exception cref="MissingMethodException">尝试访问未公开的函数</exception>
		/// <returns>操作成功返回 0</returns>
		public int InvokeCQAppDisable (AppEvent appEvent)
		{
			if (appEvent is null)
			{
				throw new ArgumentNullException (nameof (appEvent));
			}

			if (appEvent.Type != AppEventType.CQAppDisable)
			{
				throw new ArgumentException ($"函数信息不是 {AppEventType.CQAppDisable} 类型", nameof (appEvent));
			}
			int recode = this.GetFunction<CQ_AppDisable>(appEvent.Function)();
			if(recode==0) this.IsEnable = false;
			return recode;
		}
		/// <summary>
		/// 调用 <see cref="CQ_PrivateMessage"/> 方法
		/// </summary>
		/// <param name="appEvent">目标事件信息</param>
		/// <param name="subType">事件类型</param>
		/// <param name="fromQQ">来源QQ</param>
		/// <param name="msg">消息内容</param>
		/// <param name="font">字体指针</param>
		/// <exception cref="ObjectDisposedException">当前对象已经被释放</exception>
		/// <exception cref="MissingMethodException">尝试访问未公开的函数</exception>
		/// <returns>返回函数处理结果</returns>
		public int InvokeCQPrivateMessage (AppEvent appEvent, PrivateMessageType subType, int msgId, long fromQQ, string msg, IntPtr font)
		{
			if (appEvent is null)
			{
				throw new ArgumentNullException (nameof (appEvent));
			}

			if (msg is null)
			{
				throw new ArgumentNullException (nameof (msg));
			}

			if (appEvent.Type != AppEventType.PrivateMessage)
			{
				throw new ArgumentException ($"函数信息不是 {AppEventType.PrivateMessage} 类型", nameof (appEvent));
			}

			GCHandle msgHandle = ((string)msg).GetGCHandle (_defaultEncoding);
			try
			{
				return this.GetFunction<CQ_PrivateMessage> (appEvent.Function) ((int)subType, msgId, fromQQ, msgHandle.AddrOfPinnedObject (), font.ToInt32 ());
			}
			finally
			{
				msgHandle.Free ();
			}
		}
		/// <summary>
		/// 调用 <see cref="CQ_GroupMessage"/> 方法
		/// </summary>
		/// <param name="appEvent">目标事件信息</param>
		/// <param name="subType">事件类型</param>
		/// <param name="fromGroup">来源群</param>
		/// <param name="fromQQ">来源QQ</param>
		/// <param name="fromAnonymous">来源匿名</param>
		/// <param name="msg">消息内容</param>
		/// <param name="font">字体指针</param>
		/// <exception cref="ObjectDisposedException">当前对象已经被释放</exception>
		/// <exception cref="MissingMethodException">尝试访问未公开的函数</exception>
		/// <returns>返回函数处理结果</returns>
		public int InvokeCQGroupMessage (AppEvent appEvent, GroupMessageType subType, int msgId, long fromGroup, long fromQQ, string fromAnonymous, string msg, IntPtr font)
		{
			if (appEvent is null)
			{
				throw new ArgumentNullException (nameof (appEvent));
			}

			if (fromAnonymous is null)
			{
				throw new ArgumentNullException (nameof (fromAnonymous));
			}

			if (msg is null)
			{
				throw new ArgumentNullException (nameof (msg));
			}

			if (appEvent.Type != AppEventType.GroupMessage)
			{
				throw new ArgumentException ($"函数信息不是 {AppEventType.GroupMessage} 类型", nameof (appEvent));
			}

			GCHandle anonymousHandle = fromAnonymous.GetGCHandle (_defaultEncoding);
			GCHandle msgHandle = ((string)msg).GetGCHandle (_defaultEncoding);
			try
			{
				return this.GetFunction<CQ_GroupMessage> (appEvent.Function) ((int)subType, msgId, fromGroup, fromQQ, anonymousHandle.AddrOfPinnedObject (), msgHandle.AddrOfPinnedObject (), font.ToInt32 ());
			}
			finally
			{
				anonymousHandle.Free ();
				msgHandle.Free ();
			}
		}
		/// <summary>
		/// 调用 <see cref="CQ_DiscussMessage"/> 方法
		/// </summary>
		/// <param name="appEvent">目标事件信息</param>
		/// <param name="subType">事件类型</param>
		/// <param name="fromDiscuss">来源讨论组</param>
		/// <param name="fromQQ">来源QQ</param>
		/// <param name="msg">消息内容</param>
		/// <param name="font">字体指针</param>
		/// <exception cref="ObjectDisposedException">当前对象已经被释放</exception>
		/// <exception cref="MissingMethodException">尝试访问未公开的函数</exception>
		/// <returns>返回函数处理结果</returns>
		public int InvokeCQDiscussMessage (AppEvent appEvent, DiscussMessageType subType, int msgId, long fromDiscuss, long fromQQ, string msg, IntPtr font)
		{
			if (appEvent is null)
			{
				throw new ArgumentNullException (nameof (appEvent));
			}

			if (msg is null)
			{
				throw new ArgumentNullException (nameof (msg));
			}

			if (appEvent.Type != AppEventType.DiscussMessage)
			{
				throw new ArgumentException ($"函数信息不是 {AppEventType.DiscussMessage} 类型", nameof (appEvent));
			}

			GCHandle msgHandle = ((string)msg).GetGCHandle (_defaultEncoding);
			try
			{
				return this.GetFunction<CQ_DiscussMessage> (appEvent.Function) ((int)subType, msgId, fromDiscuss, fromQQ, msgHandle.AddrOfPinnedObject (), font.ToInt32 ());
			}
			finally
			{
				msgHandle.Free ();
			}
		}
		/// <summary>
		/// 调用 <see cref="CQ_GroupUpload"/> 方法
		/// </summary>
		/// <param name="appEvent">目标事件信息</param>
		/// <param name="subType">事件类型</param>
		/// <param name="sendTime">发送时间</param>
		/// <param name="fromGroup">来源群</param>
		/// <param name="fromQQ">来源QQ</param>
		/// <param name="file">文件信息</param>
		/// <exception cref="ObjectDisposedException">当前对象已经被释放</exception>
		/// <exception cref="MissingMethodException">尝试访问未公开的函数</exception>
		/// <returns>返回函数处理结果</returns>
		public int InvokeCQGroupUpload (AppEvent appEvent, GroupUploadType subType, int sendTime, long fromGroup, long fromQQ, string file)
		{
			if (appEvent is null)
			{
				throw new ArgumentNullException (nameof (appEvent));
			}

			if (file is null)
			{
				throw new ArgumentNullException (nameof (file));
			}

			if (appEvent.Type != AppEventType.GroupFileUpload)
			{
				throw new ArgumentException ($"函数信息不是 {AppEventType.GroupFileUpload} 类型", nameof (appEvent));
			}

			GCHandle fileHandle = file.GetGCHandle (_defaultEncoding);

			try
			{
				return this.GetFunction<CQ_GroupUpload> (appEvent.Function) ((int)subType, sendTime, fromGroup, fromQQ, fileHandle.AddrOfPinnedObject ());
			}
			finally
			{
				fileHandle.Free ();
			}
		}
		/// <summary>
		/// 调用 <see cref="CQ_GroupManagerChange"/> 方法
		/// </summary>
		/// <param name="appEvent">目标事件信息</param>
		/// <param name="subType">事件类型</param>
		/// <param name="sendTime">发送时间</param>
		/// <param name="fromGroup">来源群</param>
		/// <param name="beingOperateQQ">被操作QQ</param>
		/// <exception cref="ObjectDisposedException">当前对象已经被释放</exception>
		/// <exception cref="MissingMethodException">尝试访问未公开的函数</exception>
		/// <returns>返回函数处理结果</returns>
		public int InvokeCQGroupManagerChange (AppEvent appEvent, GroupManagerChangeType subType, int sendTime, long fromGroup, long beingOperateQQ)
		{
			if (appEvent is null)
			{
				throw new ArgumentNullException (nameof (appEvent));
			}

			if (appEvent.Type != AppEventType.GroupManagerChange)
			{
				throw new ArgumentException ($"函数信息不是 {AppEventType.GroupManagerChange} 类型", nameof (appEvent));
			}

			return this.GetFunction<CQ_GroupManagerChange> (appEvent.Function) ((int)subType, sendTime, fromGroup, beingOperateQQ);
		}
		/// <summary>
		/// 调用 <see cref="CQ_GroupMemberDecrease"/> 方法
		/// </summary>
		/// <param name="appEvent">目标事件信息</param>
		/// <param name="subType">事件类型</param>
		/// <param name="sendTime">发送时间</param>
		/// <param name="fromGroup">来源群</param>
		/// <param name="fromQQ">来源QQ</param>
		/// <param name="beingOperateQQ">被操作QQ</param>
		/// <exception cref="ObjectDisposedException">当前对象已经被释放</exception>
		/// <exception cref="MissingMethodException">尝试访问未公开的函数</exception>
		/// <returns>返回函数处理结果</returns>
		public int InvokeCQGroupMemberDecrease (AppEvent appEvent, GroupMemberDecreaseType subType, int sendTime, long fromGroup, long fromQQ, long beingOperateQQ)
		{
			if (appEvent is null)
			{
				throw new ArgumentNullException (nameof (appEvent));
			}

			if (appEvent.Type != AppEventType.GroupMemberDecrease)
			{
				throw new ArgumentException ($"函数信息不是 {AppEventType.GroupMemberDecrease} 类型", nameof (appEvent));
			}

			return this.GetFunction<CQ_GroupMemberDecrease> (appEvent.Function) ((int)subType, sendTime, fromGroup, fromQQ, beingOperateQQ);
		}
		/// <summary>
		/// 调用 <see cref="CQ_GroupMemberIncrease"/> 方法
		/// </summary>
		/// <param name="appEvent">目标事件信息</param>
		/// <param name="subType">事件类型</param>
		/// <param name="sendTime">发送时间</param>
		/// <param name="fromGroup">来源群</param>
		/// <param name="fromQQ">来源QQ</param>
		/// <param name="beingOperateQQ">被操作QQ</param>
		/// <exception cref="ObjectDisposedException">当前对象已经被释放</exception>
		/// <exception cref="MissingMethodException">尝试访问未公开的函数</exception>
		/// <returns>返回函数处理结果</returns>
		public int InvokeCQGroupMemberIncrease (AppEvent appEvent, GroupMemberIncreaseType subType, int sendTime, long fromGroup, long fromQQ, long beingOperateQQ)
		{
			if (appEvent is null)
			{
				throw new ArgumentNullException (nameof (appEvent));
			}

			if (appEvent.Type != AppEventType.GroupMemberIncrease)
			{
				throw new ArgumentException ($"函数信息不是 {AppEventType.GroupMemberIncrease} 类型", nameof (appEvent));
			}

			return this.GetFunction<CQ_GroupMemberIncrease> (appEvent.Function) ((int)subType, sendTime, fromGroup, fromQQ, beingOperateQQ);
		}
		/// <summary>
		/// 调用 <see cref="CQ_GroupBanSpeak"/> 方法
		/// </summary>
		/// <param name="appEvent">目标事件信息</param>
		/// <param name="subType">事件类型</param>
		/// <param name="sendTime">发送时间</param>
		/// <param name="fromGroup">来源群</param>
		/// <param name="fromQQ">来源QQ</param>
		/// <param name="beingOperateQQ">被操作QQ</param>
		/// <param name="duration">禁言时间</param>
		/// <exception cref="ObjectDisposedException">当前对象已经被释放</exception>
		/// <exception cref="MissingMethodException">尝试访问未公开的函数</exception>
		/// <returns>返回函数处理结果</returns>
		public int InvokeCQGroupBanSpeak (AppEvent appEvent, GroupBanSpeakType subType, int sendTime, long fromGroup, long fromQQ, long beingOperateQQ, long duration)
		{
			if (appEvent is null)
			{
				throw new ArgumentNullException (nameof (appEvent));
			}

			if (appEvent.Type != AppEventType.GroupMemberBanSpeak)
			{
				throw new ArgumentException ($"函数信息不是 {AppEventType.GroupMemberBanSpeak} 类型", nameof (appEvent));
			}

			return this.GetFunction<CQ_GroupBanSpeak> (appEvent.Function) ((int)subType, sendTime, fromGroup, fromQQ, beingOperateQQ, duration);
		}
		/// <summary>
		/// 调用 <see cref="CQ_FriendAdd"/> 方法
		/// </summary>
		/// <param name="appEvent">目标事件信息</param>
		/// <param name="subType">事件类型</param>
		/// <param name="sendTime">发送时间</param>
		/// <param name="fromQQ">来源QQ</param>
		/// <exception cref="ObjectDisposedException">当前对象已经被释放</exception>
		/// <exception cref="MissingMethodException">尝试访问未公开的函数</exception>
		/// <returns>返回函数处理结果</returns>
		public int InvokeCQFriendAdd (AppEvent appEvent, FriendAddType subType, int sendTime, long fromQQ)
		{
			if (appEvent is null)
			{
				throw new ArgumentNullException (nameof (appEvent));
			}

			if (appEvent.Type != AppEventType.FriendAdd)
			{
				throw new ArgumentException ($"函数信息不是 {AppEventType.FriendAdd} 类型", nameof (appEvent));
			}

			return this.GetFunction<CQ_FriendAdd> (appEvent.Function) ((int)subType, sendTime, fromQQ);
		}
		/// <summary>
		/// 调用 <see cref="CQ_FriendAddRequest"/> 方法
		/// </summary>
		/// <param name="appEvent">目标事件信息</param>
		/// <param name="subType">事件类型</param>
		/// <param name="sendTime">发送时间</param>
		/// <param name="fromQQ">来源QQ</param>
		/// <param name="appendMsg">附加消息</param>
		/// <param name="responseFlag">反馈标识</param>
		/// <exception cref="ObjectDisposedException">当前对象已经被释放</exception>
		/// <exception cref="MissingMethodException">尝试访问未公开的函数</exception>
		/// <returns>返回函数处理结果</returns>
		public int InvokeCQFriendAddRequest (AppEvent appEvent, FriendAddRequestType subType, int sendTime, long fromQQ, string appendMsg, string responseFlag)
		{
			if (appEvent is null)
			{
				throw new ArgumentNullException (nameof (appEvent));
			}

			if (appendMsg is null)
			{
				throw new ArgumentNullException (nameof (appendMsg));
			}

			if (string.IsNullOrEmpty (responseFlag))
			{
				throw new ArgumentException ("反馈请求标识不能为空", nameof (responseFlag));
			}

			if (appEvent.Type != AppEventType.FriendAddRequest)
			{
				throw new ArgumentException ($"函数信息不是 {AppEventType.FriendAddRequest} 类型", nameof (appEvent));
			}

			GCHandle msgHandle = appendMsg.GetGCHandle (_defaultEncoding);
			GCHandle flagHandle = responseFlag.GetGCHandle (_defaultEncoding);
			try
			{
				return this.GetFunction<CQ_FriendAddRequest> (appEvent.Function) ((int)subType, sendTime, fromQQ, msgHandle.AddrOfPinnedObject (), flagHandle.AddrOfPinnedObject ());
			}
			finally
			{
				msgHandle.Free ();
				flagHandle.Free ();
			}
		}
		/// <summary>
		/// 调用 <see cref="CQ_FriendAddRequest"/> 方法
		/// </summary>
		/// <param name="appEvent">目标事件信息</param>
		/// <param name="subType">事件类型</param>
		/// <param name="sendTime">发送时间</param>
		/// <param name="fromGroup">来源群</param>
		/// <param name="fromQQ">来源QQ</param>
		/// <param name="appendMsg">附加消息</param>
		/// <param name="responseFlag">反馈标识</param>
		/// <exception cref="ObjectDisposedException">当前对象已经被释放</exception>
		/// <exception cref="MissingMethodException">尝试访问未公开的函数</exception>
		/// <returns>返回函数处理结果</returns>
		public int InvokeCQGroupAddRequest (AppEvent appEvent, GroupAddRequestType subType, int sendTime, long fromGroup, long fromQQ, string appendMsg, string responseFlag)
		{
			if (appEvent is null)
			{
				throw new ArgumentNullException (nameof (appEvent));
			}

			if (appendMsg is null)
			{
				throw new ArgumentNullException (nameof (appendMsg));
			}

			if (string.IsNullOrEmpty (responseFlag))
			{
				throw new ArgumentException ("反馈请求标识不能为空", nameof (responseFlag));
			}

			if (appEvent.Type != AppEventType.GroupAddRequest)
			{
				throw new ArgumentException ($"函数信息不是 {AppEventType.GroupAddRequest} 类型", nameof (appEvent));
			}

			GCHandle msgHandle = appendMsg.GetGCHandle (_defaultEncoding);
			GCHandle flagHandle = responseFlag.GetGCHandle (_defaultEncoding);
			try
			{
				return this.GetFunction<CQ_GroupAddRequest> (appEvent.Function) ((int)subType, sendTime, fromGroup, fromQQ, msgHandle.AddrOfPinnedObject (), flagHandle.AddrOfPinnedObject ());
			}
			finally
			{
				msgHandle.Free ();
				flagHandle.Free ();
			}
		}
		/// <summary>
		/// 调用 <see cref="CQ_Menu"/> 方法
		/// </summary>
		/// <param name="appMenu">目标菜单信息</param>
		/// <exception cref="ObjectDisposedException">当前对象已经被释放</exception>
		/// <exception cref="MissingMethodException">尝试访问未公开的函数</exception>
		/// <returns>返回函数处理结果</returns>
		public int InvokeCQMenu (AppMenu appMenu)
		{
			if (appMenu is null)
			{
				throw new ArgumentNullException (nameof (appMenu));
			}

			return this.GetFunction<CQ_Menu> (appMenu.Function) ();
		}
		/// <summary>
		/// 调用 <see cref="CQ_Status"/> 方法
		/// </summary>
		/// <param name="appMenu">目标状态信息</param>
		/// <exception cref="ObjectDisposedException">当前对象已经被释放</exception>
		/// <exception cref="MissingMethodException">尝试访问未公开的函数</exception>
		/// <returns>返回函数处理结果</returns>
		public string InvokeCQStatus (AppStatus appStatus)
		{
			if (appStatus is null)
			{
				throw new ArgumentNullException (nameof (appStatus));
			}

			return this.GetFunction<CQ_Status> (appStatus.Function) ();
		}
		#endregion
	}
}
