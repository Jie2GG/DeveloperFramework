using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DeveloperFramework.LogWindow.Controls
{
	/// <summary>
	/// 表示用于自动滚动显示数据项列表的控件
	/// </summary>
	public class AutoScrollListView : ListView
	{
		#region --字段--
		public static readonly DependencyProperty IsEnableScrollProperty = DependencyProperty.Register ("IsEnabledScroll", typeof (bool), typeof (AutoScrollListView), new FrameworkPropertyMetadata (false));
		private ScrollViewer _scrollViewer; 
		#endregion

		#region --属性--
		/// <summary>
		/// 获取或设置是否启用自动滚动
		/// </summary>
		[Bindable (true)]
		public bool IsEnabledScroll
		{
			get => (bool)base.GetValue (IsEnableScrollProperty);
			set => base.SetValue (IsEnableScrollProperty, value);
		} 
		#endregion

		#region --公开方法--
		/// <summary>
		/// 每当应用程序代码或内部进程调用 <see cref="System.Windows.FrameworkElement.ApplyTemplate"/>，都将调用此方法
		/// </summary>
		public override void OnApplyTemplate ()
		{
			base.OnApplyTemplate ();

			this._scrollViewer = RecursiveVisualChildFinder<ScrollViewer> (this) as ScrollViewer;
		} 
		#endregion

		#region --私有方法--
		protected override void OnItemsSourceChanged (IEnumerable oldValue, IEnumerable newValue)
		{
			base.OnItemsSourceChanged (oldValue, newValue);

			if (oldValue as INotifyCollectionChanged != null)
			{
				(oldValue as INotifyCollectionChanged).CollectionChanged -= ItemsCollectionChanged;
			}

			if (newValue as INotifyCollectionChanged == null)
			{
				return;
			}

			(newValue as INotifyCollectionChanged).CollectionChanged += ItemsCollectionChanged;
		}
		private void ItemsCollectionChanged (object sender, NotifyCollectionChangedEventArgs e)
		{
			if (_scrollViewer == null)
			{
				return;
			}

			if (!_scrollViewer.VerticalOffset.Equals (_scrollViewer.ScrollableHeight))
			{
				return;
			}

			UpdateLayout ();

			if (this.IsEnabledScroll)
			{
				_scrollViewer.ScrollToBottom ();
			}
		}
		private static DependencyObject RecursiveVisualChildFinder<T> (DependencyObject rootObject)
		{
			var child = VisualTreeHelper.GetChild (rootObject, 0);
			if (child == null) return null;

			return child.GetType () == typeof (T) ? child : RecursiveVisualChildFinder<T> (child);
		} 
		#endregion
	}
}
