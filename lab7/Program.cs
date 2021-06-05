using System;
using System.Linq;

namespace lab7
{
    public class Program
    {
        public class RatNum
        {
            private long _numerator;
            private ulong _denominator;

            public RatNum(long numerator, ulong denominator)
            {
                this._numerator = numerator;
                this._denominator = denominator;
            }

            public RatNum(string number)
            {
                _numerator = long.Parse(number.Split('/').First());
                _denominator = ulong.Parse(number.Split('/').Last());
            }

            public static RatNum Parse(string number) => new RatNum(number);

            public static RatNum operator ++(RatNum n)
            {
                n._numerator += (long) n._denominator;
                return n;
            }

            public static RatNum operator --(RatNum n)
            {
                n._numerator -= (long) n._denominator;
                return n;
            }

            public static RatNum operator +(RatNum a, RatNum b)
            {
                var output = new RatNum(a._numerator * (long)b._denominator + b._numerator * (long)a._denominator,
                    a._denominator * b._denominator);
                return output;
            }
            
            public static RatNum operator -(RatNum a, RatNum b)
            {
                var output = new RatNum(a._numerator * (long)b._denominator - b._numerator * (long)a._denominator,
                    a._denominator * b._denominator);
                return output;
            }
            
            public static RatNum operator -(RatNum n)
            {
                n._numerator = -n._numerator;
                return n;
            }

            public static RatNum operator *(RatNum a, RatNum b)
            {
                return new RatNum(a._numerator * a._numerator,
                    a._denominator * b._denominator);
            }
            
            public static RatNum operator /(RatNum a, RatNum b)
            {
                return new RatNum(a._numerator * (long)a._denominator,
                    a._denominator * (ulong)b._numerator);
            }

            public static implicit operator RatNum(int i) => new RatNum(i, 1);
            public static implicit operator RatNum(long l) => new RatNum(l, 1);
            
            public static implicit operator double(RatNum r) => (double) r._numerator / r._denominator;
            public static explicit operator float(RatNum r) => (float) r._numerator / r._denominator;

            public override string ToString() => $"{_numerator}/{_denominator}";
        }

        static void Main()
        {
            RatNum a = RatNum.Parse("2/3"), b = RatNum.Parse("1/4");
            Console.WriteLine(a+b);
            Console.WriteLine(a-b);
            Console.WriteLine(a*b);
            Console.WriteLine(a/b);
        }
    }
}