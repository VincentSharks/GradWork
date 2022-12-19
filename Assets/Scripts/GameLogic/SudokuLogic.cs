using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  //In all 9 sub matrices 3×3 the elements should be 1-9, without repetition.
//In all rows there should be elements between 1-9 , without repetition.
//In all columns there should be elements between 1-9 , without repetition.

//smallest number of starting clues a Sudoku puzzle can contain is 17

//one single solution



//SOLUTION: box check
//https://www.geeksforgeeks.org/program-sudoku-generator/

//SOLUTION: backtracking solver
//https://www.101computing.net/sudoku-generator-algorithm/

//SOLUTION: faster backtracking?
//https://www.codeproject.com/Articles/23206/Sudoku-Algorithm-Generates-a-Valid-Sudoku-in-0-018

//SOLUTION: unique way, doesn't rely on solvers (don't understand yet)
//https://www.researchgate.net/publication/251863893_A_New_Algorithm_for_Generating_Unique-Solution_Sudoku


//JSON
//0 as an empty slot, 1-9 representing the numbers 
/// </summary>


public class SudokuLogic : MonoBehaviour, IPuzzleLogic
{
    //hold list with all possibilities 
    //

    private List<int> possibleInputs = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    public List<int> PossibleInputs { get { return possibleInputs; } set { value = possibleInputs; } }

    //Have list of possible inputs
    //get list of input fields
    //

    private void OnEnable()
    {
        //fields = board.inputFields;
    }


}
