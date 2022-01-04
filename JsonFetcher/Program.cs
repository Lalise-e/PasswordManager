using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Password;

namespace JsonFetcher
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
                goto Fail;
            if (!Uri.TryCreate(args[1], UriKind.Absolute, out Uri url))
                goto Fail;

            PasswordEntry mainEntry;
            byte[] key = getHash(args[0]);
            List<PasswordEntry> entries = new(); 
            foreach(string path in Directory.GetDirectories($"{Directory.GetCurrentDirectory()}\\Entries"))
            {
                PasswordEntry entry;
                try { entry = new(key, path); }
                catch { continue; }
                entries.Add(entry);
            }
            if (!entries.Exists(x => x.Domain.Authority == url.Authority))
                goto Fail;
            mainEntry = entries.Find(x => x.Domain.Authority == url.Authority);
            Bundle bundle;
            
            try { bundle = new(mainEntry, key); }
            catch { goto Fail; }

            Console.WriteLine(JsonSerializer.Serialize(bundle));
            return;

            Fail:
            Console.WriteLine(JsonSerializer.Serialize(new Bundle()));
            return;
        }
        private static byte[] getHash(string text) => SHA256.Create().ComputeHash(Encoding.Unicode.GetBytes(text));
    }
    [Serializable]
    internal class Bundle
    {
        public string Username { get; }
        public string Password { get; }
        public bool Success { get; }
        public Bundle()
        {
            Success = false;
        }
        public Bundle(PasswordEntry entry, byte[] key)
        {
            Username = entry.AccountName;
            Password = entry.GetPassword(key);
            Success = true;
        }
    }
}
