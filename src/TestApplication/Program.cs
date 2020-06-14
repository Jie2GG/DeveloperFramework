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

using DeveloperFramework.LibraryModel.CQP;
using DeveloperFramework.Log.CQP;
using DeveloperFramework.Utility;

namespace TestApplication
{
	public class Program : DeveloperFramework.Log.CQP.IObservable<LogItem>
	{
		public static void Main (string[] args)
		{
			Logger.Instance.AddObserver (new Program ());
			EnironmentSetup ();
			

			Console.ReadLine ();
		}

		private List<LogItem> items = new List<LogItem> ();

		public void Initialize (IEnumerable<LogItem> list)
		{
			foreach (LogItem item in list)
			{
				OnAdd (item);
			}
		}

		public void OnAdd (LogItem item)
		{
			Console.WriteLine (item.ToString ());
		}

		public void OnRemove (LogItem item)
		{ }

		public void OnReplace (LogItem item)
		{
			Console.WriteLine (item.ToString ());
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
