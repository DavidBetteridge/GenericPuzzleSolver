using GenericPuzzleSolver.Games;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GenericPuzzleSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            var board = SkyScrapers.Setup();
            //var board = Futoshiki.Setup();
            //var board = RoundNumbers.Setup();

            while (!Complete(board))
            {
                for (int column = 0; column < board.GetLength(0); column++)
                {
                    for (int row = 0; row < board.GetLength(1); row++)
                    {
                        if (board[column, row].PossibleValues.Count > 1)
                        {
                            foreach (var possibleValue in board[column, row].PossibleValues.ToList())
                            {
                                if (board[column, row].Constraints.Any(c => !c.ValueIsValid(possibleValue, board)))
                                {
                                    board[column, row].PossibleValues.Remove(possibleValue);
                                }
                            }
                        }
                    }
                }

                for (int column = 0; column < board.GetLength(0); column++)
                {
                    for (int possibleValue = 0; possibleValue <= board.GetLength(0); possibleValue++)
                    {
                        var cellsContainingValue = new List<int>();
                        for (int row = 0; row < board.GetLength(1); row++)
                        {
                            if (board[column, row].PossibleValues.Contains(possibleValue))
                            {
                                cellsContainingValue.Add(row);
                            }
                        }

                        if (cellsContainingValue.Count == 1)
                        {
                            if (board[column, cellsContainingValue.Single()].PossibleValues.Count > 1)
                            {
                                board[column, cellsContainingValue.Single()].PossibleValues = new List<int> { possibleValue };
                            }
                        }

                    }
                }

                for (int row = 0; row < board.GetLength(1); row++)
                {
                    for (int possibleValue = 0; possibleValue <= board.GetLength(1); possibleValue++)
                    {
                        var cellsContainingValue = new List<int>();
                        for (int column = 0; column < board.GetLength(0); column++)
                        {
                            if (board[column, row].PossibleValues.Contains(possibleValue))
                            {
                                cellsContainingValue.Add(column);
                            }
                        }

                        if (cellsContainingValue.Count == 1)
                        {
                            if (board[cellsContainingValue.Single(), row].PossibleValues.Count > 1)
                            {
                                board[cellsContainingValue.Single(), row].PossibleValues = new List<int> { possibleValue };
                            }
                        }

                    }
                }

                //DisplayOptions(board, 6, 6);

                DisplayBoard(board);
                Console.ReadKey();

            }

            Console.ReadKey();

        }

        private static bool Complete(Cell[,] board)
        {
            for (int row = 0; row < board.GetLength(1); row++)
            {
                for (int column = 0; column < board.GetLength(0); column++)
                {
                    if (board[column, row].PossibleValues.Count > 1)
                        return false;
                }
            }

            return true;
        }

        private static void DisplayOptions(Cell[,] board, int column, int row)
        {
            var values = string.Join(',', board[column, row].PossibleValues);
            Console.WriteLine($"Cell {column},{row} can be {values}");
        }

        private static void DisplayBoard(Cell[,] board)
        {
            Console.WriteLine("");

            for (int row = 0; row < board.GetLength(1); row++)
            {
                for (int column = 0; column < board.GetLength(0); column++)
                {
                    if (board[column, row].PossibleValues.Count > 1)
                    {
                        Console.Write("_");
                    }
                    else
                    {
                        Console.Write(board[column, row].PossibleValues.Single());
                    }
                }
                Console.WriteLine("");
            }

            Console.WriteLine("");
        }
    }
}
