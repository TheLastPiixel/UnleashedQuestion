using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnleashedQuestion
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while(true)
            {
                Console.WriteLine(GetUserInput());
            }
        }

        #region Main Methods
        //Description: Converts a decimal number representing currency to its text format
        public static string ConvertCurrencyToString(decimal currencyValue)
        {
            int wholeNumber, decimalNumber;
            string dollarString = "";
            string centsString = "";
            bool dollarsPlural, centsPlural, hasCents;

            (wholeNumber, decimalNumber) = SeperateNumbers(currencyValue);
            dollarsPlural = wholeNumber > 1 ? true : false;
            centsPlural = decimalNumber > 1 ? true : false;
            hasCents = decimalNumber > 0 ? true : false;

            if (decimalNumber > 0)
            {
                centsString = ConvertIntToString(decimalNumber) + "Cent" + (centsPlural ? "s" : "");
            }

            if (wholeNumber > 0)
            {
                dollarString = ConvertIntToString(wholeNumber) + "Dollar" + (dollarsPlural ? "s " : " ") + (hasCents ? "And " : "");
            }

            return dollarString + centsString;
        }

        //Description: Converts integers between 1 - 999,999,999 to spelt out text format
        public static string ConvertIntToString(int value)
        {
            int noOfDigits = NumberOfDigits(value);
            switch (noOfDigits)
            {
                case int n when (n > 0 && n <= 3):
                    return HandleHundreds(value, false);
                    break;
                case int n when (n > 3 && n <= 6):
                    return HandleThousands(value);
                    break;
                case int n when (n > 6 && n <= 9):
                    return HandleMillions(value);
                    break;
                default:
                    return "Invalid Input In ConvertIntToString()";
                    break;
            }
        }
        #endregion

        #region HandlerFunctions
        //Description: Handles int to string conversion for numbers between 1 - 999,999,999
        //Input: 1-9 digit positive integer
        public static string HandleMillions(int value)
        {
            int millions = Convert.ToInt32(Decimal.Round(value / 1000000));
            int thousands = Convert.ToInt32(Decimal.Round(value / 1000)) % 1000;
            int hundreds = (value % 1000000) - (thousands * 1000);

            //Helpers for thousands string
            bool requiresConjunction = thousands < 100 ? false : true;
            string thousandSuffix = thousands != 0 ? "Thousand " : "";

            //Assemble the strings for values in the millions, thousands, and hundreds
            string thousandsString = HandleHundreds(thousands, requiresConjunction) + thousandSuffix;
            string hundredsString = HandleHundreds(hundreds, true);
            string millionsString = HandleHundreds(millions, false) + "Million ";

            return millionsString + thousandsString + hundredsString;
        }

        //Description: Handles int to string conversion for numbers between 1 - 999,999
        //Input: 1-6 digit positive integer
        public static string HandleThousands(int value)
        {
            int thousands = Convert.ToInt32(Decimal.Round(value / 1000));
            int hundreds = value % 1000;
            if (value != 0)
            {
                return HandleHundreds(thousands, false) + "Thousand " + HandleHundreds(hundreds, true);
            }
            return "";
        }

        //Description: Handles the int to string conversaion for numbers between 1 - 999
        //Input: 1-3 digit positive integer and a bool to enable or disable the prefix "And"
        public static string HandleHundreds(int value, bool needsConjunction)
        {
            int digitCount = NumberOfDigits(value);
            string prefixConjunction = (needsConjunction ? "And " : "");
            switch (digitCount)
            {
                case 0:
                    return "";
                    break;
                case 1:
                    return prefixConjunction + FirstDigit(value);
                    break;
                case 2:
                    {
                        return prefixConjunction + HandleTens(value);
                    }
                    break;
                case 3:
                    {
                        int thirdDigit = Convert.ToInt32(Decimal.Round(value / 100));
                        int firstTwoDigits = value % 100;
                        string compoundConjunction = firstTwoDigits == 0 ? "" : "And ";
                        return ThirdDigit(thirdDigit) + compoundConjunction + HandleTens(firstTwoDigits);
                    }
                    break;
                default:
                    return "ERROR in NumberToText() ";
                    break;
            }
        }


        //Description: Handles the int to string conversion for numbers between 1 - 99
        //Input: 1-2 digit positive integer
        public static string HandleTens(int value)
        {
            int firstDigit = Convert.ToInt32(Decimal.Round(value / 10));
            int secondDigit = value % 10;
            if (value >= 11 && value <= 19)
            {
                return TeenDigits(value);
            }
            else
            {
                return SecondDigit(firstDigit) + FirstDigit(secondDigit);
            }
        }
        #endregion

        #region NumbersToString Functions
        //Description: Returns the string for a value in the hundreds (ie One Hundred, Two Hundred, .., Nine Hundred)
        //Input: Takes in a single digit (ie X in 20,X25)
        public static string ThirdDigit(int value)
        {
            return FirstDigit(value) + "Hundred ";
        }


        //Description: Returns a string for a value in the tens (ie Ten, Twenty, .., Ninety)
        //Input: Takes in a single digit (ie X in 2,3X9)
        public static string SecondDigit(int value)
        {
            switch (value)
            {
                case 0:
                    return "";
                    break;
                case 1:
                    return "Ten ";
                    break;
                case 2:
                    return "Twenty ";
                    break;
                case 3:
                    return "Thirty ";
                    break;
                case 4:
                    return "Forty ";
                    break;
                case 5:
                    return "Fifty ";
                    break;
                case 6:
                    return "Sixty ";
                    break;
                case 7:
                    return "Seventy ";
                    break;
                case 8:
                    return "Eighty ";
                    break;
                case 9:
                    return "Ninety ";
                    break;
                default:
                    return "ERROR in SecondDigit() ";
                    break;
            }
        }

        //Description: Returns the string for "Teen" values (ie 11, 12, 13, .., 19)
        //Input: Takes in a 2 digit integer >= 11 and <= 19
        public static string TeenDigits(int value)
        {
            switch (value)
            {
                case 11:
                    return "Eleven ";
                    break;
                case 12:
                    return "Twelve ";
                    break;
                case 13:
                    return "Thirteen ";
                    break;
                case 15:
                    return "Fifteen ";
                    break;
                case 18:
                    return "Eighteen ";
                    break;
                case int n when (n == 14 || n == 16 || n == 17 || n == 19): 
                    string numberString = FirstDigit(n % 10);
                    //Removes the space after the ones value (ie "Six " - to make "Sixteen")
                    int stringLength = numberString.Length;
                    return numberString.Remove(stringLength - 1, 1) + "teen ";
                default:
                    return "ERROR in TeenDigits() ";
                    break;
            }
        }

        //Description: Returns the string for value in the ones (ie One,Two, .., Nine)
        //Input: Takes in a signle digit integer (ie X in 9,23X)
        public static string FirstDigit(int value)
        {
            switch (value)
            {
                case 0:
                    return "";
                    break;
                case 1:
                    return "One ";
                    break;
                case 2:
                    return "Two ";
                    break;
                case 3:
                    return "Three ";
                    break;
                case 4:
                    return "Four ";
                    break;
                case 5:
                    return "Five ";
                    break;
                case 6:
                    return "Six ";
                    break;
                case 7:
                    return "Seven ";
                    break;
                case 8:
                    return "Eight ";
                    break;
                case 9:
                    return "Nine ";
                    break;
                default:
                    return "ERROR in FirstDigit() ";
                    break;
            }
        }
        #endregion

        #region Helper Functions
        //Description: Seperates the whole number from the decimal number
        public static (int wholeNum, int decimalNum) SeperateNumbers(decimal value)
        {
            int wholeNum = Convert.ToInt32(Math.Floor(value));
            int decimalNum = Convert.ToInt32((value - wholeNum) * 100);
            return (wholeNum, decimalNum);
        }

        //Description: Returns the number of digits in a number
        public static int NumberOfDigits(int value)
        {
            try
            {
                int digitCount = Convert.ToInt32(Math.Floor(Math.Log10(value) + 1));
                return digitCount;
            }
            catch (OverflowException)
            {
                return 0;
            }
        }

        //Description: Gets and validates user input then passes it to be converted into text
        public static string GetUserInput()
        {
            decimal input = 0;
            Console.WriteLine("Enter currency values to convert: ");
            try
            {
                input = Math.Round(Convert.ToDecimal(Console.ReadLine()), 2, MidpointRounding.ToEven);
                if (input < (decimal)0.01 || input > (decimal)999999999.99)
                {
                    return "Invalid Input! value must be between 0.01 and 999999999.99";
                }
                else
                {
                    return ConvertCurrencyToString(input);
                }
            }
            catch (FormatException)
            {
                return "Invalid Input Type! please only enter decimal types";
            }
        }
        #endregion
    }
}