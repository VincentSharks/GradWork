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

    public bool CheckBoard(List<int> board)
    {
        bool horizontalCorrect = false;
        bool verticalCorrect = false;
        bool boxCorrect = false;

        List<int> listForSum = new List<int>();
        
        int index = 1;
        int limit = 9;
        int counter = 1;

        #region Horizontal
        while (!horizontalCorrect && !verticalCorrect && !boxCorrect)
        {
            for (int i = index; i <= limit; i++)
            {
                listForSum.Add(board[i]); //Get first 9 put them in a list
            }

            if (GetSum(listForSum) != 45) //is the sum of is not 45
            {
                Debug.Log("Horizontal Line ISN'T correct");
                return false; //else false and get out            
            }
            else
            {
                Debug.Log("Horizontal Line IS correct");

                listForSum.Clear();

                index += 9; //start at the first one of the next line 
                //limit += 9; //   
            }

            if (limit >= board.Count) //Limit has been set outside of the board
            {
                Debug.Log("Horizontal Lines are ALL correct");

                index = 1;
                limit = 9;
                horizontalCorrect = true; //if all are true
            }
        }
        #endregion

        #region Vertical
        while (horizontalCorrect && !verticalCorrect && !boxCorrect)
        {
            //get first one
            //first one + 9
            //previous + 9 ...
            //put them in a list
            //check the sum
            //return true or false
            
            for (int i = index; i <= limit; i++)
            {
                listForSum.Add(board[index]); //Get first 9 put them in a list
                index += 9;
            }

            if (GetSum(listForSum) != 45) //is the sum of is not 45
            {
                Debug.Log("Vertical Line ISN'T correct");
                return false; //else false and get out            
            }
            else
            {
                Debug.Log("Vertical Line IS correct");

                listForSum.Clear();

                index = 1 + counter; //start at the first one of the next line 
                counter++;
                //limit += 9; //   
            }

            if (counter >= 9) //Limit has been set outside of the board
            {
                Debug.Log("Vertical Lines are ALL correct");

                index = 1;
                limit = 9;
                counter = 1;
                verticalCorrect = true; //if all are true
            }
        }
        #endregion

        //Box
        #region Box
        while (horizontalCorrect && verticalCorrect && !boxCorrect)
        {
            //get 3
            //first + 9, get those 3
            //get +9 and get those 3
            //put them in a list and check

            for (int i = index; i <= limit; i++)
            {
                listForSum.Add(board[index]); //Get first 9 put them in a list
                
                if(i % 3 == 0)
                {
                    index += 6;
                }
                else
                {
                    index += 1;
                }
            }

            if (GetSum(listForSum) != 45) //is the sum of is not 45
            {
                Debug.Log("Box ISN'T correct");
                return false; //else false and get out            
            }
            else
            {
                Debug.Log("Box IS correct");

                listForSum.Clear();

                index = 1 + (counter * 3);
            }

            if (counter >= 8) //Limit has been set outside of the board
            {
                Debug.Log("Boxes are ALL correct");

                index = 1;
                limit = 9;
                counter = 1;
                boxCorrect = true; //if all are true
            }
        }
        #endregion

        if(horizontalCorrect && verticalCorrect && boxCorrect)
        {
            return true;
        }

        return false;
    }

    private int GetSum(List<int> listForSum)
    {
        int totalSum = 0;

        foreach (int entry in listForSum)
        {
            totalSum += entry;
        }

        return totalSum;
    }
}