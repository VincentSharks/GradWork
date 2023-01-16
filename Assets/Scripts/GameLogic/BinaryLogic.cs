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
        //board size / 2  is max of one type
        if (GetValueCountInRow(board, row, num) > board.Count / 2 ||
            GetValueCountInCollumn(board, col, num) > board.Count / 2)
        {
            return false;
        }

        int valueRowCount = 0;
        int colIndex = 0;
        //no more than 2 same digits in a row
        foreach (var cell in board[row])
        {
            //because the cell value hasn't been filled in,
            //you need to also check at the collumn index of that new value
            if (colIndex == col || cell == num)
                ++valueRowCount;
            else
                valueRowCount = 0;

            if (valueRowCount >= 3)
                return false;

            ++colIndex;
        }

        //no more than 2 same digits in a row for collumn
        int valueCollumnCount = 0;
        int rowIndex = 0;
        foreach (var rowList in board)
        {
            if (row == rowIndex || rowList[col] == num)
                ++valueCollumnCount;
            else
                valueCollumnCount = 0;

            if (valueCollumnCount >= 3)
                return false;

            ++rowIndex;
        }

        //check if unique
        //first, check the rows (only whent the row is complete can this be checked)
        if (col == board.Count - 1)
        {
            for (int rowListIndex = 0; rowListIndex < board.Count; rowListIndex++)
            {
                //compare itself will always return true
                if (rowListIndex == row)
                    continue;

                //if one line is that same as the new line 
                if (true)
                    return false;
            }
        }

        //second, check the collumns (only whent the collumn is complete can this be checked)
        if (row == board.Count - 1)//binary board is always a square
        {

        }

        return true;
    }

    int GetValueCountInRow(List<List<int>> board, int row, int value)
    {
        int count = 0;

        foreach (var cell in board[row])
        {
            if (cell == value)
                ++count;
        }

        //+1 becase the evaluted cell value hasn't been put into the grid yet
        return count + 1;
    }

    int GetValueCountInCollumn(List<List<int>> board, int col, int value)
    {
        int count = 0;

        foreach (var row in board)
        {
            if (row[col] == value)
                ++count;
        }

        //+1 becase the evaluted cell value hasn't been put into the grid yet
        return count + 1;
    }
}