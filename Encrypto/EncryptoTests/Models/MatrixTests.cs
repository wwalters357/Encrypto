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
   
            Matrix matrix = new Matrix(new int[,] { { 1, 0 }, { 0, 1 } });
            Assert.IsNotNull(matrix);
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
            Matrix m = new Matrix(new int[,] { { 4, 2, 6}, { -1, -4, 5 }, { 3, 7, 2 } });
            int actual = m.Determinant();
            int expected = -108;
            Assert.AreEqual(expected, actual);

            new Matrix(new int[,] { { 2, -3, 5 }, { -3, 6, 2 }, { 1, -2, 5 } });
            actual = m.Determinant();
            expected = 17;
            Assert.AreEqual(expected, actual);

            new Matrix(new int[,] { { 3, 4, 1 }, { 2, 5, -2 }, { -1, 6, -3 } });
            actual = m.Determinant();
            expected = 40;
            Assert.AreEqual(expected, actual);

            new Matrix(1, 2, 3, 4);
            actual = m.Determinant();
            expected = -2;
            Assert.AreEqual(expected, actual);

            new Matrix(-1, -2, 6, 3);
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
            expected = 0;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void InverseTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void Is_InvertibleTest()
        {
            Assert.Fail();
        }
    }
}