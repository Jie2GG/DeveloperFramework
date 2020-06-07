using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using DeveloperFramework.CQP;
using DeveloperFramework.LibraryModel.CQP;
using DeveloperFramework.Log.CQP;
using DeveloperFramework.Simulator.CQP;
using DeveloperFramework.Simulator.CQP.Domain;
using DeveloperFramework.Simulator.CQP.Domain.Context;
using DeveloperFramework.SimulatorModel.CQP;
using DeveloperFramework.Utility;

namespace TestApplication
{
	public class Program : ILogObserver
	{
		public static void Main (string[] args)
		{
			Logger.Instance.AddObserver (new Program ());
			EnironmentSetup ();
			CQPSimulator simulator = new CQPSimulator (CQPType.Pro, ApiType.V9);
			simulator.Start ();
			//TaskContext context = new GroupMessageTaskContext (GroupMessageType.Group, simulator.DataPool.GroupCollection[0], simulator.DataPool.GroupCollection[0].MemberCollection[0], null, new Message ("aaa", simulator.DataPool.GroupCollection[0].MemberCollection[0]), IntPtr.Zero);
			//simulator.AddTask (context);
			simulator.GroupMessage (GroupMessageType.Group, 1, 10000, 947295340, "", "", IntPtr.Zero);

			Console.ReadLine ();
			simulator.Stop ();
			Console.ReadLine ();
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

		public static void EnironmentSetup ()
		{
			string output = Path.GetDirectoryName (Assembly.GetExecutingAssembly ().Location);
			DirectoryCopy (NestedFoundDirectory ("env"), output);
		}

		private static string NestedFoundDirectory (string directoryName)
		{
			try
			{
				directoryName = directoryName.Insert (0, "../");
				string path = Path.GetFullPath (directoryName);
				if (Directory.Exists (path))
					return path;
				return NestedFoundDirectory (directoryName);
			}
			catch { throw; }
		}

		private static void DirectoryCopy (string sourceDirName, string destDirName)
		{
			DirectoryInfo dir = new DirectoryInfo (sourceDirName);
			DirectoryInfo[] dirs = dir.GetDirectories ();
			if (!Directory.Exists (destDirName))
			{
				Directory.CreateDirectory (destDirName);
			}
			FileInfo[] files = dir.GetFiles ();
			foreach (FileInfo file in files)
			{
				string temppath = Path.Combine (destDirName, file.Name);
				if (!File.Exists (temppath))
					file.CopyTo (temppath, false);
			}
			foreach (DirectoryInfo subdir in dirs)
			{
				string temppath = Path.Combine (destDirName, subdir.Name);
				DirectoryCopy (subdir.FullName, temppath);
			}
		}
	}
}
