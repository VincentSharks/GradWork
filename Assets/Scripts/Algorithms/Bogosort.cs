using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System;

/// <summary>
/// Logic for the Bogosort algorithm (Version 1)
/// </summary>

public class Bogosort : MonoBehaviour, IAlgorithm
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
        EmptyField();

        _algorithmStart.Invoke();
        Info.StartAlgorithmTime = DateTime.Now;

        FillInField();

        Info.EndAlgorithmTime = DateTime.Now;
        Info.IsReadyForData = true;
        _algorithmEnd.Invoke();
    }

    private int GetRandom()
    {
        var random = UnityEngine.Random.Range(0, Count);
        var output = Inputs[random];
        return output;
    }

    private void FillInField()
    {
        for (int i = 0; i < Fields.Count; i++) //Go over all fields
        {
            Fields[i].gameObject.GetComponent<TMP_InputField>().text = GetRandom().ToString();
        }
    }

    private void EmptyField()
    {
        for (int i = 0; i < Fields.Count; i++) //Go over all fields
        {
            Fields[i].gameObject.GetComponent<TMP_InputField>().text = "X";
        }
    }
}