using System.Collections;
using System.Collections.Generic;

namespace MiniStringLib
{
    internal class MiniCharStream : IEnumerable<MiniChar>
    {
        private readonly string m_Str;

        public MiniCharStream(string str)
        {
            m_Str = str;
        }

        public IEnumerator<MiniChar> GetEnumerator()
        {
            foreach (char c in m_Str) {
                MiniChar simple = MiniString.GetSimpleChar(c);
                if (simple != null) {
                    yield return simple;
                } else {
                    foreach (MiniChar complex in MiniString.GetComplexChar(c)) {
                        yield return complex;
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