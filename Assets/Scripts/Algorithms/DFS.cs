using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DFS : MonoBehaviour, IAlgorithm
{
    public List<int> Inputs => throw new System.NotImplementedException();

    public List<GameObject> Fields => throw new System.NotImplementedException();

    public InfoManager Info => throw new System.NotImplementedException();

    public int AlgorithmVersion => throw new System.NotImplementedException();

    public UnityEvent AlgorithmStart => throw new System.NotImplementedException();

    public UnityEvent AlgorithmEnd => throw new System.NotImplementedException();

    public void Run()
    {
        throw new System.NotImplementedException();
    }

    ///
    //generate a list of solution
    //then paste that solution into the list of the infoManager

    //private List<int> Solve(List<int> board)
    //{
    //    bool solved = false;

    //    if (solved)
    //    {
    //        return board;
    //    }
    //    else
    //    {
    //        return new List<int>();
    //    }
    //}

    //private bool DFS(List<int> board, int index)
    //{
    //    if (index == Info.inputFields.Count - 1) //At the last cell
    //    {
    //        return true;
    //    }

    //    if (board[index] != 0) //Cell not empty
    //    {
    //        index++;
    //    }

    //    for (int i = 1; i <= Inputs.Count; i++) //try all possibilities
    //    {
    //        if(IsValidNumber(board, index, i))
    //        {
    //            board[index] = i;
    //            if()
    //        }

    //    }
    //}

    //private bool IsValidNumber(List<int> board, int index, int num)
    //{
    //    for (int i = 0; i < Info.boardSize.x; i++)
    //    {
    //        if (board[i] == num)
    //        {
    //            return false;
    //        }
    //    }
    //}


}
