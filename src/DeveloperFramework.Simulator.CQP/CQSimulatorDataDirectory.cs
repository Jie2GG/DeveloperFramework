using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.Simulator.CQP
{
	/// <summary>
	/// 表示酷Q应用模拟器数据目录类
	/// </summary>
	public class CQSimulatorDataDirectory
	{
		#region --字段--
		private readonly string _appDir;
		private readonly string _devDir;
		private readonly string _dataDir;
		private readonly string _dataAppDir;
		private readonly string _dataBfaceDir;
		private readonly string _dataShowDir;
		private readonly string _dataImageDir;
		private readonly string _dataRecordDir;
		private readonly string _dataLogDir;
		private readonly string _dataTmpDir;
		private readonly string _dataTmpCappDir;
		private readonly string _confDir;
		private readonly string _binDir;
		#endregion

		#region --属性--
		/// <summary>
		/// 获取一个值, 指示模拟器的 "app" 目录
		/// </summary>
		public string AppDirectory => this._appDir;
		/// <summary>
		/// 获取一个值, 指示模拟器的 "dev" 目录
		/// </summary>
		public string DevDirectory => this._devDir;
		/// <summary>
		/// 获取一个值, 指示模拟器的 "tmp/capp" 目录
		/// </summary>
		public string CappDirectory => this._dataTmpCappDir;
		#endregion

		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="CQSimulatorDataDirectory"/> 类的新实例, 并指定具体的父路径
		/// </summary>
		/// <param name="baseDirectory">父路径</param>
		public CQSimulatorDataDirectory (string baseDirectory)
		{
			this._appDir = Path.Combine (baseDirectory, "app");
			this._dataDir = Path.Combine (baseDirectory, "data");
			this._confDir = Path.Combine (baseDirectory, "conf");
			this._devDir = Path.Combine (baseDirectory, "dev");
			this._binDir = Path.Combine (baseDirectory, "bin");

			Directory.CreateDirectory (this._appDir);
			Directory.CreateDirectory (this._dataDir);
			Directory.CreateDirectory (this._confDir);
			Directory.CreateDirectory (this._devDir);
			Directory.CreateDirectory (this._binDir);

			this._dataAppDir = Path.Combine (this._dataDir, "app");
			this._dataBfaceDir = Path.Combine (this._dataDir, "bface");
			this._dataShowDir = Path.Combine (this._dataDir, "show");
			this._dataImageDir = Path.Combine (this._dataDir, "image");
			this._dataRecordDir = Path.Combine (this._dataDir, "record");
			this._dataLogDir = Path.Combine (this._dataDir, "log");
			this._dataTmpDir = Path.Combine (this._dataDir, "tmp");

			Directory.CreateDirectory (this._dataAppDir);
			Directory.CreateDirectory (this._dataBfaceDir);
			Directory.CreateDirectory (this._dataShowDir);
			Directory.CreateDirectory (this._dataImageDir);
			Directory.CreateDirectory (this._dataRecordDir);
			Directory.CreateDirectory (this._dataLogDir);
			Directory.CreateDirectory (this._dataTmpDir);

			this._dataTmpCappDir = Path.Combine (this._dataTmpDir, "capp");
			Directory.CreateDirectory (this._dataTmpCappDir);
		}
		#endregion
	}
}
