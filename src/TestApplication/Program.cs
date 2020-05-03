using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using DeveloperFramework.Utility;
using DeveloperFramework.Win32.LibraryCLR;

namespace TestApplication
{
	class Program
	{
		public delegate string AppInfo ();

		static void Main (string[] args)
		{
			var lib= new DynamicLibrary ("app.dll");
			lib.InvokeFunction<AppInfo> ("AppInfo");
			//OtherUtility.GetAbsolutePath ("C:\\app.dll", "app.dll");
			Console.WriteLine (lib.ResultMessage);
			Console.ReadKey ();
		}
	}
}
