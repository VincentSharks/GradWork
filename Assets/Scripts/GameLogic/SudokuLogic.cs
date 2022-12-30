using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>

public class SudokuLogic : MonoBehaviour, IPuzzleLogic
{
    private List<int> _possibleInputs = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    public List<int> PossibleInputs { get { return _possibleInputs; } }



    //check board

    private bool CheckBoard()
    {
        //get sum

        //9 in a row is horizontal
        //i + 9 will be vertical
        //3 + 9 for grid





        return false;
    }
}
