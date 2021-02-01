using System;
using System.Collections.Generic;
using System.Text;

namespace Encrypto.Models
{
    class Homophonic_Cipher : Cipher
    {
        public Homophonic_Cipher(string message, string key) : base(message, key)
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
    }
}
