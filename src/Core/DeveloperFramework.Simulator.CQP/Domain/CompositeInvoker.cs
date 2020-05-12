using DeveloperFramework.Simulator.CQP.Domain.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.Simulator.CQP.Domain
{
	/// <summary>
	/// 接收命令复合调用程序
	/// </summary>
	public static class CompositeInvoker
	{
		#region --字段--
		private static readonly Assembly ExecuteAssembly;
		#endregion

		#region --构造函数--
		static CompositeInvoker ()
		{
			ExecuteAssembly = Assembly.GetAssembly (typeof (ICommand));
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
		public static ICommand GetCommandHandle (CQPSimulatorApp app, string funcName, params object[] objs)
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
						object[] param = new object[objs.Length + 1];
						param[0] = app;
						for (int i = 1; i < param.Length; i++)
						{
							param[i] = objs[i - 1];
						}

						return (ICommand)Activator.CreateInstance (type, param);
					}
				}
			}

			// TODO: 返回默认处理命令
			
		}
		#endregion
	}
}
