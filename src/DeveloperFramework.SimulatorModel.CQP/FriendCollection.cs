using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

using DeveloperFramework.Extension;

namespace DeveloperFramework.SimulatorModel.CQP
{
	/// <summary>
	/// 描述好友列表的类
	/// </summary>
	public class FriendCollection : Collection<Friend>, IToBase64
	{
		#region --构造函数--
		/// <summary>
		/// 初始化为空的 <see cref="FriendCollection"/> 类的新实例
		/// </summary>
		public FriendCollection ()
			: base ()
		{
		}
		#endregion

		#region --公开方法--
		/// <summary>
		/// 返回当前实例的 Base64 字符串
		/// </summary>
		/// <returns>当前实例的 Base64 字符串</returns>
		public string ToBase64 ()
		{
			using (BinaryWriter writer = new BinaryWriter (new MemoryStream ()))
			{
				writer.Write_Ex (this.Count);   // 写入个数
				foreach (Friend friend in this)
				{
					writer.Write_Ex (friend.ToByteArray ());
				}
				return Convert.ToBase64String (writer.ToArray ());
			}
		}
		#endregion
	}
}
