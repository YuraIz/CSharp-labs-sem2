using System;
using System.Text;
using Lab8.People;
using Lab8.Weapons;

namespace lab8
{
    internal static class CreateHumanMenu
    {
        private static int _currentIndex;


        private static readonly StringBuilder Name = new();
        private static byte _physicalPower = 15;
        private static byte _luck = 15;
        private static byte _intelligence = 15;
        private static byte _constitution = 15;
        private static byte _agility = 15;

        private static HumanClasses _humanClass = HumanClasses.Barbarian;

        private static Weapons _weapon = Weapons.Axe;

        private static int _damage = 10;

        private static void UpDownMoving(ConsoleKeyInfo key)
        {
            switch (key.Key)
            {
                case ConsoleKey.UpArrow when _currentIndex > 0:
                    _currentIndex--;
                    break;
                case ConsoleKey.DownArrow when _currentIndex < 9:
                    _currentIndex++;
                    break;
            }
        }

        private static void NameChanging(ConsoleKeyInfo key)
        {
            if (_currentIndex != 0) return;

            switch (key.Key)
            {
                case >= ConsoleKey.A and <= ConsoleKey.Z:
                    Name.Append(key.KeyChar);
                    break;
                case ConsoleKey.Backspace when Name.Length > 0:
                    Name.Remove(Name.Length - 1, 1);
                    break;
            }
        }

        private static void CharacteristicsChooser(ConsoleKeyInfo key)
        {
            if (_currentIndex is <= 1 or >= 7) return;

            switch (key.Key)
            {
                case ConsoleKey.LeftArrow:
                    switch (_currentIndex)
                    {
                        case 2:
                            _physicalPower--;
                            break;
                        case 3:
                            _luck--;
                            break;
                        case 4:
                            _intelligence--;
                            break;
                        case 5:
                            _constitution--;
                            break;
                        case 6:
                            _agility--;
                            break;
                    }

                    break;
                case ConsoleKey.RightArrow:
                    switch (_currentIndex)
                    {
                        case 2:
                            _physicalPower++;
                            break;
                        case 3:
                            _luck++;
                            break;
                        case 4:
                            _intelligence++;
                            break;
                        case 5:
                            _constitution++;
                            break;
                        case 6:
                            _agility++;
                            break;
                    }

                    break;
            }
        }

        private static void ClassSelection(ConsoleKeyInfo key)
        {
            if (_currentIndex != 1) return;

            switch (key.Key)
            {
                case ConsoleKey.LeftArrow when _humanClass > HumanClasses.Barbarian:
                    _humanClass--;
                    break;
                case ConsoleKey.LeftArrow:
                    _humanClass = HumanClasses.Rogue;
                    break;
                case ConsoleKey.RightArrow when _humanClass < HumanClasses.Rogue:
                    _humanClass++;
                    break;
                case ConsoleKey.RightArrow:
                    _humanClass = HumanClasses.Barbarian;
                    break;
            }
        }

        private static void WeaponSelection(ConsoleKeyInfo key)
        {
            if (_currentIndex != 7) return;

            switch (key.Key)
            {
                case ConsoleKey.LeftArrow when _weapon > Weapons.Axe:
                    _weapon--;
                    break;
                case ConsoleKey.LeftArrow:
                    _weapon = Weapons.Sword;
                    break;
                case ConsoleKey.RightArrow when _weapon < Weapons.Sword:
                    _weapon++;
                    break;
                case ConsoleKey.RightArrow:
                    _weapon = Weapons.Axe;
                    break;
            }
        }

        private static void DamageChooser(ConsoleKeyInfo key)
        {
            if (_currentIndex != 8) return;

            switch (key.Key)
            {
                case ConsoleKey.LeftArrow:
                    _damage--;
                    break;
                case ConsoleKey.RightArrow:
                    _damage++;
                    break;
            }
        }

        private static void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine($" {(_currentIndex == 0 ? '>' : ' ')} Name: {Name}");
            Console.WriteLine($" {(_currentIndex == 1 ? '>' : ' ')} Class: {_humanClass}");
            Console.WriteLine($" {(_currentIndex == 2 ? '>' : ' ')} PhysicalPower: {_physicalPower}");
            Console.WriteLine($" {(_currentIndex == 3 ? '>' : ' ')} Luck: {_luck}");
            Console.WriteLine($" {(_currentIndex == 4 ? '>' : ' ')} Intelligence: {_intelligence}");
            Console.WriteLine($" {(_currentIndex == 5 ? '>' : ' ')} Constitution: {_constitution}");
            Console.WriteLine($" {(_currentIndex == 6 ? '>' : ' ')} Agility: {_agility}");
            Console.WriteLine($" {(_currentIndex == 7 ? '>' : ' ')} Weapon: {_weapon}");
            Console.WriteLine($" {(_currentIndex == 8 ? '>' : ' ')} Damage: {_damage}");
            Console.WriteLine($" {(_currentIndex == 9 ? '>' : ' ')} Create");
        }

        public static Human Create()
        {
            KeyHandler keyHandler = UpDownMoving;
            keyHandler += NameChanging;
            keyHandler += CharacteristicsChooser;
            keyHandler += ClassSelection;
            keyHandler += WeaponSelection;
            keyHandler += DamageChooser;
            ConsoleKeyInfo currentKey = new();
            while (_currentIndex != 9 || currentKey.Key != ConsoleKey.Enter)
            {
                ShowMenu();
                keyHandler(currentKey = Console.ReadKey());
            }

            return _humanClass switch
            {
                HumanClasses.Barbarian => new Barbarian(Name.ToString(), _weapon switch
                {
                    Weapons.Axe => new Axe(_damage),
                    Weapons.Hammer => new Hammer(_damage),
                    _ => new Sword(_damage)
                }, _physicalPower, _luck, _intelligence, _constitution, _agility),
                HumanClasses.Knight => new Knight(Name.ToString(), _weapon switch
                {
                    Weapons.Axe => new Axe(_damage),
                    Weapons.Hammer => new Hammer(_damage),
                    _ => new Sword(_damage)
                }, _physicalPower, _luck, _intelligence, _constitution, _agility),
                _ => new Rogue(Name.ToString(), _weapon switch
                {
                    Weapons.Axe => new Axe(_damage),
                    Weapons.Hammer => new Hammer(_damage),
                    _ => new Sword(_damage)
                }, _physicalPower, _luck, _intelligence, _constitution, _agility)
            };
        }

        private delegate void KeyHandler(ConsoleKeyInfo key);

        private enum HumanClasses : byte
        {
            Barbarian,
            Knight,
            Rogue
        }


        private enum Weapons : byte
        {
            Axe,
            Sword,
            Hammer
        }
    }
}