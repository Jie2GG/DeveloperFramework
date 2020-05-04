using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using DeveloperFramework.Library.CQP;
using DeveloperFramework.LibraryModel.CQP;
using DeveloperFramework.Utility;
using DeveloperFramework.Win32.LibraryCLR;

namespace TestApplication
{
	class Program
	{
		public delegate string AppInfo ();

		private static void Main (string[] args)
		{
			for (int i = 0; i < 10; i++)
			{
				Console.WriteLine (new Message (i.ToString ()).Id);
			}
			Console.ReadKey ();
		}
	}
}
