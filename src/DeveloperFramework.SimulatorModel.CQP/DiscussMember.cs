using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.SimulatorModel.CQP
{
	/// <summary>
	/// 描述 讨论组成员 类型
	/// </summary>
	public class DiscussMember : QQ
	{
		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="DiscussMember"/> 类的新实例
		/// </summary>
		/// <param name="id">QQ号</param>
		/// <param name="nick">昵称</param>
		/// <param name="sex">性别</param>
		/// <param name="age">年龄</param>
		public DiscussMember (long id, string nick, Sex sex, int age)
			: base (id, nick, sex, age)
		{ }
		#endregion
	}
}
