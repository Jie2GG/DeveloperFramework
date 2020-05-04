using DeveloperFramework.LibraryModel.CQP;
using DeveloperFramework.Utility;
using DeveloperFramework.Win32.LibraryCLR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.Library.CQP
{
	/// <summary>
	/// 提供用于操作 酷Q(C/C++) 动态链接库的操作类
	/// </summary>
	public sealed class CQPDynamicLibrary : DynamicLibrary
	{
		#region --字段--
		private readonly string _jsonPath;
		private readonly CQPAppInfo _appInfo;
		#endregion

		#region --属性--
		/// <summary>
		/// 获取当前加载 Json 的路径
		/// </summary>
		public string JsonPath => this._jsonPath;
		/// <summary>
		/// 获取当前加载 酷Q(C/C++) 动态库的定义信息
		/// </summary>
		public CQPAppInfo AppInfo => this._appInfo;
		#endregion

		#region --委托--
		private delegate string CQ_AppInfo ();
		private delegate int CQ_Initialize (int authCode);
		private delegate int CQ_Startup ();
		private delegate int CQ_Exit ();
		private delegate int CQ_AppEnable ();
		private delegate int CQ_AppDisable ();
		private delegate int CQ_PrivateMessage (int subType, int msgId, long fromQQ, string msg, int font);
		private delegate int CQ_GroupMessage (int subType, int msgId, long fromGroup, long fromQQ, string fromAnonymous, string msg, int font);
		private delegate int CQ_DiscussMessage (int subType, int msgId, long fromDiscuss, long fromQQ, string msg, int font);
		private delegate int CQ_GroupUpload (int subType, int sendTime, long fromGroup, long fromQQ, string file);
		private delegate int CQ_GroupManagerChange (int subType, int sendTime, long fromGroup, long beingOperateQQ);
		private delegate int CQ_GroupMemberDecrease (int subType, int sendTime, long fromGroup, long fromQQ, long beingOperateQQ);
		private delegate int CQ_GroupMemberIncrease (int subType, int sendTime, long fromGroup, long fromQQ, long beingOperateQQ);
		private delegate int CQ_GroupBanSpeak (int subType, int sendTime, long fromGroup, long fromQQ, long beingOperateQQ, long duration);
		private delegate int CQ_FriendAdd (int subType, int sendTime, long fromQQ);
		private delegate int CQ_FriendAddRequest (int subType, int sendTime, long fromQQ, string msg, string responseFlag);
		private delegate int CQ_GroupAddRequest (int subType, int sendTime, long fromGroup, long fromQQ, string msg, string responseFlag);
		private delegate int CQ_MenuCall ();
		private delegate string CQ_StatusCall ();
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
				this._appInfo = JsonConvert.DeserializeObject<CQPAppInfo> (reader.ReadToEnd ());
			}
		}
		#endregion

		#region --公开方法--
		/// <summary>
		/// 调用 <see cref="CQ_AppInfo"/> 方法
		/// </summary>
		/// <returns>返回描述 App 的信息</returns>
		public string InvokeAppInfo ()
		{
			return (string)this.InvokeFunction<CQ_AppInfo> ("AppInfo");
		}
		/// <summary>
		/// 调用 <see cref="CQ_Initialize"/> 方法
		/// </summary>
		/// <param name="authCode">验证码, 随后调用 Api 都要以此码为依据</param>
		/// <returns>操作成功返回 0</returns>
		public int InvokeInitialize (int authCode)
		{
			return (int)this.InvokeFunction<CQ_Initialize> ("Initialize", authCode);
		}
		/// <summary>
		/// 调用 <see cref="CQ_Startup"/> 方法
		/// </summary>
		/// <param name="appEvent">目标事件信息</param>
		/// <returns>操作成功返回 0</returns>
		public int InvokeCQStartup (CQPAppEvent appEvent)
		{
			if (appEvent is null)
			{
				throw new ArgumentNullException (nameof (appEvent));
			}

			return (int)this.InvokeFunction<CQ_Startup> (appEvent.Function);
		}
		/// <summary>
		/// 调用 <see cref="CQ_Exit"/> 方法
		/// </summary>
		/// <param name="appEvent">目标事件信息</param>
		/// <returns>操作成功返回 0</returns>
		public int InvokeCQExit (CQPAppEvent appEvent)
		{
			if (appEvent is null)
			{
				throw new ArgumentNullException (nameof (appEvent));
			}

			return (int)this.InvokeFunction<CQ_Exit> (appEvent.Function);
		}
		/// <summary>
		/// 调用 <see cref="CQ_AppEnable"/> 方法
		/// </summary>
		/// <param name="appEvent">目标事件信息</param>
		/// <returns>操作成功返回 0</returns>
		public int InvokeCQAppEnable (CQPAppEvent appEvent)
		{
			if (appEvent is null)
			{
				throw new ArgumentNullException (nameof (appEvent));
			}

			return (int)this.InvokeFunction<CQ_AppEnable> (appEvent.Function);
		}
		/// <summary>
		/// 调用 <see cref="CQ_AppDisable"/> 方法
		/// </summary>
		/// <param name="appEvent">目标事件信息</param>
		/// <returns>操作成功返回 0</returns>
		public int InvokeCQAppDisable (CQPAppEvent appEvent)
		{
			if (appEvent is null)
			{
				throw new ArgumentNullException (nameof (appEvent));
			}

			return (int)this.InvokeFunction<CQ_AppDisable> (appEvent.Function);
		}
		/// <summary>
		/// 调用 <see cref="CQ_PrivateMessage"/> 方法
		/// </summary>
		/// <param name="appEvent">目标事件信息</param>
		/// <param name="subType">事件类型</param>
		/// <param name="fromQQ">来源QQ</param>
		/// <param name="msg">消息内容</param>
		/// <param name="font">字体指针</param>
		/// <returns>返回函数处理结果</returns>
		public CQPAppEventHandleType InvokeCQPrivateMessage (CQPAppEvent appEvent, CQPAppEventPrivateMessageType subType, QQ fromQQ, Message msg, IntPtr font)
		{
			if (appEvent is null)
			{
				throw new ArgumentNullException (nameof (appEvent));
			}

			if (fromQQ is null)
			{
				throw new ArgumentNullException (nameof (fromQQ));
			}

			if (msg is null)
			{
				throw new ArgumentNullException (nameof (msg));
			}

			return (CQPAppEventHandleType)(int)this.InvokeFunction<CQ_PrivateMessage> (appEvent.Function, (int)subType, msg.Id, fromQQ.Id, msg.Text, font.ToInt32 ());
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
		/// <returns>返回函数处理结果</returns>
		public CQPAppEventHandleType InvokeCQGroupMessage (CQPAppEvent appEvent, CQPAppEventGroupMessageType subType, Group fromGroup, QQ fromQQ, Anonymous fromAnonymous, Message msg, IntPtr font)
		{
			if (appEvent is null)
			{
				throw new ArgumentNullException (nameof (appEvent));
			}

			if (fromGroup is null)
			{
				throw new ArgumentNullException (nameof (fromGroup));
			}

			if (fromQQ is null)
			{
				throw new ArgumentNullException (nameof (fromQQ));
			}

			if (fromAnonymous is null)
			{
				throw new ArgumentNullException (nameof (fromAnonymous));
			}

			if (msg is null)
			{
				throw new ArgumentNullException (nameof (msg));
			}

			return (CQPAppEventHandleType)(int)this.InvokeFunction<CQ_GroupMessage> (appEvent.Function, (int)subType, msg.Id, fromGroup.Id, fromQQ.Id, fromAnonymous.ToBase64String (), msg.Text, font.ToInt32 ());
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
		/// <returns>返回函数处理结果</returns>
		public CQPAppEventHandleType InvokeCQDiscussMessage (CQPAppEvent appEvent, CQPAppEventDiscussMessageType subType, Discuss fromDiscuss, QQ fromQQ, Message msg, IntPtr font)
		{
			if (appEvent is null)
			{
				throw new ArgumentNullException (nameof (appEvent));
			}

			if (fromDiscuss is null)
			{
				throw new ArgumentNullException (nameof (fromDiscuss));
			}

			if (fromQQ is null)
			{
				throw new ArgumentNullException (nameof (fromQQ));
			}

			if (msg is null)
			{
				throw new ArgumentNullException (nameof (msg));
			}

			return (CQPAppEventHandleType)(int)this.InvokeFunction<CQ_DiscussMessage> (appEvent.Function, subType, msg.Id, fromDiscuss.Id, fromQQ.Id, msg.Text, font.ToInt32 ());
		}
		#endregion
	}
}
