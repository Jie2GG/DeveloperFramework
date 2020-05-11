using DeveloperFramework.CQP;
using DeveloperFramework.Library.CQP;
using DeveloperFramework.Log.CQP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DeveloperFramework.Simulator.CQP
{
	/// <summary>
	/// 表示 CQP 应用模拟器
	/// </summary>
	public class CQPSimulator
	{
		#region --常量--
		private const string STR_SIMULATOR_INIT = "初始化";
		private const string STR_APPLOAD = "应用加载";
		#endregion

		#region --字段--
		private static readonly Regex appIdRegex = new Regex (@"(?:[a-z]*)\.(?:[a-z\-_]*)\.(?:[a-zA-Z0-9\.\-_]*)", RegexOptions.Compiled);
		private static readonly Random appRandom = new Random ();
		#endregion

		#region --属性--
		/// <summary>
		/// 获取当前实例加载的 <see cref="CQPDynamicLibrary"/> 集合
		/// </summary>
		public List<CQPDynamicLibrary> Libraries { get; }
		/// <summary>
		/// 获取当前实例的应用路径
		/// </summary>
		public string AddDirectory { get; }
		/// <summary>
		/// 获取一个值, 指示当前实例是否已启动
		/// </summary>
		public bool IsStart { get; private set; }
		#endregion

		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="CQPSimulator"/> 类的新实例
		/// </summary>
		/// <param name="appDirectory">应用路径</param>
		public CQPSimulator (string appDirectory)
		{
			if (!Directory.Exists (appDirectory))
			{
				Directory.CreateDirectory (appDirectory);
				LogCenter.Instance.InfoSuccess (STR_SIMULATOR_INIT, $"已创建应用目录: {appDirectory}");
			}

			this.AddDirectory = appDirectory;
			this.Libraries = new List<CQPDynamicLibrary> ();
		}
		#endregion

		#region --公开方法--
		/// <summary>
		/// 启动 <see cref="CQPSimulator"/>
		/// </summary>
		public void Start ()
		{
			LogCenter.Instance.InfoSuccess (STR_APPLOAD, "应用加载开始");

			if (this.IsStart)
			{
				return;
			}

			string[] pathes = Directory.GetDirectories (this.AddDirectory);
			foreach (string path in pathes)
			{
				string appId = Path.GetFileName (path);     // 获取最后一段字符串
				if (!appIdRegex.IsMatch (appId))
				{
					continue;   // 跳过加载
				}

				string dllName = Path.Combine (path, "app.dll");
				string jsonName = Path.Combine (path, "app.json");

				try
				{
					CQPDynamicLibrary library = new CQPDynamicLibrary (dllName, jsonName);
					string appInfo = library.InvokeAppInfo ();
					if (!appInfo.Equals ($"{library.AppInfo.ApiVersion},{appId}"))
					{
						// 卸载 Library
						library.Dispose ();
						// 写入日志
						LogCenter.Instance.Warning (STR_APPLOAD, $"应用: {appId} 返回的 AppID 错误.");
						continue;
					}

					int authCode = appRandom.Next (int.MaxValue);
					// 传递验证码
					int resCode = library.InvokeInitialize (authCode);
					if (resCode != 0)
					{
						LogCenter.Instance.Error (STR_APPLOAD, $"应用 {appId} 的 Initialize 方法未返回 0");
					}

					// 加入列表
					this.Libraries.Add (library);

					LogCenter.Instance.InfoSuccess (STR_APPLOAD, $"应用: {appId} 加载成功");
				}
				catch (Exception ex)
				{
					LogCenter.Instance.Warning (STR_APPLOAD, $"应用: {appId} 加载失败, 原因: {ex.Message}");
				}
			}

			LogCenter.Instance.InfoSuccess (STR_APPLOAD, $"应用加载结束. 加载成功: {this.Libraries.Count} 个, 失败: {this.Libraries.Count - pathes.Length} 个");
		}
		#endregion
	}
}
