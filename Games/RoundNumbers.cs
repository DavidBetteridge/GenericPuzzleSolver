using System.Collections.Generic;
using System.Linq;

namespace GenericPuzzleSolver.Games
{
    internal static class RoundNumbers
    {
        internal static Cell[,] Setup()
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
    }
}