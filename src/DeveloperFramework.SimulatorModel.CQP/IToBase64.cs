namespace DeveloperFramework.SimulatorModel.CQP
{
	/// <summary>
	/// 表示获取 Base64 字符串的接口
	/// </summary>
	public interface IToBase64
	{
		/// <summary>
		/// 当在派生类中重写时, 返回当前实例的 Base64 字符串
		/// </summary>
		/// <returns>Base64 字符串</returns>
		string ToBase64 ();
	}
}
