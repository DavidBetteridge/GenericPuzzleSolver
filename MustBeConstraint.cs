using System;

namespace GenericPuzzleSolver
{
    internal class MustBeConstraint : IConstraint
    {
        private readonly Func<int, bool> _rule;

        public MustBeConstraint(Func<int, bool> rule)
        {
            _rule = rule;
        }

        public bool ValueNotValid(int possibleValue, Cell[,] board)
        {
            return !_rule(possibleValue);
        }
    }
}