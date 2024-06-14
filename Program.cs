﻿using System;
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
class Cell
{
    public string Occupant { get; set; } = "-";
}
class Board
{
    public Cell[,] Grid { get; }

    private void PlaceElements() //this method was generated by the POE AI Assistant
    {
        // Place players
        Grid[0, 0].Occupant = "P1";
        Grid[5, 5].Occupant = "P2";

        // Place gems and obstacles randomly
        List<string> elements = new List<string>();
        for (int i = 0; i < 5; i++) elements.Add("G");
        for (int i = 0; i < 5; i++) elements.Add("O");

        Random rand = new Random();
        foreach (var elem in elements)
        {
            while (true)
            {
                int x = rand.Next(6);
                int y = rand.Next(6);
                if (Grid[y, x].Occupant == "-")
                {
                    Grid[y, x].Occupant = elem;
                    break;
                }
            }
        }
    }
    public Board()
    {
        Grid = new Cell[6, 6];
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                Grid[i, j] = new Cell();
            }
        }
        PlaceElements();
    }
    public void Display()
    {
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                Console.Write(Grid[i, j].Occupant + " ");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    public bool IsValidMove(Player player, char direction)
    {
        int newX = player.Position.X;
        int newY = player.Position.Y;

        switch (direction)
        {
            case 'U':
                newY -= 1;
                break;
            case 'D':
                newY += 1;
                break;
            case 'L':
                newX -= 1;
                break;
            case 'R':
                newX += 1;
                break;
        }

        if (newX < 0 || newX >= 6 || newY < 0 || newY >= 6)
            return false;

        string occupant = Grid[newY, newX].Occupant;
        return occupant != "O" && occupant != "P1" && occupant != "P2";
    }

    public void CollectGem(Player player)
    {
        int playerX = player.Position.X;
        int playerY = player.Position.Y;

        if (Grid[playerY, playerX].Occupant == "G")
        {
            player.GemCount += 1;
            Grid[playerY, playerX].Occupant = "-"; //removes the gem
            player.Score += 10;
        }
    }
}
class Game
{
    private Board board;
    private Player player1;
    private Player player2;
    private Player currentTurn;
    private int totalTurns;

    public Game()
    {
        board = new Board();
        player1 = new Player("P1", new Position(0, 0));
        player2 = new Player("P2", new Position(5, 5));
        currentTurn = player1;
        totalTurns = 0;
    }
    private void SwitchTurns()
    {
        currentTurn = currentTurn == player1 ? player2 : player1;
    }

    private bool IsGameOver()
    {
        int maxTurns = 30;
        return totalTurns >= maxTurns;
    }
    private void DisplayScores()
    {
        Console.WriteLine($"Scores: P1 = {player1.Score}, P2 = {player2.Score}");
        Console.WriteLine($"Gems: P1 = {player1.GemCount}, P2 = {player2.GemCount}");
    }
    private void AnnounceWinner()
    {
        Console.WriteLine("Game Over!");
        DisplayScores();
        if (player1.Score > player2.Score)
        {
            Console.WriteLine("Player 1 wins!");
        }
        else if (player2.Score > player1.Score)
        {
            Console.WriteLine("Player 2 wins!");
        }
        else
        {
            Console.WriteLine("It's a draw!");
        }
    }
}