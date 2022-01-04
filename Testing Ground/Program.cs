using System;
using Password;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Testing_Ground
{
    class Program
    {
        internal static byte[] GetHash(string text) => SHA256.Create().ComputeHash(Encoding.Unicode.GetBytes(text));
    }
}
