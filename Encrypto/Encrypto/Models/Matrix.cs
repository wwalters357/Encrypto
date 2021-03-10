using System;
using System.Collections.Generic;
using System.Text;

namespace Encrypto.Models
{
    class Matrix
    {
        // --------------------------------------------------------------------
        // ------------------- Defined Cosntants ------------------------------
        // --------------------------------------------------------------------

        // 2x2 Identity matrix
        private static readonly int[,] I2x2 = new int[2, 2] { { 1, 0 }, { 0, 1 } };

        // 3x3 Identity matrix
        private static readonly int[,] I3x3 = new int[3, 3] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } };

        // --------------------------------------------------------------------
        // ---------------------- Constructors --------------------------------
        // --------------------------------------------------------------------

        // Initialize zero matrix
        public Matrix(int rows, int cols)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Data[i, j] = 0;
                }
            }
        }

        // Initialize using a 2-D array
        public Matrix(int[,] matrix)
        {
            Data = matrix;
        }

        // Initialize 2x2 Matrix
        public Matrix(int a, int b, int c, int d)
        {
            Data = new int[,] { { a, b }, { c, d } };
        }

        // Initialize 3x3 Matrix
        public Matrix(int a, int b, int c, int d, int e, int f, int g, int h, int i)
        {
            Data = new int[,] { { a, b, c }, { d, e, f }, { g, h, i } };
        }

        // Initialize Vector
        public Matrix(int a, int b, int c)
        {
            Data = new int[,] { { a, b, c } };
        }

        // --------------------------------------------------------------------
        // ------------------- Accessor Methods -------------------------------
        // --------------------------------------------------------------------

        public int[,] Data { get; }

        public int Length { get =>  Data.GetLength(0); }

        // --------------------------------------------------------------------
        // --------------------- Matrix Methods -------------------------------
        // --------------------------------------------------------------------

        private int Determinant()
        {
            int calculate(int a, int b, int c, int d) => a * d - b * c;

            if (Length == 2)
            {
                /*
                 * a b 
                 * c d
                 */
                return calculate(Data[0,0], Data[0,1], Data[1,0], Data[1,1]);
            }
            else // Length == 3
            {
                /*
                 * a b c
                 * d e f
                 * g h i
                 */
                
                /*
                 * e f
                 * h i
                 */
                int d1 = calculate(Data[1, 1], Data[1, 2], Data[2, 1], Data[2, 2]);
                /*
                 * d f
                 * g i
                 */
                int d2 = calculate(Data[1, 0], Data[1, 2], Data[2, 0], Data[2, 2]);
                /*
                 * d e
                 * g h
                 */
                int d3 = calculate(Data[1, 0], Data[1, 1], Data[2, 0], Data[2, 1]);

                // |A| = a(ei − fh) − b(di − fg) + c(dh − eg)
                return Data[0,0] * d1 - Data[0,1] * d2 + Data[0,2] * d3;
            }
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

        private int[,] Inverse(int[,] m)
        {
            int det = Determinant();
            if (Is_Invertible())
            {
                return null;
            }
            for (int i = 0; i < m.GetLength(0); i++)
            {
                for (int j = 0; j < m.GetLength(1); j++)
                {
                    m[i, j] /= det;
                }
            }
            return m;
        }

        // Check determinant doesn't equal 0 and no common factors with 26
        // Matrix has an inverse if and only if the determinant doesn't equal zero.
        private bool Is_Invertible()
        {
            int det = Determinant();
            return !(det == 0 || GCD(det, 26) != 1);
        }
    }
}
