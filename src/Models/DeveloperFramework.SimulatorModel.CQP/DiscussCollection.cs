using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.SimulatorModel.CQP
{
	public class DiscussCollection : Collection<Discuss>
	{
		public DiscussCollection ()
		{
		}

		public DiscussCollection (IList<Discuss> list) 
			: base (list)
		{
		}
	}
}
