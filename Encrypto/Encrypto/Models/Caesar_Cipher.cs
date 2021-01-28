using System;
using System.Collections.Generic;
using System.Text;

namespace Encrypto
{
    class Caesar_Cipher : Cipher
    {
        public Caesar_Cipher(string message, string key, int option) : base(message, key)
        {
            Option = option;
        }

        public override string Decrypt()
        {
            throw new NotImplementedException();
        }

        public override string Encrypt()
        {
            throw new NotImplementedException();
        }

        private string Caesar_Substitution(string input, int key)
        {
            throw new NotImplementedException();
        }

        private string Double_Caesar_Substitiution(string input, string key)
        {
            throw new NotImplementedException();
        }

        // --------------------------------------------------------------------
        // ------------------- Accessor Methods -------------------------------
        // --------------------------------------------------------------------
        public int Option { get; set; }
    }
}
