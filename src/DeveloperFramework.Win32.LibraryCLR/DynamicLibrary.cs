using DeveloperFramework.Utility;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.Win32.LibraryCLR
{
	/// <summary>
	/// 提供用于操作 (C/C++) 动态链接库的操作类
	/// </summary>
	public class DynamicLibrary : IEquatable<DynamicLibrary>, IDisposable
	{
		#region --字段--
		private static readonly List<string> _baseDirectories;
		private IntPtr _hModule;
		private readonly string _libraryName;
		private readonly string _libraryPath;
		private readonly string _libraryDirectory;
		private bool _disposedValue;
		#endregion

		#region --属性--
		/// <summary>
		/// 获取当前加载的动态链接库 (DLL) 的文件名称
		/// </summary>
		public string LibraryName => this._libraryName;
		/// <summary>
		/// 获取当前加载的动态链接库 (DLL) 的路径
		/// </summary>
		public string LibraryPath => this._libraryPath;
		/// <summary>
		/// 获取当前加载的动态链接库 (DLL) 的目录
		/// </summary>
		public string LibraryDirectory => this._libraryDirectory;
		#endregion

		#region --构造函数--
		static DynamicLibrary ()
		{
			DynamicLibrary._baseDirectories = new List<string>
			{
				AppDomain.CurrentDomain.BaseDirectory
			};
			DynamicLibrary._baseDirectories.AddRange (Environment.GetEnvironmentVariable ("Path").Split (';'));
		}
		/// <summary>
		/// 初始化 <see cref="DynamicLibrary"/> 类的新实例, 并加载指定的动态链接库 (DLL)
		/// </summary>
		/// <param name="libFileName">要加载的动态链接库 (DLL) 的路径</param>
		/// <exception cref="BadImageFormatException">试图加载格式不正确的程序</exception>
		/// <exception cref="DllNotFoundException">找不到指定的模块</exception>
		public DynamicLibrary (string libFileName)
		{
			foreach (string item in DynamicLibrary._baseDirectories)
			{
				string fullPath = OtherUtility.GetAbsolutePath (item, libFileName);
				if (File.Exists (fullPath))
				{
					// 初始化属性
					this._disposedValue = false;
					this._libraryPath = fullPath;
					this._libraryName = Path.GetFileName (fullPath);
					this._libraryDirectory = Path.GetDirectoryName (fullPath);

					// 加载动态库
					this._hModule = Kernel32.LoadLibraryA (fullPath);
					if (this._hModule.ToInt64 () == 0)
					{
						throw new BadImageFormatException ("试图加载格式不正确的程序", libFileName);
					}
					return;
				}
			}

			throw new DllNotFoundException ($"无法加载 DLL \"{libFileName}\": 找不到指定的模块");
		}
		/// <summary>
		/// 释放 <see cref="DynamicLibrary"/> 类所使用的资源
		/// </summary>
		~DynamicLibrary ()
		{
			// 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
			Dispose (disposing: false);
		}
		#endregion

		#region --公开方法--
		/// <summary>
		/// 获取当前实例 <see cref="DynamicLibrary"/> 执行过程中的结果代码
		/// </summary>
		/// <returns>返回从操作系统底层获取的结果代码</returns>
		public int GetResultCode ()
		{
			if (this._disposedValue)
			{
				throw new ObjectDisposedException (nameof (DynamicLibrary));
			}

			return Kernel32.GetLastError ();
		}
		/// <summary>
		/// 获取当前实例 <see cref="DynamicLibrary"/> 执行过程中的详细信息
		/// </summary>
		/// <returns>返回从操作系统底层获取的详细执行信息</returns>
		public string GetResultMessage ()
		{
			if (this._disposedValue)
			{
				throw new ObjectDisposedException (nameof (DynamicLibrary));
			}

			int errorCode = this.GetResultCode ();
			IntPtr temp = IntPtr.Zero;
			string msg = null;
			Kernel32.FormatMessageA (0x1300, ref temp, errorCode, 0, ref msg, 255, ref temp);
			return msg;
		}
		/// <summary>
		/// 获取当前实例 <see cref="DynamicLibrary"/> 指定的函数指针, 并转换为 <see cref="Delegate"/>
		/// </summary>
		/// <typeparam name="TDelegate">要返回的委托的类型</typeparam>
		/// <param name="funcName">函数名称</param>
		/// <returns>指定委托类型的实例</returns>
		/// <exception cref="ObjectDisposedException">当前对象已经被释放</exception>
		/// <exception cref="EntryPointNotFoundException">在 DLL 中找不到名为 funcName 的入口点</exception>
		/// <exception cref="ArgumentException">TDelegate 不是委托，或它是一个开放式泛型类型</exception>
		public TDelegate GetFunction<TDelegate> (string funcName)
			where TDelegate : Delegate
		{
			if (this._disposedValue)
			{
				throw new ObjectDisposedException (nameof (DynamicLibrary));
			}

			IntPtr funPtr = Kernel32.GetProcAddress (this._hModule, funcName);
			if (funPtr.ToInt64 () == 0)
			{
				throw new EntryPointNotFoundException ($"在 DLL \"{this.LibraryName}\" 中找不到名为 \"{funcName}\" 的入口点");
			}

			return Marshal.GetDelegateForFunctionPointer<TDelegate> (funPtr);
		}
		/// <summary>
		/// 执行与释放或重置非托管资源关联的应用程序定义的任务
		/// </summary>
		public void Dispose ()
		{
			// 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
			Dispose (disposing: true);
			GC.SuppressFinalize (this);
		}
		/// <summary>
		/// 返回一个值，该值指示此实例是否等于指定的对象
		/// </summary>
		/// <param name="other">要与此示例比较的对象，或 <see langword="null"/></param>
		/// <returns>如果 <see langword="true"/> 是 other 的实例并且等于此实例的值，则为 <see cref="DynamicLibrary"/>；否则为 <see langword="false"/></returns>
		public bool Equals (DynamicLibrary other)
		{
			if (other is null)
			{
				return false;
			}

			return this._hModule.ToInt64 () == other._hModule.ToInt64 ();
		}
		/// <summary>
		/// 返回一个值，该值指示此实例是否等于指定的对象
		/// </summary>
		/// <param name="obj">要与此示例比较的对象，或 <see langword="null"/></param>
		/// <returns>如果 <see langword="true"/> 是 obj 的实例并且等于此实例的值，则为 <see cref="DynamicLibrary"/>；否则为 <see langword="false"/></returns>
		public override bool Equals (object obj)
		{
			return this.Equals (obj as DynamicLibrary);
		}
		/// <summary>
		/// 返回此实例的哈希代码
		/// </summary>
		/// <returns>位有符号整数哈希代码</returns>
		public override int GetHashCode ()
		{
			return _hModule.GetHashCode ();
		}
		/// <summary>
		/// 将当前 <see cref="DynamicLibrary"/> 对象的数值转换为其等效字符串表示形式
		/// </summary>
		/// <returns>当前 <see cref="DynamicLibrary"/> 对象的值的字符串表示形式</returns>
		public override string ToString ()
		{
			return $"DLL: {this.LibraryName} -> ({this._hModule})";
		}
		#endregion

		#region --私有方法--
		/// <summary>
		/// 执行与释放或重置非托管资源关联的应用程序定义的详细任务
		/// </summary>
		/// <param name="disposing">是否释放托管状态</param>
		protected virtual void Dispose (bool disposing)
		{
			if (!_disposedValue)
			{
				if (disposing)
				{ }
				if (this._hModule.ToInt64 () != 0)
				{
					Kernel32.FreeLibrary (this._hModule);
				}

				this._hModule = IntPtr.Zero;
				_disposedValue = true;
			}
		}
		#endregion
	}
}
