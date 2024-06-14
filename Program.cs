using System;
using System.Collections.Generic;

class Position
{
    public int X { get; set; }
    public int Y { get; set; }

    public Position(int x, int y)
    {
        X = x;
        Y = y;
    }
}
class Player
{
    public string Name { get; }
    public Position Position { get; set; }
    public int GemCount { get; set; }
    public int Score { get; set; }

    public Player(string name, Position position)
    {
        // initializing the gem and score count to 0
        Name = name;
        Position = position;
        GemCount = 0;
        Score = 0;
    }
    public void Move(char direction)
    {
        switch (direction)
        {
            case 'U':
                Position.Y -= 1;
                break;
            case 'D':
                Position.Y += 1;
                break;
            case 'L':
                Position.X -= 1;
                break;
            case 'R':
                Position.X += 1;
                break;
        }
    }
}
