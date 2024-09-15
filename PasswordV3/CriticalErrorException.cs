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
}
