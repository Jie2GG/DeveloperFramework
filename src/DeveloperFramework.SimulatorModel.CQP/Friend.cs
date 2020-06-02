using DeveloperFramework.Extension;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.SimulatorModel.CQP
{
	/// <summary>
	/// 描述 好友 类型
	/// </summary>
	public class Friend : QQ, IEquatable<Friend>
	{
		#region --属性--
		/// <summary>
		/// 获取或设置当前实例作为好友的备注
		/// </summary>
		public string Postscript { get; set; }
		#endregion

		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="QQ"/> 的新实例
		/// </summary>
		/// <param name="id">QQ号</param>
		/// <param name="nick">昵称</param>
		/// <param name="sex">性别</param>
		/// <param name="age">年龄</param>
		/// <param name="postscript">备注信息</param>
		public Friend (long id, string nick, Sex sex, int age, string postscript)
			: base (id, nick, sex, age)
		{
			Postscript = postscript;
		}
		#endregion

		#region --公开方法--
		/// <summary>
		/// 获取当前实例的 <see cref="byte"/> 数组
		/// </summary>
		/// <returns>当前实例的 <see cref="byte"/> 数组</returns>
		public override byte[] ToByteArray ()
		{
			using (BinaryWriter writer = new BinaryWriter (new MemoryStream ()))
			{
				writer.Write_Ex (this.Id);
				writer.Write_Ex (this.Nick);
				writer.Write_Ex (this.Postscript);
				return writer.ToArray ();
			}
		}
		/// <summary>
		/// 指示当前对象是否等于同一类型的另一个对象
		/// </summary>
		/// <param name="obj">一个与此对象进行比较的对象</param>
		/// <returns>如果当前对象等于 obj 参数，则为 <see langword="true"/>；否则为 <see langword="false"/></returns>
		public bool Equals (Friend obj)
		{
			return base.Equals (obj as QQ) && this.Postscript.Equals (obj.Postscript);
		}
		/// <summary>
		/// 指示当前对象是否等于同一类型的另一个对象
		/// </summary>
		/// <param name="obj">一个与此对象进行比较的对象</param>
		/// <returns>如果当前对象等于 obj 参数，则为 <see langword="true"/>；否则为 <see langword="false"/></returns>
		public override bool Equals (object obj)
		{
			return this.Equals (obj as Friend);
		}
		/// <summary>
		/// 返回此实例的哈希代码
		/// </summary>
		/// <returns>32 位有符号整数哈希代码</returns>
		public override int GetHashCode ()
		{
			return base.GetHashCode () & this.Postscript.GetHashCode ();
		}
		#endregion
	}
}
