using DeveloperFramework.LibraryModel.CQP.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.Simulator.CQP.Domain.Command
{
	public class DeleteMsgCommand : AbstractCommand
	{
		#region --属性--
		public long MsgId { get; }
		#endregion

		#region --属性--
		public DeleteMsgCommand (CQPSimulator simulator, CQPSimulatorApp app, bool isAuth, long msgId)
			: base (simulator, app, isAuth)
		{
			MsgId = msgId;
		} 
		#endregion

		public override object ExecuteHaveAuth ()
		{
			throw new NotImplementedException ();
		}

		public override object ExecuteHaveNoAuth ()
		{
			throw new NotImplementedException ();
		}
	}
}
