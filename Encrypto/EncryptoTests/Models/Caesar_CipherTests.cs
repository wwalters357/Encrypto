using Microsoft.VisualStudio.TestTools.UnitTesting;
using Encrypto;
using System;
using System.Collections.Generic;
using System.Text;
using Encrypto.Models;

namespace Encrypto.Tests
{
    [TestClass()]
    public class Caesar_CipherTests
    {
        [TestMethod()]
        public void Caesar_CipherTest()
        {
            Cipher cipher = new Caesar_Cipher("Hello World", "13", Cipher_Type.Caesar);
            Assert.IsNotNull(cipher);
            cipher = null;
            Assert.IsNull(cipher);
            cipher = new Caesar_Cipher("hello you", "dog", Cipher_Type.Double_Caesar);
            Assert.IsNotNull(cipher);
        }

        [TestMethod()]
        public void DecryptTest()
        {
            // Testing Caesar Cipher
            string expected = "Hello World";
            Cipher cipher = new Caesar_Cipher("Uryyb Jbeyq", "13", Cipher_Type.Caesar);
            string result = cipher.Decrypt();
            Assert.AreEqual(expected, result);

            // Testing Double Caesar Cipher
            expected = "hello you";
            cipher = new Caesar_Cipher("ksroc eri", "dog", Cipher_Type.Double_Caesar);
            result = cipher.Decrypt();
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void EncryptTest()
        {
            // Testing Caesar Cipher
            string actual = "Uryyb Jbeyq";
            Cipher cipher = new Caesar_Cipher("Hello World", "13", Cipher_Type.Caesar);     
            string expected = cipher.Encrypt();
            Assert.AreEqual(expected, actual);

            // Testing Double Caesar Cipher
            actual = "ksroc eri";
            cipher = new Caesar_Cipher("hello you", "dog", Cipher_Type.Double_Caesar);
            expected = cipher.Encrypt();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Is_Key_ValidTest()
        {
            Cipher cipher = new Caesar_Cipher("Hello World", "dog", Cipher_Type.Caesar);
            Assert.IsFalse(cipher.Is_Key_Valid());
            cipher = new Caesar_Cipher("Hello World", "13", Cipher_Type.Double_Caesar);
            Assert.IsFalse(cipher.Is_Key_Valid());
        }
    }
}