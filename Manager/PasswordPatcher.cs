using System;
using System.IO;
using Password;
using Password_old;

namespace Manager
{
    static class PasswordPatcher
    {
        /// <summary>
        /// Updates all the passwords in <paramref name="path"/> to v2
        /// </summary>
        public static void OneToTwoPatcher(string path, byte[] key)
        {
            PasswordEntry.EntryDirectory = path;
            PasswordEntry_old.EntryDirectory = path;
            foreach (string folder in Directory.GetDirectories(path))
            {
                PasswordEntry_old entry1 = new(key, folder);
                entry1.Delete();
                PasswordEntry entry2 = new(key)
                {
                    Domain = entry1.Domain,
                    AccountName = entry1.AccountName,
                    Service = entry1.Service,
                    Password = entry1.GetPassword(key)
                };
                entry2.Save();
            }
        }
    }
}
