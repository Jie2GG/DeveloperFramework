using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using DeveloperFramework.CQP;
using DeveloperFramework.LibraryModel.CQP.Dynamic;
using DeveloperFramework.Log.CQP;
using DeveloperFramework.Simulator.CQP;
using DeveloperFramework.Simulator.CQP.Domain;

namespace TestApplication
{
	class Program
	{
		static void Main (string[] args)
		{
			CQPSimulator simulator = new CQPSimulator (Path.Combine (AppDomain.CurrentDomain.BaseDirectory, "dev"));
			simulator.Start ();
			var lib = simulator.CQPApps[0].Library;
			lib.InvokeCQPrivateMessage (lib.AppInfo.Events[1], PrivateMessageType.Friend, 947295340L, "发送的消息", IntPtr.Zero);
			simulator.Stop ();
			//Console.WriteLine (nameof (CQPExport.CQ_sendPrivateMsg));
			Console.Read ();
		}
	}
}
