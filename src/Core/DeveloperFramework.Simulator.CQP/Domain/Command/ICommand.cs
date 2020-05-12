using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.Simulator.CQP.Domain.Command
{
	/// <summary>
	/// 表示接收到外部命令的处理接口
	/// </summary>
	public interface ICommand
	{
		/// <summary>
		/// 当在派生类中重写时, 处理命令的详细过程
		/// </summary>
		/// <returns>处理结果值</returns>
		object Execute ();
	}
}
