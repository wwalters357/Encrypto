using Microsoft.VisualStudio.TestTools.UnitTesting;
using Encrypto.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Encrypto.Models.Tests
{
    [TestClass()]
    public class Homophonic_CipherTests
    {
        [TestMethod()]
        public void Homophonic_CipherTest()
        {
            Cipher cipher = new Homophonic_Cipher("Hello World", "13;");
            Assert.IsNotNull(cipher);
            cipher = null;
            Assert.IsNull(cipher);
            string msg = "Hello how has it been going my dude?";
            string key = "21,27,31,40;15;01,33;20,34;22,28,32,36,37;05;17;14;02,29,38,41;19;03;07,39,42;09,43;12,48,97;18,60,85;26,44;25;24,49;10,30,45,99;06,96,55;16,94;23;13;11;08;04;";
            cipher = new Homophonic_Cipher(msg, key);
            cipher.Message = cipher.Encrypt();
            string expected = cipher.Decrypt();
            Assert.AreEqual(expected, msg, true);
        }

        [TestMethod()]
        public void DecryptTest()
        {
            string msg = "211543";
            string key = "21,27,31,40;15;01,33;20,34;22,28,32,36,37;05;17;14;02,29,38,41;19;03;07,39,42;09,43;12,48,97;18,60,85;26,44;25;24,49;10,30,45,99;06,96,55;16,94;23;13;11;08;04;";
            Cipher cipher = new Homophonic_Cipher(msg, key);
            string expected = cipher.Decrypt();
            Assert.IsNotNull(expected);
            Assert.AreNotEqual(expected, msg);
        }

        [TestMethod()]
        public void EncryptTest()
        {
            string msg = "Hello how has it been going my dude?";
            string key = "21,27,31,40;15;01,33;20,34;22,28,32,36,37;05;17;14;02,29,38,41;19;03;07,39,42;09,43;12,48,97;18,60,85;26,44;25;24,49;10,30,45,99;06,96,55;16,94;23;13;11;08;04;";
            Cipher cipher = new Homophonic_Cipher(msg, key);
            string expected = cipher.Encrypt();
            Assert.IsNotNull(expected);
            Assert.AreNotEqual(expected, msg);
        }

        [TestMethod()]
        public void Is_Key_ValidTest()
        {
            string key = "21,27,31,40;15;01,33;20,34;22,28,32,36,37;05;17;14;02,29,38,41;19;03;07,39,42;09,43;12,48,97;18,60,85;26,44;25;24,49;10,30,45,99;06,96,55;16,94;23;13;11;08;04;";
            Cipher cipher = new Homophonic_Cipher("", key);
            Assert.IsTrue(cipher.Is_Key_Valid());

            // Too many entries
            cipher.Key += "21,27,31,40;";
            Assert.IsFalse(cipher.Is_Key_Valid());

            // Key is too short
            cipher.Key = "21,27,31,40;15;01,33;20,34;22,28,32,36,37;05;17;14;02,29,38,41;19;03;07,39,42;09,43;";
            Assert.IsFalse(cipher.Is_Key_Valid());

            // Replaced a number with a letter
            cipher.Key = "21,a7,31,40;15;01,33;20,34;22,28,32,36,37;05;17;14;02,29,38,41;19;03;07,39,42;09,43;12,48,97;18,60,85;26,44;25;24,49;10,30,45,99;06,96,55;16,94;23;13;11;08;04;";
            Assert.IsFalse(cipher.Is_Key_Valid());

            // Wrong length as there is a 1-digit number
            cipher.Key = "2,27,31,40;15;01,33;20,34;22,28,32,36,37;05;17;14;02,29,38,41;19;03;07,39,42;09,43;12,48,97;18,60,85;26,44;25;24,49;10,30,45,99;06,96,55;16,94;23;13;11;08;04;";
            Assert.IsFalse(cipher.Is_Key_Valid());
        }
    }
}