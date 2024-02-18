using System;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using System.Text;

namespace encryptiontest
{
	public class Encryption
	{
        public string Encrypt(string s)
        {
            string projectDirectory = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory)));
            string configDirectory = Path.Combine(projectDirectory, "Config");
            string keyFileName = "key.txt";
            string keyFilePath = Path.Combine(configDirectory, keyFileName);
            try
            {
                string key = File.ReadAllText(keyFilePath);
                StringBuilder result = new StringBuilder();
                for (int i = 0; i < s.Length; i++)
                {
                    char c = s[i];
                    char k = key[i % key.Length];
                    int encryptedValue = (int)c + (int)k;
                    result.Append((char)encryptedValue);
                }
                return result.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Lesen des Schlüssels: {ex.Message}");
                return null;
            }
        }

        public string Decrypt(string encryptedText)
        {
            string projectDirectory = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory)));
            string configDirectory = Path.Combine(projectDirectory, "Config");
            string keyFileName = "key.txt";
            string keyFilePath = Path.Combine(configDirectory, keyFileName);
            try
            {
                string key = File.ReadAllText(keyFilePath);
                StringBuilder result = new StringBuilder();
                for (int i = 0; i < encryptedText.Length; i++)
                {
                    char c = encryptedText[i];
                    char k = key[i % key.Length];
                    int decryptedValue = (int)c - (int)k;
                    result.Append((char)decryptedValue);
                }
                result.Length -= 9;
                return result.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Lesen des Schlüssels: {ex.Message}");
                return null;
            }
        }
        public void KeyGen()
		{
			const string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789=+*%&()-_.:,;^!";
            string projectDirectory = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory)));
            string configDirectory = Path.Combine(projectDirectory, "Config");
            string keyFileName = "key.txt";
            string keyFilePath = Path.Combine(configDirectory, keyFileName);
            using (var rng = new RNGCryptoServiceProvider())
			{
                try
                {
                    int length = 128;
                    byte[] keyBytes = new byte[length];
                    rng.GetBytes(keyBytes);
                    StringBuilder key = new StringBuilder(length);
                    foreach (byte keyByte in keyBytes)
                    {
                        key.Append(validChars[keyByte % validChars.Length]);
                    }
                    string strkey = key.ToString();
                    
                    Directory.CreateDirectory(Path.GetDirectoryName(keyFilePath));
                    File.WriteAllText(keyFilePath, strkey);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error writing key to file: " + ex.Message);
                }
            }
		}
        public string AppendRandStr(string s)
        {
            Random random = new Random();
            const string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder result = new StringBuilder(s);
            for (int i = 0; i < 9; i++)
            {
                result.Append(validChars[random.Next(validChars.Length)]);
            }
            return result.ToString();
        }
    }
}
