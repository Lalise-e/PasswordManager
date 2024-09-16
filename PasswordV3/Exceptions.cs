//V3 seeks to expand functionality by making a base virtual class and then
//have additional classes like PasswordEntry, Text and meta stuff
using System;

namespace Password
{
	public class CriticalErrorException : Exception
	{
		public CriticalErrorException(string message) : base(message)
		{
		}
	}
	public class MissingAttributeException : Exception
	{
		public Type MissingAttribute;
		public Type Class;
		public MissingAttributeException(string message, Type missingAttribute = null, Type @class = null) : base(message)
		{
			MissingAttribute = missingAttribute;
			Class = @class;
		}
	}
	public class UnhandledTypeException : Exception
	{
		public Type unhandledType { get; set; }
		public UnhandledTypeException(string message, Type type)
		{
			unhandledType = type;
		}
	}
}
