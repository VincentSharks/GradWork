using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System.Linq;

/// <summary>
/// 
/// </summary>

public class WordSearchLogic : MonoBehaviour, IPuzzleLogic
{
    private InfoManager _info;
    private UnityEvent _algorithmEndEvent;

    private List<GameObject> _inputFields = new List<GameObject>();
    
    private List<int> _possibleInputs = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26 };
    public List<int> PossibleInputs { get { return _possibleInputs; } set { value = _possibleInputs; } }

    private List<string> _shortWordsInput = new List<string>() { };
    private List<string> _longWordsInput = new List<string>() { };


    private void OnEnable()
    {
        _info = FindObjectOfType<InfoManager>();

        //Algorithm End Event
        var algorithmHolder = FindObjectOfType<AlgorithmHolder>();

        //if algorithm is selected

        _algorithmEndEvent = algorithmHolder.GetComponentInChildren<IAlgorithm>().AlgorithmEnd;
        _algorithmEndEvent.AddListener(() => ChangeToLetters(_inputFields));
    }

    private void Update()
    {
        _inputFields = _info.inputFields;
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
