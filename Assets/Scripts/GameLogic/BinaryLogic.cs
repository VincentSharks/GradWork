using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds the logic of the Bainry puzzles (List of possible inputs & logic on checking the correctness of the generated puzzle)
/// </summary>

public class BinaryLogic : MonoBehaviour, IPuzzleLogic
{   
    private List<int> _possibleInputs = new List<int>() { 0, 1 };
    public List<int> PossibleInputs { get { return _possibleInputs; } }

    public bool IsValidNumber(List<List<int>> board, int row, int col, int num, InfoManager info)
    {
        throw new System.NotImplementedException();
    }
}