using System;
using System.Collections.Generic;
using System.Linq;
using Lab8;
using Lab8.People;

namespace lab8
{
    internal static class MainMenu
    {
        private static int _currentIndex;

        private static List<Human> _humans = new();

        private static void UpDownMoving(ConsoleKeyInfo key)
        {
            switch (key.Key)
            {
                case ConsoleKey.UpArrow when _currentIndex > 0:
                    _currentIndex--;
                    break;
                case ConsoleKey.DownArrow when _currentIndex < 3:
                    _currentIndex++;
                    break;
                default: return;
            }
        }

        private static void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine($" {(_currentIndex == 0 ? '>' : ' ')} Create new human");
            Console.WriteLine($" {(_currentIndex == 1 ? '>' : ' ')} Show all humans");
            Console.WriteLine($" {(_currentIndex == 2 ? '>' : ' ')} To map");
            Console.WriteLine($" {(_currentIndex == 3 ? '>' : ' ')} Exit");
        }

        public static void Selection()
        {
            KeyHandler keyHandler = UpDownMoving;
            ConsoleKeyInfo currentKey = new();
            while (currentKey.Key != ConsoleKey.Escape)
            {
                while (currentKey.Key != ConsoleKey.Enter)
                {
                    ShowMenu();
                    keyHandler(currentKey = Console.ReadKey());
                }

                currentKey = new();

                switch (_currentIndex)
                {
                    case 0:
                        _humans.Add(CreateHumanMenu.Create());
                        break;
                    case 1:
                        Console.Clear();
                        if (_humans.Count == 0)
                        {
                            Console.WriteLine("Now you don't have humans");
                        }
                        else
                        {
                            Console.WriteLine("Your humans:");
                            foreach (var human in _humans)
                            {
                                Console.WriteLine(human);
                            }
                        }

                        Console.ReadKey();
                        break;
                    case 2:
                        if (_humans.Count == 0)
                        {
                            Console.WriteLine("Now you don't have humans");
                        }
                        else
                        {
                            GameBoard.RunGameBoard(_humans);
                        }

                        Console.ReadKey();
                        break;
                    default: return;
                }
            }
        }

        private delegate void KeyHandler(ConsoleKeyInfo key);
    }
}