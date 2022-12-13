using System.Collections.Generic;
using UnityEngine;

public interface IHolder
{
    //List<GameObject> Childs { get; set; }
    //int value { get; set; }
    void DisableAllChilds();
    void CompareValue(int value);
    void SwitchChild(int index);
}