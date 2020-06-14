using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.SimulatorModel.CQP
{
	/// <summary>
	/// 描述 讨论组列表 类型
	/// </summary>
	public class DiscussCollection : ObservableCollection<Discuss>
	{
		#region --构造函数--
		/// <summary>
		/// 初始化为空的 <see cref="FriendCollection"/> 类的新实例
		/// </summary>
		public DiscussCollection ()
		{
		}
		/// <summary>
		/// 新实例初始化 <see cref="FriendCollection"/> 包装指定列表的类
		/// </summary>
		/// <param name="list">用于包装由新的集合的列表</param>
		public DiscussCollection (IList<Discuss> list)
			: base (list)
		{
		} 
		#endregion
	}
}
