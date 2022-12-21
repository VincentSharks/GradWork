using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System;

/// <summary>
/// get list of possible input
/// random number
/// get that index from the list
/// export and repeat
/// </summary>

public class Bogosort : MonoBehaviour, IAlgorithm
{
    public InfoManager Info { get { return FindObjectOfType<InfoManager>(); } set { value = FindObjectOfType<InfoManager>(); } }
    public List<int> Inputs { get { return Info.possibleInputs; } set { value = Info.possibleInputs; } }
    public int Count { get { return Inputs.Count; } set { value = Inputs.Count; } }
    public List<GameObject> Fields { get { return Info.inputFields; } set { value = Info.inputFields; } }
    public int AlgorithmVersion { get { return 1; } set { value = 1; } }


    private UnityEvent algorithmEnd = new UnityEvent();
    public UnityEvent AlgorithmEnd { get { return algorithmEnd; } set { value = algorithmEnd; } }

    private UnityEvent algorithmStart = new UnityEvent();
    public UnityEvent AlgorithmStart { get { return algorithmStart; } set { value = algorithmStart; } }

    public void Init(List<GameObject> inputFields, List<int> possibleInput)
    {
        Count = possibleInput.Count;
        Inputs = possibleInput;
        Fields = inputFields;
    }

    private void OnEnable()
    {
        if(algorithmEnd == null)
        {
            algorithmEnd = new UnityEvent();
        }

        if (algorithmStart == null)
        {
            algorithmStart = new UnityEvent();
        }


        Info = FindObjectOfType<InfoManager>();
        UnityEngine.Random.seed = Info.RandomSeed;

        Inputs = new List<int>();
        Fields = new List<GameObject>();    
    }

    private int GetRandom()
    {
        var random = UnityEngine.Random.Range(0, Count);
        var output = Inputs[random];
        return output;
    }

    private void FillInField()
    {
        for(int i = 0; i < Fields.Count; i++) //Go over all fields
        {
            Fields[i].gameObject.GetComponent<TMP_InputField>().text = GetRandom().ToString();
        }

        Info.endAlgorithmTime = DateTime.Now;
    }

    public void Run()
    {
        algorithmStart.Invoke();
        Info.startAlgorithmTime = DateTime.Now;
        
        FillInField();
        algorithmEnd.Invoke();
    }
}
