using System.ComponentModel;

namespace DeveloperFramework.SimulatorModel.CQP
{
	/// <summary>
	/// 描述性别的枚举
	/// </summary>
	public enum Gender
	{
		/// <summary>
		/// 男性
		/// </summary>
		[Description ("男")]
		Male = 0,
		/// <summary>
		/// 女性
		/// </summary>
		[Description ("女")]
		Female = 1,
		/// <summary>
		/// 未知性别
		/// </summary>
		[Description ("未知")]
		Unknown = 255
	}
}
