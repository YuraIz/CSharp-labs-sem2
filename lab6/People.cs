using System;
using System.Collections.Generic;
using Lab6.Weapons;

namespace Lab6
{
    namespace People
    {
        public enum Sex : byte
        {
            Male,
            Female
        }

        ///This class made in lab4
        public abstract class Human : IComparable<Human>, IComparer<Human>
        {
            private int _age;

            private Sex _sex;

            public int Health;

            private Human()
            {
                HumanCount++;
            }

            protected Human(
                string name,
                byte physicalPower = 15,
                byte luck = 15,
                byte intelligence = 15,
                byte constitution = 15,
                byte agility = 15
            ) : this()
            {
                Name = name;
                PhysicalPower = physicalPower;
                Luck = luck;
                Intelligence = intelligence;
                Constitution = constitution;
                Agility = agility;
                Health = physicalPower * constitution * agility / 20;
            }

            protected static int HumanCount { get; set; }

            public string Sex
            {
                set
                {
                    _sex = value.ToLower() switch
                    {
                        "male" => People.Sex.Male,
                        "female" => People.Sex.Female,
                        _ => _sex
                    };
                }
                get => _sex.ToString();
            }

            public string Name { private set; get; }

            public int Age
            {
                set
                {
                    if (value < 150) _age = value;
                }
                get => _age;
            }

            private byte PhysicalPower { set; get; }
            private byte Luck { set; get; }
            private byte Intelligence { set; get; }
            private byte Constitution { set; get; }
            private byte Agility { set; get; }

            private int Level => PhysicalPower + Luck + Intelligence + Constitution + Agility;

            public byte this[char characteristic]
            {
                set
                {
                    switch (characteristic)
                    {
                        case 'P':
                            PhysicalPower = value;
                            break;
                        case 'L':
                            Luck = value;
                            break;
                        case 'I':
                            Intelligence = value;
                            break;
                        case 'C':
                            Constitution = value;
                            break;
                        case 'A':
                            Agility = value;
                            break;
                    }

                    Health = 200 + PhysicalPower + Constitution + Agility;
                }
                get
                {
                    return characteristic switch
                    {
                        'P' => PhysicalPower,
                        'L' => Luck,
                        'I' => Intelligence,
                        'C' => Constitution,
                        'A' => Agility,
                        _ => 0
                    };
                }
            }

            public override string ToString()
            {
                return $"{Name} Health:{Health} P:{PhysicalPower}" +
                       $" L:{Luck} I:{Intelligence} C:{Constitution} A:{Agility}";
            }

            public int CompareTo(Human other) => this.Level.CompareTo(other.Level);

            public int Compare(Human a, Human b)
            {
                return a.Level.CompareTo(b.Level);
            }

            ~Human()
            {
                HumanCount--;
            }
        }

        public interface IAttack
        {
            public void Attack(Human other);
        }

        public class Barbarian : Human, IAttack
        {
            public Barbarian(
                string name,
                Weapon weapon,
                byte physicalPower = 15,
                byte luck = 15,
                byte intelligence = 10,
                byte constitution = 15,
                byte agility = 20
            ) : base(name, physicalPower, luck, intelligence, constitution, agility)
            {
                Weapon = weapon;
            }

            private Weapon Weapon { get; }

            public void Attack(Human other)
            {
                other.Health -= (int) (Weapon.Damage * Weapon.Scale(this) * ((Weapon is Hammer) ? 1.25 : 1));
            }

            public override string ToString()
            {
                return "Barbarian " + base.ToString();
            }

            ~Barbarian()
            {
                HumanCount--;
            }
        }

        public class Knight : Human, IAttack
        {
            public Knight(
                string name,
                Weapon weapon,
                byte physicalPower = 30,
                byte luck = 10,
                byte intelligence = 5,
                byte constitution = 10,
                byte agility = 20
            ) : base(name, physicalPower, luck, intelligence, constitution, agility)
            {
                Weapon = weapon;
            }

            private Weapon Weapon { get; }

            public void Attack(Human other)
            {
                other.Health -= (int) (Weapon.Damage * Weapon.Scale(this) * ((Weapon is Sword) ? 1.25 : 1));
            }

            public override string ToString()
            {
                return "Knight " + base.ToString();
            }

            ~Knight()
            {
                HumanCount--;
            }
        }

        public class Rogue : Human, IAttack
        {
            public Rogue(
                string name,
                Weapon weapon,
                byte physicalPower = 10,
                byte luck = 10,
                byte intelligence = 15,
                byte constitution = 10,
                byte agility = 30
            ) : base(name, physicalPower, luck, intelligence, constitution, agility)
            {
                Weapon = weapon;
            }

            private Weapon Weapon { get; }

            public void Attack(Human other)
            {
                other.Health -= (int) (Weapon.Damage * Weapon.Scale(this) * ((Weapon is Axe) ? 1.25 : 1));
            }

            public override string ToString()
            {
                return "Rogue " + base.ToString();
            }

            ~Rogue()
            {
                HumanCount--;
            }
        }
    }
}