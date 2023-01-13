using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Logic for the Depth Frist Search algorithm (Version 1) (Made with support from ChatGPT)
/// </summary>

public class BreadthFirstSearch : MonoBehaviour, IAlgorithm
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
        _algorithmStart.Invoke();
        Info.StartAlgorithmTime = DateTime.Now;

        CallGenerator();

        Info.EndAlgorithmTime = DateTime.Now;
        Info.IsReadyForData = true;
        _algorithmEnd.Invoke();
    }

    private void CallGenerator()
    {
        var info = FindObjectOfType<InfoManager>();
        var board = new List<int>();

        for (int i = 0; i < info.InputFields.Count; i++)
        {
            board.Add(0);
        }

        var output = Solver(board);

        for (int j = 0; j < info.InputFields.Count; j++)
        {
            info.InputFields[j].GetComponent<TMP_InputField>().text = output[j].ToString();
        }
    }

    private List<int> Solver(List<int> board) // This function takes a 1D list of integers representing a Sudoku board and returns a solved version of the board using a DFS algorithm.
    {
        List<List<int>> board2D = ConvertTo2D(board); // Convert the 1D list to a 2D list.        
        bool solved = BFS(board2D); // Start the DFS at the first cell.

        if (solved) // If the board was solved, convert it back to a 1D list and return it. Otherwise, return an empty list.
        {
            return ConvertTo1D(board2D);
        }
        else
        {
            return new List<int>();
        }
    }

    private bool BFS(List<List<int>> board) // This function attempts to solve the sudoku board using a BFS approach. If a solution is found, it returns true. Otherwise, it returns false.
    {
        (int, int) cell = NextEmptyCell(board); // Find the next empty cell

        if (cell.Item1 == -1)  // If there are no empty cells, the board is solved
        {
            return true;
        }

        Shuffle(_inputs);

        for (int i = 1; i <= 9; i++)
        {
            var index = _inputs[i - 1];

            if (IsValid(board, cell.Item1, cell.Item2, index)) // If the number is valid for the cell, fill the cell with the number
            {
                board[cell.Item1][cell.Item2] = index;

                if (BFS(board)) // If the board can be solved with this change, return true
                {
                    return true;
                }

                board[cell.Item1][cell.Item2] = 0; // Otherwise, reset the cell to empty and try the next number
            }
        }

        return false; // If no solution was found, return false
    }

    private (int,int) NextEmptyCell(List<List<int>> board) // This function returns the next empty cell using a BFS approach. If there are no empty cells, it returns (-1, -1).
    {
        int n = board.Count;

        Queue<(int, int)> cellsToVisit = new Queue<(int, int)>(); // Create a queue to store the cells that we need to visit

        for (int i = 0; i < n; i++) // Add all empty cells to the queue
        {
            for (int j = 0; j < n; j++)
            {
                if (board[i][j] == 0)
                {
                    cellsToVisit.Enqueue((i, j));
                }
            }
        }

        if (cellsToVisit.Count == 0) // If there are no empty cells, return (-1, -1)
        {
            return (-1, -1);
        }

        return cellsToVisit.Dequeue(); // Otherwise, return the next empty cell
    }

    private bool IsValid(List<List<int>> board, int row, int col, int num) //This function returns true if the given number is valid for the given cell. Otherwise, it returns false.
    {
        for (int i = 0; i < board.Count; i++) // Check if the number is already in the row
        {
            if (board[row][i] == num)
            {
                return false;
            }
        }

        for (int i = 0; i < board.Count; i++) // Check if the number is already in the column
        {
            if (board[i][col] == num)
            {
                return false;
            }
        }

        // Check if the number is already in the 3x3 subgrid
        int subgridRow = row - row % 3;
        int subgridCol = col - col % 3;
        for (int i = subgridRow; i < subgridRow + 3; i++)
        {
            for (int j = subgridCol; j < subgridCol + 3; j++)
            {
                if (board[i][j] == num)
                {
                    return false;
                }
            }
        }

        return true; // If the number is not in the row, column, or subgrid, it is valid
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