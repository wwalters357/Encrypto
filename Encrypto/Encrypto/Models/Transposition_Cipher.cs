using System;
using System.Collections.Generic;
using System.Text;

namespace Encrypto.Models
{
    class Transposition_Cipher : Cipher
    {
        public Transposition_Cipher(string message, string key) : base(message, key, "Transposition Cipher")
        {

        }

        public override string Decrypt()
        {
            throw new NotImplementedException();
        }

        public override string Encrypt()
        {
            throw new NotImplementedException();
        }

        public override bool Is_Key_Valid()
        {
            throw new NotImplementedException();
        }

        private string Transposition_Translation(string input, string key, bool encryptMessage)
        {
            string output = "";
            return output;
        }
    }
}
