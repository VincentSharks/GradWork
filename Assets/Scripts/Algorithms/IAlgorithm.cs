using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IAlgorithm
{
    int Count { get; set; }
    List<int> Inputs { get; set; }
    List<GameObject> Fields { get; set; }
    InfoManager Info { get; set; }

    int AlgorithmVersion { get; set; }

    void Init(List<GameObject> inputFields, List<int> possibleInput);

    void Run();

    //End event
    UnityEvent AlgorithmEnd { get; set; }
}
