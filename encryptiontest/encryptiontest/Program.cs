using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace encryptiontest
{
	internal class Program
	{
		static void Main(string[] args)
		{
            Encryption encryption = new Encryption();
			encryption.KeyGen();
            Console.Write("Nachricht eingeben: ");
			string rawinput = Console.ReadLine();
			string input = encryption.AppendRandStr(rawinput);
			string ciffre = encryption.Encrypt(input);
			Console.WriteLine();
			Console.Write("verschlüsselt: ");
			foreach(char c in ciffre)
			{
				Console.Write(c);
				Thread.Sleep(15);
			}
			Console.WriteLine();
			string decrypted = encryption.Decrypt(ciffre);
			Console.Write($"Entschlüsselt: {decrypted}");
			Console.ReadLine();
		}

	}
}
