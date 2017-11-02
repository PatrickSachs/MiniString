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
            Debug.WriteLine("Decoded again:");
            Debug.WriteLine(decoded);
            return string.Equals(str, decoded, StringComparison.Ordinal);
        }

        [TestMethod]
        public void Encode_Hello_World()
        {
            Assert.IsTrue(PerformEncode("Hello_World"));
        }
        [TestMethod]
        public void Encode_MiniString()
        {
            Assert.IsTrue(PerformEncode("MiniString"));
        }
        [TestMethod]
        public void Encode_Underscore()
        {
            Assert.IsTrue(PerformEncode("_"));
        }
        [TestMethod]
        public void Encode_1()
        {
            Assert.IsTrue(PerformEncode("1"));
        }
        [TestMethod]
        public void Encode_7()
        {
            Assert.IsTrue(PerformEncode("7"));
        }
        [TestMethod]
        public void Encode_A()
        {
            Assert.IsTrue(PerformEncode("A"));
        }
        [TestMethod]
        public void Encode_X()
        {
            Assert.IsTrue(PerformEncode("X"));
        }
        [TestMethod]
        public void Encode_Z()
        {
            Assert.IsTrue(PerformEncode("Z"));
        }
        [TestMethod]
        public void Encode_a()
        {
            Assert.IsTrue(PerformEncode("a"));
        }
        [TestMethod]
        public void Encode_c()
        {
            Assert.IsTrue(PerformEncode("c"));
        }
        [TestMethod]
        public void Encode_i()
        {
            Assert.IsTrue(PerformEncode("i"));
        }
        [TestMethod]
        public void Encode_z()
        {
            Assert.IsTrue(PerformEncode("z"));
        }
        [TestMethod]
        public void Encode_ä()
        {
            Assert.IsTrue(PerformEncode("ä"));
        }
        [TestMethod]
        public void Encode_CJK()
        {
            Assert.IsTrue(PerformEncode("𤽜"));
        }
        [TestMethod]
        public void Encode_TestFile1()
        {
            Assert.IsTrue(PerformEncode(File.ReadAllText("..\\..\\Properties\\test1.txt")));
        }
        [TestMethod]
        public void Encode_TestFile2()
        {
            Assert.IsTrue(PerformEncode(File.ReadAllText("..\\..\\Properties\\test2.txt")));
        }
        [TestMethod]
        public void Encode_TestFile3()
        {
            Assert.IsTrue(PerformEncode(File.ReadAllText("..\\..\\Properties\\test3.txt")));
        }
    }
}