using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.CQP
{
	/// <summary>
	/// 定义用于提供函数处理过程的接口
	/// </summary>
	public interface IFuncProcess
	{
		/// <summary>
		/// 在派生类中重写时, 处理函数过程
		/// </summary>
		/// <param name="authCode">授权码</param>
		/// <param name="funcName">函数名称</param>
		/// <param name="args">参数列表</param>
		/// <returns>返回指定的结果</returns>
		dynamic FuncProcess (int authCode, string funcName, params object[] args);
	}
}
