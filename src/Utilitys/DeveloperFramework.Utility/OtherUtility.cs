using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperFramework.Utility
{
	/// <summary>
	/// 其它实用工具
	/// </summary>
	public static class OtherUtility
	{
		/// <summary>
		/// 基于指定的父路径将相对路径转换为绝对路径
		/// </summary>
		/// <param name="baseDirectory">基础父路径</param>
		/// <param name="filePath">需要转换的相对路径</param>
		/// <returns>转换后的绝对路径</returns>
		public static string GetAbsolutePath (string baseDirectory, string filePath)
		{
			if (baseDirectory is null)
			{
				throw new ArgumentNullException (nameof (baseDirectory));
			}

			if (filePath is null)
			{
				throw new ArgumentNullException (nameof (filePath));
			}

			baseDirectory = GetFormatPath (baseDirectory);
			filePath = GetFormatPath (filePath);

			List<string> relativePath = new List<string> (baseDirectory.Split (Path.DirectorySeparatorChar));
			string[] fileDirectories = filePath.Split (Path.DirectorySeparatorChar);

			int lastRoot = -1;
			for (int i = 0; i < relativePath.Count; i++)
			{
				if (string.Compare (relativePath[i], fileDirectories[0], true) == 0)
				{
					lastRoot = i;
					break;
				}
			}

			if (lastRoot == relativePath.Count - 1)
			{
				if (fileDirectories.Length == 1)
				{
					relativePath.AddRange (fileDirectories);
				}
				else
				{
					relativePath.RemoveAt (relativePath.Count - 1);
					relativePath.AddRange (fileDirectories);
				}
			}
			else if (lastRoot == -1)
			{
				relativePath.AddRange (fileDirectories);
			}
			else
			{
				for (int i = 1; i < fileDirectories.Length; i++)
				{
					if (string.Compare (relativePath[i + lastRoot], fileDirectories[i], true) != 0)
					{
						relativePath.AddRange (fileDirectories);
						break;
					}

					if ((i + lastRoot) >= (relativePath.Count - 1))
					{
						relativePath.AddRange (fileDirectories.Skip (i + 1));
						break;
					}
				}
			}
			return string.Join ("\\", relativePath);
		}
		/// <summary>
		/// 获取格式化后的路径
		/// </summary>
		/// <param name="path">需要格式化的路径字符串</param>
		/// <returns>格式化后的路径</returns>
		public static string GetFormatPath (string path)
		{
			StringBuilder builder = new StringBuilder (path);
			builder.Replace ('/', '\\');
			while (builder[0] == '\\')
			{
				builder.Remove (0, 1);
			}
			while (builder[builder.Length - 1] == '\\')
			{
				builder.Remove (builder.Length - 1, 1);
			}
			return builder.ToString ();
		}
	}
}
