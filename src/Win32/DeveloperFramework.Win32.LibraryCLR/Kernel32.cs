using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.Win32.LibraryCLR
{
	internal class Kernel32
	{
		#region --常量--
		internal const string LibraryName = "Kernel32.dll";
		#endregion

		#region --函数定义--
		[DllImport (LibraryName, EntryPoint = nameof (LoadLibraryA), CallingConvention = CallingConvention.StdCall)]
		internal static extern IntPtr LoadLibraryA (string lpLibFileName);

		[DllImport (LibraryName, EntryPoint = nameof (FreeLibrary), CallingConvention = CallingConvention.StdCall)]
		internal static extern int FreeLibrary (IntPtr hModule);

		[DllImport (LibraryName, EntryPoint = nameof (FreeLibraryAndExitThread), CallingConvention = CallingConvention.StdCall)]
		internal static extern int FreeLibraryAndExitThread (IntPtr hLibModule, int dwExitCode);

		[DllImport (LibraryName, EntryPoint = nameof (GetProcAddress), CallingConvention = CallingConvention.StdCall)]
		internal static extern IntPtr GetProcAddress (IntPtr hModule, string lpProcName);

		[DllImport (LibraryName, EntryPoint = nameof (GetLastError), CallingConvention = CallingConvention.StdCall)]
		internal static extern int GetLastError ();

		[DllImport (LibraryName, EntryPoint = nameof (FormatMessageA), CallingConvention = CallingConvention.StdCall)]
		internal static extern int FormatMessageA (int dwFlags, ref IntPtr lpSource, int dwMessageId, int dwLanageId, ref string lpBuffer, int nSize, ref IntPtr args);
		#endregion
	}
}
