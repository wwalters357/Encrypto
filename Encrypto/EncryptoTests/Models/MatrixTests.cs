using Microsoft.VisualStudio.TestTools.UnitTesting;
using Encrypto.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Encrypto.Models.Tests
{
    [TestClass()]
    public class MatrixTests
    {
        [TestMethod()]
        public void MatrixTest()
        {
            Matrix matrix = new Matrix(4, 4);
            Assert.IsNotNull(matrix);
        }

        [TestMethod()]
        public void MatrixTest1()
        {
            int[,] m = new int[,] { { 1, 0 }, { 0, 1 } };
            Matrix matrix = new Matrix(m);
            Assert.IsNotNull(matrix);
            m[0, 0] = 666;
            Assert.AreNotEqual(matrix, m);
        }

        [TestMethod()]
        public void MatrixTest2()
        {
            Matrix matrix = new Matrix(1, 0, 1, 0);
            Assert.IsNotNull(matrix);
        }

        [TestMethod()]
        public void MatrixTest3()
        {
            Matrix matrix = new Matrix(1, 0, 0, 0, 1, 0, 0, 0, 1);
            Assert.IsNotNull(matrix);
        }

        [TestMethod()]
        public void MatrixTest4()
        {
            Matrix matrix = new Matrix(1, 1, 1);
            Assert.IsNotNull(matrix);
        }

        [TestMethod()]
        public void DeterminantTest()
        {
            Matrix m = new Matrix(new int[,] { { 4, 2, 6 }, { -1, -4, 5 }, { 3, 7, 2 } });
            int actual = m.Determinant();
            int expected = -108;
            Assert.AreEqual(expected, actual);

            m = new Matrix(new int[,] { { 2, -3, 5 }, { -3, 6, 2 }, { 1, -2, 5 } });
            actual = m.Determinant();
            expected = 17;
            Assert.AreEqual(expected, actual);

            m = new Matrix(new int[,] { { 3, 4, 1 }, { 2, 5, -2 }, { -1, 6, -3 } });
            actual = m.Determinant();
            expected = 40;
            Assert.AreEqual(expected, actual);

            m = new Matrix(1, 2, 3, 4);
            actual = m.Determinant();
            expected = -2;
            Assert.AreEqual(expected, actual);

            m = new Matrix(-1, -2, 6, 3);
            actual = m.Determinant();
            expected = 9;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GCDTest()
        {
            Matrix m = new Matrix(0, 0);
            int actual = m.GCD(1023, 421);
            int expected = 1;
            Assert.AreEqual(expected, actual);

            m = new Matrix(0, 0);
            actual = m.GCD(76234, 128);
            expected = 2;
            Assert.AreEqual(expected, actual);

            m = new Matrix(0, 0);
            actual = m.GCD(601484, 26);
            expected = 26;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
         public void InverseTest()
        {
            // Testing 2x2 Matrix
            Matrix actual = new Matrix(-3, -2, 7, 5);
            actual = actual.Inverse();
            Matrix expected = new Matrix(-5, -2, 7, 3);
            Assert.IsTrue(expected == actual);

            actual = new Matrix(234, 23, 45, 346);
            actual = actual.Inverse();
            expected = new Matrix(0, 0, 0, 0);
            Assert.IsTrue(expected == actual);

            // Testing 3x3 Matrix
            /*actual = new Matrix(1, 2, 3, 0, 1, 4, 5, 6, 0);
            actual = actual.Inverse();
            expected = new Matrix(-24, 18, 5, 20, -15, -4, -5, 4, 1);
            Assert.IsTrue(expected == actual);

            actual = new Matrix(6, 24, 1, 13, 16, 10, 20, 17, 15);
            actual = actual.Inverse();
            expected = new Matrix(0, 0, 0, 0, 0, 0, 0, 0, 0);
            Assert.IsTrue(expected == actual);*/
        }

        [TestMethod()]
        public void Is_InvertibleTest()
        {
            Matrix m = new Matrix(2, 3, 3, 4);
            Assert.IsTrue(m.Is_Invertible());

            m = new Matrix(-4, 3, 3, -2);
            Assert.IsTrue(m.Is_Invertible());

           /* m = new Matrix(1, 2, 3, 4, 5, 6, 7, 8, 9);
            Assert.IsFalse(m.Is_Invertible());

            m = new Matrix(6, 24, 1, 13, 16, 10, 20, 17, 15);
            Assert.IsTrue(m.Is_Invertible());*/
        }

        [TestMethod()]
        public void TransposeTest()
        {
            // Test 2x2 matrix
            Matrix actual = new Matrix(-3, -2, 7, 5);
            actual = actual.Transpose();
            Matrix expected = new Matrix(-3, 7, -2, 5);
            Assert.IsTrue(expected == actual);

            // Test 3x3 matrix
            actual = new Matrix(0, 1, 2, 3, 4, 5, 6, 7, 8);
            actual = actual.Transpose();
            expected = new Matrix(0, 3, 6, 1, 4, 7, 2, 5, 8);
            Assert.IsTrue(expected == actual);
        }

        [TestMethod()]
        public void Cofactor_MatrixTest()
        {
            // Test 2x2 matrix
            Matrix actual = new Matrix(1, 1, 1, 1);
            actual = actual.Cofactor_Matrix();
            Matrix expected = new Matrix(1, -1, -1, 1);
            Assert.IsTrue(expected == actual);

            // Test 3x3 matrix
            actual = new Matrix(1, 1, 1, 1, 1, 1, 1, 1, 1);
            actual = actual.Cofactor_Matrix();
            expected = new Matrix(1, -1, 1, -1, 1, -1, 1, -1, 1);
            Assert.IsTrue(expected == actual);
        }

        [TestMethod()]
        public void AdjucateTest()
        {
            Matrix actual = new Matrix(1, 0, 5, 2, 1, 6, 3, 4, 0);
            actual = actual.Adjucate();
            Matrix expected = new Matrix(-24, -18, 5, -20, -15, 4, -5, -4, 1);
            Assert.IsTrue(expected == actual);
        }

        [TestMethod()]
        public void MultiplicationTest()
        {
            Matrix x = new Matrix(1, 2, 3, 4);
            Matrix y = new Matrix(2, 1);
            y[0, 0] = 3;
            y[1, 0] = 7;
            Matrix expected = x * y;
            Matrix actual = new Matrix(2, 1);
            actual[0, 0] = 17;
            actual[1, 0] = 37;
            Assert.IsTrue(expected == actual);

            x = new Matrix(88, 34, 23, 314);
            y = new Matrix(324, 53, 43, 893);
            expected = x * y;
            actual = new Matrix(29974, 35026, 20954, 281621);
            Assert.IsTrue(expected == actual);

            x = new Matrix(1,2,3,4,5,6,7,8,9);
            y = new Matrix(9,8,7,6,5,4,3,2,1);
            expected = x * y;
            actual = new Matrix(30,24,18,84,69,54,138,114,90);
            Assert.IsTrue(expected == actual);
        }
    }
}