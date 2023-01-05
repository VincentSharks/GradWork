using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//ChatGPT

/// <summary>
/// The SolveSudoku function then calls the Dfs function, which is a recursive function that tries 
/// all possible numbers for a given cell and continues the search until a solution is found or all 
/// possibilities have been exhausted. 
/// If a solution is found, the function returns true; 
/// otherwise, it returns false. The Dfs function also uses the DfsNextCell function to 
/// move to the next cell in the board, and the IsValidNumber function to check if a given 
/// number is valid for a given cell in the Sudoku board.
/// </summary>

public class DepthFirstSearch : MonoBehaviour
{
    //2Dmlist of integers? representing sudoku board
    //return solved board
    //DFS algorithm
    //

    public void CallSolver()
    {
        var info = FindObjectOfType<InfoManager>();
        var board = new List<int>();

        for (int i = 0; i < info.inputFields.Count; i++)
        {
            board.Add(0);
        }

        var output = SolveSudoku(board);

        for (int j = 0; j < info.inputFields.Count; j++)
        {
            info.inputFields[j].GetComponent<TMP_InputField>().text = output[j].ToString();
        }
    }


    // This function takes a 1D list of integers representing a Sudoku board and returns a solved version of the board using a DFS algorithm.
    public List<int> SolveSudoku(List<int> board)
    {
        // Convert the 1D list to a 2D list.
        List<List<int>> board2D = ConvertTo2D(board); 

        // Start the DFS at the first cell.
        bool solved = Dfs(board2D, 0, 0);

        // If the board was solved, convert it back to a 1D list and return it. Otherwise, return an empty list.
        if (solved)
        {
            return ConvertTo1D(board2D);
        }
        else
        {
            return new List<int>();
        }
    }

    // This function uses DFS to try all possible numbers for a given cell and continues the search until a solution is found or all possibilities have been exhausted.
    private bool Dfs(List<List<int>> board, int row, int col)
    {
        // If the end of the board has been reached, a solution has been found.
        if (row == 9)
        {
            return true;
        }

        // If the current cell is not empty, move on to the next cell.
        if (board[row][col] != 0)
        {
            return DfsNextCell(board, row, col);
        }

        // Try all possible numbers for the current cell.
        for (int i = 1; i <= 9; i++)
        {
            // If the number is valid for the current cell, set the cell to that number and continue the search.
            if (IsValidNumber(board, row, col, i))
            {
                //Debug.Log("Number: " + i);

                board[row][col] = i;
                if (DfsNextCell(board, row, col))
                {
                    return true;
                }
            }
        }

        // If no solution was found, reset the current cell to 0 and backtrack.
        board[row][col] = 0;
        return false;
    }

    // This function moves to the next cell in the board. If the end of the row has been reached, it moves to the first cell of the next row.
    private bool DfsNextCell(List<List<int>> board, int row, int col)
    {
        if (col == 8)
        {
            return Dfs(board, row + 1, 0);
        }
        else
        {
            return Dfs(board, row, col + 1);
        }
    }

    // This function checks if a given number is valid for a given cell in the Sudoku board.
    private bool IsValidNumber(List<List<int>> board, int row, int col, int num)
    {
        // Check the row.
        for (int i = 0; i < 9; i++)
        {
            if (board[row][i] == num)
            {
                return false;
            }
        }

        // Check the column.
        for (int i = 0; i < 9; i++)
        {
            if (board[i][col] == num)
            {
                return false;
            }
        }

        // Check the 3x3 grid.
        int gridRow = row - row % 3;
        int gridCol = col - col % 3;
        for (int i = gridRow; i < gridRow + 3; i++)
        {
            for (int j = gridCol; j < gridCol + 3; j++)
            {
                if (board[i][j] == num)
                {
                    return false;
                }
            }
        }

        return true;        
    }

    // This function converts a 1D list to a 2D list.
    private List<List<int>> ConvertTo2D(List<int> board)
    {
        List<List<int>> board2D = new List<List<int>>();
        for (int i = 0; i < 9; i++)
        {
            List<int> row = new List<int>();
            for (int j = 0; j < 9; j++)
            {
                row.Add(board[i * 9 + j]);
            }
            board2D.Add(row);
        }
        return board2D; //All 0
    }

    // This function converts a 2D list to a 1D list.
    private List<int> ConvertTo1D(List<List<int>> board)
    {
        List<int> board1D = new List<int>();
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                board1D.Add(board[i][j]);
            }
        }
        return board1D;
    }
}
