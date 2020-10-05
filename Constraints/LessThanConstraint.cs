using System.Linq;

namespace GenericPuzzleSolver
{
    class LessThanConstraint : IConstraint
    {
        public LessThanConstraint(int thisColumn, int thisRow, int otherColumn, int otherRow)
        {
            ThisColumn = thisColumn;
            ThisRow = thisRow;
            OtherColumn = otherColumn;
            OtherRow = otherRow;
        }

        public int ThisColumn { get; }
        public int ThisRow { get; }
        public int OtherColumn { get; }
        public int OtherRow { get; }

        public bool ValueIsValid(int possibleValue, Cell[,] board)
        {
            var largestValue = board[OtherColumn, OtherRow].PossibleValues.Max();
            return possibleValue < largestValue;
        }
    }
}
