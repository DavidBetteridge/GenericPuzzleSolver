using System.Linq;

namespace GenericPuzzleSolver
{
    class GreaterThanConstraint : IConstraint
    {
        public GreaterThanConstraint(int thisColumn, int thisRow, int otherColumn, int otherRow)
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
            var smallestValue = board[OtherColumn, OtherRow].PossibleValues.Min();
            return possibleValue > smallestValue;
        }
    }
}
