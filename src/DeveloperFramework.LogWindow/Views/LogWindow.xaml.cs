using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DeveloperFramework.LogWindow.Views
{
	/// <summary>
	/// LogWindow.xaml 的交互逻辑
	/// </summary>
	public partial class LogWindow : Window
	{
		/// <summary>
		/// 初始化 <see cref="LogWindow"/> 类的新实例
		/// </summary>
		/// <exception cref="InvalidOperationException">无法在控制台应用中创建新的 WPF 窗体</exception>
		public LogWindow ()
		{
			if (Console.In != StreamReader.Null)
			{
				throw new InvalidOperationException ("无法在控制台应用程序中新建 WPF 窗体");
			}

			InitializeComponent ();
		}
	}
}
