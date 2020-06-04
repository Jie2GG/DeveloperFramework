using DeveloperFramework.Simulator.CQP.Domain.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.Simulator.CQP.Domain
{
	/// <summary>
	/// 命令路由调用程序
	/// </summary>
	public class CommandRouteInvoker
	{
		#region --字段--
		private static readonly Assembly ExecuteAssembly = Assembly.GetAssembly (typeof (ICommand));
		#endregion

		#region --属性--
		/// <summary>
		/// 获取当前实例关联的 <see cref="CQPSimulator"/>
		/// </summary>
		public CQPSimulator Simulator { get; }
		/// <summary>
		/// 获取当前实例关联的 <see cref="CQPSimulatorApp"/>
		/// </summary>
		public CQPSimulatorApp App { get; }
		/// <summary>
		/// 获取当前实例表示权限验证是否通过
		/// </summary>
		public bool IsAuth { get; }
		#endregion

		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="CommandRouteInvoker"/> 类的新实例
		/// </summary>
		/// <param name="simulator">相关联的 <see cref="CQPSimulator"/></param>
		/// <param name="app">相关联的 <see cref="CQPSimulatorApp"/></param>
		/// <param name="isAuth">表示验证授权是否通过</param>
		public CommandRouteInvoker (CQPSimulator simulator, CQPSimulatorApp app, bool isAuth)
		{
			Simulator = simulator;
			this.App = app;
			this.IsAuth = isAuth;
		}
		#endregion

		#region --公开方法--
		/// <summary>
		/// 获取指定的命令处理程序
		/// </summary>
		/// <param name="app">相关联的 <see cref="CQPSimulatorApp"/> 实例</param>
		/// <param name="funcName">触发此操作的函数名称</param>
		/// <param name="objs">附加的参数列表</param>
		/// <returns>返回指定的命令处理程序</returns>
		public ICommand GetCommandHandle (string funcName, params object[] objs)
		{
			foreach (Type type in ExecuteAssembly.GetTypes ())
			{
				// 跳过抽象类和接口
				if (type.IsInterface || type.IsAbstract)
				{
					continue;
				}

				FunctionBindingAttribute attribute = type.GetCustomAttribute<FunctionBindingAttribute> ();
				if (attribute != null)
				{
					if (attribute.Function.Equals (funcName))
					{
						object[] param = new object[objs.Length + 3];
						param[0] = this.Simulator;
						param[1] = this.App;
						param[2] = this.IsAuth;
						Array.Copy (objs, 0, param, param.Length - objs.Length, objs.Length);

						return (ICommand)Activator.CreateInstance (type, param);
					}
				}
			}

			throw new MissingManifestResourceException ($"无法找到用于适配: {funcName} 的程序集命令");
		}
		#endregion
	}
}
