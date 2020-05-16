using DeveloperFramework.Extension;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.SimulatorModel.CQP
{
	/// <summary>
	/// 描述 好友列表 类型
	/// </summary>
	public class FriendCollection : Collection<Friend>
	{
		#region --公开方法--
		/// <summary>
		/// 获取当前实例的 Base64 字符串
		/// </summary>
		/// <returns>当前实例的 Base64 字符串</returns>
		public string ToBase64String ()
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
