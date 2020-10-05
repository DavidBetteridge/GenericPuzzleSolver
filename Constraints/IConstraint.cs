namespace GenericPuzzleSolver
{
    interface IConstraint
    {
        bool ValueIsValid(int possibleValue, Cell[,] board);
    }
}
