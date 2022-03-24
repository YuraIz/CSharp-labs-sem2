using System;
using System.Linq;

namespace Lab_2
{
    internal static class Check
    {
        public static string Number(string item)
        {
            if (item.All(i => i <= '9' && i >= '.' && i != '/')) return item;
            Console.WriteLine("It is not a number");
            return "0";
        }
        public static string Word(string item)
        {
            if (item.All(i => ((i >= 'A' && i <= 'Z') || (i >= 'a' && i <= 'z')))) return item;
            Console.WriteLine("It is not a word");
            return "0";
        }
        public static string Text(string item)
        {
            if (item.All(i => ((i >= 'A' && i <= 'Z') || (i >= 'a' && i <= 'z') || i == ' '))) return item;
            Console.WriteLine("It is not a word");
            return "0";
        }
    }
    
    internal static class
        Task1 //Calculate the maximum degree of two that divides the product of consecutive numbers from a to b
    {
        public static ulong ReturnDeg(ulong first, ulong last)
        {
            if (first > last) return 0;
            first += first & 1;
            last -= last & 1;
            ulong i = 0;
            ulong productOfNumbers = first;
            for (; (productOfNumbers & 1) == 0; i++) productOfNumbers = productOfNumbers >> 1;

            while (first < last)
            {
                for (productOfNumbers *= first += 2; (productOfNumbers & 1) == 0; i++)
                {
                    productOfNumbers = productOfNumbers >> 1;
                }

                productOfNumbers = 1;
            }

            return i;
        }
    }

    internal static class Task2 //Convert string to a double
    {
        private static int Pow(int x, int a)
        {
            if (a < 1) return 1;
            for (int x0 = x; a > 1; a--)
            {
                x *= x0;
            }

            return x;
        }

        public static double StrToDouble(string @string)
        {
            double number = 0;
            short point = 0;
            for (int i = 0; i < @string.Length; i++, number *= 10)
            {
                if (point > 0)
                {
                    point++;
                }
                else if (@string[i] == 46)
                {
                    i++;
                    point++;
                }

                number += @string[i] - 48;
            }

            number /= Pow(10, point + 1);

            return number;
        }
    }

    internal static class Task3 //Write character numbers as hexadecimal numbers
    {
        private static int Pow(int x, int a)
        {
            if (a < 1) return 1;
            for (int x0 = x; a > 1; a--)
            {
                x *= x0;
            }

            return x;
        }

        private static string IntToHex(int number)
        {
            var arr = new byte[4];
            for (var i = 0; i < 4; i++)
            {
                arr[i] = Convert.ToByte((number / Pow(16, 3 - i)) % 16);
            }

            var charArr = new char[4];

            for (var i = 0; i < 4; i++)
            {
                charArr[i] = (arr[i] < 9) ? Convert.ToChar(48 + arr[i]) : Convert.ToChar(65 + arr[i] % 10);
            }

            var hex = new string(charArr);
            return hex;
        }

        public static void WriteNumbers(string text)
        {
            foreach (var t in text)
            {
                Console.Write(IntToHex(Convert.ToInt32(t)) + " ");
            }
        }
    }

    internal static class Program
    {
        static void Main()
        {
            Console.WriteLine("Please, select between 1, 2 and 3");
            switch (Convert.ToChar(Console.Read()))
            {
                case '1':
                {
                    Console.ReadLine();
                    Console.WriteLine("Selected Task 1");
                    Console.WriteLine(
                        "Calculate the maximum degree of two that divides the product of consecutive numbers from a to b");
                    Console.Write("a=");
                    ulong a = Convert.ToUInt64(Check.Number(Console.ReadLine()));
                    Console.Write("b=");
                    ulong b = Convert.ToUInt64(Check.Number(Console.ReadLine()));
                    Console.WriteLine("Answer is " + Task1.ReturnDeg(a, b));
                    return;
                }
                case '2':
                {
                    Console.ReadLine();
                    Console.WriteLine("Selected Task 2");
                    Console.WriteLine("Convert string to a double");
                    Console.WriteLine(Task2.StrToDouble(Check.Number(Console.ReadLine())));
                    return;
                }
                case '3':
                {
                    Console.ReadLine();
                    Console.WriteLine("Selected Task 3");
                    Console.WriteLine("Write character numbers as hexadecimal numbers");
                    Task3.WriteNumbers(Console.ReadLine());
                    Console.WriteLine();
                    return;
                }
                default:
                {
                    Console.WriteLine("Selected nothing");
                    return;
                }
            }
        }
    }
}