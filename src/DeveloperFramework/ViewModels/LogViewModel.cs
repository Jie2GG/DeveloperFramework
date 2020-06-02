using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows;
using DeveloperFramework.Log.CQP;

namespace DeveloperFramework.LogWindow.ViewModels
{
	/// <summary>
	/// 表示日志窗体的业务逻辑类
	/// </summary>
	public class LogViewModel : BindableBase
	{
		#region --字段--
		private bool _isEnableScroll = true;
		#endregion

		#region --属性--
		/// <summary>
		/// 获取日志项目集合
		/// </summary>
		public ObservableCollection<LogItem> LogItems { get; }
		/// <summary>
		/// 获取或设置是否启用列表自动滚动
		/// </summary>
		public bool IsEnableScroll { get => this._isEnableScroll; set => SetProperty (ref _isEnableScroll, value); }
		#endregion

		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="LogViewModel"/> 类的新实例
		/// </summary>
		public LogViewModel ()
		{
			this.LogItems = new ObservableCollection<LogItem> ();

		}
		#endregion

		#region --公开方法--

		#endregion
	}
}
