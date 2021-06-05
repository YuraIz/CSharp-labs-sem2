using System;
using Lab6;
using Lab6.People;
using Lab6.Weapons;

var gameBoard = new GameBoard(
    new HumanContainer(new Knight("arnold", new Sword(30)), 4, 'e'),
    new HumanContainer(new Barbarian("glaukos", new Axe(20)), 1, 'a'),
    new HumanContainer(new Barbarian("amarachi", new Hammer(20)), 8, 'h'),
    new HumanContainer(new Rogue("robin", new Axe(15)), 8, 'a')
);
while (true)
{
    gameBoard.Humans.Sort();
    Console.Clear();
    Console.WriteLine("Commands:\n[human] move [direction]\n[human] attack [other]\nexit");
    gameBoard.Print();
    Console.Write("Humans: ");
    Console.WriteLine("Humans:");
    char current = 'A';
    for (int j = 1; j <= 8; j++)
    {
        for (char i = 'a'; i <= 'h'; i++)
        {
            var hc = gameBoard.Humans.Find(human => human.Coords == (j, i));
            if (hc?.ToString() != null)
                Console.WriteLine($"{current++}: {hc}");
        }
    }

    Console.WriteLine();
    var command = Console.ReadLine();
    if (command != "")
    {
        if (command == "exit")
        {
            break;
        }

        gameBoard.DoCommand(command);
        gameBoard.Humans.RemoveAll(hc => hc.Health < 0);
    }
}