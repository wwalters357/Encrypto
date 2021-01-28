using System;
using System.Collections.Generic;
using System.Text;

namespace Encrypto
{
    class Monoalphabetic_Cipher : Cipher
    {
        public Monoalphabetic_Cipher(string message, string key) : base(message, key)
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

        private string Substitution(string key)
        {
            throw new NotImplementedException();
        }
    }
}
