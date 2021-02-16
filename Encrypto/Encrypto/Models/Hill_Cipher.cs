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

        public override string Description => throw new NotImplementedException();

        public override string History => throw new NotImplementedException();

        // --------------------------------------------------------------------
        // --------------------- Cipher Methods -------------------------------
        // --------------------------------------------------------------------

        public override string Decrypt()
        {
			if (!Is_Key_Valid())
			{
				throw new Exception("Invalid Key");
			}
			return Hill_Substitution(Message, Key, false);
		}

        public override string Encrypt()
        {
			if (!Is_Key_Valid())
			{
				throw new Exception("Invalid Key");
			}
			return Hill_Substitution(Message, Key, true);
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
			return true;
        }

        private string Hill_Substitution(string input, string key, bool encryptMessage)
        {
			string output = "";
			int n = (int)Math.Sqrt(key.Length);
			var keyMatrix = Matrix<Single>.Build.Dense(n, n);

			// Fill key matrix with values
			for (int i = 0; i < n; i++)
			{
				for (int j = 0; j < n; j++)
				{
					keyMatrix[i, j] = Get_Alphabetic_Value(key[i * 2 + j]);
				}
			}

			if (!Is_Invertible(keyMatrix, 26))
			{
				throw new Exception("Invalid key, matrix is not invertible!");
			}

			if (!encryptMessage)
			{
				keyMatrix = keyMatrix.Inverse();
			}

			string plainText = "";
			foreach (char c in input)
			{
				if (Char.IsLetter(c))
				{
					plainText += c;
				}
			}

			// Check length of input message 
			int mod = Mod(plainText.Length, n);
			while (mod != 0)
			{
				Random rand = new Random();
				plainText += (char)(rand.Next(0, 26) + (int)'A');
				mod--;
			}

			for (int i = 0; i <= plainText.Length - n; i += n)
			{
				// Break up string into groups of n
				string strGroup = plainText.Substring(i, n);
				int[] vector = new int[n];
				for (int j = 0; j < n; j++)
				{
					vector[j] = Get_Alphabetic_Value(strGroup[j]);
				}

				// Multiply matrices
				for (int j = 0; j < n; j++)
				{
					int sum = 0;
					for (int k = 0; k < n; k++)
					{
						sum += (int)(keyMatrix[j, k] * vector[k]);
					}
					output += (char)(Mod(sum, 26) + (int)'A');
				}
			}
			return output;
		}

		private bool Is_Invertible(Matrix<Single> matrix, int modBase)
		{
			int determinant = (int)matrix.Determinant();
			if (determinant % 1 != 0)
			{
				return false;
			}
			// Check if determinant is Co-Prime with base number
			if (GCD(Math.Abs(determinant), modBase) == 1)
			{
				return true;
			}
			return false;
		}
    }
}
