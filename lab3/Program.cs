using System;
using System.Collections.Generic;

namespace Lab3
{
    public enum Sex : byte
    {
        Male,
        Female
    }
    public class Human
    {
        private Sex _sex;

        public string Sex
        {
            set
            {
                switch (value.ToLower())
                {
                    case "male":
                        _sex = Lab3.Sex.Male;
                        break;
                    case "female":
                        _sex = Lab3.Sex.Female;
                        break;
                    default:
                        Console.WriteLine("Wrong sex value");
                        break;
                }
            }
            get => _sex.ToString();
        }

        public static int HumanCount { get; private set; }

        public Human()
        {
            HumanCount++;
        }
        
        public string Name { set; get; } = "Ivan";

        public string Surname { set; get; } = "Ivanov";

        public string Patronymic { set; get; } = "Ivanovich";

        public string Fullname
        {
            set
            {
                if (value.Split(' ').Length < 3)
                {
                    Console.WriteLine("Wrong fullname length");
                    return;
                }
                Surname = value.Split(' ')[0];
                Name = value.Split(' ')[1];
                Patronymic = value.Split(' ')[2];
            }
            get => Surname + ' ' + Name + ' ' + Patronymic;
        }

        private int _age;

        public int Age
        {
            set
            {
                if (value < 150)
                {
                    _age = value;
                }
                else
                {
                    Console.WriteLine("Wrong age");
                }
            }
            get => _age;
        }


        public byte this[int index]
        {
            set
            {
                switch (index)
                {
                    case 0: PhysicalPower = value; break;
                    case 1: Luck = value; break;
                    case 2: Intelligence = value; break;
                    case 3: Constitution = value; break;
                    case 4: Agility = value; break;
                }
            }
            get
            {
                return index switch
                {
                    0 => PhysicalPower,
                    1 => Luck,
                    2 => Intelligence,
                    3 => Constitution,
                    4 => Agility,
                    _ => 0
                };
            }
        }

        public byte[] Characteristics
        {
            set
            {
                for (var i = 0; i < value.Length; i++)
                {
                    this[i] = value[i];
                }
            }
            get
            {
                var characteristics = new byte[5];
                for (var i = 0; i < 5; i++)
                {
                    characteristics[i] = this[i];
                }
                return characteristics;
            }
        }
        
        protected byte PhysicalPower { set; get; }
        protected byte Luck { set; get; }
        protected byte Intelligence { set; get; }
        protected byte Constitution { set; get; }
        protected byte Agility { set; get; }

        ~Human()
        {
            HumanCount--;
        }
    }

    public static class Program
    {
        static void Main()
        {
            var human1 = new Human();
            var human2 = new Human();
            human1.Sex = "male";
            human1.Fullname = "Ivanov Igor Vasilievich";
            human2.Sex = "female";
            
            human1.Characteristics = new byte[] {4, 8, 16, 25, 42};
            Console.WriteLine(human1.Fullname);
            Console.WriteLine(human1.Sex);
            Console.WriteLine(human2.Fullname);
            Console.WriteLine(human2.Sex);
            Console.WriteLine(Human.HumanCount);
        }
    }
}