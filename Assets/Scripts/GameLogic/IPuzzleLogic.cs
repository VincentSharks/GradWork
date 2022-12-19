using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPuzzleLogic
{
    List<int> PossibleInputs { get; set; }
}
