using Microsoft.VisualStudio.TestTools.UnitTesting;
using Encrypto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Encrypto.Tests
{
    [TestClass()]
    public class Vigenere_CipherTests
    {
        [TestMethod()]
        public void Vigenere_CipherTest()
        {
            Cipher cipher = new Vigenere_Cipher("hello you", "dog");
            Assert.IsNotNull(cipher);
            cipher = null;
            Assert.IsNull(cipher);            
        }

        [TestMethod()]
        public void DecryptTest()
        {
            // Testing Vigenere Cipher
            string expected = "hello you";
            Cipher cipher = new Vigenere_Cipher("ksroc eri", "dog");
            string result = cipher.Decrypt();
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void EncryptTest()
        {
            // Testing Vigenere Cipher
            string actual = "ksroc eri";
            Cipher cipher = new Vigenere_Cipher("hello you", "dog");
            string expected = cipher.Encrypt();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Is_Key_ValidTest()
        {
            Cipher cipher = new Vigenere_Cipher("Hello World", "13");
            Assert.IsFalse(cipher.Is_Key_Valid());
        }
    }
}
