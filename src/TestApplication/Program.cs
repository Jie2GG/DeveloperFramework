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
			Console.WriteLine (new BanSpeakTimeSpan (30, 0, 0, 0));
			Console.WriteLine (new BanSpeakTimeSpan (0, 0, 0, 0));
			Console.WriteLine (new BanSpeakTimeSpan (29, 23, 59, 59));
			Console.WriteLine (BanSpeakTimeSpan.MinValue);
			Console.ReadKey ();
		}
	}
}
