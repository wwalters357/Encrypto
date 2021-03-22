using System;
using System.Collections.Generic;
using System.Text;

namespace Encrypto.Models
{
    public class Matrix
    {
        // --------------------------------------------------------------------
        // ------------------- Defined Cosntants ------------------------------
        // --------------------------------------------------------------------

        // 2x2 Identity matrix
        private static readonly int[,] I2x2 = new int[2, 2] { { 1, 0 }, { 0, 1 } };

        // 3x3 Identity matrix
        private static readonly int[,] I3x3 = new int[3, 3] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } };

        // 2x2 Cofactor matrix
        private static readonly int[,] C2x2 = new int[2, 2] { { 1, -1 }, { -1, 1 } };

        // 3x3 Cofactor Matrix
        private static readonly int[,] C3x3 = new int[,] { { 1, -1, 1 }, { -1, 1, -1 }, { 1, -1, 1 } };

        // --------------------------------------------------------------------
        // ---------------------- Constructors --------------------------------
        // --------------------------------------------------------------------

        // Initialize zero matrix
        public Matrix(int rows, int cols)
        {
            Data = new int[rows, cols];
        }

        // Initialize using a 2-D array
        public Matrix(int[,] matrix)
        {
            Data = (int[,])matrix.Clone();
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
            Data = new int[,] { { a }, { b }, { c } };
        }

        // --------------------------------------------------------------------
        // ------------------- Accessor Methods -------------------------------
        // --------------------------------------------------------------------

        public int this[int i, int j]
        {
            get { return Data[i, j]; }
            set { Data[i, j] = value; }
        }
        public int[,] Data { get; }
        public int Length { get =>  Data.GetLength(0); }
        public int Width { get => Data.GetLength(1); }
        public int Calculated_Determinant { get; private set; }

        // --------------------------------------------------------------------
        // --------------------- Matrix Methods -------------------------------
        // --------------------------------------------------------------------

        public Matrix Cofactor_Matrix()
        {
            if (Length != Width)
            {
                Console.WriteLine("Both dimensions must be equal!");
                return null;
            }
            Matrix m = new Matrix(Data);
            for (int i = 0; i < Length; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    m[i, j] *= (i == j || i % 2 == 0 && j % 2 == 0) ? 1 : -1;
                }
            }
            return m;
        }

        public int Determinant()
        {
            int calculate(int a, int b, int c, int d) => a * d - b * c;
            int result;

            if (Length == 2)
            {
                /*
                 * a b 
                 * c d
                 */
                result = calculate(Data[0,0], Data[0,1], Data[1,0], Data[1,1]);
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
                result = Data[0,0] * d1 - Data[0,1] * d2 + Data[0,2] * d3;
            }
            return result;
        }

        // Calculate GCD using Euler's algorithm 
        public int GCD(int x, int y)
        {    
            if (x == 0)
            {
                return y;
            }
            else if (x < 0 || y < 0)
            {
                return 1;
            }
            return GCD(y % x, x);
        }

        // Maybe drastically different approach for 2x2 vs 3x3 matricies 
        public Matrix Inverse()
        {
            Matrix Inverse = new Matrix(Data);
            if (Length == 2)
            {
                int temp;
                temp = Inverse[0, 0];
                // a
                Inverse[0, 0] = Inverse[1, 1];
                // d
                Inverse[1, 1] = temp;
            }
            else
            {
                // Apply signs and tranpose matrix
                Inverse = Transpose().Adjucate();
            }

            // Apply Cofactor Matrix
            Inverse = Inverse.Cofactor_Matrix();

            // Divide each entry by determinant
            int divsor = Determinant();

            if (Length == 3 && Math.Abs(divsor) != 1)
            {
                throw new Exception("3x3 matrix must have determinant of (-1, 1)!");
            }

            for (int i = 0; i < Data.GetLength(0); i++)
            {
                for (int j = 0; j < Data.GetLength(1); j++)
                {
                    Inverse[i, j] = (int)Math.Floor(Inverse[i, j] / divsor + 0.5);
                }
            }
            return Inverse;
        }

        // Check determinant doesn't equal 0 and no common factors with 26
        // Matrix has an inverse if and only if the determinant doesn't equal zero.
        public bool Is_Invertible()
        {
            Calculated_Determinant = Determinant();
            bool result = !(Calculated_Determinant == 0 || GCD(Calculated_Determinant, 26) != 1);
            Matrix actual = this * this.Inverse();
            // Ensure the product of matrix and inverse equals the identity matrix.        
            return (result) ? actual == new Matrix(I2x2) : result;
        }

        // Calculate modulus division
        protected int Mod(int x, int b)
        {
            int r = x % b;
            return (x < 0) ? r + b : r;
        }

        public Matrix Transpose()
        {
            Matrix m = new Matrix(Data);
            for (int i = 0; i < Length; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    // swap [i, j] => [j, i]
                    int temp = m[i, j];
                    m[i, j] = m[j, i];
                    m[j, i] = temp;
                }
            }
            return m;
        }

        public Matrix Adjucate()
        {
            Matrix Create_SubMatrix(int row, int col)
            {
                int[] arr = new int[4];
                int index = 0;
                for (int i = 0; i < Length; i++)
                {
                    for (int j = 0; j < Width; j++)
                    {
                        if (i != row && j != col)
                        {
                            arr[index++] = this[i, j];
                        }
                    }
                }
                return new Matrix(arr[0], arr[1], arr[2], arr[3]);
            }

            Matrix m = new Matrix(Data);
            if (Length == 3)
            {
                for (int i = 0; i < Length; i++)
                {
                    for (int j = 0; j < Width; j++)
                    {
                        m[i, j] = Create_SubMatrix(i, j).Determinant();
                    }
                }
            }
            return m;
        }

        // --------------------------------------------------------------------
        // ------------------ Overrided Operators -----------------------------
        // --------------------------------------------------------------------

        // Returns result of Matrix x Matrix
        public static Matrix operator *(Matrix A, Matrix B)
        {
            if (A.Width != B.Length)
            {
                throw new Exception("Invalid operation! Width of first and Length of second must be equal");
            }
            Matrix result = new Matrix(B.Length, B.Width);
            for (int i = 0; i < B.Width; i++)
            {
                for (int j = 0; j < A.Length; j++)
                {
                    int sum = 0;
                    for (int k = 0; k < B.Length; k++)
                    {
                        sum += A[j, k] * B[k, i];
                    }
                    result[j, i] = sum;
                }
            }
            return result;
        }

        public static bool operator ==(Matrix A, Matrix B)
        {
            if (A.Length != B.Length || A.Width != B.Width)
            {
                return false;
            }
            for (int i = 0; i < A.Length; i++)
            {
                for (int j = 0; j < B.Width; j++)
                {
                    if (A[i, j] != B[i, j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static bool operator !=(Matrix A, Matrix B)
        {
            if (A.Length != B.Length || A.Width != B.Width)
            {
                return true;
            }
            for (int i = 0; i < A.Length; i++)
            {
                for (int j = 0; j < B.Width; j++)
                {
                    if (A[i, j] == B[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        // --------------------------------------------------------------------
        // -------------------- Display Methods -------------------------------
        // --------------------------------------------------------------------

        public override string ToString()
        {
            string str = "\nMatrix:\n";
            for (int i = 0; i < Length; i++)
            {
                str += "| ";
                for (int j = 0; j < Width; j++)
                {
                    str += String.Format(" {0,3} ", Data[i, j]);
                }
                str += "|\n";
            }
            return str + "\n";
        }

        public void Print(int[,] matrix)
        {
            string str = "";
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                str += "|";
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    str += String.Format(" {0,3} ", matrix[i, j]);
                }
                str += "|\n";
            }
            Console.WriteLine(str);
        }
    }
}
