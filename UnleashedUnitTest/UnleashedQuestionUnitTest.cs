using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnleashedQuestion;
using System;

namespace UnleashedUnitTest
{
    [TestClass]
    public class UnleashedQuestionUnitTest
    {
        [DataRow(15, true)]
        [DataRow(20, false)]
        [DataRow(19, true)]
        [DataRow(-Int32.MaxValue, false)]
        [DataTestMethod]
        public void TestCheckContainsTeen(int testVal, bool expected)
        {
            bool actual = Program.CheckContainsTeen(testVal);
            Assert.AreEqual(expected, actual);
        }

        [DataRow(876235, 6)]
        [DataRow(0, 0)]
        [DataRow(1, 1)]
        [DataRow(-Int32.MaxValue, 0)]
        [DataTestMethod]
        public void TestNumberOfDigits(int testVal, int expected)
        {
            int actual = Program.NumberOfDigits(testVal);
            Assert.AreEqual(expected, actual);
        }

        [DataRow(8235, 8235, 0)]
        [DataRow(0, 0, 0)]
        [DataRow(214141.5624, 214141, 56)]
        [DataRow(12345.67, 12345, 67)]
        [DataTestMethod]
        public void TestSeperateNumbers(double testVal, int expectedWhole, int expectedDecimal)
        {
            (int actualWhole, int actualDecimal) = Program.SeperateNumbers(Convert.ToDecimal(testVal));
            Assert.AreEqual(expectedWhole, actualWhole);
            Assert.AreEqual(expectedDecimal, actualDecimal);
        }

        [DataRow(8, "Eight ")]
        [DataRow(-231, "ERROR in FirstDigit() ")]
        [DataRow(1, "One ")]
        [DataRow(0, "")]
        [DataTestMethod]
        public void TestFirstDigit(int testVal, string expected)
        {
            string actual = Program.FirstDigit(testVal);
            Assert.AreEqual(expected, actual);
        }

        [DataRow(19, "Nineteen ")]
        [DataRow(17, "Seventeen ")]
        [DataRow(1, "ERROR in TeenDigits() ")]
        [DataRow(12, "Twelve ")]
        [DataTestMethod]
        public void TestTeenDigits(int testVal, string expected)
        {
            string actual = Program.TeenDigits(testVal);
            Assert.AreEqual(expected, actual);
        }

        [DataRow(8, "Eighty ")]
        [DataRow(-31, "ERROR in SecondDigit() ")]
        [DataRow(1, "Ten ")]
        [DataRow(0, "")]
        [DataTestMethod]
        public void TestSecondDigit(int testVal, string expected)
        {
            string actual = Program.SecondDigit(testVal);
            Assert.AreEqual(expected, actual);
        }

        [DataRow(7, "Seven Hundred ")]
        [DataRow(-5, "ERROR in FirstDigit() Hundred ")]
        [DataRow(1, "One Hundred ")]
        [DataRow(9, "Nine Hundred ")]
        [DataTestMethod]
        public void TestThirdDigit(int testVal, string expected)
        {
            string actual = Program.ThirdDigit(testVal);
            Assert.AreEqual(expected, actual);
        }

        [DataRow(7, "Seven ")]
        [DataRow(500, "ERROR in SecondDigit() ")]
        [DataRow(12, "Twelve ")]
        [DataRow(99, "Ninety Nine ")]
        [DataTestMethod]
        public void TestHandleTens(int testVal, string expected)
        {
            string actual = Program.HandleTens(testVal);
            Assert.AreEqual(expected, actual);
        }

        [DataRow(70, true, "And Seventy ")]
        [DataRow(5000, false, "ERROR in NumberToText() ")]
        [DataRow(12, false, "Twelve ")]
        [DataRow(7, false, "Seven ")]
        [DataRow(878, false, "Eight Hundred And Seventy Eight ")]
        [DataRow(700, false, "Seven Hundred ")]
        [DataTestMethod]
        public void TestHandleHundreds(int testVal, bool compoundAnd, string expected)
        {
            string actual = Program.HandleHundreds(testVal, compoundAnd);
            Assert.AreEqual(expected, actual);
        }

        [DataRow(9787, "Nine Thousand Seven Hundred And Eighty Seven ")]
        [DataRow(911876, "Nine Hundred And Eleven Thousand Eight Hundred And Seventy Six ")]
        [DataRow(70, "Thousand And Seventy ")]
        [DataRow(999999999, "ERROR in NumberToText() Thousand Nine Hundred And Ninety Nine ")]
        [DataTestMethod]
        public void TestHandleThousands(int testVal, string expected)
        {
            string actual = Program.HandleThousands(testVal);
            Assert.AreEqual(expected, actual);
        }

        [DataRow(1000000, "One Million ")]
        [DataRow(7070, "Million Seven Thousand And Seventy ")]
        [DataRow(128389787, "One Hundred And Twenty Eight Million Three Hundred And Eighty Nine Thousand Seven Hundred And Eighty Seven ")]
        [DataRow(1007534, "One Million Seven Thousand Five Hundred And Thirty Four ")]
        [DataRow(1000006, "One Million And Six ")]
        [DataTestMethod]
        public void TestHandleMillions(int testVal, string expected)
        {
            string actual = Program.HandleMillions(testVal);
            Assert.AreEqual(expected, actual);
        }

        [DataRow(128389787, "One Hundred And Twenty Eight Million Three Hundred And Eighty Nine Thousand Seven Hundred And Eighty Seven ")]
        [DataRow(852576874, "Eight Hundred And Fifty Two Million Five Hundred And Seventy Six Thousand Eight Hundred And Seventy Four ")]
        [DataRow(1007534, "One Million Seven Thousand Five Hundred And Thirty Four ")]
        [DataRow(795914, "Seven Hundred And Ninety Five Thousand Nine Hundred And Fourteen ")]
        [DataRow(89787, "Eighty Nine Thousand Seven Hundred And Eighty Seven ")]
        [DataRow(6981, "Six Thousand Nine Hundred And Eighty One ")]
        [DataRow(453, "Four Hundred And Fifty Three ")]
        [DataRow(88, "Eighty Eight ")]
        [DataRow(7, "Seven ")]
        [DataRow(-7, "Invalid Input In ConvertIntToString()")]
        [DataTestMethod]
        public void TestConvertToString(int testVal, string expected)
        {
            string actual = Program.ConvertIntToString(testVal);
            Assert.AreEqual(expected, actual);
        }

        [DataRow(8000000.77, "Eight Million Dollars And Seventy Seven Cents")]
        [DataRow(889787.25, "Eight Hundred And Eighty Nine Thousand Seven Hundred And Eighty Seven Dollars And Twenty Five Cents")]
        [DataRow(675.00, "Six Hundred And Seventy Five Dollars ")]
        [DataRow(0.95, "Ninety Five Cents")]
        [DataRow(26, "Twenty Six Dollars ")]
        [DataRow(1049219421.4213, "Invalid Input In ConvertIntToString()Dollars And Forty Two Cents")]
        [DataTestMethod]
        public void TestConvertCurrencyToString(double testVal, string expected)
        {
            string actual = Program.ConvertCurrencyToString(Convert.ToDecimal(testVal));
            Assert.AreEqual(expected, actual);
        }
    }
}
