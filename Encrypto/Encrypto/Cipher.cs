using System;
using System.Collections.Generic;
using System.Text;

namespace Encrypto
{
    class Cipher
    {
        public Cipher(string plainText, bool encrypt)
        {
            PlainText = plainText;
            Encrypt = encrypt;
        }

        public string Caesar_Cipher(int key)
        {
            return null;
        }

        public string Double_Caesar_Cipher(string key)
        {
            return null;
        }

        // Calculate modulus division
        private int Mod(int x, int b)
        {
            return (x < 0) ? (x + b) % b : x % b;
        }

        // --------------------------------------------------------------------
        // ------------------- Accessor Methods -------------------------------
        // --------------------------------------------------------------------
        public string PlainText { get; }
        public bool Encrypt { get; }
    }
}
