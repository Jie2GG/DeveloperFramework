using DeveloperFramework.Library.CQP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.Simulator.CQP
{
	/// <summary>
	/// 表示 CQP 应用模拟器
	/// </summary>
	public class CQPSimulator
	{
		#region --字段--
		private readonly List<CQPDynamicLibrary> _libraries;
		#endregion

		#region --属性--
		public List<CQPDynamicLibrary> Libraries => this._libraries;
		#endregion

		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="CQPSimulator"/> 类的新实例
		/// </summary>
		/// <param name="appDirectory"></param>
		public CQPSimulator (string appDirectory)
		{
			this._libraries = new List<CQPDynamicLibrary> ();
		}
		#endregion
	}
}
