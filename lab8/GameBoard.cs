using System;
using System.Collections.Generic;
using System.Linq;
using Lab8.People;

namespace Lab8
{
    public enum Direction : byte
    {
        Left = 15,
        Right = 240,
        Up = 85,
        Down = 170
    }

    public class HumanContainer : IComparable<HumanContainer>, IComparer<HumanContainer>
    {
        public void ArrowMove(ConsoleKeyInfo key)
        {
            switch (key.Key)
            {
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.RightArrow:
                    case ConsoleKey.DownArrow:
                        break;
                    default: return;
            }
            Move(
                key.Key switch
                {
                    ConsoleKey.UpArrow => Direction.Up,
                    ConsoleKey.LeftArrow => Direction.Left,
                    ConsoleKey.RightArrow => Direction.Right,
                    ConsoleKey.DownArrow => Direction.Down,
                    _ => Direction.Down
                }
            );
        }

        public HumanContainer(Human human, byte xCoord, char yCoord)
        {
            _human = human;
            _x = xCoord;
            _y = yCoord;
        }

        private readonly Human _human;

        private byte _x;
        private char _y;

        public (byte, char) Coords => (_x, _y);

        public string Name => _human.Name;

        public int CompareTo(HumanContainer other) => this._human.CompareTo(other?._human);

        public int Compare(HumanContainer a, HumanContainer b)
        {
            return a?.CompareTo(b) ?? -1;
        }

        public override string ToString() => _human.ToString();

        public int Health => _human.Health;

        public void Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    if (_y > 'a') _y--;
                    break;
                case Direction.Right:
                    if (_y < 'z') _y++;
                    break;
                case Direction.Down:
                    if (_x < 8) _x++;
                    break;
                case Direction.Up:
                    if (_x > 1) _x--;
                    break;
            }
        }

        public bool TryAttack(HumanContainer other)
        {
            var (x, y) = other.Coords;
            if ((x - _x >= -1 && x - _x <= 1) && (y - _y >= -1 && y - _y <= 1))
            {
                ((IAttack) _human).Attack(other._human);
                return true;
            }

            return false;
        }
    }

    public readonly struct GameBoard
    {
        private delegate void KeyHandler(ConsoleKeyInfo key);
        
        public static void RunGameBoard(List<Human> humans)
        {
            List<HumanContainer> humanContainers = new();
            Random rand = new Random();
            foreach (var human in humans)
            {
                humanContainers.Add(new HumanContainer(human,
                    Convert.ToByte(rand.Next(minValue:1, maxValue: 8)),
                    Convert.ToChar(rand.Next(minValue: 'a', maxValue: 'z'))));
            }

            GameBoard gameBoard = new GameBoard(humanContainers.ToArray());
            
            KeyHandler keyHandler = gameBoard._humans[0].ArrowMove;
            while (true)
            {
                gameBoard._humans.Sort();
                Console.Clear();
                gameBoard.Print();
                Console.Write("Humans: ");
                Console.WriteLine("Humans:");
                char current = 'A';
                for (int j = 1; j <= 8; j++)
                {
                    for (char i = 'a'; i <= 'z'; i++)
                    {
                        var hc = gameBoard._humans.Find(human => human.Coords == (j, i));
                        if (hc?.ToString() != null)
                            Console.WriteLine($"{current++}: {hc}");
                    }
                }

                Console.WriteLine();
                var currentKey = Console.ReadKey();
                if (currentKey.Key != ConsoleKey.Escape && currentKey.Key != ConsoleKey.Enter)
                {
                    keyHandler(currentKey);
                }
                else
                {
                    break;
                }
            }
        }

        private readonly List<HumanContainer> _humans;

        private GameBoard(params HumanContainer[] humans)
        {
            _humans = humans.ToList();
        }

        public void DoCommand(string command)
        {
            var args = command.Split(' ');

            switch (args[1])
            {
                case "move":
                {
                    _humans.Find(hc => hc.Name == args[0])
                        ?.Move(
                            args[2] switch
                            {
                                "up" => Direction.Up,
                                "down" => Direction.Down,
                                "left" => Direction.Left,
                                "right" => Direction.Right,
                                _ => 0
                            }
                        );
                    break;
                }
                case "attack":
                {
                    _humans.Find(hc => hc.Name == args[0])
                        ?.TryAttack(_humans.Find(hc => hc.Name == args[2]));
                    break;
                }
                default: return;
            }
        }

        private void Print()
        {
            char h = 'A';
            Console.WriteLine("  a b c d e f g h i j k l m n o p q r s t u v w x y z");
            for (int j = 1; j <= 8; j++)
            {
                Console.Write(j + " ");
                for (char i = 'a'; i <= 'z'; i++)
                {
                    Console.Write(_humans.Any(human => human.Coords == (j, i)) ? h++ + " " : ". ");
                }

                Console.WriteLine(" ");
            }
        }
    }
}