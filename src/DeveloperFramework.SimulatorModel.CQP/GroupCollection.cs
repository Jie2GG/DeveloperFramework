using DeveloperFramework.Extension;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DeveloperFramework.SimulatorModel.CQP
{
	/// <summary>
	/// 描述群列表的类
	/// </summary>
	public class GroupCollection : Collection<Group>, IToBase64
	{
		#region --构造函数--
		/// <summary>
		/// 初始化为空的 <see cref="GroupCollection"/> 类的新实例
		/// </summary>
		public GroupCollection ()
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
				foreach (Group group in this)
				{
					writer.Write_Ex (group.Id);
					writer.Write_Ex (group.Name);
				}
				return Convert.ToBase64String (writer.ToArray ());
			}
		}
		#endregion
	}
}
