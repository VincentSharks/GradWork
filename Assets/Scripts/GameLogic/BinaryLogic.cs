using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Holds the logic of the Bainry puzzles (List of possible inputs & logic on checking the correctness of the generated puzzle)
/// </summary>

public class BinaryLogic : MonoBehaviour, IPuzzleLogic
{
    private List<int> _possibleInputs = new List<int>() { 0, 1 };
    public List<int> PossibleInputs { get { return _possibleInputs; } }

    public bool IsValidNumber(List<List<int>> board, int row, int col, int num, InfoManager info)
    {
        //temp put value in the list to make logic easier
        board[row][col] = num;

        //board size / 2  is max of one type
        if (GetValueCountInRow(board, row, num) > board.Count / 2 ||
            GetValueCountInCollumn(board, col, num) > board.Count / 2)
        {
            board[row][col] = -1;
            return false;
        }

        int valueRowCount = 0;
        //no more than 2 same digits in a row
        foreach (var cell in board[row])
        {
            if (cell == num)
                ++valueRowCount;
            else
                valueRowCount = 0;

            if (valueRowCount >= 3)
            {
                board[row][col] = -1;
                return false;
            }
        }

        //no more than 2 same digits in a row for collumn
        int valueCollumnCount = 0;
        foreach (var rowList in board)
        {
            if (rowList[col] == num)
                ++valueCollumnCount;
            else
                valueCollumnCount = 0;

            if (valueCollumnCount >= 3)
            {
                board[row][col] = -1;
                return false;
            }
        }

        //check if unique
        //first, check the rows (only whent the row is complete can this be checked)
        if (col == board.Count - 1)
        {
            //loop only through completed rows
            for (int rowListIndex = 0; rowListIndex < row; rowListIndex++)
            {
                //compare itself will always return true
                if (rowListIndex == row)
                    continue;

                //if one line is that same as the new line 
                if (board[rowListIndex].SequenceEqual(board[row]))
                {
                    board[row][col] = -1;
                    return false;
                }
            }
        }

        //second, check the collumns (only whent the collumn is complete can this be checked)
        if (row == board.Count - 1)//binary board is always a square
        {
            //var tempColList = new IEnumerable<IEnumerable<int>>();
            var tempColList = board.Select((a, b) => board.Select(c => c[b]));
            //transposed = rows.Select((_, i) => rows.Select(col => col[i]))

            var collumToCompare = tempColList.ElementAt(col);

            //loop only through completed collumns
            //for (int colListIndex = 0; colListIndex < col; colListIndex++)
            int loopCount = -1;
            bool returnValue = tempColList.All(currentCol =>
              {
                  ++loopCount;

                //compare itself will always return true
                if (loopCount == col)
                      return true;

                //if one line is that same as the new line 
                if (currentCol.SequenceEqual(collumToCompare))
                      return false;

                  return true;
              });

            if (returnValue==false)
            {
                board[row][col] = -1;
                return false;
            }
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


        return count;
    }

    int GetValueCountInCollumn(List<List<int>> board, int col, int value)
    {
        int count = 0;

        foreach (var row in board)
        {
            if (row[col] == value)
                ++count;
        }

        return count;
    }
}