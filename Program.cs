using System;
using System.Collections.Generic;
using System.Linq;

namespace GenericPuzzleSolver
{
    class Program
    {

        private static Cell[,] SetupRoundNumbersGame()
        {
            var board = new Cell[8, 8];

            var cellMustBeEven = false;
            for (int column = 0; column < board.GetLength(0); column++)
            {
                for (int row = 0; row < board.GetLength(1); row++)
                {
                    board[column, row] = new Cell();
                    board[column, row].PossibleValues = Enumerable.Range(1, board.GetLength(0)).ToList();
                    board[column, row].Constraints = new List<IConstraint>
                    {
                        new ColumnConstraint(column, row),
                        new RowConstraint(column, row)
                    };

                    if (cellMustBeEven)
                        board[column, row].Constraints.Add(new MustBeConstraint(valueToCheck => valueToCheck % 2 == 0));
                    else
                        board[column, row].Constraints.Add(new MustBeConstraint(valueToCheck => valueToCheck % 2 == 1));

                    cellMustBeEven = !cellMustBeEven;
                }

                cellMustBeEven = !cellMustBeEven;
            }

            board[2, 0].Constraints.Add(new MustBeConstraint(valueToCheck => valueToCheck == 1));
            board[6, 0].Constraints.Add(new MustBeConstraint(valueToCheck => valueToCheck == 7));

            board[6, 1].Constraints.Add(new MustBeConstraint(valueToCheck => valueToCheck == 6));

            board[1, 2].Constraints.Add(new MustBeConstraint(valueToCheck => valueToCheck == 2));
            board[4, 2].Constraints.Add(new MustBeConstraint(valueToCheck => valueToCheck == 1));
            board[7, 2].Constraints.Add(new MustBeConstraint(valueToCheck => valueToCheck == 8));

            board[1, 3].Constraints.Add(new MustBeConstraint(valueToCheck => valueToCheck == 1));
            board[2, 3].Constraints.Add(new MustBeConstraint(valueToCheck => valueToCheck == 2));
            board[6, 3].Constraints.Add(new MustBeConstraint(valueToCheck => valueToCheck == 4));
            board[7, 3].Constraints.Add(new MustBeConstraint(valueToCheck => valueToCheck == 7));

            board[2, 4].Constraints.Add(new MustBeConstraint(valueToCheck => valueToCheck == 5));
            board[3, 4].Constraints.Add(new MustBeConstraint(valueToCheck => valueToCheck == 2));
            board[5, 4].Constraints.Add(new MustBeConstraint(valueToCheck => valueToCheck == 4));

            board[0, 5].Constraints.Add(new MustBeConstraint(valueToCheck => valueToCheck == 2));
            board[1, 5].Constraints.Add(new MustBeConstraint(valueToCheck => valueToCheck == 5));

            board[0, 6].Constraints.Add(new MustBeConstraint(valueToCheck => valueToCheck == 3));
            board[1, 6].Constraints.Add(new MustBeConstraint(valueToCheck => valueToCheck == 6));

            board[0, 7].Constraints.Add(new MustBeConstraint(valueToCheck => valueToCheck == 4));
            board[3, 7].Constraints.Add(new MustBeConstraint(valueToCheck => valueToCheck == 1));
            board[5, 7].Constraints.Add(new MustBeConstraint(valueToCheck => valueToCheck == 3));

            return board;
        }

        static void Main(string[] args)
        {
            var board = SetupRoundNumbersGame();

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
                                if (board[column, row].Constraints.Any(c => c.ValueNotValid(possibleValue, board)))
                                {
                                    board[column, row].PossibleValues.Remove(possibleValue);
                                }
                            }
                        }
                    }
                }

                for (int column = 0; column < board.GetLength(0); column++)
                {
                    for (int possibleValue = 0; possibleValue < board.GetLength(0); possibleValue++)
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
                    for (int possibleValue = 0; possibleValue < board.GetLength(1); possibleValue++)
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

                DisplayBoard(board);

            }

            Console.ReadKey();

        }

        private static Cell[,] SetupFutoshikiGame()
        {
            var board = new Cell[5, 5];

            for (int column = 0; column < board.GetLength(0); column++)
            {
                for (int row = 0; row < board.GetLength(1); row++)
                {
                    board[column, row] = new Cell();
                    board[column, row].PossibleValues = Enumerable.Range(1, board.GetLength(0)).ToList();
                    board[column, row].Constraints = new List<IConstraint>
                    {
                        new ColumnConstraint(column, row),
                        new RowConstraint(column, row)
                    };
                }
            }
            board[0, 1].PossibleValues = new List<int> { 2 };
            AddLessThanGreaterThanConstraint(board, 0, 0, 1, 0);
            AddLessThanGreaterThanConstraint(board, 4, 0, 4, 1);
            AddLessThanGreaterThanConstraint(board, 0, 0, 0, 1);

            AddLessThanGreaterThanConstraint(board, 0, 1, 1, 1);
            AddLessThanGreaterThanConstraint(board, 2, 1, 2, 2);

            AddLessThanGreaterThanConstraint(board, 1, 2, 2, 2);
            AddLessThanGreaterThanConstraint(board, 3, 2, 2, 2);
            AddLessThanGreaterThanConstraint(board, 4, 2, 3, 2);
            AddLessThanGreaterThanConstraint(board, 3, 2, 3, 3);

            AddLessThanGreaterThanConstraint(board, 1, 3, 1, 4);
            AddLessThanGreaterThanConstraint(board, 3, 3, 3, 4);

            AddLessThanGreaterThanConstraint(board, 1, 4, 2, 4);
            AddLessThanGreaterThanConstraint(board, 2, 4, 2, 3);
            AddLessThanGreaterThanConstraint(board, 2, 4, 3, 4);

            return board;
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

        private static void AddLessThanGreaterThanConstraint(Cell[,] board, int columnGreaterThan, int rowGreaterThan, int columnLessThan, int rowLessThan)
        {
            board[columnGreaterThan, rowGreaterThan].Constraints.Add(new GreaterThanConstraint(columnGreaterThan, rowGreaterThan, columnLessThan, rowLessThan));
            board[columnLessThan, rowLessThan].Constraints.Add(new LessThanConstraint(columnLessThan, rowLessThan, columnGreaterThan, rowGreaterThan));
        }
    }
}
