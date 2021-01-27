namespace Encrypto
{
    abstract class Cipher
    {
        public Cipher(string plainText)
        {
            PlainText = plainText;
        }

        public abstract string Decrypt();

        public abstract string Encrypt();

        protected abstract string Convert();

        // Calculate modulus division
        private int Mod(int x, int b)
        {
            int r = x % b;
            return (x < 0) ? r + b : r;
        }

        // If the character is a letter it return A,a=>0 ... Z,z=>25
        private int Get_Alphabetic_Value(char c)
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
        public string PlainText { get; }
    }
}
