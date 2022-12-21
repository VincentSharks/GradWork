using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System.Linq;

public class WordSearchLogic : MonoBehaviour, IPuzzleLogic
{
    private UnityEvent algorithmEndEvent;
    private InfoManager info;

    private List<GameObject> inputFields = new List<GameObject>();

    private List<int> possibleInputs = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26 };
    public List<int> PossibleInputs { get { return possibleInputs; } set { value = possibleInputs; } }


    private void OnEnable()
    {
        //Info
        info = FindObjectOfType<InfoManager>(); //Get list

        //Algorithm End Event
        var algorithmHolder = FindObjectOfType<AlgorithmHolder>();
        algorithmEndEvent = algorithmHolder.GetComponentInChildren<IAlgorithm>().AlgorithmEnd;
        algorithmEndEvent.AddListener(() => ChangeToLetters(inputFields));
    }

    private void Update()
    {
        inputFields = info.inputFields;
    }

    private void ChangeToLetters(List<GameObject> filledInFields)
    {
        foreach(GameObject go in filledInFields)
        {
            int number = 0;
            var inputText = go.GetComponent<TMP_InputField>().text;
            int.TryParse(inputText, out number);
            
            go.GetComponent<TMP_InputField>().text = OtherWay(number);
        }
    }

    private string OtherWay(int index)
    {
        if(index > 26 || index < 0)
        {
            return " ";
        }

        string alphabet = "abcdefghijklmnopqrstuvwxyz";
        return alphabet.ElementAt(index-1).ToString().ToUpper();
    }
}
