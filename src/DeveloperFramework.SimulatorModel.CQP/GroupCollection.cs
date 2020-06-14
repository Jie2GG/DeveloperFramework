using DeveloperFramework.Extension;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.SimulatorModel.CQP
{
	/// <summary>
	/// 描述 群列表 类型
	/// </summary>
	public class GroupCollection : ObservableCollection<Group>
	{
		#region --构造函数--
		/// <summary>
		/// 初始化为空的 <see cref="GroupCollection"/> 类的新实例
		/// </summary>
		public GroupCollection ()
			: base ()
		{
		}
		/// <summary>
		/// 新实例初始化 <see cref="GroupCollection"/> 包装指定列表的类
		/// </summary>
		/// <param name="list">用于包装由新的集合的列表</param>
		public GroupCollection (IList<Group> list)
			: base (list)
		{ }
		#endregion

		#region --公开方法--
		/// <summary>
		/// 获取当前实例的 <see cref="byte"/> 数组
		/// </summary>
		/// <returns>当前实例的 <see cref="byte"/> 数组</returns>
		public byte[] ToByteArray ()
		{
			using (BinaryWriter writer = new BinaryWriter (new MemoryStream ()))
			{
				writer.Write_Ex (this.Count);
				foreach (Group group in this)
				{
					writer.Write_Ex (group.Id);
					writer.Write_Ex (group.Name);
				}
				return writer.ToArray ();
			}
		}
		/// <summary>
		/// 获取当前实例的 Base64 字符串
		/// </summary>
		/// <returns>当前实例的 Base64 字符串</returns>
		public string ToBase64String ()
		{
			return Convert.ToBase64String (this.ToByteArray ());
		} 
		#endregion
	}
}
