using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>

public class SuguruLogic : MonoBehaviour, IPuzzleLogic
{  
    private List<int> _possibleInputs = new List<int>() { 1, 2, 3, 4, 5 };
    public List<int> PossibleInputs { get { return _possibleInputs; } }
}
