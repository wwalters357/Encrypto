﻿using System;
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
            // return Substitution(Key, false);
            throw new NotImplementedException();
        }

        public override string Encrypt()
        {
            return Substitution(Key, true);
        }

        public override bool Is_Key_Valid()
        {
            throw new NotImplementedException();
        }

        private string Substitution(string key, bool encryptMesage)
        {
            throw new NotImplementedException();
        }
    }
}
