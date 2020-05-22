using DeveloperFramework.LibraryModel.CQP;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.Simulator.CQP.Domain.Command
{
	/// <summary>
	/// 表示接收到命令的处理类型, 该类是抽象的
	/// </summary>
	public abstract class AbstractCommand : ICommand
	{
		#region --属性--
		/// <summary>
		/// 获取当前实例关联的 <see cref="CQPSimulator"/>
		/// </summary>
		public CQPSimulator Simulator { get; }
		/// <summary>
		/// 获取当前实例的调用者 <see cref="CQPSimulatorApp"/> 实例
		/// </summary>
		public CQPSimulatorApp App { get; }
		/// <summary>
		/// 获取当前实例的是否授权
		/// </summary>
		public bool IsAuth { get; }
		#endregion

		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="AbstractCommand"/> 类的新实例
		/// </summary>
		/// <param name="app">相关联的应用 <see cref="CQPSimulatorApp"/></param>
		/// <param name="isAuth">指示当前命令是否得到授权</param>
		public AbstractCommand (CQPSimulator simulator, CQPSimulatorApp app, bool isAuth)
		{
			this.Simulator = simulator ?? throw new ArgumentNullException (nameof (simulator));
			this.App = app ?? throw new ArgumentNullException (nameof (app));
			this.IsAuth = isAuth;
		}
		#endregion

		#region --公开方法--
		/// <summary>
		/// 处理命令的详细过程
		/// </summary>
		/// <returns>处理结果值</returns>
		public object Execute ()
		{
			return this.IsAuth ? ExecuteHaveAuth () : ExecuteHaveNoAuth ();
		}
		/// <summary>
		/// 当在派生类中重写时, 处理有权限的处理过程
		/// </summary>
		/// <returns>处理结果值</returns>
		public abstract object ExecuteHaveAuth ();
		/// <summary>
		/// 当在派生类中重写时, 处理没有权限的处理过程
		/// </summary>
		/// <returns>处理结果值</returns>
		public abstract object ExecuteHaveNoAuth ();
		#endregion
	}
}
