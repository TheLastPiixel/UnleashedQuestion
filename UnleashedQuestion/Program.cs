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
            decimal input = 0;
            while (true)
            {
                Console.WriteLine("Enter currency values to convert: ");
                try
                {
                    input = Math.Round(Convert.ToDecimal(Console.ReadLine()), 2, MidpointRounding.ToEven);
                    if (input < (decimal)0.01 || input > (decimal)999999999.99)
                    {
                        Console.WriteLine("Invalid Input!");
                    }
                    else
                    {
                        Console.WriteLine(ConvertCurrencyToString(input));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Invalid Input!");
                }
            }
        }

        #region Main Methods
        //Description: Converts a decimal currency to its spelt out text format
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
                dollarString = ConvertIntToString(wholeNumber) + "Dollar" + (dollarsPlural ? "s " : " ") + (hasCents ? "and " : "");
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
        //Description: Handles int to string conversion for values in the millions
        //Input: Int > 999,999 and < 1,000,000,000
        //Output: String of the value
        public static string HandleMillions(int value)
        {
            int millions = Convert.ToInt32(Decimal.Round(value / 1000000));
            int thousands = Convert.ToInt32(Decimal.Round(value / 1000)) % 1000;
            int hundreds = (value % 1000000) - (thousands * 1000);
            return HandleHundreds(millions, false) + "Million " + HandleHundreds(thousands, true) + "Thousand " + HandleHundreds(hundreds, true);
        }

        //Description: Handles int to string conversion for values in the thousands
        //Input: Int > 999 and < 1,000,000
        //Output: String of the value
        public static string HandleThousands(int value)
        {
            int thousands = Convert.ToInt32(Decimal.Round(value / 1000));
            int hundreds = value % 1000;
            return HandleHundreds(thousands, false) + "Thousand " + HandleHundreds(hundreds, true);
        }

        //Description: Handles int to string conversaion for values in the hundreds
        //Input: Takes in a 1-3 digit value and a bool to enable or disable the compounding "And"
        //Output: String of the value
        public static string HandleHundreds(int value, bool compoundAnd)
        {
            int digitCount = NumberOfDigits(value);
            string compoundString = (compoundAnd ? "And " : "");
            switch (digitCount)
            {
                case 1:
                    return compoundString + SingleDigit(value);
                    break;
                case 2:
                    {
                        return compoundString + HandleTens(value);
                    }
                    break;
                case 3:
                    {
                        int firstDigit = Convert.ToInt32(Decimal.Round(value / 100));
                        int lastTwoDigits = value % 100;
                        return TripleDigits(firstDigit) + "And " + HandleTens(lastTwoDigits);
                    }
                    break;
                default:
                    return "ERROR in NumberToText ";
                    break;
            }
        }

        //Input: Takes in the last two digits in a number (ie XX in 2,4XX)
        //Output: String of the last two digits
        public static string HandleTens(int value)
        {
            int firstDigit = Convert.ToInt32(Decimal.Round(value / 10));
            int secondDigit = value % 10;
            if (CheckContainsTeen(value))
            {
                return IsTeen(value);
            }
            else
            {
                return DoubleDigits(firstDigit) + SingleDigit(secondDigit);
            }
        }
        #endregion

        #region NumbersToString Functions
        //Input: Takes in a single digit (ie X in 20,X25)
        //Output: String for third digit
        public static string TripleDigits(int value)
        {
            return SingleDigit(value) + "Hundred ";
        }

        //Input: Takes in a single digit (ie X in 2,3X9)
        //Output: String for second digit
        public static string DoubleDigits(int value)
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
                    return "Fourty ";
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
                    return "Ninty ";
                    break;
                default:
                    return "ERROR in DoubleDigit ";
                    break;
            }
        }

        //Description: Used to return the string for "Teen" values (ie 12, 13, .. ,19)
        //Input: Takes in a digit integer
        //Output: String for the "Teen" value
        public static string IsTeen(int value)
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
                case int n when (n == 14 || n == 16 || n == 17 || n == 19): //ISSUE WITH SPACES BETWEEN NUMBER AND "TEEN"
                    string numberString = SingleDigit(n % 10);
                    int stringLength = numberString.Length;
                    return numberString.Remove(stringLength - 1, 1) + "teen ";
                default:
                    return "ERROR in IsTeen ";
                    break;
            }
        }

        //Input: Takes in the last digit (ie X in 9,23X)
        //Output: String for last digit
        public static string SingleDigit(int value)
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
                    return "ERROR in SingleDigit ";
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
            catch (System.OverflowException)
            {
                return 0;
            }
        }

        //Description: Check if the last two digits in a value is a "Teen" (ie 11, 12, .., 19)
        //Input: Takes in an an integer of any length
        public static bool CheckContainsTeen(int value)
        {
            int twoDigitNumber = value % 100;
            if (twoDigitNumber >= 11 && twoDigitNumber <= 19)
            {
                return true;
            }
            return false;
        }
        #endregion
    }
}