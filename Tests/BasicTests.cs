using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniStringLib;

namespace Tests
{
    [TestClass]
    public class BasicTests
    {
        [TestMethod]
        public void Decode_1()
        {
            Assert.AreEqual(
                MiniString.Decode(new byte[] { 1 }).WriteLine(),
                "1".WriteLine());
        }

        [TestMethod]
        public void Decode_HELLO_WoRld()
        {
            Assert.AreEqual(
                MiniString.Decode(new byte[] { 210, 99, 89, 217, 31, 206, 28, 140, 2 }).WriteLine(),
                "HELLO_WoRld".WriteLine());
        }

        [TestMethod]
        public void Encode_1()
        {
            Assert.AreEqual(
                MiniString.Encode("1").GetBitString().WriteLine(),
                "0000_0001".WriteLine());
        }

        [TestMethod]
        public void Encode_01()
        {
            Assert.AreEqual(
                MiniString.Encode("01").GetBitString().WriteLine(),
                "0100_1010 0000_0000".WriteLine());
        }

        [TestMethod]
        public void Encode_11()
        {
            Assert.AreEqual(
                MiniString.Encode("11").GetBitString().WriteLine(),
                "0100_0001 0000_0000".WriteLine());
        }

        [TestMethod]
        public void Encode_3746()
        {
            Assert.AreEqual(
                MiniString.Encode("3746").GetBitString().WriteLine(),
                "1100_0011 0100_0001 0001_1000".WriteLine());
        }

        [TestMethod]
        public void Encode_ABC()
        {
            Assert.AreEqual(
                MiniString.Encode("ABC").GetBitString().WriteLine(),
                "0000_1011 1101_0011 0000_0000".WriteLine());
        }

        [TestMethod]
        public void Encode_HELLOWORLD()
        {
            Assert.AreEqual(
                MiniString.Encode("HELLOWORLD").GetBitString().WriteLine(),
                "1101_0010 0110_0011 0101_1001 0101_1001 1001_1000 0111_0001 1001_0110 0000_0011".WriteLine());
        }

        [TestMethod]
        public void Encode_1234567890()
        {
            Assert.AreEqual(
                MiniString.Encode("1234567890").GetBitString().WriteLine(),
                "1000_0001 0011_0000 0001_0000 1000_0101 0111_0001 0010_0000 1000_1001 0000_0010".WriteLine());
        }

        [TestMethod]
        public void Encode_ABCDEFGIJKLMNOPQRSTUVXYZ()
        {
            Assert.AreEqual(
                MiniString.Encode("ABCDEFGIJKLMNOPQRSTUVXYZ").GetBitString().WriteLine(),
                "0000_1011 1101_0011 0011_1000 0000_1111 0001_0100 0100_1101 0101_0100 0110_0101 0101_1101 0101_1000 1010_0110 0110_1101 0101_1100 1110_0111 0111_1101 1010_0000 0011_1000 1001_0010".WriteLine());
        }

        [TestMethod]
        public void Encode_WeirdStuff()
        {
            Assert.AreEqual(
                MiniString.Encode("öäüß*abc123$%&/%").GetBitString().WriteLine(),
                "0000_0000 1111_0110 0000_0000 0000_0000 1110_0100 0000_0000 0000_0000 1111_1100 0000_0000 0000_0000 1101_1111 0000_0000 0000_0000 0010_1010 0000_0000 1010_0101 0111_1001 0000_0110 1100_0010 0000_0000 0100_0000 0000_0010 0000_0000 0101_0000 0000_0010 0000_0000 0110_0000 0000_0010 0000_0000 1111_0000 0000_0010 0000_0000 0101_0000 0000_0010 0000_0000".WriteLine());
        }
    }
}