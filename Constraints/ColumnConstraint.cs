using System.Collections.Generic;
using System.Linq;

namespace GenericPuzzleSolver
{
    class ColumnConstraint : IConstraint
    {
        public ColumnConstraint(int columnNumber, int rowNumber)
        {
            RowNumber = rowNumber;
            ColumnNumber = columnNumber;
        }

        public int RowNumber { get; }
        public int ColumnNumber { get; }

        public bool ValueIsValid(int possibleValue, Cell[,] board)
        {
            var allPossibleValues = new List<string>();
            for (int row = 0; row < board.GetLength(1); row++)
            {
                if (row != RowNumber)
                {
                    var possibleValues = board[ColumnNumber, row].PossibleValues;
                    if (possibleValues.Count == 1 && possibleValues.Single() == possibleValue)
                        return false;

                    if (possibleValues.Count == 2 && possibleValues.Contains(possibleValue))
                        allPossibleValues.Add(string.Join("", possibleValues));
                }
            }

            // Are there two pairs in this column which contain this value?
            if (allPossibleValues.Distinct().Count() != allPossibleValues.Count())
                return false;

            return true;
        }
    }
}
