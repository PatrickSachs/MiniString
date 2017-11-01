using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniStringLib;

namespace Tests
{
    [TestClass]
    public class ComplexTest
    {
        [TestMethod]
        public void Encode_Letter()
        {
            MiniString.Encode(@"Hello my friend,

I am writing this letter with the hopes of not blowing up the debug output.

Yours, Patrick".WriteLine()).GetBitString().WriteLine();
        }
        [TestMethod]
        public void Encode_SimpleLetter()
        {
            MiniString.Encode(@"Hello my friend I am writing this letter with the hopes of not blowing up the debug output Yours Patrick".WriteLine()).GetBitString().WriteLine();
        }
    }
}
