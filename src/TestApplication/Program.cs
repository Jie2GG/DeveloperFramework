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
			object value = new DynamicLibrary ("app.dll").InvokeFunction<AppInfo> ("AppInfo");
			//OtherUtility.GetAbsolutePath ("C:\\app.dll", "app.dll");

			Console.ReadKey ();
		}
	}
}
