using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// get list of possible input
/// random number
/// get that index from the list
/// export and repeat
/// </summary>

public class Bogosort : MonoBehaviour
{
    private int count;
    private List<int> inputs;    

    public Bogosort(List<int> possibleInput)
    {
        count = possibleInput.Count;
        inputs = possibleInput;
    }

    private void Start()
    {
        inputs = new List<int>();
        Random.seed = System.DateTime.Now.Millisecond;
    }

    public void Calculate()
    {
        GetRandom();
    }

    private int GetRandom()
    {
        var random = Random.Range(0, count-1);
        var output = inputs[random];
        return output;
    }
}
