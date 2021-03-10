using Microsoft.VisualStudio.TestTools.UnitTesting;
using Encrypto;
using System;
using System.Collections.Generic;
using System.Text;
using Encrypto.Models;

namespace Encrypto.Models.Tests
{
    [TestClass()]
    public class Caesar_CipherTests
    {
        [TestMethod()]
        public void Caesar_CipherTest()
        {
            Cipher cipher = new Caesar_Cipher("Hello World", "13");
            Assert.IsNotNull(cipher);
            cipher = null;
            Assert.IsNull(cipher);
        }

        [TestMethod()]
        public void DecryptTest()
        {
            // Testing Caesar Cipher
            string expected = "Hello World";
            Cipher cipher = new Caesar_Cipher("Uryyb Jbeyq", "13");
            string result = cipher.Decrypt();
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void EncryptTest()
        {
            // Testing Caesar Cipher
            string actual = "Uryyb Jbeyq";
            Cipher cipher = new Caesar_Cipher("Hello World", "13");     
            string expected = cipher.Encrypt();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Is_Key_ValidTest()
        {
            Cipher cipher = new Caesar_Cipher("Hello World", "dog");
            Assert.IsFalse(cipher.Is_Key_Valid());
        }
    }
}