using System;

namespace Password
{
	internal class ClassTypeAttribute : Attribute
	{
		internal FileType FileType { get; set; }
		internal ClassTypeAttribute(FileType type)
		{
			FileType = type;
		}
	}
	internal class PropertyIDAttribute : Attribute
	{
		internal int ID { get; set; }
		internal PropertyIDAttribute(int iD)
		{
			ID = iD;
		}
	}
}
