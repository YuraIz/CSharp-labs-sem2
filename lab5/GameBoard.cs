using System;
using System.Collections.Generic;
using System.Linq;
using Lab5.People;

namespace Lab5
{
    public enum Direction : byte
    {
        Left = 15,
        Right = 240, 
        Up = 85,
        Down = 170
    }

    public class HumanContainer
    {
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

        public string Name => _human.ToString();

        public int Health => _human.Health;

        public void Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    if (_y > 'a') _y--;
                    break;
                case Direction.Right:
                    if (_y < 'h') _y++;
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
        public readonly List<HumanContainer> Humans;

        public GameBoard(params HumanContainer[]humans)
        {
            Humans = humans.ToList();
        }

        public void DoCommand(string command)
        {
            var args = command.Split(' ');
            
            switch (args[1])
            {
                case "move":
                {
                    Humans.Find(hc => hc.Name == args[0])
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
                    Humans.Find(hc => hc.Name == args[0])
                        ?.TryAttack(Humans.Find(hc => hc.Name == args[2]));
                    break;
                }
                default: return;
            }
        }

        public void Print()
        {
            char h = 'A';
            Console.WriteLine("  a b c d e f g h");
            for (int j = 1; j <= 8; j++)
            {
                Console.Write(j + " ");
                for (char i = 'a'; i <= 'h'; i++ )
                {
                    Console.Write(Humans.Any(human => human.Coords == (j, i))?h++ + " ":". ");
                }
                Console.WriteLine(" ");
            }
        }
    }
}