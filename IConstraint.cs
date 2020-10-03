namespace GenericPuzzleSolver
{
    interface IConstraint
    {
        bool ValueNotValid(int possibleValue, Cell[,] board);
    }
}
