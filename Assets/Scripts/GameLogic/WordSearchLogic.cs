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
    private AlgorithmHolder _algorithmHolder;
    private UnityEvent _algorithmEndEvent;
    private bool _eventListenerSet = false;

    private List<GameObject> _inputFields = new List<GameObject>();
    
    private List<int> _possibleInputs = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26 };

    //https://www.thefreedictionary.com/3-letter-words.htm
    private List<string> _3characterWords = new List<string>() { "cat", "dog", "boy", "dad", "eye", "elk", "git", "mug", "mad", "ham",
                                                                 "hub", "far", "box", "big", "jam", "new", "rem", "spa", "yes", "ufo",
                                                                 "top", "tin", "sir", "owl", "zoo", "tea", "sea", "paw", "nun", "bug"};

    //https://www.thefreedictionary.com/4-letter-words.htm
    private List<string> _4CharacterWords = new List<string>() { "anti", "half", "isle", "leaf", "town", "list", "pine", "scar", "demo", "gift",
                                                                 "gone", "lane", "idle", "halt", "kirk", "grip", "euro", "cube", "rank", "tier",
                                                                 "year", "yard", "yarn", "trim", "slam", "next", "love", "meal", "boil", "ball"};

    //https://www.thefreedictionary.com/5-letter-words.htm
    private List<string> _5CharacterWords = new List<string>() { "beach", "couch", "first", "great", "later", "index", "guess", "panic", "level", "prime",
                                                                 "scene", "solid", "total", "small", "pitch", "match", "mercy", "yield", "grand", "logic",
                                                                  "honey", "ghost", "fence", "drill", "clear", "cabin", "about", "reply", "skill", "state"};

    public List<int> PossibleInputs { get { return _possibleInputs; } set { value = _possibleInputs; } }

    private List<string> _shortWordsInput = new List<string>() { };
    private List<string> _longWordsInput = new List<string>() { };


    private void OnEnable()
    {
        _info = FindObjectOfType<InfoManager>();
        _algorithmHolder = FindObjectOfType<AlgorithmHolder>();

        AddListener();
    }

    private void Update()
    {
        _inputFields = _info.inputFields;

        AddListener();
    }

    private void AddListener()
    {
        if(!_eventListenerSet)
        {
            //if algorithm is selected
            if (_info.isAlgorithmSelected)
            {
                _algorithmEndEvent = _algorithmHolder.GetComponentInChildren<IAlgorithm>().AlgorithmEnd;
                _algorithmEndEvent.AddListener(() => ChangeToLetters(_inputFields));
                _eventListenerSet = true;
            }
        }
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
        if(index > 26 || index <= 0)
        {
            return " ";
        }

        string alphabet = "abcdefghijklmnopqrstuvwxyz";
        return alphabet.ElementAt(index-1).ToString().ToUpper(); //out of range
    }

    public bool IsValidNumber(List<List<int>> board, int row, int col, int num, InfoManager info)
    {
        throw new System.NotImplementedException();
    }
}
