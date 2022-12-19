using System.Collections.Generic;
using UnityEngine;

public class WordSearchLogic : MonoBehaviour, IPuzzleLogic
{
    //diagonal?

    //SOLUTION: (to solve) use graph theory
    //

    //SOLUTION: algorithm generate board and place random letters in the remaining slots, backtracking
    //https://weblog.jamisbuck.org/2015/9/26/generating-word-search-puzzles.html
    //https://github.com/jamis/wordsearch

    //SOLUTION: 
    //https://stackoverflow.com/questions/6332652/a-fast-algorithm-for-creating-a-puzzle?noredirect=1&lq=1
    //https://stackoverflow.com/questions/943113/algorithm-to-generate-a-crossword/23435654#23435654

    //SOLUTION:
    //

    private List<int> possibleInputs = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26 };
    public List<int> PossibleInputs { get { return possibleInputs; } set { value = possibleInputs; } }

    //get a board of numbers
    //change the numbers to letters

    private void ChangeToLetters(List<GameObject> filledInFields)
    {
        
    }
}
