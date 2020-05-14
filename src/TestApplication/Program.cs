﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using DeveloperFramework.CQP;
using DeveloperFramework.LibraryModel.CQP.Dynamic;
using DeveloperFramework.Log.CQP;
using DeveloperFramework.Simulator.CQP;
using DeveloperFramework.Simulator.CQP.Domain;

namespace TestApplication
{
	public class Program : ILogObserver
	{
		public static void Main (string[] args)
		{
			LogCenter.Instance.AddObserver (new Program ());

			CQPSimulator simulator = new CQPSimulator (Path.Combine (AppDomain.CurrentDomain.BaseDirectory, "dev"));
			simulator.Start ();

			simulator.Stop ();
			Console.Read ();
		}

		public void Initialize (ICollection<LogItem> logs)
		{
			foreach (var item in logs)
			{
				ReceiveLog (item);
			}
		}

		public void ReceiveLog (LogItem log)
		{
			Console.WriteLine (log);
		}
	}
}
