using System;
using System.Collections.Generic;
using System.Text;

namespace Encrypto
{
    class Caesar_Cipher : Cipher
    {
        public Caesar_Cipher(string plainText, int key) : base(plainText)
        {
            Key = key;
        }

        public override string Decrypt()
        {
            throw new NotImplementedException();
        }

        public override string Encrypt()
        {
            throw new NotImplementedException();
        }

        public override string Convert()
        {
            throw new NotImplementedException();
        }

        // --------------------------------------------------------------------
        // ------------------- Accessor Methods -------------------------------
        // --------------------------------------------------------------------
        public int Key { get; }
    }
}
