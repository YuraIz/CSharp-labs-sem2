using System;
using System.Runtime.InteropServices;

namespace Task2
{
    public class MathOperations
    {
        [DllImport("../../../MathOperations.dll", CharSet = CharSet.Unicode, EntryPoint = "?Fact@@YAKI@Z")]
        public static extern ulong Fact(uint a);

        [DllImport("../../../MathOperations.dll", CharSet = CharSet.Unicode, EntryPoint = "?Sum@@YAHHZZ")]
        private static extern int MSum(int count, int a, int b, int c = 0, int d = 0);

        public static int Sum(params int[] ints)
        {
            return ints.Length switch
            {
                1 => ints[0],
                2 => MSum(2, ints[0], ints[1]),
                3 => MSum(3, ints[0], ints[1], ints[2]),
                4 => MSum(4, ints[0], ints[1], ints[2], ints[3]),
                _ => Sum(ints[0..4]) + Sum(ints[4..^0])
            };
        }
        
        [DllImport("../../../MathOperations.dll", CharSet = CharSet.Unicode, EntryPoint = "?Multiply@@YAHHZZ")]
        private static extern int MMultiply(int count, int a, int b, int c = 1, int d = 1);
        
        public static int Multiply(params int[] ints)
        {
            return ints.Length switch
            {
                1 => ints[0],
                2 => MMultiply(2, ints[0], ints[1]),
                3 => MMultiply(3, ints[0], ints[1], ints[2]),
                4 => MMultiply(4, ints[0], ints[1], ints[2], ints[3]),
                _ => Multiply(ints[0..4]) * Multiply(ints[4..^0])
            };
        }
    }
    
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Factorial of 5 is " + MathOperations.Fact(5));
            Console.WriteLine("5 + 32 + 5 + 15 + 89 + 36 + 145 == " + MathOperations.Sum(5, 32, 5, 15, 89, 36, 145));
            Console.WriteLine("20 * 15 * 32 * 40 * 25 == " + MathOperations.Multiply(20, 15, 32, 40, 25));
        }
    }
}