using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using Password;

namespace rofl
{
   class Program
   {
      public static void Main()
      {
			EncryptedFile.FileDirectory = Directory.GetCurrentDirectory() + @"\Eeeeee";
         byte[] key = new byte[32];
         for (int i = 0; i < key.Length; i++)
			{
				key[i] = (byte)i;
			}
         TextEntry text = EncryptedFile.GetFile(key, @"C:\Users\alice\source\repos\PasswordManager\Testing Ground\bin\Debug\net6.0\Eeeeee\8937290756000345436.entry") as TextEntry;
			//TextEntry text = new(key)
			//{
			//   Text = "Meri is super great and I big love him",
			//   Title = "Feelings"
			//};
			//text.Save();
		}
   }
}