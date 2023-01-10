using System.Collections.Generic;

public interface IPuzzleLogic
{
    /// <summary> List with all the possibly inputs (in int form E.G. Sudoku = 1-9, Word Search 1-26, ...) </summary>
    public List<int> PossibleInputs { get; }

    //Correctness check

    public bool IsValidNumber(List<List<int>> board, int row, int col, int num, InfoManager info);
}
