using DeveloperFramework.LibraryModel.CQP;
using DeveloperFramework.SimulatorModel.CQP;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.Simulator.CQP.Domain.Context
{
	public class GroupMessageTaskContext : TaskContext
	{
		#region --常量--
		public const string KeySubType = "subType";
		public const string KeyFromGroup = "fromGroup";
		public const string KeyFromQQ = "fromQQ";
		public const string KeyFromAnonymous = "fromAnonymous";
		public const string KeyMessage = "msg";
		public const string KeyFont = "font";
		#endregion

		#region --属性--
		public GroupMessageType SubType => base.GetValue (KeySubType);
		public Group FromGroup => base.GetValue (KeyFromGroup);
		public QQ FromQQ => base.GetValue (KeyFromQQ);
		public GroupAnonymous FromAnonymous => base.GetValue (KeyFromAnonymous);
		public Message Message => base.GetValue (KeyMessage);
		public IntPtr Font => base.GetValue (KeyFont);
		#endregion

		#region --构造函数--
		public GroupMessageTaskContext (GroupMessageType subType, Group fromGroup, QQ fromQQ, GroupAnonymous fromAnonymous, Message msg, IntPtr font)
			: base (2)
		{
			base.SetValue (KeySubType, subType);
			base.SetValue (KeyFromGroup, fromGroup);
			base.SetValue (KeyFromQQ, fromQQ);
			base.SetValue (KeyFromAnonymous, fromAnonymous);
			base.SetValue (KeyMessage, msg);
			base.SetValue (KeyFont, font);
		}
		#endregion
	}
}
