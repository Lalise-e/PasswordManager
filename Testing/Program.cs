using System;
using System.IO;
using Password;
using System.Reflection;
namespace Testing
{
	internal class Program
	{
		static void Main(string[] args)
		{
			byte[] key = new byte[32];
			for (int i = 0; i < key.Length; i++)
			{
				key[i] = (byte)i;
			}
			EncryptedFile.FileDirectory = Directory.GetCurrentDirectory() + @"\EE";
			object o = new Person()
			{
				Name = "Alice",
				Age = 24,
				Gender = "Woman"
			};
			string t = o.GetType().AssemblyQualifiedName;
			Console.WriteLine(t);
			Type type = Type.GetType(t);
			Console.WriteLine(type.FullName);
		}
	}
	internal class Person
	{
		public string Name { get; set; }
		public short Age { get; set; }
		public string Gender { get; set; }
	}
}
