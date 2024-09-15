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
	internal class ProperyIDAttribute : Attribute
	{
		internal int ID { get; set; }
		internal ProperyIDAttribute(int iD)
		{
			ID = iD;
		}
	}
}
