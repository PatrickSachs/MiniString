using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MiniStringLib
{
    internal class MiniChar : IEnumerable<bool>
    {
        public readonly bool B1;
        public readonly bool B2;
        public readonly bool B3;
        public readonly bool B4;
        public readonly bool B5;
        public readonly bool B6;

        public MiniChar(bool b6, bool b5, bool b4, bool b3, bool b2, bool b1)
        {
            B1 = b1;
            B2 = b2;
            B3 = b3;
            B4 = b4;
            B5 = b5;
            B6 = b6;
        }

        public bool this[int bit] {
            get {
                switch (bit) {
                    case 1: return B1;
                    case 2: return B2;
                    case 3: return B3;
                    case 4: return B4;
                    case 5: return B5;
                    case 6: return B6;
                    default: throw new IndexOutOfRangeException("can only get bit 1-6. requested: " + bit);
                }
            }
        }

        public IEnumerator<bool> GetEnumerator()
        {
            yield return B1;
            yield return B2;
            yield return B3;
            yield return B4;
            yield return B5;
            yield return B6;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        protected bool Equals(MiniChar other)
        {
            return B1 == other.B1 && B2 == other.B2 && B3 == other.B3 && B4 == other.B4 && B5 == other.B5 && B6 == other.B6;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) {
                return false;
            }
            if (ReferenceEquals(this, obj)) {
                return true;
            }
            if (obj.GetType() != GetType()) {
                return false;
            }
            return Equals((MiniChar) obj);
        }

        public override int GetHashCode()
        {
            unchecked {
                int hashCode = 12 + (B1 ? 0 : 11);
                hashCode = (hashCode * 397) ^ (B2 ? 1 : 2);
                hashCode = (hashCode * 397) ^ (B3 ? 3 : 4);
                hashCode = (hashCode * 397) ^ (B2 ? 5 : 6);
                hashCode = (hashCode * 397) ^ (B2 ? 7 : 8);
                hashCode = (hashCode * 397) ^ (B2 ? 9 : 10);
                return hashCode;
            }
        }

        public override string ToString()
        {
            StringBuilder b = new StringBuilder();
            foreach (bool bit in this) {
                b.Append(bit ? "X" : "O");
            }
            return b.ToString();
        }
    }
}

/*



                bool hasByte = false;
                byte myByte = 0;
                foreach (Nibble nibble in bits) {
                    if (!hasByte) {
                        if (nibble.B1) {
                            myByte |= 1 << 0;
                        }
                        if (nibble.B2) {
                            myByte |= 1 << 1;
                        }
                        if (nibble.B3) {
                            myByte |= 1 << 2;
                        }
                        if (nibble.B4) {
                            myByte |= 1 << 3;
                        }
                        hasByte = true;
                    } else {
                        if (nibble.B1) {
                            myByte |= 1 << 4;
                        }
                        if (nibble.B2) {
                            myByte |= 1 << 5;
                        }
                        if (nibble.B3) {
                            myByte |= 1 << 6;
                        }
                        if (nibble.B4) {
                            myByte |= 1 << 7;
                        }
                        stream.WriteByte(myByte);
                        hasByte = false;
                        myByte = 0;
                    }
                }
                if (hasByte) {
                    stream.WriteByte(myByte);
                }
    */