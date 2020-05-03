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
		private readonly IntPtr _hModule;
		private readonly string _libraryPath;
		private readonly string _libraryDirectory;
		private bool _isDispose;
		#endregion

		#region --属性--
		/// <summary>
		/// 获取当前加载动态链接库 (DLL) 的路径
		/// </summary>
		public string LibraryPath => this._libraryPath;
		/// <summary>
		/// 获取当前加载动态链接库 (DLL) 的目录
		/// </summary>
		public string LibraryDirectory => this._libraryDirectory;
		/// <summary>
		/// 获取当前实例 <see cref="DynamicLibrary"/> 执行过程中的结果代码
		/// </summary>
		public int ResultCode => Kernel32.GetLastError ();
		/// <summary>
		/// 获取当前实例 <see cref="DynamicLibrary"/> 执行过程中的详细信息
		/// </summary>
		public string ResultMessage
		{
			get
			{
				int errorCode = this.ResultCode;
				IntPtr temp = IntPtr.Zero;
				string msg = null;
				Kernel32.FormatMessageA (0x1300, ref temp, errorCode, 0, ref msg, 255, ref temp);
				return msg;
			}
		}
		#endregion

		#region --构造函数--
		static DynamicLibrary ()
		{
			DynamicLibrary._baseDirectories = new List<string> ();
			DynamicLibrary._baseDirectories.Add (AppDomain.CurrentDomain.BaseDirectory);
			DynamicLibrary._baseDirectories.AddRange (Environment.GetEnvironmentVariable ("Path").Split (';'));
		}
		/// <summary>
		/// 初始化 <see cref="DynamicLibrary"/> 类的新实例, 并加载指定的动态链接库 (DLL)
		/// </summary>
		/// <param name="libFileName">要加载的动态链接库 (DLL) 的路径</param>
		public DynamicLibrary (string libFileName)
		{
			foreach (string item in DynamicLibrary._baseDirectories)
			{
				string fullPath = OtherUtility.GetAbsolutePath (item, libFileName);
				if (File.Exists (fullPath))
				{
					// 初始化属性
					this._isDispose = false;
					this._libraryPath = fullPath;
					this._libraryDirectory = Path.GetDirectoryName (fullPath);

					// 加载动态库
					this._hModule = Kernel32.LoadLibraryA (fullPath);
					if (this._hModule.ToInt64 () == 0)
					{
						Dispose ();
						throw new FileLoadException ($"试图加载格式不正确的程序集 {fullPath}");
					}
					return;
				}
			}

			throw new FileNotFoundException ("无法找到指定的程序集", libFileName);
		}
		#endregion

		#region --公开方法--
		/// <summary>
		/// 调用当前 <see cref="DynamicLibrary"/> 实例指定名称的函数
		/// </summary>
		/// <typeparam name="TDelegate">与函数指针对应的委托类型, 该类型不允许是泛型</typeparam>
		/// <param name="funcName">寻找的函数入口名称</param>
		/// <param name="args">传入库函数的参数列表</param>
		/// <exception cref="ObjectDisposedException">当前对象已释放</exception>
		/// <exception cref="MissingMethodException">尝试访问不存在的公开函数</exception>
		/// <exception cref="InvalidCastException">无法将指定的函数指针转换为 funcType</exception>
		/// <exception cref="MemberAccessException">调用方没有访问 （例如，如果该方法是私有的），委托所表示的方法。 - 或 - 数量、 顺序或中列出的参数类型 args 无效</exception>
		/// <exception cref="ArgumentException">委托所表示的方法被调用一个或多个不支持它的类</exception>
		/// <exception cref="TargetInvocationException">委托所表示的方法是实例方法，目标对象是 <see langword="null"/>。 - 或 - 一个封装的方法引发的异常</exception>
		/// <returns>返回动态库函数的返回值</returns>
		public object InvokeFunction<TDelegate> (string funcName, params object[] args)
			where TDelegate : Delegate
		{

			return this.InvokeFunction (typeof (TDelegate), funcName, args);
		}
		/// <summary>
		/// 调用当前 <see cref="DynamicLibrary"/> 实例指定名称的函数
		/// </summary>
		/// <param name="funcType">符合方法的委托类型</param>
		/// <param name="funcName">寻找的函数入口名称</param>
		/// <param name="args">传入库函数的参数列表</param>
		/// <exception cref="ObjectDisposedException">当前对象已释放</exception>
		/// <exception cref="MissingMethodException">尝试访问不存在的公开函数</exception>
		/// <exception cref="InvalidCastException">无法将指定的函数指针转换为 funcType</exception>
		/// <exception cref="MemberAccessException">调用方没有访问 （例如，如果该方法是私有的），委托所表示的方法。 - 或 - 数量、 顺序或中列出的参数类型 args 无效</exception>
		/// <exception cref="ArgumentException">委托所表示的方法被调用一个或多个不支持它的类</exception>
		/// <exception cref="TargetInvocationException">委托所表示的方法是实例方法，目标对象是 <see langword="null"/>。 - 或 - 一个封装的方法引发的异常</exception>
		/// <returns>返回动态库函数的返回值</returns>
		public object InvokeFunction (Type funcType, string funcName, params object[] args)
		{
			if (this._isDispose)
			{
				throw new ObjectDisposedException (nameof (DynamicLibrary));
			}

			IntPtr funPtr = Kernel32.GetProcAddress (this._hModule, funcName);
			if (funPtr.ToInt64 () == 0)
			{
				throw new MissingMethodException ($"尝试访问不存在的公开函数 { funcName }");
			}
			Delegate callback = Marshal.GetDelegateForFunctionPointer (funPtr, funcType);
			if (callback == null)
			{
				throw new InvalidCastException ($"无法将公开函数 {funcName} 转换为委托");
			}

			return callback.DynamicInvoke (args);
		}
		/// <summary>
		/// 执行与释放或重置非托管资源关联的应用程序定义的任务
		/// </summary>
		public void Dispose ()
		{
			if (!this._isDispose)
			{
				this._isDispose = true;

				// 释放指针
				Kernel32.FreeLibrary (this._hModule);   // 加载失败, 释放指针
			}
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
			return $"{Path.GetFileName (this.LibraryPath)} -> {this._hModule}";
		}
		#endregion
	}
}
