using System;
using System.Linq.Expressions;
using System.Numerics;
using Xamarin.Forms.Xaml.Internals;

namespace Encrypto
{
    public abstract class Cipher
    {
        public Cipher(string message, string key)
        {
            Message = message;
            Key = key;
        }

        public abstract string Decrypt();

        public abstract string Encrypt();

        public abstract bool Is_Key_Valid();

        /* 
         * This function which may become a class will account for all the 
         * different methods for determining if a number is prime. Note: Not
         * all tests are deterministic by themselfves.
         * 
         * ex.
         * Fermat's little theorem
         * must satisfy x^(p-1) = 1 mod p
         * If for some x this is true than it is pseudoprime
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
            catch (Exception e)
            {
                // Maybe from time-outs 
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
        public string Message { get; }
        public string Key { get; }
    }
}
