using System;
using System.Linq.Expressions;
using System.Numerics;
using Xamarin.Forms.Xaml.Internals;

namespace Encrypto
{
    public abstract class Cipher
    {
        // Cipher Constructor
        public Cipher(string message, string key, string name)
        {
            Message = message;
            Key = key;
            Name = name;
        }

        // --------------------------------------------------------------------
        // ------------------- Accessor Methods -------------------------------
        // --------------------------------------------------------------------

        public string Message { get; set; }

        public string Key { get; set; }

        public string Name { get; set; }

        public abstract string Image { get; }

        public abstract string Description { get; }

        public abstract string History { get; }

        // --------------------------------------------------------------------
        // --------------------- Cipher Methods -------------------------------
        // --------------------------------------------------------------------

        // Decodes ciphertext and returns plaintext.
        public abstract string Decrypt();

        // Encodes plaintext and returns ciphertext.
        public abstract string Encrypt();

        // Checks if the key is valid for selected cipher.
        public abstract bool Is_Key_Valid();

        // Checks if there is enough info is entered to translate the message.
        public virtual bool Is_Initialized()
        {
            return Message.Length > 0 && Key.Length > 0;
        }

        // Calculate modulus division
        protected int Mod(int x, int b)
        {
            int r = x % b;
            return (x < 0) ? r + b : r;
        }

        // If the character is a letter it return A,a=>0 ... Z,z=>25
        protected int Get_Alphabetic_Value(char c)
        {
            if ( char.IsLetter(c) )
            {
                if ( (int)c >= (int)'a' )
                {
                    return (int)c - (int)'a';
                }
                else
                {
                    return (int)c - (int)'A';
                }
            }
            return -1;
        }
    }
}
