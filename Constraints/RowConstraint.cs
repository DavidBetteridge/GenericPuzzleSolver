using System.Collections.Generic;
using System.Linq;

namespace GenericPuzzleSolver
{
    class RowConstraint : IConstraint
    {
        public RowConstraint(int columnNumber, int rowNumber)
        {
            RowNumber = rowNumber;
            ColumnNumber = columnNumber;
        }

        public int RowNumber { get; }
        public int ColumnNumber { get; }

        public bool ValueIsValid(int possibleValue, Cell[,] board)
        {
            var allPossibleValues = new List<string>();
            for (int column = 0; column < board.GetLength(0); column++)
            {
                if (column != ColumnNumber)
                {
                    var possibleValues = board[column, RowNumber].PossibleValues;
                    if (possibleValues.Count == 1 && possibleValues.Single() == possibleValue)
                        return false;

                    if (possibleValues.Count == 2 && possibleValues.Contains(possibleValue))
                        allPossibleValues.Add(string.Join("", possibleValues));
                }
            }

            // Are there two pairs in this row which contain this value?
            if (allPossibleValues.Distinct().Count() != allPossibleValues.Count())
                return false;

            return true;
        }
    }
}
