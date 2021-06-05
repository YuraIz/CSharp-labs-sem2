using System;

namespace Lab6
{
    namespace Weapons
    {
        public abstract class Weapon : IComparable<Weapon>
        {
            protected Weapon(int damage) => Damage = damage;
            public int Damage { get; }

            public virtual double Scale(People.Human human) => human['P'] * human['A'] * 0.01;

            public int CompareTo(Weapon other) => this.Damage.CompareTo(other.Damage);
        }

        public class Axe : Weapon
        {
            public Axe(int damage) : base(damage)
            {
            }

            public override double Scale(People.Human human) => human['P'] * 0.03 + human['A'] * 0.03;
        }

        public class Sword : Weapon
        {
            public Sword(int damage) : base(damage)
            {
            }

            public override double Scale(People.Human human) => human['P'] * 0.01 + human['A'] * 0.04;
        }

        public class Hammer : Weapon
        {
            public Hammer(int damage) : base(damage)
            {
            }

            public override double Scale(People.Human human) => human['P'] * 0.04 + human['A'] * 0.01;
        }
    }
}