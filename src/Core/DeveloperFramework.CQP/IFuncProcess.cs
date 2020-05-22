using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.CQP
{
	/// <summary>
	/// 提供用于处理函数调用过程的接口
	/// </summary>
	public interface IFuncProcess
	{
		/// <summary>
		/// 在派生类中重写时, 处理函数调用过程
		/// </summary>
		/// <param name="objs">传入的欲处理对象</param>
		/// <returns>返回指定的结果</returns>
		object GetProcess (int authCode, [CallerMemberName] string funcName = null, params object[] objs);
	}
}
