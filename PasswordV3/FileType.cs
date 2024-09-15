//V3 seeks to expand functionality by making a base virtual class and then
//have additional classes like PasswordEntry, Text and meta stuff
namespace Password
{
	public enum FileType
	{
		PasswordFile = 0,
		TextFile = 1,
		MetaFile = 2,
		GenericFile = 3,
		OtherFile = 4
	}
}
