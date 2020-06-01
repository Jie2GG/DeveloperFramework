using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.Simulator.CQP.Domain.Command
{
	public class GetRecordV2Command : AbstractCommand
	{
		public string File { get; }
		public string Format { get; }

		public GetRecordV2Command (CQPSimulator simulator, CQPSimulatorApp app, bool isAuth)
			: base (simulator, app, isAuth)
		{
		}

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
