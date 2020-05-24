using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.SimulatorModel.CQP
{
	/// <summary>
	/// 描述 讨论组成员列表 类型
	/// </summary>
	public class DiscussMemberCollection : Collection<DiscussMember>
	{
		/// <summary>
		/// 初始化为空的 <see cref="DiscussMemberCollection"/> 类的新实例
		/// </summary>
		public DiscussMemberCollection ()
			: base ()
		{ }
		/// <summary>
		/// 新实例初始化 <see cref="GroupMemberCollection"/> 包装指定列表的类
		/// </summary>
		/// <param name="list">用于包装由新的集合的列表</param>
		public DiscussMemberCollection (IList<DiscussMember> list)
			: base (list)
		{ }
	}
}
