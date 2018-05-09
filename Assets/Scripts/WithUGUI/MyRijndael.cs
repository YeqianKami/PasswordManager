using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace Assets.Scripts.WithUGUI
{
	static class MyRijndael
	{
		private static RijndaelManaged rijndaelManaged = new RijndaelManaged();
		private static bool isInit = false;
		private static string Key = "1912112318413812";
		private static string Iv = "16842593";
		public static string FilePath;

		private static void InitRijndaelManaged()
		{
			try
			{
				rijndaelManaged.Key = Encoding.Unicode.GetBytes(Key);
				rijndaelManaged.IV = Encoding.Unicode.GetBytes(Iv);
			}
			catch (Exception)
			{
				throw new Exception(Encoding.Unicode.GetBytes(Key).Length.ToString());
			}
			isInit = true;
		}

		/// <summary>
		/// 加密
		/// </summary>
		/// <param name="InPlainText">明文</param>
		public static void Encrypt(string InPlainText)
		{
			if (!isInit) InitRijndaelManaged();//保证初始化
			ICryptoTransform encryptor = rijndaelManaged.CreateEncryptor();

			byte[] encrypted;//密文
			using (MemoryStream msEncrypt = new MemoryStream())
			{
				using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
				{
					using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
					{

						//Write all data to the stream.
						swEncrypt.Write(InPlainText);
					}
					encrypted = msEncrypt.ToArray();
				}
			}
			//保存密文
			FileStream fileStream = new FileStream(FilePath, FileMode.Create);
			fileStream.Write(encrypted, 0, encrypted.Length);
			fileStream.Close();
		}

		/// <summary>
		/// 解密
		/// </summary>
		/// <returns>解密得到的明文</returns>
		/// <exception cref="Exception">如果路径指向的文件不存在</exception>
		public static string Decrypt()
		{
			if (!isInit) InitRijndaelManaged();//保证初始化
			string OutPlainText = null;
			
			if (!File.Exists(FilePath)) throw new Exception(FilePath);
			//读取密文
			byte[] getFromFile = File.ReadAllBytes(FilePath);
			NGUIDebug.Log(getFromFile.Length);

			ICryptoTransform decryptor = rijndaelManaged.CreateDecryptor();

			using (MemoryStream msDecrypt = new MemoryStream(getFromFile))
			{
				using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
				{
					using (StreamReader srDecrypt = new StreamReader(csDecrypt))
					{
						OutPlainText = srDecrypt.ReadToEnd();
					}
				}
			}
			return OutPlainText;
		}
	}
}
