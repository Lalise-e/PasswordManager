//Improved version of Password which aims to merge all the fields into one file
using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;

namespace Password
{
    public class PasswordEntry
    {
        /// <summary>
        /// The directory where a <see cref="PasswordEntry"/> will be saved when using the <see cref="Save()"/> method.
        /// </summary>
        public static string EntryDirectory { get; set; }
        /// <summary>
        /// The account name for the account.
        /// </summary>
        public string AccountName
        {
            get { return accountName; }
            set { accountName = value; }
        }
        /// <summary>
        /// Path of the file containing the encrypted <see cref="PasswordEntry"/>.
        /// </summary>
        public string Filename
        {
            get
            {
                byte[] bytes = SHA512.Create().ComputeHash(Encoding.Unicode.GetBytes($"{Service} {AccountName}"));
                StringBuilder builder = new(bytes.Length * 2);
                foreach (byte b in bytes)
                    builder.AppendFormat("{0:x2}", b);
                return $"{EntryDirectory}\\{builder}.entry";
            }
        }
        /// <summary>
        /// The domain name for the service. Leave as <see cref="null"/> when not applicable. 
        /// </summary>
        public Uri Domain
        {
            get
            {
                if (domain == null)
                    return null;
                return new(domain);
            }
            set
            {
                if (value == null)
                    domain = null;
                else
                    domain = value.OriginalString;
            }
        }
        /// <summary>
        /// The name of the service.
        /// </summary>
        public string Service
        {
            get { return service; }
            set { service = value; }
        }
        /// <summary>
        /// The password.
        /// </summary>
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        private string accountName { get; set; }
        private string domain { get; set; } = null;
        private string service { get; set; }
        private string password { get; set; }
        private Aes myAes { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="PasswordEntry"/> class.
        /// </summary>
        /// <param name="key">Key used for encryption and decryption. Needs to be 32 bytes long.</param>
        public PasswordEntry(byte[] key)
        {
            if (key.Length != 32)
                throw new ArgumentException("The key needs to be 32 bytes long");
            myAes = Aes.Create();
            myAes.KeySize = 256;
            myAes.Key = key;
            myAes.Mode = CipherMode.CBC;
            myAes.Padding = PaddingMode.PKCS7;
            myAes.GenerateIV();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="PasswordEntry"/> class.
        /// </summary>
        /// <param name="key">Key used for encryption and decryption. Needs to be 32 bytes long.</param>
        /// <param name="path">Directory where the files containing the enrypted data are located.</param>
        public PasswordEntry(byte[] key, string path) : this(key)
        {
            byte[] rawData = File.ReadAllBytes(path);
            myAes.IV = rawData[0..16];
            string rawString = Decrypt(rawData[16..rawData.Length]);
            string[] fields = rawString.Split('\0');
            password = fields[0];
            if (fields[1].Length != 0)
                domain = fields[1];
            else
                domain = null;
            accountName = fields[2];
            service = fields[3];
        }
        /// <summary>
        /// Generates a random password based on the parameters.
        /// </summary>
        /// <param name="length">The length of the password. Will throw an <see cref="ArgumentException"/> if it less than 10.</param>
        /// <param name="hasCapital">Leave as true if the password can have/needs capital letters.</param>
        /// <param name="hasNumerals">Leave as true if the password can have/needs numbers.</param>
        /// <param name="hasSpecial">Leave as true if the password can have/needs special characters (#, @...).</param>
        /// <param name="bannedCharacters">Use this if one of the the generated characters causes problems.</param>
        /// <returns>A random password.</returns>
        public static string GetRandomPassword(byte length = 30, bool hasCapital = true, bool hasNumerals = true, bool hasSpecial = true, params char[] bannedCharacters)
        {
            if (length < 10)
                throw new ArgumentException($"{nameof(length)} needs to be at least 10 long.");
            string letters = "abcdefghijklmnopqrstuvwxyz";
            string upperLetters = letters.ToUpper();
            string numbers = "0123456789";
            string special = "!#()?+@£$€{[]}";
            List<char> bannedList = new();
            if (bannedCharacters.Length != 0)
                bannedList = new(bannedCharacters);
            List<char> allowedCharacters = new List<char>();
            allowedCharacters.AddRange(letters.ToCharArray());
            if (CompareTextToList(letters, bannedList))
                throw new ArgumentException("All lowercase letters are banned.");
            if (hasCapital)
            {
                if (CompareTextToList(upperLetters, bannedList))
                    throw new ArgumentException("All capital letters are banned.");
                allowedCharacters.AddRange(upperLetters.ToCharArray());
            }
            if (hasNumerals)
            {
                if (CompareTextToList(numbers, bannedList))
                    throw new ArgumentException("All numbers are banned");
                allowedCharacters.AddRange(numbers.ToCharArray());
            }
            if (hasSpecial)
            {
                if (CompareTextToList(special, bannedList))
                    throw new ArgumentException("All special characters are banned.");
                allowedCharacters.AddRange(special);
            }
            Random rng = new();
        PasswordGeneration:
            string result = null;
            for (int i = 0; i < length; i++)
            {
            StartOfLoop:
                char character = allowedCharacters[rng.Next(allowedCharacters.Count)];
                if (bannedList != null)
                    if (bannedList.Contains(character))
                        goto StartOfLoop;
                result += character;
            }
            if (!CompareString(result, letters))
                goto PasswordGeneration;
            if (hasCapital)
                if (!CompareString(result, upperLetters))
                    goto PasswordGeneration;
            if (hasNumerals)
                if (!CompareString(result, numbers))
                    goto PasswordGeneration;
            if (hasSpecial)
                if (!CompareString(result, special))
                    goto PasswordGeneration;
            return result;
        }
        /// <returns>Returns <see langword="true"/> if the two strings share at least one character.</returns>
        private static bool CompareString(string firstString, string secondString)
        {
            foreach (char character in firstString.ToCharArray())
                if (secondString.Contains(character))
                    return true;
            return false;
        }
        /// <returns>Returns <see langword="true"/> if <paramref name="list"/> contains all the characters of <paramref name="text"/>.</returns>
        private static bool CompareTextToList(string text, List<char> list)
        {
            foreach (char c in text)
                if (!list.Contains(c))
                    return false;
            return true;
        }
        /// <summary>
        /// Saves the data in a subfolder of <see cref="EntryDirectory"/>.
        /// </summary>
        public void Save()
        {
            List<byte> byteList = new();
            string temp = $"{password}\0{Domain}\0{accountName}\0{service}";
            byte[] encodedString = Encoding.Unicode.GetBytes(temp);
            byteList.AddRange(encodedString);
            byte[] encrypted = Encrypt(temp);
            byteList = new(myAes.IV);
            byteList.AddRange(encrypted);
            File.WriteAllBytes(Filename, byteList.ToArray());
        }
        /// <summary>
        /// Deletes any data that's been saved to the disk if it exists.
        /// </summary>
        public void Delete()
        {
            if (File.Exists(Filename))
                File.Delete(Filename);
        }
        private byte[] Encrypt(string toBeEncrypted)
        {
            if (toBeEncrypted == null)
                throw new ArgumentNullException(nameof(toBeEncrypted));
            ICryptoTransform crypto = myAes.CreateEncryptor(myAes.Key, myAes.IV);
            byte[] result;
            using (MemoryStream stream = new())
            {
                using (CryptoStream cryptoStream = new(stream, crypto, CryptoStreamMode.Write))
                {
                    using (StreamWriter writer = new(cryptoStream))
                    {
                        writer.Write(toBeEncrypted);
                    }
                    result = stream.ToArray();
                }
            }
            return result;
        }
        private string Decrypt(byte[] encryptedInfo)
        {
            if (encryptedInfo == null)
                throw new ArgumentNullException(nameof(encryptedInfo));
            ICryptoTransform crypto = myAes.CreateDecryptor(myAes.Key, myAes.IV);
            string result = null;
            using (MemoryStream stream = new(encryptedInfo))
            {
                using (CryptoStream cryptoStream = new(stream, crypto, CryptoStreamMode.Read))
                {
                    using (StreamReader reader = new(cryptoStream))
                    {
                        result = reader.ReadToEnd();
                    }
                }
            }
            return result;
        }
    }
}
