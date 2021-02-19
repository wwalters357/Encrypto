using Microsoft.VisualStudio.TestTools.UnitTesting;
using Encrypto.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Encrypto.Models.Tests
{
    [TestClass()]
    public class Vernam_CipherTests
    {
        [TestMethod()]
        public void Vernam_CipherTest()
        {
            string msg = "Hello world I am the ultimate cipher!";
            Cipher cipher = new Vernam_Cipher(msg);
            Assert.IsNotNull(cipher);
            cipher = null;
            Assert.IsNull(cipher);           
            cipher = new Vernam_Cipher(msg);
            cipher.Message = cipher.Encrypt();
            string expected = cipher.Decrypt();
            Assert.AreEqual(expected, msg, true);
        }

        [TestMethod()]
        public void DecryptTest()
        {
            string msg = "Hello World!";
            Cipher cipher = new Vernam_Cipher(msg);
            string expected = cipher.Decrypt();
            Assert.IsNotNull(expected);
            Assert.AreNotEqual(expected, msg);
        }

        [TestMethod()]
        public void EncryptTest()
        {
            string msg = "Hello World?";
            Cipher cipher = new Vernam_Cipher(msg);
            string expected = cipher.Encrypt();
            Assert.IsNotNull(expected);
            Assert.AreNotEqual(expected, msg);
        }

        [TestMethod()]
        public void Is_Key_ValidTest()
        {
            Cipher cipher = new Vernam_Cipher("Hello World");
            Assert.IsTrue(cipher.Is_Key_Valid());
            
        }

        [TestMethod()]
        public void Is_InitializedTest()
        {
            Cipher cipher = new Vernam_Cipher("Hello World");
            Assert.IsTrue(cipher.Is_Initialized());
        }
    }
}