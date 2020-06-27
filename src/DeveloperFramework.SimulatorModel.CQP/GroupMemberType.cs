using System.ComponentModel;

namespace DeveloperFramework.SimulatorModel.CQP
{
	/// <summary>
	/// 描述群成员类型的枚举
	/// </summary>
	public enum GroupMemberType
	{
		/// <summary>
		/// 正常成员
		/// </summary>
		[Description ("群成员")]
		Normal = 1,
		/// <summary>
		/// 管理员
		/// </summary>
		[Description ("管理员")]
		Manager = 2,
		/// <summary>
		/// 创建者
		/// </summary>
		[Description ("群主")]
		Creator = 3
	}
}
