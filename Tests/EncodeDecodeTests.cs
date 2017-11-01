using System;
using System.Diagnostics;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniStringLib;

namespace Tests
{
    /// <summary>
    ///     Tests if encoded srings can also be decoded again and the other way around.
    /// </summary>
    [TestClass]
    public class EncodeDecodeTests
    {
        public static bool PerformEncode(string str)
        {
            Debug.WriteLine("Encoding:");
            Debug.WriteLine(str);
            Debug.WriteLine(" -------------------------------- ");
            byte[] bytes = MiniString.Encode(str);
            Debug.WriteLine("Encoded decimal:");
            for (int i = 0; i < bytes.Length; i++)
            {
                byte b = bytes[i];
                if (i != 0)
                {
                    Debug.Write(" ");
                }
                Debug.Write(b);
            }
            Debug.WriteLine("");
            Debug.WriteLine("Encoded binary:");
            Debug.WriteLine(bytes.GetBitString());
            Debug.WriteLine(" -------------------------------- ");
            string decoded = MiniString.Decode(bytes);
            Debug.WriteLine("Encoded again:");
            Debug.WriteLine(decoded);
            return string.Equals(str, decoded, StringComparison.Ordinal);
        }

        [TestMethod]
        public void Encode_Hello_World()
        {
            Assert.IsTrue(PerformEncode("Hello World"));
        }
        [TestMethod]
        public void Encode_TestFile()
        {
            Assert.IsTrue(PerformEncode(File.ReadAllText("..\\..\\Properties\\test1.txt")));
        }
    }
}