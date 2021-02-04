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

        /* 
         * This function which may become a class will account for all the 
         * different methods for determining if a number is prime. Note: Not
         * all tests are deterministic by themselves.
         * 
         * ex.
         * Fermat's little theorem
         * must satisfy x^(p-1) = 1 mod p
         * If for some x this is true than it is pseudoprime.
        */
        private bool IsPrime(BigInteger x, Primality_Test option)
        {
            try
            {
                switch (option)
                {
                    case Primality_Test.Fermats_Little_Theorem:
                        break;
                    default:
                        return Simple_Primality_Test(x);
                }
            }
            catch (Exception)
            {
                // Maybe from time-outs because some of the methods will
                // be computationally expensive.
                return false;
            }
            return false;
        }

        enum Primality_Test
        {
            Fermats_Little_Theorem
        }

        // Check is any number between [2 - (number / 2)] can divide the number
        private bool Simple_Primality_Test(BigInteger x)
        {
            for(int i = 2; i < BigInteger.Divide(x, 2); i++)
            {
                if ( BigInteger.Remainder(x, i) == 0)
                {
                    return false;
                }
            }
            return true;
        }

        private bool Fermats_Little_Theorem(BigInteger x)
        {
            throw new NotImplementedException();
        }

        // Calculate modulus division
        protected int Mod(int x, int b)
        {
            int r = x % b;
            return (x < 0) ? r + b : r;
        }

        // Calculate GCD using Euler's algorithm 
        protected int GCD(int x, int y)
        {
            if (x == 0)
            {
                return y;
            }
            return GCD(y % x, x);
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

        // --------------------------------------------------------------------
        // ------------------- Accessor Methods -------------------------------
        // --------------------------------------------------------------------
        public string Message { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
    }
}
