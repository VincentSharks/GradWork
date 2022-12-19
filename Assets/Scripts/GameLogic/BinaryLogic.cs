using System.Collections.Generic;
using UnityEngine;

public class BinaryLogic : MonoBehaviour, IPuzzleLogic
{
    //

    //SOLUTION: (to solve) go over the board when 2 00 are found add a 1 on either side, go over the board horizontally once done go over it vertically and repeat
    //randomly fill in some data, then try to solve

    //SOLUTION: 
    //

    private List<int> possibleInputs = new List<int>() { 0, 1 };
    public List<int> PossibleInputs { get { return possibleInputs; } set { value = possibleInputs; } }
}
