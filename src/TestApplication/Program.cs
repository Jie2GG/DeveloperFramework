using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using DeveloperFramework.Log.CQP;
using DeveloperFramework.Simulator.CQP;

namespace TestApplication
{
	class Program : ILogObserver
	{
		private static void Main (string[] args)
		{
			LogCenter.Instance.AddObserver (new Program ());

			CQPSimulator simulator = new CQPSimulator (Path.Combine (AppDomain.CurrentDomain.BaseDirectory, "dev"));
			simulator.Start ();

			Console.ReadKey ();
		}

		public void Initialize (ICollection<LogItem> logs)
		{

		}

		public void ReceiveLog (LogItem log)
		{
			Console.WriteLine (log);
		}
	}
}
