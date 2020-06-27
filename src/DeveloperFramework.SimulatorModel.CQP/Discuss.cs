namespace DeveloperFramework.SimulatorModel.CQP
{
	/// <summary>
	/// 描述讨论组的类
	/// </summary>
	public class Discuss
	{
		#region --属性--
		/// <summary>
		/// 获取当前实例的唯一标识 (ID)
		/// </summary>
		public long Id { get; }
		/// <summary>
		/// 获取或设置当前实例的名字
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 获取当前实例的成员列表
		/// </summary>
		public DiscussMemberCollection Member { get; }
		#endregion

		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="Discuss"/> 类的新实例
		/// </summary>
		/// <param name="id">绑定于当前实例的唯一标识 (ID)</param>
		public Discuss (long id)
		{
			this.Id = id;
			this.Member = new DiscussMemberCollection (50);  // 根据TX官方解释, 讨论组最多 50人
		}
		#endregion
	}
}
