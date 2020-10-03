using System.Collections.Generic;

namespace GenericPuzzleSolver
{
    class Cell
    {
        public List<int> PossibleValues { get; set; }

        public List<IConstraint> Constraints { get; set; }
    }
}
