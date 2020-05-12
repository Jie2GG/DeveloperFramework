using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeveloperFramework.Extension;

namespace DeveloperFramework.LibraryModel.CQP.Dynamic
{
	/// <summary>
	/// 描述 CQP 应用的 Anonymous 类型
	/// </summary>
	public class GroupAnonymous : IEquatable<GroupAnonymous>
	{
		#region --属性--
		/// <summary>
		/// 获取当前实例的匿名标识
		/// </summary>
		public long Id { get; }
		/// <summary>
		/// 获取当前实例的匿名称号
		/// </summary>
		public string Name { get; }
		/// <summary>
		/// 获取当前实例的执行令牌
		/// </summary>
		public byte[] Token { get; }
		#endregion

		#region --构造函数--
		/// <summary>
		/// 初始化 <see cref="GroupAnonymous"/> 类的新实例
		/// </summary>
		/// <param name="id">匿名标识</param>
		/// <param name="name">匿名称号</param>
		/// <param name="token">匿名令牌</param>
		public GroupAnonymous (long id, string name, byte[] token)
		{
			Id = id;
			Name = name ?? throw new ArgumentNullException (nameof (name));
			Token = token ?? throw new ArgumentNullException (nameof (token));
		}
		#endregion

		#region --公开方法--
		/// <summary>
		/// 返回 Base64 字符串
		/// </summary>
		/// <returns>Base64 字符串</returns>
		public string ToBase64String ()
		{
			using (BinaryWriter writer = new BinaryWriter (new MemoryStream ()))
			{
				writer.Write_Ex (this.Id);
				writer.Write_Ex (this.Name);
				writer.Write_Ex (this.Token);
				return Convert.ToBase64String (writer.ToArray ());
			}
		}
		/// <summary>
		/// 指示当前对象是否等于同一类型的另一个对象
		/// </summary>
		/// <param name="other">一个与此对象进行比较的对象</param>
		/// <returns>如果当前对象等于 other 参数，则为 <see langword="true"/>；否则为 <see langword="false"/></returns>
		public bool Equals (GroupAnonymous other)
		{
			if (other is null)
			{
				return false;
			}

			string data1 = this.ToBase64String ();
			string data2 = other.ToBase64String ();
			return data1.Equals (data2);
		}
		/// <summary>
		/// 指示当前对象是否等于同一类型的另一个对象
		/// </summary>
		/// <param name="obj">一个与此对象进行比较的对象</param>
		/// <returns>如果当前对象等于 other 参数，则为 <see langword="true"/>；否则为 <see langword="false"/></returns>
		public override bool Equals (object obj)
		{
			return this.Equals (obj as GroupAnonymous);
		}
		/// <summary>
		/// 返回此实例的哈希代码
		/// </summary>
		/// <returns>32 位有符号整数哈希代码</returns>
		public override int GetHashCode ()
		{
			return this.Id.GetHashCode () & this.Name.GetHashCode () & this.Token.GetHashCode ();
		}
		/// <summary>
		/// 返回表示当前对象的字符串
		/// </summary>
		/// <returns>表示当前对象的字符串</returns>
		public override string ToString ()
		{
			StringBuilder builder = new StringBuilder ();
			builder.AppendLine ($"ID: {this.Id}");
			builder.AppendLine ($"称号: {this.Name}");
			builder.AppendLine ($"令牌: {Convert.ToBase64String (this.Token)}");
			return builder.ToString ();
		}
		#endregion
	}
}
