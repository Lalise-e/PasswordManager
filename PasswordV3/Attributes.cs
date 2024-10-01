using System;

namespace Password
{
	[AttributeUsage(AttributeTargets.Class)]
	internal class ClassTypeAttribute : Attribute
	{
		internal FileType FileType { get; set; }
		internal ClassTypeAttribute(FileType type)
		{
			FileType = type;
		}
	}
	[AttributeUsage(AttributeTargets.Property)]
	internal class PropertyIDAttribute : Attribute
	{
		internal short ID { get; set; }
		internal PropertyIDAttribute(short id)
		{
			ID = id;
		}
	}
	/// <summary>
	/// Apply this to properties that you want <see cref="MetaEntry"/> to serialize.</br>
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class MetaSerializableAttribute : Attribute
	{
		public string Name;
		public MetaSerializableAttribute(string name)
		{
			if(string.IsNullOrEmpty(name))
				throw new ArgumentNullException("name");
			Name = name;
		}
	}
}
