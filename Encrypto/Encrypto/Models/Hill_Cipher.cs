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

        // --------------------------------------------------------------------
        // --------------------- Cipher Methods -------------------------------
        // --------------------------------------------------------------------

        public override string Decrypt()
        {
			if (!Is_Key_Valid())
			{
				throw new Exception("Invalid Key");
			}
			int n = (int)Math.Sqrt(Key.Length);
			int[,] keyMatrix = Generate_KeyMatrix(n);
			
			return Hill_Substitution(Message, keyMatrix, false);
		}

        public override string Encrypt()
        {
			if (!Is_Key_Valid())
			{
				throw new Exception("Invalid Key");
			}
			int n = (int)Math.Sqrt(Key.Length);
			int[,] keyMatrix = Generate_KeyMatrix(n);
			return Hill_Substitution(Message, keyMatrix, true);
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

		private int[,] Generate_KeyMatrix(int n)
        {
			int[,] keyMatrix = new int[n,n];
			// Fill key matrix with values
			for (int i = 0; i < n; i++)
			{
				for (int j = 0; j < n; j++)
				{
					keyMatrix[i, j] = Get_Alphabetic_Value(Key[i * 2 + j]);
				}
			}
			return keyMatrix;
        }

		private bool Is_Invertible(int[,] m)
        {
			return true;
        }

		private int[,] Inverse(int[,] m)
        {

        }

        private string Hill_Substitution(string input, int[,] keyMatrix, bool encryptMessage)
        {
			string output = "";
			int n = (int)Math.Sqrt(key.Length);
			DenseMatrix keyMatrix = new DenseMatrix(n, n);

			

			if (!Is_Invertible(keyMatrix, 26))
			{
				throw new Exception("Invalid key, matrix is not invertible!");
			}

			if (!encryptMessage)
			{
				keyMatrix = (DenseMatrix)keyMatrix.Inverse();
			}

			string plainText = "";
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
				DenseVector vector = new DenseVector(n);
				for (int j = 0; j < n; j++)
				{
					vector[j] = Get_Alphabetic_Value(strGroup[j]);
				}

				// Multiply matrices
				vector = (DenseVector)keyMatrix.Multiply(vector);

				// Add newly calculated letters to output
				for (int j = 0; j < n; j++)
                {
					int x = (int)Math.Round(vector[j].Real);
					output += (char)(Mod(x, 26) + (int)'A');
                }
			}
			return (output.Length <= length) ? output : output.Substring(0, length);
		}

		private bool Is_Invertible(DenseMatrix matrix, int modBase)
		{
			int determinant = (int)Math.Round(matrix.Determinant().Real);
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
