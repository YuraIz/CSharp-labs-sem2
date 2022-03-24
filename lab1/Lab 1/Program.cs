using System;
using System.Linq;
using System.Collections.Generic;

namespace Lab_1
{
    internal static class Check
    {
        public static string Number(string item)
        {
            if (item.All(i => i <= '9' && i >= '0')) return item;
            Console.WriteLine("It is not a number");
            return "0";
        }
    }

    internal static class Compare
    {
        public static bool Strings(string first, string second)
        {
            if (first.Length != second.Length)
            {
                Console.WriteLine($"Wrong length, current length is {first.Length}");
                return false;
            }

            var matchingNumbers = new List<char>();

            for (var i = 0; i < first.Length; i++)
            {
                if (matchingNumbers.Any(j => j == first[i]) || first[i] != second[i]) continue;
                matchingNumbers.Add(first[i]);
                Console.Write('B');
            }

            foreach (var i in first.Where(i => second.Any(j => j == i)))
            {
                if (matchingNumbers.Any(j => j == i)) continue;
                matchingNumbers.Add(i);
                Console.Write('K');
            }

            Console.WriteLine();

            return first == second;
        }
    }

    internal static class Program
    {
        public static void Main()
        {
            var random = new Random();
            Console.WriteLine("Choose difficulty level");
            var number = new char[Convert.ToInt32(Check.Number(Console.ReadLine()))];

            for (var i = 0; i < number.Length; i++) number[i] = Convert.ToChar(random.Next() % 10 + '0');

            var super = new string(number);

            Console.WriteLine("Try to guess the number");
            var counter = 0;
            while (!Compare.Strings(super, Check.Number(Console.ReadLine()))) counter++;

            Console.WriteLine($"You guessed the number in {counter} tries");
        }
    }
}