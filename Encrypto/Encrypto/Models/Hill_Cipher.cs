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

		private static readonly int AlphabetLength = 26;

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
			else if (Message.Length % 2 != 0)
            {
				throw new Exception("Invalid Message Length, Try adding a letter!");
			}
			Matrix inverse = KeyMatrix.Inverse();
			for (int i = 0; i < inverse.Length; i++)
            {
				for (int j = 0; j < inverse.Width; j++)
                {
					inverse[i, j] = Mod(inverse[i, j], AlphabetLength);
				}
            }
			return Hill_Substitution(Message, inverse);
		}

        public override string Encrypt()
        {
			if (!Is_Key_Valid())
			{
				throw new Exception("Invalid Key");
			}
			string plainText = Format_Text(Message);
			return Hill_Substitution(plainText, KeyMatrix);
		}

		// Must be either 4 or 9 letters
		// Must also form an invertible matrix
		// For now only going to allow matricies of size 2x2
		// because the arithmetic for 3x3 leads to decimals
		// when calculating matrix inverse.
        public override bool Is_Key_Valid()
        {
			if (Key.Length != 4)
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

		// Will need to be modified if alphabet is expanded
		private string Format_Text(string input)
        {
			string plainText = "";

			// Create plaintext with only letters
			foreach (char c in input)
			{
				if (Char.IsLetter(c))
				{
					plainText += c;
				}
			}

			if (plainText.Length % 2 != 0)
			{
				throw new Exception("Invalid Message Length, Try adding a letter!");
			}

			// Check length of input message 
			int mod = Mod(plainText.Length, KeyMatrix.Length);
			while (mod >= 0)
			{
				plainText += 'A';
				mod--;
			}

			return plainText;
		}

        private string Hill_Substitution(string input, Matrix keyMatrix)
        {
			string output = "";
			int n = keyMatrix.Length;

			for (int i = 0; i <= input.Length - n; i += n)
			{
				// Break up string into groups of n
				string strGroup = input.Substring(i, n);
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
					output += (char)(Mod(vector[j, 0], AlphabetLength) + (int)'A');
                }
			}
			return output;
		}
    }
}
