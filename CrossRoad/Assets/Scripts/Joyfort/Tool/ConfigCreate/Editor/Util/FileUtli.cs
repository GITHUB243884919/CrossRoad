using System;
using System.IO;
using System.Text;
using UnityEngine;
using System.Threading;
using System.Security.Cryptography;


public class FileUtil
{
	/// <summary>
	/// 递归删除目录
	/// </summary>
	/// <param name="str">String.</param>
	public static void DeleteDir(string str)
	{
		DirectoryInfo dir = new DirectoryInfo (str);
		if (!dir.Exists) {
			return;
		}

		foreach (var file in dir.GetFiles()) {
			if (file.FullName.EndsWith(".meta", StringComparison.Ordinal)) {
				continue;
			}
			file.Delete ();
		}

		foreach (var d in dir.GetDirectories()) {
			DeleteDir (d.FullName);
		}

		dir.Delete (true);
	}

	/// <summary>
	/// 递归创建目录
	/// </summary>
	/// <param name="str">String.</param>
	public static void CreateDir(string str, bool clean = false) {
		if (clean)
		{
			DeleteDir(str);
		}

		DirectoryInfo dir = new DirectoryInfo (str);
		if (!dir.Parent.Exists) {
			CreateDir (dir.Parent.FullName);
		}
		if(!dir.Exists)
			dir.Create ();
	}

	/// <summary>
	/// 拷贝目录
	/// </summary>
	/// <param name="from">From.</param>
	/// <param name="to">To.</param>
	public static void CopyDir(string from, string to) {
		var dir = new DirectoryInfo (from);
		if (!dir.Exists) {
			return;
		}
		CreateDir (to);

		foreach (var f in dir.GetFiles()) {
			var name = f.Name;
			if (name.StartsWith (".", StringComparison.Ordinal) || name.StartsWith ("~", StringComparison.Ordinal)) {
				continue;
			}

			var to_file = Path.Combine (to, name);
			if (File.Exists (to_file)) {
				File.Delete (to_file);
			}
			File.Copy (f.FullName, to_file);
		}

		foreach (var d in dir.GetDirectories()) {
			string sub = Path.Combine (to, d.Name);
			CreateDir (sub);
			CopyDir(d.FullName, sub);
		}
	}

	/// <summary>
	/// 获得文件Md5值
	/// </summary>
	/// <returns>The 5 file.</returns>
	/// <param name="fileName">File name.</param>
	/// <param name="md5">Md5.</param>
	public static string MD5File(string fileName, MD5 md5 = null)
	{
		try
		{
			return MD5(File.ReadAllBytes(fileName), md5);
		}
		catch (Exception ex)
		{
			throw new Exception("MD5File() fail,error:" + ex.Message);
		}
	}

	/// <summary>
	/// 获取字节数字的md5值
	/// </summary>
	/// <returns>The MD.</returns>
	/// <param name="bytes">Bytes.</param>
	/// <param name="md5">Md5.</param>
	public static string MD5(byte[] bytes, MD5 md5 = null)
	{
		if (md5 == null) {
			md5 = new MD5CryptoServiceProvider();
		}
		byte[] retVal = md5.ComputeHash(bytes);
		StringBuilder sb = new StringBuilder();
		for (int i = 0; i < retVal.Length; i++)
		{
			sb.Append(retVal[i].ToString("X2"));
		}
		return sb.ToString();
	}

	/// <summary>
	/// 读取文件内容
	/// </summary>
	/// <returns>The file.</returns>
	/// <param name="file">File.</param>
	public static byte[] ReadFile(string file) {
		if (!File.Exists(file)) {
			return null;
		}

		return File.ReadAllBytes(file);
	}

	/// <summary>
	/// 写文件, 如果目录不存在自动创建
	/// </summary>
	/// <param name="file">File.</param>
	/// <param name="data">Data.</param>
	public static void WriteFile(string file, byte[] data) {
		try {
			var parent = Directory.GetParent(file);
			CreateDir(parent.FullName);

			File.WriteAllBytes(file, data);
		} catch(Exception e) {
			Debug.LogError(e);
		}

	}

	/// <summary>
	/// 写文件, 如果目录不存在自动创建
	/// </summary>
	/// <param name="file">File.</param>
	/// <param name="data">Data.</param>
	public static void WriteFile(string file, string data) {
		try {
			var parent = Directory.GetParent(file);
			CreateDir(parent.FullName);

			File.WriteAllText(file, data);
		} catch(Exception e) {
			Debug.LogError(e);
		}

	}

	/// <summary>
	/// 判断文件或目录是否存在
	/// </summary>
	/// <param name="file">File.</param>
	public static bool Exists(string file) {
		return File.Exists (file);
	}
}