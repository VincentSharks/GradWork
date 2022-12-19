using System.Collections.Generic;
using UnityEngine;

public class SuguruLogic : MonoBehaviour, IPuzzleLogic
{
    //numbers 1-5
    //http://sugurulines.com/suguru-puzzle-rules.html


    //SOLUTION: Trial & error
    //generate grid of shapes, try to fill in the grid, after getting a board randomly empty spaces

    //SOLUTION: Trial & error (depth-first recursive backtracking)
    //https://community.wolfram.com/groups/-/m/t/1077888

    //SOLUTION: 
    //https://pzl.org.uk/suguru.html?

    private List<int> possibleInputs = new List<int>() { 1, 2, 3, 4, 5 };
    public List<int> PossibleInputs { get { return possibleInputs; } set { value = possibleInputs; } }
}
