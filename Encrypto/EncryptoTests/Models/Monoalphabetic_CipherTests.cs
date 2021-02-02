using Microsoft.VisualStudio.TestTools.UnitTesting;
using Encrypto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Encrypto.Tests
{
    [TestClass()]
    public class Monoalphabetic_CipherTests
    {
        [TestMethod()]
        public void Monoalphabetic_CipherTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DecryptTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void EncryptTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void Is_Key_ValidTest()
        {
            Cipher cipher = new Monoalphabetic_Cipher("message", "QWERTYUIOPLKJHGFDSAZXCVBNM");
            Assert.IsTrue(cipher.Is_Key_Valid());
            cipher = new Monoalphabetic_Cipher("message", "QWERTYUQOPLKJHGFDSAZQCVBNM");
            Assert.IsFalse(cipher.Is_Key_Valid());
            cipher = new Monoalphabetic_Cipher("message", "QWERTYU");
            Assert.IsFalse(cipher.Is_Key_Valid());
            cipher = new Monoalphabetic_Cipher("message", "QWERTYUIOP2KJHGFDSAZXCVBNM");
            Assert.IsFalse(cipher.Is_Key_Valid());
        }
    }
}