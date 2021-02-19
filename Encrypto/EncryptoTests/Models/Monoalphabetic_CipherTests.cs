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
            Cipher cipher = new Monoalphabetic_Cipher("hello world", "QWERTYUIOPLKJHGFDSAZXCVBNM");
            Assert.IsNotNull(cipher);
            cipher = null;
            Assert.IsNull(cipher);
        }

        [TestMethod()]
        public void DecryptTest()
        {
            Cipher cipher = new Monoalphabetic_Cipher("KGGL VIQZ NGX'CT RGHT", "QWERTYUIOPLKJHGFDSAZXCVBNM");
            string expected = cipher.Decrypt();
            string actual = "Look what you've done";
            Assert.AreEqual(expected, actual, true);
        }

        [TestMethod()]
        public void EncryptTest()
        {
            Cipher cipher = new Monoalphabetic_Cipher("Look what you've done", "QWERTYUIOPLKJHGFDSAZXCVBNM");
            string expected = cipher.Encrypt();
            string actual = "KGGL VIQZ NGX'CT RGHT";
            Assert.AreEqual(expected, actual, true);
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