using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using DeveloperFramework.CQP;
using DeveloperFramework.LibraryModel.CQP;
using DeveloperFramework.Log.CQP;
using DeveloperFramework.Simulator.CQP;
using DeveloperFramework.Simulator.CQP.Domain;
using DeveloperFramework.Utility;

namespace TestApplication
{
	public class Program : ILogObserver
	{
		public static void Main (string[] args)
		{
			//LogCenter.Instance.AddObserver (new Program ());

			//CQPSimulator simulator = new CQPSimulator (Path.Combine (AppDomain.CurrentDomain.BaseDirectory, "dev"));
			//simulator.Start ();

			//simulator.Stop ();
			//new Faker<QQ> ("zh_CN")
			//	.RuleFor (p => p.Id, f => id)
			//	.RuleFor (p => p.Nick, f => f.Name.FullName ())
			//	.RuleFor (p => p.Age, f => f.Random.Number (1, 120))
			//	.RuleFor (p => p.Area, f => f.Address.FullAddress ())
			//	.RuleFor (p => p.Sex, f => f.PickRandom<Sex> ());

			Faker faker = new Faker ("zh_CN");

			for (int i = 0; i < 19; i++)
			{
				
			}

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
