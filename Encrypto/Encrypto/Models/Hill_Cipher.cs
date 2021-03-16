using System;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex;

namespace Encrypto.Models
{
    public class Hill_Cipher : Cipher
    {
        public Hill_Cipher(string message, string key) : base(message, key, "Hill Cipher")
        {

        }

		// --------------------------------------------------------------------
		// ------------------- Accessor Methods -------------------------------
		// --------------------------------------------------------------------

		public override string Image
        {
			get
            {
				return "hill_cipher_part_1.png";
            }
        }

        public override string Description => "Uses Matrix multiplication to convert each byte and shift its' location.";

        public override string History => "Created at some point for a good reason.";

        public Matrix KeyMatrix { get; private set; }

        // --------------------------------------------------------------------
        // --------------------- Cipher Methods -------------------------------
        // --------------------------------------------------------------------

        public override string Decrypt()
        {
			if (!Is_Key_Valid())
			{
				throw new Exception("Invalid Key");
			}
			return Hill_Substitution(Message, KeyMatrix.Inverse());
		}

        public override string Encrypt()
        {
			if (!Is_Key_Valid())
			{
				throw new Exception("Invalid Key");
			}
			return Hill_Substitution(Message, KeyMatrix);
		}

		// Must be either 4 or 9 letters
		// Must also form an invertible matrix
        public override bool Is_Key_Valid()
        {
			if (Key.Length != 4 && Key.Length != 9)
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
            KeyMatrix = new Matrix(Generate_KeyMatrix());
			return KeyMatrix.Is_Invertible();
        }

		private int[,] Generate_KeyMatrix()
        {
			int n = (int)Math.Sqrt(Key.Length);
			int[,] keyMatrix = new int[n,n];
			int count = 0;
			// Fill key matrix with values
			for (int i = 0; i < n; i++)
			{
				for (int j = 0; j < n; j++)
				{
					keyMatrix[i, j] = Get_Alphabetic_Value(Key[count++]);
				}
			}
			return keyMatrix;
        }

        private string Hill_Substitution(string input, Matrix keyMatrix)
        {
			string output = "";
			string plainText = "";
			int n = keyMatrix.Length;

			// Create plaintext with only letters
			foreach (char c in input)
			{
				if (Char.IsLetter(c))
				{
					plainText += c;
				}
			}
			int length = plainText.Length;

			// Check length of input message 
			int mod = Mod(plainText.Length, n);
			while (mod != 0)
			{
				plainText += 'A';
				mod--;
			}

			for (int i = 0; i <= plainText.Length - n; i += n)
			{
				// Break up string into groups of n
				string strGroup = plainText.Substring(i, n);
				Matrix vector = new Matrix(n, 1);
				for (int j = 0; j < n; j++)
				{
					vector[j, 0] = Get_Alphabetic_Value(strGroup[j]);
				}

				// Multiply matricies
				vector = keyMatrix * vector;

				// Add newly calculated letters to output
				for (int j = 0; j < n; j++)
                {
					output += (char)(Mod(vector[j, 0], 26) + (int)'A');
                }
			}
			return (output.Length <= length) ? output : output.Substring(0, length);
		}
    }
}
