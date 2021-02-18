using Encrypto.Models;
using System;
using System.Resources;

namespace Encrypto
{
    public class Vigenere_Cipher : Cipher
    {
        public Vigenere_Cipher(string message, string key) : base(message, key, "Vigenere Cipher")
        {

        }

        // --------------------------------------------------------------------
        // ------------------- Accessor Methods -------------------------------
        // --------------------------------------------------------------------

        public override string Image => "vigenere_cipher.png";

        public override string Description => throw new NotImplementedException();

        public override string History => throw new NotImplementedException();


        // --------------------------------------------------------------------
        // --------------------- Cipher Methods -------------------------------
        // --------------------------------------------------------------------

        // Run error checking and return decoded string.
        public override string Decrypt()
        {
            if (!Is_Key_Valid())
            {
                throw new Exception("Invalid Key");
            }
            return Vigenere_Substitiution(Message, Key, false);
        }

        // Run error checking and return encoded string. 
        public override string Encrypt()
        {
            if (!Is_Key_Valid())
            {
                throw new Exception("Invalid Key");
            }
            return Vigenere_Substitiution(Message, Key, true);
        }

        // Check if the key is a string for Vigenere Cipher.
        public override bool Is_Key_Valid()
        {
            if (Key.Length < 1)
            {
                return false;
            }
            foreach (char c in Key)
            {
                if (!Char.IsLetter(c))
                {
                    return false;
                }
            }
            return true;
        }

        // Repeat key for the length of the plain text and for encryption
        // shift each key by the current key letter and for decryption shift the
        // opposite direction for current key letter.
        private string Vigenere_Substitiution(string input, string key, bool encryptMessage)
        {
            int decrypt = (encryptMessage) ? 1 : -1;
            string output = "";
            int j = 0;
            for (int i = 0; i < input.Length; i++)
            {
                int x = Mod(Get_Alphabetic_Value(input[i]) + (Get_Alphabetic_Value(key[Mod(j, key.Length)]) * decrypt), 26);
                if (Char.IsUpper(input, i))
                {
                    output += (char)(x + (int)'A');
                    j++;
                }
                else if (Char.IsLower(input, i))
                {
                    output += (char)(x + (int)'a');
                    j++;
                }
                else
                {
                    output += input[i];
                }
            }
            return output;
        }   
    }
}
