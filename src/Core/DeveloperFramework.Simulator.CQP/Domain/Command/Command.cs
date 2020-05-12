using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.Simulator.CQP.Domain.Command
{
	/// <summary>
	/// 表示接收到命令的处理类型, 该类是抽象的
	/// </summary>
	public abstract class Command : ICommand
	{
		#region --属性--
		/// <summary>
		/// 获取当前实例的调用者 <see cref="CQPSimulatorApp"/> 实例
		/// </summary>
		public CQPSimulatorApp App { get; }
		#endregion

		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="Command"/> 类的新实例
		/// </summary>
		/// <param name="app">相关联的应用 <see cref="CQPSimulatorApp"/></param>
		public Command (CQPSimulatorApp app)
		{
			this.App = app ?? throw new ArgumentNullException (nameof (app));
		}
		#endregion

		#region --公开方法--
		/// <summary>
		/// 当在派生类中重写时, 处理命令的详细过程
		/// </summary>
		/// <returns>处理结果值</returns>
		public abstract object Execute (); 
		#endregion
	}
}
