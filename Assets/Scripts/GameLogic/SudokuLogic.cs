using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>

public class SudokuLogic : MonoBehaviour, IPuzzleLogic
{
    private List<int> _possibleInputs = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    public List<int> PossibleInputs { get { return _possibleInputs; } }
}
