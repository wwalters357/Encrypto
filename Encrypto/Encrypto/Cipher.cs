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
            return (x < 0) ? (x + b) % b : x % b;
        }

        // --------------------------------------------------------------------
        // ------------------- Accessor Methods -------------------------------
        // --------------------------------------------------------------------
        public string PlainText { get; }
    }
}
