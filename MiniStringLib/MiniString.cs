using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MiniStringLib
{
    /// <summary>
    ///     MiniStrings are 6 bit encoded strings.<br />
    ///     The following characters are supported: <c>0-9, A-Z, a-z, _, NULL</c><br />
    ///     Despite the very narrow native character support unicode characters can be encoded in a MiniString, however they
    ///     require 24 bits instead of 6.<br />
    ///     When should you use the MiniString? The MiniString is mainly meant for encoded strings in a scenario when every bit
    ///     counts. This might be when sending data over the network or in very big files. Due to its character limitation it
    ///     is
    ///     recommended to use it in conjunction with machine generated strings(such as IDs).<br />
    /// </summary>
    /// <seealso href="https://patrick-sachs.de/projekte/ministring/" />
    /// <seealso cref="Encode" />
    public static class MiniString
    {
        private const bool X = true;
        private const bool O = false;

        private static readonly Dictionary<char, MiniChar> s_ToMini = new Dictionary<char, MiniChar>();
        private static readonly Dictionary<MiniChar, char> s_ToChar = new Dictionary<MiniChar, char>();

        static MiniString()
        {
            Register('\0', new MiniChar(O, O, O, O, O, O));
            Register('1', new MiniChar(O, O, O, O, O, X));
            Register('2', new MiniChar(O, O, O, O, X, O));
            Register('3', new MiniChar(O, O, O, O, X, X));
            Register('4', new MiniChar(O, O, O, X, O, O));
            Register('5', new MiniChar(O, O, O, X, O, X));
            Register('6', new MiniChar(O, O, O, X, X, O));
            Register('7', new MiniChar(O, O, O, X, X, X));
            Register('8', new MiniChar(O, O, X, O, O, O));
            Register('9', new MiniChar(O, O, X, O, O, X));
            Register('0', new MiniChar(O, O, X, O, X, O));
            Register('A', new MiniChar(O, O, X, O, X, X));
            Register('B', new MiniChar(O, O, X, X, O, O));
            Register('C', new MiniChar(O, O, X, X, O, X));
            Register('D', new MiniChar(O, O, X, X, X, O));
            Register('E', new MiniChar(O, O, X, X, X, X));
            Register('F', new MiniChar(O, X, O, O, O, O));
            Register('G', new MiniChar(O, X, O, O, O, X));
            Register('H', new MiniChar(O, X, O, O, X, O));
            Register('I', new MiniChar(O, X, O, O, X, X));
            Register('J', new MiniChar(O, X, O, X, O, O));
            Register('K', new MiniChar(O, X, O, X, O, X));
            Register('L', new MiniChar(O, X, O, X, X, O));
            Register('M', new MiniChar(O, X, O, X, X, X));
            Register('N', new MiniChar(O, X, X, O, O, O));
            Register('O', new MiniChar(O, X, X, O, O, X));
            Register('P', new MiniChar(O, X, X, O, X, O));
            Register('Q', new MiniChar(O, X, X, O, X, X));
            Register('R', new MiniChar(O, X, X, X, O, O));
            Register('S', new MiniChar(O, X, X, X, O, X));
            Register('T', new MiniChar(O, X, X, X, X, O));
            Register('U', new MiniChar(O, X, X, X, X, X));
            Register('V', new MiniChar(X, O, O, O, O, O));
            Register('W', new MiniChar(X, O, O, O, O, X));
            Register('X', new MiniChar(X, O, O, O, X, O));
            Register('Y', new MiniChar(X, O, O, O, X, X));
            Register('Z', new MiniChar(X, O, O, X, O, O));
            Register('a', new MiniChar(X, O, O, X, O, X));
            Register('b', new MiniChar(X, O, O, X, X, O));
            Register('c', new MiniChar(X, O, O, X, X, X));
            Register('d', new MiniChar(X, O, X, O, O, O));
            Register('e', new MiniChar(X, O, X, O, O, X));
            Register('f', new MiniChar(X, O, X, O, X, O));
            Register('g', new MiniChar(X, O, X, O, X, X));
            Register('h', new MiniChar(X, O, X, X, O, O));
            Register('i', new MiniChar(X, O, X, X, O, X));
            Register('j', new MiniChar(X, O, X, X, X, O));
            Register('k', new MiniChar(X, O, X, X, X, X));
            Register('l', new MiniChar(X, X, O, O, O, O));
            Register('m', new MiniChar(X, X, O, O, O, X));
            Register('n', new MiniChar(X, X, O, O, X, O));
            Register('o', new MiniChar(X, X, O, O, X, X));
            Register('p', new MiniChar(X, X, O, X, O, O));
            Register('q', new MiniChar(X, X, O, X, O, X));
            Register('r', new MiniChar(X, X, O, X, X, O));
            Register('s', new MiniChar(X, X, O, X, X, X));
            Register('t', new MiniChar(X, X, X, O, O, O));
            Register('u', new MiniChar(X, X, X, O, O, X));
            Register('v', new MiniChar(X, X, X, O, X, O));
            Register('w', new MiniChar(X, X, X, O, X, X));
            Register('x', new MiniChar(X, X, X, X, O, O));
            Register('y', new MiniChar(X, X, X, X, O, X));
            Register('z', new MiniChar(X, X, X, X, X, O));
            Register('_', new MiniChar(X, X, X, X, X, X));
        }

        /// <summary>
        ///     Registers the given character aswell as the mini character in both lookups.
        /// </summary>
        /// <param name="c">The character.</param>
        /// <param name="mc">The mini character.</param>
        private static void Register(char c, MiniChar mc)
        {
            s_ToMini.Add(c, mc);
            s_ToChar.Add(mc, c);
        }


        /// <summary>
        ///     Encodes the given string into a mini string.
        /// </summary>
        /// <param name="str">The string to encode.</param>
        /// <returns>The encoded bytes.</returns>
        /// <seealso href="https://patrick-sachs.de/projekte/ministring/" />
        /// <remarks>
        ///     Bitflags:<br />
        ///     0       - null<br />
        ///     1 - 10  - 1 - 0<br />
        ///     11 - 36 - A-Z<br />
        ///     37 - 62 - a-z<br />
        ///     63 - _
        /// </remarks>
        public static byte[] Encode(string str)
        {
            byte[] bytes;
            MiniCharStream chars = new MiniCharStream(str);
            using (MemoryStream stream = new MemoryStream()) {
                byte currentByte = 0;
                int offset = 0;
                foreach (MiniChar miniChar in chars) {
                    foreach (bool bit in miniChar) {
                        if (bit) {
                            currentByte |= (byte) (1 << offset);
                        }
                        offset++;
                        if (offset == 8) {
                            stream.WriteByte(currentByte);
                            offset = 0;
                            currentByte = 0;
                        }
                    }
                }
                if (offset != 0) {
                    stream.WriteByte(currentByte);
                }
                bytes = stream.ToArray();
            }
            return bytes;
        }

        /// <summary>
        ///     Decodes the given mini string into a regular string.
        /// </summary>
        /// <param name="bytes">The beytes to decode.</param>
        /// <returns>The string.</returns>
        /// <seealso href="https://patrick-sachs.de/projekte/ministring/" />
        public static string Decode(byte[] bytes)
        {
            StringBuilder builder = new StringBuilder();
            var chars = new List<MiniChar>();
            using (MemoryStream stream = new MemoryStream(bytes)) {
                int byte1 = stream.ReadByte();
                int byte2 = stream.ReadByte();
                bool isDone = byte1 == -1;
                var b = new bool[6];
                int bitOffset = 0;
                while (!isDone) {
                    for (int i = 0; i < 6; i++) {
                        b[i] = ((byte) byte1).GetBit(bitOffset);
                        bitOffset++;
                        if (bitOffset == 8) {
                            byte1 = byte2;
                            if (byte1 == -1) {
                                isDone = true;
                                byte1 = 0;
                                byte2 = -1;
                            } else {
                                byte2 = stream.ReadByte();
                            }
                            bitOffset = 0;
                        }
                    }
                    MiniChar mini = new MiniChar(b[5], b[4], b[3], b[2], b[1], b[0]);
                    chars.Add(mini);
                }
            }
            MiniCharInStream inStream = new MiniCharInStream(chars);
            foreach (char c in inStream) {
                builder.Append(c);
            }
            return builder.ToString();
        }

        /// <summary>
        ///     Gets the character the given mini char represents.
        /// </summary>
        /// <param name="c">The mini character.</param>
        /// <returns>The character.</returns>
        internal static char GetSimpleChar(MiniChar c)
        {
            return s_ToChar[c];
        }

        /// <summary>
        ///     Gets the mini char the given character represents.
        /// </summary>
        /// <param name="c">The cahracter.</param>
        /// <returns>The mini character or null.</returns>
        internal static MiniChar GetSimpleChar(char c)
        {
            s_ToMini.TryGetValue(c, out MiniChar chr);
            return chr;
        }

        /// <summary>
        ///     Gets all mini chars needed to represent a 16-bit UTF-16LE code point.
        /// </summary>
        /// <param name="codePoint">The code point.</param>
        /// <returns>The mini chars.</returns>
        internal static IEnumerable<MiniChar> GetComplexChar(ushort codePoint)
        {
            yield return s_ToMini['\0'];
            yield return new MiniChar(codePoint.GetBit(3), codePoint.GetBit(2), codePoint.GetBit(1), codePoint.GetBit(0), O, O);
            yield return new MiniChar(codePoint.GetBit(9), codePoint.GetBit(8), codePoint.GetBit(7), codePoint.GetBit(6), codePoint.GetBit(5), codePoint.GetBit(4));
            yield return new MiniChar(codePoint.GetBit(15), codePoint.GetBit(14), codePoint.GetBit(13), codePoint.GetBit(12), codePoint.GetBit(11), codePoint.GetBit(10));
        }

        /// <summary>
        ///     Gets the given bit of a ushort.
        /// </summary>
        /// <param name="b">The ushort.</param>
        /// <param name="bitNumber">The bit number.</param>
        /// <returns>The bit.</returns>
        private static bool GetBit(this ushort b, int bitNumber)
        {
            return (b & (1 << bitNumber)) != 0;
        }

        /// <summary>
        ///     Gets the given bit of a byte.
        /// </summary>
        /// <param name="b">The byte.</param>
        /// <param name="bitNumber">The bit number.</param>
        /// <returns>The bit.</returns>
        private static bool GetBit(this byte b, int bitNumber)
        {
            return (b & (1 << bitNumber)) != 0;
        }
    }
}