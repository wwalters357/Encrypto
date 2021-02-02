﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Encrypto.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Encrypto.Models.Tests
{
    [TestClass()]
    public class Hill_CipherTests
    {
        [TestMethod()]
        public void Hill_CipherTest()
        {
            Cipher cipher = new Hill_Cipher("DCODEZ", "CDFH");
            Assert.IsNotNull(cipher);
            cipher = null;
            Assert.IsNull(cipher);
            cipher = new Hill_Cipher("DCODEZ", "CDFH");
            Assert.IsNotNull(cipher);
        }

        [TestMethod()]
        public void DecryptTest()
        {
            Cipher cipher = new Hill_Cipher("MDLNFN", "CDFH");
            string expected = cipher.Decrypt();
            string actual = "DCODEZ";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void EncryptTest()
        {
            Cipher cipher = new Hill_Cipher("DCODEZ", "CDFH");
            string expected = cipher.Encrypt();
            string actual = "MDLNFN";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Is_Key_ValidTest()
        {
            // 2x2 Matrix
            Cipher cipher = new Hill_Cipher("DCODEZ", "CDFH");
            Assert.IsTrue(cipher.Is_Key_Valid());

            // 3x3 Matrix
            cipher = new Hill_Cipher("DCODEZ", "ABCDEFGHI");
            Assert.IsTrue(cipher.Is_Key_Valid());

            // False
            cipher = new Hill_Cipher("DCODEZ", "ACDFHWDE");
            Assert.IsFalse(cipher.Is_Key_Valid());

            // Checks after runnning encryption
            try
            {
                cipher = new Hill_Cipher("DCODEZ", "ABCD");
                cipher.Encrypt();
                Assert.Fail();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}