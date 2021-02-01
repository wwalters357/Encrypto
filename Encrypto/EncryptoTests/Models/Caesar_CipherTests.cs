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
            string text = "Uryyb Jbeyq";
            string expected = "Hello World";
            string key = "13";
            Cipher_Type option = Cipher_Type.Caesar;
            Cipher cipher = new Caesar_Cipher(text, key, option);
            string result = cipher.Decrypt();
            Assert.AreEqual(expected, result);

            // Testing Double Caesar Cipher
            text = "ksroc eri";
            expected = "hello you";
            key = "dog";
            option = Cipher_Type.Double_Caesar;
            cipher = new Caesar_Cipher(text, key, option);
            result = cipher.Decrypt();
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void EncryptTest()
        {
            // Testing Caesar Cipher
            string expected = "Uryyb Jbeyq";
            string text = "Hello World";
            string key = "13";
            Cipher_Type option = Cipher_Type.Caesar;
            Cipher cipher = new Caesar_Cipher(text, key, option);     
            string result = cipher.Encrypt();
            Assert.AreEqual(expected, result);

            // Testing Double Caesar Cipher
            expected = "ksroc eri";
            text = "hello you";
            key = "dog";
            option = Cipher_Type.Double_Caesar;
            cipher = new Caesar_Cipher(text, key, option);
            result = cipher.Encrypt();
            Assert.AreEqual(expected, result);
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