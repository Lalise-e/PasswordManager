//V3 seeks to expand functionality by making a base virtual class and then
//have additional classes like PasswordEntry, Text and meta stuff
using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Collections.Generic;
using System.Reflection;

namespace Password
{
	/// <summary>
	/// Abstract class used for managing various types of encrypted files.
	/// Use <see cref="GetFile(byte[], string)"/> to load a file from disk.
	/// </summary>
	public abstract class EncryptedFile
	{
		/// <summary>
		/// The directory where an <see cref="EncryptedFile"/> and its derived classes will be saved when using the <see cref="Save()"/> method.
		/// </summary>
		public static string FileDirectory { get; set; }
		public abstract FileType FileType { get; }
		/// <summary>
		/// ID of the <see cref="EncryptedFile"/>.
		/// </summary>
		public ulong ID { get { return _id; } }
		private ulong _id;
		private static List<ulong> _takenIDs = null;
		private static Random _rng = null;
		private static bool classesLoaded = false;
		private static Dictionary<FileType, Type> enumKey = new Dictionary<FileType, Type>();
		private static Dictionary<Type, FileType> typeKey = new Dictionary<Type, FileType>();
		private static Dictionary<Type, PropertyInfo[]> propertiesToSave = new Dictionary<Type, PropertyInfo[]>();
		/// <summary>
		/// Path of the file containing the encrypted <see cref="PasswordEntry"/>.
		/// </summary>
		public string Filename
		{
			get
			{
				return $"{FileDirectory}\\{_id}.entry";
			}
		}
		protected Aes myAes { get; set; }
		/// <summary>
		/// Initializes a new instance of the <see cref="PasswordEntry"/> class.
		/// </summary>
		/// <param name="key">Key used for encryption and decryption. Needs to be 32 bytes long.</param>
		protected EncryptedFile(byte[] key)
		{
			myAes = GenerateAES(key);
			NewID();
		}
		/// <summary>
		/// This should be the same as <see cref="GetFileContent"/> but backwards.
		/// </summary>
		/// <param name="aes"></param>
		/// <param name="content"></param>
		protected EncryptedFile(Aes aes, string content)
		{
			myAes = aes;
		}
		/// <summary>
		/// Generates an <see cref="Aes"/>.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		/// <exception cref="ArgumentException"></exception>
		protected static Aes GenerateAES(byte[] key)
		{
			if (key.Length != 32)
				throw new ArgumentException("The key needs to be 32 bytes long");
			Aes localAes;
			localAes = Aes.Create();
			localAes.KeySize = 256;
			localAes.Key = key;
			localAes.Mode = CipherMode.CBC;
			localAes.Padding = PaddingMode.PKCS7;
			localAes.GenerateIV();
			return localAes;
		}
		/// <summary>
		/// Initializes a new instance of a class inherited from <see cref="EncryptedFile"/>.
		/// </summary>
		/// <param name="key">Key used for encryption and decryption. Needs to be 32 bytes long.</param>
		/// <param name="path">Directory where the files containing the enrypted data are located.</param>
		public static EncryptedFile GetFile(byte[] key, string path)
		{
			EncryptedFile result = null;
			Aes aes = GenerateAES(key);
			byte[] rawData = File.ReadAllBytes(path);
			aes.IV = rawData[0..16];
			string rawString = Decrypt(rawData[16..rawData.Length], aes);
			FileType ft;
			if (int.TryParse(rawString[0].ToString(), out int intType))
				ft = (FileType)intType;
			else
				throw new IOException("Invalid file format, the string needs to lead with a number");
			switch (ft)
			{
				case FileType.PasswordFile:
					result = new PasswordEntry(aes, rawString[1..]);
					break;
				case FileType.TextFile:
					result = new TextEntry(aes, rawString[1..]);
					break;
				case FileType.GenericFile:
					result = new FileEntry(aes, rawString[1..]);
					break;
				default:
					throw new NotImplementedException($"filetype {ft} is not supported");
			}
			string name = Path.GetFileNameWithoutExtension(path);
			if (ulong.TryParse(name, out ulong id))
				result.SetID(id);
			else
				throw new Exception($"File {path} has an invalid file name, it needs to be a number between 0 and {ulong.MaxValue}");
			return result;
		}
		/// <summary>
		/// Saves the data in <see cref="FileDirectory"/>.
		/// </summary>
		public virtual void Save()
		{
			List<byte> byteList = new();
			string content = GetFileContent();
			byte[] encrypted = Encrypt((int)FileType + content);
			byteList = new(myAes.IV);
			byteList.AddRange(encrypted);
			File.WriteAllBytes(Filename, byteList.ToArray());
		}
		/// <summary>
		/// Deletes any data that's been saved to the disk if it exists.
		/// </summary>
		public virtual void Delete()
		{
			if (File.Exists(Filename))
				File.Delete(Filename);
		}
		private string GetFileContent()
		{
			LoadClasses();
			return null;
		}
		protected string ToBase64(string text)
		{
			if (string.IsNullOrEmpty(text))
				return "";
			byte[] bytes = Encoding.UTF8.GetBytes(text);
			return Convert.ToBase64String(bytes);
		}
		protected string FromBase64(string encodedString)
		{
			if (string.IsNullOrEmpty(encodedString))
				return "";
			byte[] bytes = Convert.FromBase64String(encodedString);
			return Encoding.UTF8.GetString(bytes);
		}
		/// <summary>
		/// Be very careful when touching this as it can result in data loss.
		/// </summary>
		protected void NewID()
		{
			if (_rng == null)
				_rng = new Random();
			if (_takenIDs == null)
				_takenIDs = new List<ulong>();
			ulong potentialID = 0;
			byte[] bytes = new byte[8];
			while (true)
			{
				_rng.NextBytes(bytes);
				potentialID = BitConverter.ToUInt64(bytes);
				if (potentialID == 0)
					continue;
				if (!_takenIDs.Contains(potentialID))
					break;
			}
			_takenIDs.Add(potentialID);
			_id = potentialID;
		}
		/// <summary>
		/// Be very careful when touching this as it can result in data loss.
		/// </summary>
		protected void SetID(ulong id)
		{
			if (_takenIDs == null)
				_takenIDs = new List<ulong>();
			if (id == 0)
				throw new ArgumentException("0 is not a valid argument", nameof(id));
			if (_takenIDs.Contains(id))
				throw new CriticalErrorException("Two files have the same ID, probably caused by making files before old files were loaded. Do not continue running or risk overwriting data.");
			if (_takenIDs.Contains(_id))
				_takenIDs.Remove(_id);
			_takenIDs.Add(id);
			_id = id;
		}

		protected byte[] Encrypt(string toBeEncrypted)
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
		protected string Decrypt(byte[] encryptedInfo)
		{
			return Decrypt(encryptedInfo, myAes);
		}
		private static string Decrypt(byte[] encryptedInfo, Aes aes)
		{
			if (encryptedInfo == null)
				throw new ArgumentNullException(nameof(encryptedInfo));
			ICryptoTransform crypto = aes.CreateDecryptor(aes.Key, aes.IV);
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
		public static void LoadClasses()
		{
			if(classesLoaded)
				return;
			classesLoaded = true;
			Type localType = typeof(EncryptedFile);
			Assembly ass = Assembly.GetAssembly(localType);
			Type[] ttt = ass.GetTypes();
			List<PropertyInfo> properties;
			PropertyIDAttribute id;
			ClassTypeAttribute attribute = null;
			for (int i = 0; i < ttt.Length; i++)
			{
				if (!ttt[i].IsSubclassOf(typeof(EncryptedFile)))
					continue;
				attribute = ttt[i].GetCustomAttribute<ClassTypeAttribute>();
				if (attribute == null)
					throw new MissingAttributeException($"type {ttt[i].Name} is missing attribute ClassType",
						typeof(ClassTypeAttribute), ttt[i]);
				enumKey.Add(attribute.FileType, ttt[i]);
				typeKey.Add(ttt[i],attribute.FileType);
				properties = new();
				PropertyInfo[] infos = ttt[i].GetProperties();
				for (int j = 0; j < infos.Length; j++)
				{
					id = infos[j].GetCustomAttribute<PropertyIDAttribute>();
					if(id == null)
						continue;
					properties.Add(infos[j]);
				}
				properties.Sort((o, n) => o.GetCustomAttribute<PropertyIDAttribute>().ID.CompareTo(n.GetCustomAttribute<PropertyIDAttribute>().ID));
				propertiesToSave.Add(ttt[i], properties.ToArray());
			}
		}
	}
	/// <summary>
	/// Class inherited from <see cref="EncryptedFile"/> contains properties and fields for storing passwords.
	/// </summary>
	[ClassType(FileType.PasswordFile)]
	public class PasswordEntry : EncryptedFile
	{
		/// <summary>
		/// The account name for the account.
		/// </summary>
		[PropertyID(2)]
		public string AccountName
		{
			get { return accountName; }
			set { accountName = value; }
		}
		/// <summary>
		/// The email for the account.
		/// </summary>
		[PropertyID(3)]
		public string Email
		{
			get { return email; }
			set { email = value; }
		}
		/// <summary>
		/// The domain name for the service. Leave as <see cref="null"/> when not applicable. 
		/// </summary>
		[PropertyID(4)]
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
		[PropertyID(0)]
		public string Service
		{
			get { return service; }
			set { service = value; }
		}
		/// <summary>
		/// The password.
		/// </summary>
		[PropertyID(1)]
		public string Password
		{
			get { return password; }
			set { password = value; }
		}
		private string accountName { get; set; }
		private string email { get; set; }
		private string domain { get; set; } = null;
		private string service { get; set; }
		private string password { get; set; }

		public override FileType FileType { get { return FileType.PasswordFile; } }

		public PasswordEntry(byte[] key) : base(key)
		{

		}
		internal PasswordEntry(Aes aes, string content) : base(aes, content)
		{
			string[] fields = content.Split('\0');
			password = FromBase64(fields[0]);
			service = FromBase64(fields[1]);
			if (fields[2].Length != 0)
				domain = FromBase64(fields[2]);
			else
				domain = null;
			accountName = FromBase64(fields[3]);
			email = FromBase64(fields[4]);
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
	}
	/// <summary>
	/// Class inherited from <see cref="EncryptedFile"/>, used to store plain text.
	/// </summary>
	[ClassType(FileType.TextFile)]
	public class TextEntry : EncryptedFile
	{
		[PropertyID(0)]
		public string Text { get; set; }
		[PropertyID(1)]
		public string Title { get; set; }
		public override FileType FileType { get { return FileType.TextFile; } }
		public TextEntry(byte[] key) : base(key)
		{

		}
		internal TextEntry(Aes aes, string content) : base(aes, content)
		{
			string[] encodedStrings = content.Split('\0', StringSplitOptions.None);
			Title = FromBase64(encodedStrings[0]);
			Text = FromBase64(encodedStrings[1]);
		}
	}
	/// <summary>
	/// Not implemented yet
	/// </summary>
	[ClassType(FileType.MetaFile)]
	public class MetaEntry : EncryptedFile
	{
		public MetaEntry(byte[] key) : base(key)
		{

		}
		internal MetaEntry(Aes aes, string content) : base(aes, content)
		{

		}
		public override FileType FileType {get { return FileType.MetaFile; } }
	}
	/// <summary>
	/// Class inherited from <see cref="EncryptedFile"/>, used to ecrypt and store files of any kind.
	/// Saves the encrypted file in a sub directory in <see cref="EncryptedFile.FileDirectory"/>.
	/// </summary>
	[ClassType(FileType.GenericFile)]	
	public class FileEntry : EncryptedFile
	{
		[PropertyID(0)]
		public string OriginalFileName
		{
			get { return _filename; }
			set { _filename = value; }
		}

		public string FileSource { get; set; }
		[PropertyID(1)]
		internal Aes innerAes
		{
			get { return _aes; }
			set { _aes = value; }
		}
		private Aes _aes;
		private string _filename;
		private string _path { get { return $"{_subPath}\\{Convert.ToHexString(BitConverter.GetBytes(ID))}.dump"; } }
		private string _subPath { get { return $"{FileDirectory}\\Files"; } }
		public override FileType FileType { get{ return FileType.GenericFile; } }
		public FileEntry(byte[] key) : base(key)
		{
			byte[] hiddenKey = new byte[32];
			Random rng = new Random();
			rng.NextBytes(hiddenKey);
			_aes = GenerateAES(hiddenKey);
		}
		internal FileEntry(Aes aes, string content) : base(aes, content)
		{
			string[] strings = content.Split('\0');
			_aes = GenerateAES(Convert.FromBase64String(strings[0]));
			_aes.IV = Convert.FromBase64String(strings[1]);
			_filename = strings[2];
		}
		public override void Save()
		{
			if (!File.Exists(FileSource))
				throw new FileNotFoundException("File not found", FileSource);
			if (!Directory.Exists(_subPath))
			{
				Directory.CreateDirectory(_subPath);
				DirectoryInfo info = new DirectoryInfo(_subPath);
				info.Attributes = FileAttributes.Hidden;
			}
			ICryptoTransform crypto = _aes.CreateEncryptor(_aes.Key, _aes.IV);
			FileStream sourceStream = new(FileSource, FileMode.Open, FileAccess.Read);
			using (FileStream stream = new(_path, FileMode.Create, FileAccess.Write))
			{
				using (CryptoStream cryptoStream = new(stream, crypto, CryptoStreamMode.Write))
				{
					sourceStream.CopyTo(cryptoStream);
				}
			}
			sourceStream.Close();
			sourceStream.Dispose();
			_filename = Path.GetFileName(FileSource);
			base.Save();
		}
		public override void Delete()
		{
			if(File.Exists(_path))
				File.Delete(_path);
			base.Delete();
		}
		/// <summary>
		/// Decrypts and exports the file.
		/// </summary>
		/// <param name="exportLocation">The location that the file will be saved in</param>
		public void Export(string exportLocation)
		{
			if (!Path.HasExtension(exportLocation))
			{
				if (Path.EndsInDirectorySeparator(exportLocation))
					exportLocation += OriginalFileName;
				else
					exportLocation += '\\' + OriginalFileName;
			}
			ICryptoTransform crypto = _aes.CreateDecryptor(_aes.Key, _aes.IV);
			FileStream locationStream = new(exportLocation, FileMode.Create, FileAccess.Write);
			using(FileStream stream = new(_path, FileMode.Open, FileAccess.Read))
			{
				using(CryptoStream cryptoStream = new(stream, crypto, CryptoStreamMode.Read))
				{
					cryptoStream.CopyTo(locationStream);
				}
			}
			locationStream.Close();
			locationStream.Dispose();
		}
	}
}
