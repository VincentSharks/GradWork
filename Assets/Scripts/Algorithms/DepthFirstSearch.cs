using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System;

/// <summary>
/// Logic for the Depth-Frist Search algorithm (Version 1) (Made with support from ChatGPT)
/// </summary>

public class DepthFirstSearch : MonoBehaviour, IAlgorithm
{
    public InfoManager Info { get { return FindObjectOfType<InfoManager>(); } }
    private List<int> _inputs = new List<int>();
    public List<int> Inputs { get { return _inputs; } }
    public int Count { get { return Inputs.Count; } }
    private List<GameObject> _fields = new List<GameObject>();
    public List<GameObject> Fields { get { return _fields; } }
    private int _algorithmVersion = 1;
    public int AlgorithmVersion { get { return _algorithmVersion; } }


    private UnityEvent _algorithmEnd = new UnityEvent();
    public UnityEvent AlgorithmEnd { get { return _algorithmEnd; } }

    private UnityEvent _algorithmStart = new UnityEvent();
    public UnityEvent AlgorithmStart { get { return _algorithmStart; } }


    private void OnEnable()
    {
        _inputs = Info.PossibleInputs;
        _fields = Info.InputFields;
    }
    
    public void Run()
    {
        _inputs = Info.PossibleInputs;
        _fields = Info.InputFields;


        _algorithmStart.Invoke();
        Info.StartAlgorithmTime = DateTime.Now;

        CallSolver();

        Info.EndAlgorithmTime = DateTime.Now;
        Info.IsReadyForData = true;
        //_algorithmEnd.Invoke();
    }

    private void CallSolver()
    {
        var info = FindObjectOfType<InfoManager>();
        var board = new List<int>();

        for (int i = 0; i < Info.InputFields.Count; i++)
        {
            board.Add(0);
        }

        var output = Solver(board);

        for (int j = 0; j < Info.InputFields.Count; j++)
        {
            Info.InputFields[j].GetComponent<TMP_InputField>().text = output[j].ToString();
        }
    }
    
    private List<int> Solver(List<int> board) // This function takes a 1D list of integers representing a Sudoku board and returns a solved version of the board using a DFS algorithm.
    {        
        List<List<int>> board2D = ConvertTo2D(board); // Convert the 1D list to a 2D list.        
        bool solved = DFS(board2D, 0, 0); // Start the DFS at the first cell.
        
        if (solved) // If the board was solved, convert it back to a 1D list and return it. Otherwise, return an empty list.
        {
            return ConvertTo1D(board2D);
        }
        else
        {
            //return new List<int>();
            return ConvertTo1D(board2D);
        }
    }

    // This function uses DFS to try all possible numbers for a given cell and continues the search until a solution is found or all possibilities have been exhausted.
    private bool DFS(List<List<int>> board, int row, int col)
    {        
        if (row == Info.BoardSize.y) // If the end of the board has been reached, a solution has been found.
        {
            return true;
        }
        
        if (board[row][col] != 0) // If the current cell is not empty, move on to the next cell.
        {
            return DFSNextCell(board, row, col);
        }
        
        Shuffle(_inputs); // Create a list of possible numbers and shuffle it.

        
        for (int i = 1; i <= _inputs.Count; i++) // Try all possible numbers for the current cell.
        {
            var index = _inputs[i - 1]; // If the number is valid for the current cell, set the cell to that number and continue the search.

            if (IsValidNumber(board, row, col, index))
            {
                board[row][col] = index;
                if (DFSNextCell(board, row, col))
                {
                    return true;
                }
            }
        }
        
        board[row][col] = 0; // If no solution was found, reset the current cell to 0 and backtrack.
        return false;
    }

    // This function moves to the next cell in the board. If the end of the row has been reached, it moves to the first cell of the next row.
    private bool DFSNextCell(List<List<int>> board, int row, int col)
    {
        if (col == Info.BoardSize.x-1)
        {
            return DFS(board, row + 1, 0);
        }
        else
        {
            return DFS(board, row, col + 1);
        }
    }

    // This function checks if a given number is valid for a given cell in the Sudoku board.
    private bool IsValidNumber(List<List<int>> board, int row, int col, int num)
    {
        for (int i = 0; i < Info.BoardSize.x; i++) // Check the row.
        {
            if (board[row][i] == num)
            {
                return false;
            }
        }

        for (int i = 0; i < Info.BoardSize.y; i++) // Check the column.
        {
            if (board[i][col] == num)
            {
                return false;
            }
        }

        if (Info.PuzzleName == "Sudoku") //Box check for sudoku
        {
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
        }

        return true;
    }

    // This function converts a 1D list to a 2D list.
    private List<List<int>> ConvertTo2D(List<int> board)
    {
        List<List<int>> board2D = new List<List<int>>();
        for (int i = 0; i < Info.BoardSize.y; i++)
        {
            List<int> row = new List<int>();
            for (int j = 0; j < Info.BoardSize.x; j++)
            {
                row.Add(board[i * (int)Info.BoardSize.x + j]);
            }
            board2D.Add(row);
        }
        return board2D; //All 0
    }

    // This function converts a 2D list to a 1D list.
    private List<int> ConvertTo1D(List<List<int>> board)
    {
        List<int> board1D = new List<int>();
        for (int i = 0; i < Info.BoardSize.y; i++)
        {
            for (int j = 0; j < Info.BoardSize.x; j++)
            {
                board1D.Add(board[i][j]);
            }
        }
        return board1D;
    }

    // This function shuffles a list in place.
    private void Shuffle(List<int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int temp = list[i];
            int randomIndex = UnityEngine.Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }    
}