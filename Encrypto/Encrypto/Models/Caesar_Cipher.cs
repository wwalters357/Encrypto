using Encrypto.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Encrypto
{
    public class Caesar_Cipher : Cipher
    {
        public Caesar_Cipher(string message, string key, Cipher_Type option) : base(message, key)
        {
            Option = option;
        }

        // Run error checking and return decoded string
        public override string Decrypt()
        {
            if (!Is_Key_Valid())
            {
                throw new Exception("Invalid Key");
            }
            switch (Option)
            {
                case Cipher_Type.Caesar:
                    return Caesar_Substitution(Message, -1 * Int32.Parse(Key));
                case Cipher_Type.Double_Caesar:
                    return Double_Caesar_Substitiution(Message, Key, false);
                default:
                    throw new Exception("Invalid Cipher Type");
            }
        }

        // Run error checking and return encoded string 
        public override string Encrypt()
        {
            if (!Is_Key_Valid())
            {
                throw new Exception("Invalid Key");
            }
            switch (Option)
            {
                case Cipher_Type.Caesar:
                    return Caesar_Substitution(Message, Int32.Parse(Key));
                case Cipher_Type.Double_Caesar:
                    return Double_Caesar_Substitiution(Message, Key, true);
                default:
                    throw new Exception("Invalid Cipher Type");
            }
        }

        // Check if the key is an integer for Caesar Cipher
        // Check if the key is a string for Double Caesar Cipher 
        public override bool Is_Key_Valid()
        {
            if (Key.Length < 1)
            {
                return false;
            }
            switch (Option)
            {
                case Cipher_Type.Caesar:
                {
                    return int.TryParse(Key, out _);
                }
                case Cipher_Type.Double_Caesar:
                {
                    foreach (char c in Key)
                    {
                        if (!Char.IsLetter(c))
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }
            throw new Exception("Invalid Cipher Type!");
        }

        // Shift each letter by a given key
        private string Caesar_Substitution(string input, int key)
        {
            string output = "";
            for (int i = 0; i < input.Length; i++)
            {
                int x = Mod(Get_Alphabetic_Value(input[i]) + key, 26);
                if (Char.IsUpper(input, i))
                {
                    output += (char)(x + (int)'A');
                }
                else if (Char.IsLower(input, i))
                {
                    output += (char)(x + (int)'a');
                }
                else
                {
                    output += input[i];
                }
            }
            return output;
        }

        // Repeat key for the length of the plain text and for encryptiom
        // shift each key by the current key letter and for decryption shift the
        // opposite direction for current key letter.
        private string Double_Caesar_Substitiution(string input, string key, bool encryptMessage)
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

        // --------------------------------------------------------------------
        // ------------------- Accessor Methods -------------------------------
        // --------------------------------------------------------------------
        public Cipher_Type Option { get; set; }
    }
}
