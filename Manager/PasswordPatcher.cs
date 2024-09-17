using System;
using System.IO;
using Password;
using PasswordV2;

namespace Manager
{
   static class PasswordPatcher
   {
      /// <summary>
      /// Updates all the passwords in <paramref name="path"/> to v3
      /// </summary>
      public static void TwoToThreePatcher(string path, byte[] key)
      {
         EncryptedFile.FileDirectory = path;
         PasswordEntryV2.EntryDirectory = path;
         foreach (string file in Directory.GetFiles(path))
         {
            if (ulong.TryParse(Path.GetFileNameWithoutExtension(file), out ulong dump))
               continue;
            PasswordEntryV2 entry1 = new(key, file);
            entry1.Delete();
            PasswordEntry entry2 = new(key)
            {
               Domain = entry1.Domain,
               AccountName = entry1.AccountName,
               Service = entry1.Service,
               Password = entry1.Password
            };
            entry2.Save();
         }
         EncryptedFile.ReleaseIDs();
      }
   }
}
