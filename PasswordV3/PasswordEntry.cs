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
		[PropertyID(short.MinValue)]
		public string Version
		{
			get { return _version; }
			set { return; }
		}
		private const string _version = "3.0";
		private const string _nullchar = "\u0003";
		/// <summary>
		/// The directory where an <see cref="EncryptedFile"/> and its derived classes will be saved when using the <see cref="Save()"/> method.
		/// </summary>
		public static string FileDirectory { get; set; }
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
		protected EncryptedFile(Aes aes, string content)
		{
			myAes = aes;
			Type localType = GetType();
			PropertyInfo[] Infos = propertiesToSave[localType];
			Dictionary<short, PropertyInfo> shortKey = new Dictionary<short, PropertyInfo>(Infos.Length);
			for (int i = 0; i < Infos.Length; i++)
			{
				shortKey.Add(Infos[i].GetCustomAttribute<PropertyIDAttribute>().ID, Infos[i]);
			}
			string[] encodedProperties = content.Split('\0');
			string[] idAndProp;
			for (int i = 0; i < encodedProperties.Length; i++)
			{
				idAndProp = encodedProperties[i].Split((char)0x02);
				PropertyInfo info = shortKey[Base64ToShort(idAndProp[0])];
				Type pType = info.PropertyType;
				if(pType == typeof(string))
				{ 
					info.SetValue(this, Base64ToText(idAndProp[1]));
					continue;
				}
				if(pType == typeof(Aes))
				{
					info.SetValue(this, Base64ToAes(idAndProp[1]));
					continue;
				}
				if(pType == typeof(Uri))
				{
					info.SetValue(this, Base64ToUri(idAndProp[1]));
					continue;
				}
				if(pType == typeof(bool))
				{
					info.SetValue(this, StringToBool(idAndProp[1]));
					continue;
				}
				throw new UnhandledTypeException("Decoding for type is not handled.", pType);
			}
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
			LoadClasses();
			EncryptedFile? result = null;
			Aes aes = GenerateAES(key);
			byte[] rawData = File.ReadAllBytes(path);
			aes.IV = rawData[0..16];
			string rawString = Decrypt(rawData[16..rawData.Length], aes);
			FileType ft;
			if (int.TryParse(rawString[0].ToString(), out int intType))
				ft = (FileType)intType;
			else
				throw new IOException("Invalid file format, the string needs to lead with a number");
			ConstructorInfo cInfo = enumKey[ft].GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance,
				null, new Type[] { typeof(Aes), typeof(string) }, null);
			result = cInfo.Invoke(new object[] { aes, rawString[1..] }) as EncryptedFile;
			string name = Path.GetFileNameWithoutExtension(path);
			if (ulong.TryParse(name, out ulong id))
				result.SetID(id);
			else
				throw new Exception($"File {path} has an invalid file name, it needs to be a number between 0 and {ulong.MaxValue}");
			return result;
		}
		public void ChangeKey(byte[] newKey)
		{
			myAes = GenerateAES(newKey);
			Save();
		}
		/// <summary>
		/// Saves the data in <see cref="FileDirectory"/>.
		/// </summary>
		public virtual void Save()
		{
			if (_id == 0)
				return;
			List<byte> byteList = new();
			string content = GetFileContent();
			byte[] encrypted = Encrypt((int)typeKey[GetType()] + content);
			byteList = new(myAes.IV);
			byteList.AddRange(encrypted);
			if(!Directory.Exists(FileDirectory))
				Directory.CreateDirectory(FileDirectory);
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
		/// <summary>
		/// Encodes a derived class in base64 strings where the properties are seperated by a null character.
		/// </summary>
		/// <exception cref="MissingAttributeException"></exception>
		/// <exception cref="UnhandledTypeException"></exception>
		protected virtual string GetFileContent()
		{
			string result = "";
			LoadClasses();
			PropertyInfo[] infos;
			if (!propertiesToSave.ContainsKey(GetType()))
				throw new MissingAttributeException("Missing attribute", typeof(ClassTypeAttribute), GetType());
			infos = propertiesToSave[GetType()];
			for (int i = 0; i < infos.Length; i++)
			{
				if (i != 0)
					result += '\0';
				PropertyInfo info = infos[i];
				object ob = info.GetValue(this, null);
				if (ob == null)
				{
					result += $"{ShortToBase64(info.GetCustomAttribute<PropertyIDAttribute>().ID)}{(char)0x02}{(char)0x03}";
					continue;
				}
				result += ShortToBase64(info.GetCustomAttribute<PropertyIDAttribute>().ID) + (char)0x02;
				Type objectType = ob.GetType();
				if (objectType == typeof(string))
				{
					result += TextToBase64(ob as string);
					continue;
				}
				if (typeof(Aes).IsAssignableFrom(objectType))
				{
					result += AesToBase64(ob as Aes);
					continue;
				}
				if(objectType == typeof(Uri))
				{
					result += UriToBase64(ob as Uri);
					continue;
				}
				if(objectType == typeof(bool))
				{
					result += BoolToString((bool)ob);
					continue;
				}
				throw new UnhandledTypeException("Encoding for type is not handled.", objectType);
			}
			return result;
		}
		protected string TextToBase64(string text)
		{
			if (string.IsNullOrEmpty(text))
				return ((char)0x03).ToString();
			byte[] bytes = Encoding.UTF8.GetBytes(text);
			return Convert.ToBase64String(bytes);
		}
		protected string Base64ToText(string encodedString)
		{
			if (encodedString == ((char)0x03).ToString())
				return "";
			byte[] bytes = Convert.FromBase64String(encodedString);
			return Encoding.UTF8.GetString(bytes);
		}
		protected string ShortToBase64(short value)
		{
			return ByteArrayToBase64(BitConverter.GetBytes(value));
		}
		protected short Base64ToShort(string encodedString)
		{
			return BitConverter.ToInt16(Base64ToByteArray(encodedString));
		}
		protected string BoolToString(bool value)
		{
			//I am god and you cannot tell me what to do, z is now true and w is now false.
			if (value)
				return "z";
			return "w";
		}
		protected bool StringToBool(string encodedBool)
		{
			if(encodedBool == "z")
				return true;
			if (encodedBool == "w")
				return false;
			throw new ArgumentException("Argument needs to be z or w", nameof(encodedBool));
		}
		protected string ByteArrayToBase64(byte[] bytes)
		{
			string result = "";
			if (bytes == null)
				return result;
			if(bytes.Length == 0)
				return result;
			result = Convert.ToBase64String(bytes);
			return result;
		}
		protected byte[] Base64ToByteArray(string text)
		{
			if(string.IsNullOrEmpty(text))
				return new byte[0];
			return Convert.FromBase64String(text);
		}
		protected string AesToBase64(Aes aes)
		{
			if (aes == null)
				return _nullchar;
			string result = "";
			result += ByteArrayToBase64(aes.IV);
			result += (char)0x01;
			result += ByteArrayToBase64(aes.Key);
			return result;
		}
		protected Aes Base64ToAes(string text)
		{
			if(text == _nullchar)
				return null;
			string[] temp = text.Split((char)0x01);
			Aes result = GenerateAES(Base64ToByteArray(temp[1]));
			result.IV = Base64ToByteArray(temp[0]);
			return result;
		}
		protected string UriToBase64(Uri uri)
		{
			if (uri == null)
				return _nullchar;
			return TextToBase64(uri.OriginalString);
		}
		protected Uri Base64ToUri(string text)
		{
			if (text == _nullchar)
				return null;
			return new Uri(Base64ToText(text));
		}
		/// <summary>
		/// This releases all IDs and should only be used when you know all
		/// instances of <see cref="EncryptedFile"/> are gonna be released from memory.
		/// </summary>
		public static void ReleaseIDs()
		{
			_takenIDs = null;
		}
		/// <summary>
		/// Releases current ID and sets it to be 0.
		/// </summary>
		public void ReleaseID()
		{
			_takenIDs.Remove(_id);
			_id = 0;
		}
		/// <summary>
		/// Be very careful when touching this as it can result in data loss.<br></br>
		/// Generates a new random ID.
		/// </summary>
		public void NewID()
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
		private static void LoadClasses()
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
				PropertyInfo[] infos = ttt[i].GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
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
			get { return _email; }
			set { _email = value; }
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
		private string _email { get; set; }
		private string domain { get; set; } = null;
		private string service { get; set; }
		private string password { get; set; }
		public PasswordEntry(byte[] key) : base(key)
		{

		}
		internal PasswordEntry(Aes aes, string content) : base(aes, content)
		{

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
		/// <summary>
		/// The text body.
		/// </summary>
		[PropertyID(0)]
		public string Text { get; set; }
		/// <summary>
		/// The title for the text.
		/// </summary>
		[PropertyID(1)]
		public string Title { get; set; }
		public TextEntry(byte[] key) : base(key)
		{

		}
		internal TextEntry(Aes aes, string text) : base(aes, text)
		{

		}
	}
	/// <summary>
	/// This is specific to the Windows Forms app.<br></br>
	/// I really do not like putting it here but I am lazy and not having it in<br></br>
	/// this assembly will cause problem or require extensive rewrites.
	/// </summary>
	[ClassType(FileType.MetaFile)]
	public class MetaEntry : EncryptedFile
	{
		/// <summary>
		/// Whether or not to delete the <see cref="FileEntry"/> after exporting.
		/// </summary>
		[PropertyID(0)]
		public bool DeleteExport { get; set; } = false;
		/// <summary>
		/// Whether or not to delete the the original file after importing a <see cref="FileEntry"/>.
		/// </summary>
		[PropertyID(1)]
		public bool DeleteImport { get; set; } = false;
		public MetaEntry(byte[] key) : base(key)
		{

		}
		internal MetaEntry(Aes aes, string content) : base(aes, content)
		{

		}
	}
	/// <summary>
	/// Class inherited from <see cref="EncryptedFile"/> and contains methods and properties to manage an encrypted file.<br></br>
	/// Use <see cref="Import()"/> and <see cref="Export(string)"/> to Encrypt/Decrypt target file.
	/// </summary>
	[ClassType(FileType.GenericFile)]	
	public class FileEntry : EncryptedFile
	{
		/// <summary>
		/// The name of the encrypted file.
		/// </summary>
		[PropertyID(0)]
		public string OriginalFileName
		{
			get { return _filename; }
			set { _filename = value; }
		}
		/// <summary>
		/// The file that will be encrypted.
		/// </summary>
		public string FileSource { get; set; }
		[PropertyID(1)]
		internal Aes InnerAes
		{
			get { return _aes; }
			set { _aes = value; }
		}
		private Aes _aes;
		/// <summary>
		/// The size of the file before it got encrypted.
		/// </summary>
		[PropertyID(2)]
		public string OriginalSize { get; set; }
		/// <summary>
		/// The size of the encrypted file.
		/// </summary>
		[PropertyID(3)]
		public string EncryptedSize { get; set; }
		private string _filename;
		private string _path { get { return $"{_subPath}\\{Convert.ToHexString(BitConverter.GetBytes(ID))}.dump"; } }
		private string _subPath { get { return $"{FileDirectory}\\Files"; } }
		/// <summary>
		/// Creates a new instance of <see cref="FileEntry"/>.
		/// </summary>
		/// <param name="key">The encryption key, needs to be 32 bytes.</param>
		public FileEntry(byte[] key) : base(key)
		{
			byte[] hiddenKey = new byte[32];
			Random rng = new Random();
			rng.NextBytes(hiddenKey);
			_aes = GenerateAES(hiddenKey);
		}
		internal FileEntry(Aes aes, string content) : base(aes, content)
		{

		}
		public override void Delete()
		{
			if(File.Exists(_path))
				File.Delete(_path);
			base.Delete();
		}
		public static string GetFileSize(string file)
		{
			string prefix = "B";
			char[] prefixes = new char[] { 'k', 'M', 'G', 'T', 'P', 'E' };
			double divisor = 1;
			long length = new FileInfo(file).Length;
			double exponent = Math.Log2(length);
			divisor = Math.Pow(2, ((int)exponent / 10) * 10);
			if (exponent >= 10)
				prefix = prefixes[((int)exponent / 10) - 1] + prefix;
			return string.Format("{1} {0:00}", prefix, Math.Round(length / divisor, 2));
		}
		/// <summary>
		/// Encrypts <see cref="FileSource"/> and then executes <see cref="EncryptedFile.Save"/>
		/// </summary>
		public void Import() => Import(FileSource);
		/// <summary>
		/// Encrypts the target file and then executes <see cref="EncryptedFile.Save"/>.
		/// </summary>
		/// <param name="src">The source file to be encrypted, also sets <see cref="FileSource"/> to be this.</param>
		/// <exception cref="FileNotFoundException"></exception>
		public void Import(string src)
		{
			FileSource = src;
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
			OriginalSize = GetFileSize(FileSource);
			EncryptedSize = GetFileSize(_path);
		}
		/// <summary>
		/// Decrypts and exports the file, to target location.
		/// </summary>
		/// <param name="exportLocation">The location that the file will be saved in</param>
		public void Export(string exportLocation)
		{
			if (!Path.HasExtension(exportLocation) || Directory.Exists(exportLocation))
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