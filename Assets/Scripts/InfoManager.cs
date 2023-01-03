using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System;
using UnityEngine.Events;
using System.Linq;

/// <summary>
/// Gather all the info from other scripts
/// </summary>

public class InfoManager : MonoBehaviour
{
    [Header("Board")]
    [SerializeField] private GameObject _puzzleDropDown;
    public string puzzleName;    
    [SerializeField] private GameObject _boardHolder;
    private GameObject _activeBoard;
    private IBoard _activeBoardLogic;
    private IPuzzleLogic _activePuzzleLogic;
    
    public Vector2 boardSize;
    public List<GameObject> inputFields;
    public bool isPuzzleSelected = false;

    [Header("PuzzleLogic")]
    public List<int> possibleInputs;

    [Header("Algorithm")]
    [SerializeField] private GameObject _algorithDropDown;
    public string algorithmName;
    [SerializeField] private GameObject _algorithmHolder;
    public int algorithmVersion;
    public bool isAlgorithmSelected = false;
    public bool isAlgorithmSet = false;

    [Header("Iterations")]
    [SerializeField] private GameObject _iterationsDropDown;
    public int iterationsAmount = 1;

    [Header("Timer")]
    public DateTime startAlgorithmTime;
    public DateTime endAlgorithmTime;
    public TimeSpan elapsedTime;

    [Header("Data")]
    [SerializeField] private CSVManager _csv;
    public bool isExportData = false;
    public bool isReadyForData = false;

    public List<string> boardData = new List<string>();
    public List<bool> boardIsValid = new List<bool>();
    public List<DateTime> startTimes = new List<DateTime>();
    public List<DateTime> endTimes = new List<DateTime>();
    public List<TimeSpan> elapsedTimes = new List<TimeSpan>();


    public int RandomSeed { get; set; }
    public bool isValuesChanged = false; //Bool to set to true in oter scripts when a parameter is changed

    private UnityEvent _algorithmEndEvent;
    private bool _eventListenerSet = false;

    private void Awake()
    {
        RandomSeed = DateTime.Now.Millisecond;
        UnityEngine.Random.InitState(RandomSeed);
        algorithmVersion = 0;
    }

    private void OnEnable()
    {
        inputFields = new List<GameObject>();
        possibleInputs = new List<int>();
    }

    public void IsChanged()
    {
        isValuesChanged = true;
    }

    private void Update()
    {
        if(isValuesChanged)
        {
            //Check info
            GetBoardInfo();
            GetAlgorithmInfo();
            GetIterationInfo();

            isValuesChanged = false;
        }

        if(isExportData)
        {
            isExportData = false;
        }
    }

    private void GetBoardInfo()
    {
        //Get board name
        var dropdown = _puzzleDropDown.GetComponent<TMP_Dropdown>();
        var selectedID = dropdown.value;
        
        if (selectedID <= 0)
        {
            isPuzzleSelected = false;
            return;
        }

        puzzleName = dropdown.options[selectedID].text;

        var board = _activeBoard;
        
        //Get active board
        _activeBoard = _boardHolder.transform.GetChild(selectedID - 1).gameObject;

        if (board != null)
        {
            if(board != _activeBoard) //See if board has changed
            {
                _algorithDropDown.GetComponent<TMP_Dropdown>().value = 0;
            }
        }

        _activeBoardLogic = _activeBoard.GetComponent<IBoard>();
        boardSize = _activeBoardLogic.dimensions;
        inputFields = _activeBoardLogic.inputFields;

        isPuzzleSelected = true;

        GetPuzzleLogicInfo();
    }

    private void GetPuzzleLogicInfo()
    {
        _activePuzzleLogic = _activeBoard.GetComponent<IPuzzleLogic>();
        possibleInputs = _activePuzzleLogic.PossibleInputs;
    }

    private void GetAlgorithmInfo()
    {        
        var dropdown = _algorithDropDown.GetComponent<TMP_Dropdown>();
        var selectedID = dropdown.value;
        
        if(selectedID <= 0)
        {
            isAlgorithmSelected = false;
            isAlgorithmSet = false;
            return;
        }

        algorithmName = dropdown.options[selectedID].text;

        var algorithmComponent = _algorithmHolder.GetComponentInChildren<IAlgorithm>();

        if (algorithmComponent != null)
        {
            algorithmVersion = algorithmComponent.AlgorithmVersion;
        }        
        
        isAlgorithmSelected = true;
        isAlgorithmSet = true;

        AddListener();
    }

    private void GetIterationInfo()
    {
        var iterationsDropDown = _iterationsDropDown.GetComponent<TMP_Dropdown>();
        var selectedID = iterationsDropDown.value;
        var selectedString = iterationsDropDown.options[selectedID].text;
        iterationsAmount = Convert.ToInt32(selectedString);
    }

    public void RunCombo() //Run button
    {
        var algorithmComponent = _algorithmHolder.GetComponentInChildren<IAlgorithm>();

        for (int i = 0; i < iterationsAmount; i++)
        {
            algorithmComponent.Run();
            GetElapsedTime();
            isExportData = true;
        }
    }

    public void GetElapsedTime()
    {
        elapsedTime = endAlgorithmTime - startAlgorithmTime;
    }

    private void AddListener()
    {
        if (!_eventListenerSet)
        {
            _algorithmEndEvent = _algorithmHolder.GetComponentInChildren<IAlgorithm>().AlgorithmEnd;
            
            _algorithmEndEvent.AddListener(() => _csv.GetStatsData());
            _algorithmEndEvent.AddListener(() => _csv.GetBoardData());

            //if algorithm is selected
            if (isAlgorithmSelected)
            {
                _eventListenerSet = true;
            }
        }
    }

    public void CheckBoard()
    {
        var puzzle = FindObjectOfType<SudokuLogic>();

        foreach (string board in boardData)
        {
            string value = board.Replace(" ", "");
            var intList = value.Select(digit => int.Parse(digit.ToString()));

            boardIsValid.Add(puzzle.CheckBoard(intList.ToList()));
        }
    }
}