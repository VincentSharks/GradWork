using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IAlgorithm
{
    /// <summary> List of all the possible inputs </summary>
    public List<int> Inputs { get; }

    /// <summary> List of input fields of spawned board </summary>
    public List<GameObject> Fields { get; }

    /// <summary> Reference to Infomanager </summary>
    public InfoManager Info { get; }

    /// <summary> Int to keep track of what version of the algorithm it is </summary>
    public int AlgorithmVersion { get; }

    /// <summary>  </summary>
    //public void Init(List<GameObject> inputFields, List<int> possibleInput);

    /// <summary> Run the algorithm logic </summary>
    public void Run();

    /// <summary> Event to invoke when the algorithm starts </summary>
    public UnityEvent AlgorithmStart { get; }

    /// <summary> Event to invoke when the algorithm is done </summary>
    public UnityEvent AlgorithmEnd { get; }
}
