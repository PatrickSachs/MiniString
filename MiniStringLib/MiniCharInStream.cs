using System.Collections;
using System.Collections.Generic;

namespace MiniStringLib
{
    public class MiniCharInStream : IEnumerable<char>
    {
        private readonly IEnumerable<MiniChar> m_Chars;

        public MiniCharInStream(IEnumerable<MiniChar> chars)
        {
            m_Chars = chars;
        }

        public IEnumerator<char> GetEnumerator()
        {
            using (IEnumerator<MiniChar> enmrtr = m_Chars.GetEnumerator()) {
                while (enmrtr.MoveNext()) {
                    char simple = MiniString.GetSimpleChar(enmrtr.Current);
                    if (simple != '\0') {
                        yield return simple;
                    } else {
                        var codePoints = new MiniChar[3];
                        for (int i = 0; i < 3; i++) {
                            if (!enmrtr.MoveNext()) {
                                if (i == 1) {
                                    yield break;
                                }
                                codePoints[i] = MiniString.GetSimpleChar('\0');
                            } else {
                                codePoints[i] = enmrtr.Current;
                            }
                        }
                        ushort codePoint = 0;
                        for (int i = 0; i < 16; i++) {
                            // The first two bits in the first mini char are unused.
                            MiniChar current = codePoints[(i + 2) / 6];
                            bool bit = current[(i + 2) % 6 + 1];
                            if (bit) {
                                codePoint |= (ushort) (1 << i);
                            }
                        }
                        yield return (char) codePoint;
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}