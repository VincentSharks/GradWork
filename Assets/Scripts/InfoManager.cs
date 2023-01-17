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
    public string PuzzleName;
    [SerializeField] private GameObject _boardHolder;
    private GameObject _activeBoard;
    private IBoard _activeBoardLogic;
    public IPuzzleLogic activePuzzleLogic;

    public Vector2 BoardSize;
    public List<GameObject> InputFields;
    public bool IsPuzzleSelected = false;

    [Header("PuzzleLogic")]
    public List<int> PossibleInputs;

    [Header("Algorithm")]
    [SerializeField] private GameObject _algorithDropDown;
    public string AlgorithmName;
    [SerializeField] private GameObject _algorithmHolder;
    public IAlgorithm _currentAlgorithm = null;
    public int AlgorithmVersion;
    public bool IsAlgorithmSelected = false;
    public bool IsAlgorithmSet = false;

    [Header("Iterations")]
    [SerializeField] private GameObject _iterationsDropDown;
    public int IterationsAmount = 1;

    [Header("Timer")]
    public DateTime StartAlgorithmTime;
    public DateTime EndAlgorithmTime;
    public TimeSpan ElapsedTime;
    public double TimeDifference;

    [Header("Data")]
    [SerializeField] private CSVManager _csv;
    public bool IsExportData = false;
    public bool IsReadyForData = false;

    public List<string> BoardData = new List<string>();
    public List<bool> BoardIsValid = new List<bool>();
    public List<DateTime> StartTimes = new List<DateTime>();
    public List<DateTime> EndTimes = new List<DateTime>();
    public List<TimeSpan> ElapsedTimes = new List<TimeSpan>();
    public List<double> ElapsedMiliseconds = new List<double>();


    public int RandomSeed { get; set; }
    public bool IsValuesChanged = false; //Bool to set to true in oter scripts when a parameter is changed

    private UnityEvent _algorithmEndEvent;
    private bool _eventListenerSet = false;

    private void Awake()
    {
        RandomSeed = DateTime.Now.Millisecond;
        UnityEngine.Random.InitState(RandomSeed);
        AlgorithmVersion = 0;
    }

    private void OnEnable()
    {
        InputFields = new List<GameObject>();
        PossibleInputs = new List<int>();
    }

    public void IsChanged()
    {
        IsValuesChanged = true;
    }

    private void Update()
    {
        if (IsValuesChanged)
        {
            //Check info
            GetBoardInfo();
            GetAlgorithmInfo();
            GetIterationInfo();

            IsValuesChanged = false;
        }

        if (IsExportData)
        {
            IsExportData = false;
        }
    }

    private void GetBoardInfo()
    {
        //Get board name
        var dropdown = _puzzleDropDown.GetComponent<TMP_Dropdown>();
        var selectedID = dropdown.value;

        if (selectedID <= 0)
        {
            IsPuzzleSelected = false;
            return;
        }

        PuzzleName = dropdown.options[selectedID].text;

        var board = _activeBoard;

        //Get active board
        _activeBoard = _boardHolder.transform.GetChild(selectedID - 1).gameObject;

        if (board != null)
        {
            if (board != _activeBoard) //See if board has changed
            {
                _algorithDropDown.GetComponent<TMP_Dropdown>().value = 0;
            }
        }

        _activeBoardLogic = _activeBoard.GetComponent<IBoard>();
        BoardSize = _activeBoardLogic.Dimensions;
        InputFields = _activeBoardLogic.InputFields;


        IsPuzzleSelected = true;

        GetPuzzleLogicInfo();
    }

    private void GetPuzzleLogicInfo()
    {
        activePuzzleLogic = _activeBoard.GetComponent<IPuzzleLogic>();
        PossibleInputs = activePuzzleLogic.PossibleInputs;
    }

    private void GetAlgorithmInfo()
    {
        var dropdown = _algorithDropDown.GetComponent<TMP_Dropdown>();
        var selectedID = dropdown.value;

        if (selectedID <= 0)
        {
            IsAlgorithmSelected = false;
            IsAlgorithmSet = false;
            return;
        }

        AlgorithmName = dropdown.options[selectedID].text;

        var algorithmComponents = _algorithmHolder.GetComponentsInChildren<IAlgorithm>();

        //finds first active algorithm from the component list from the object
        if (algorithmComponents != null)
        {
            foreach (var algorithmComponent in algorithmComponents)
            {
                //cast to a monobehaviour to check if it is enabled or not
                var tempComponent = algorithmComponent as MonoBehaviour;
                if (tempComponent)
                {
                    if (tempComponent.enabled)
                    {
                        AlgorithmVersion = algorithmComponent.AlgorithmVersion;
                        _currentAlgorithm = algorithmComponent;
                        break;
                    }
                }
            }
        }

        //if algo version is 0 then no valid algo was selected
        if (AlgorithmVersion != 0)
        {
            IsAlgorithmSelected = true;
            IsAlgorithmSet = true;

            AddListener();
        }
        else
        {
            Debug.LogWarning("No valid algo was found");
        }
    }

    private void GetIterationInfo()
    {
        var iterationsDropDown = _iterationsDropDown.GetComponent<TMP_Dropdown>();
        var selectedID = iterationsDropDown.value;
        var selectedString = iterationsDropDown.options[selectedID].text;
        IterationsAmount = Convert.ToInt32(selectedString);
    }

    public void RunCombo() //Run button
    {
        if (_currentAlgorithm == null)
        {
            Debug.LogWarning("No valid algo was selected, cannot run itterations");
            return;
        }

        for (int i = 0; i < IterationsAmount; i++)
        {
            ResetData();

            _currentAlgorithm.Run();
            GetElapsedTime();
            _currentAlgorithm.AlgorithmEnd.Invoke();
            IsExportData = true;
        }
    }

    public void GetElapsedTime()
    {
        ElapsedTime = EndAlgorithmTime - StartAlgorithmTime;
        TimeDifference = ElapsedTime.TotalMilliseconds;
    }

    private void AddListener()
    {
        if (!_eventListenerSet)
        {
            if (_currentAlgorithm == null)
                return;

            _algorithmEndEvent = _currentAlgorithm.AlgorithmEnd;

            _algorithmEndEvent.AddListener(() => _csv.GetStatsData());
            _algorithmEndEvent.AddListener(() => _csv.GetBoardData());

            //if algorithm is selected
            if (IsAlgorithmSelected)
            {
                _eventListenerSet = true;
            }
        }
    }

    public void CheckBoard()
    {
        var puzzle = FindObjectOfType<SudokuLogic>();

        foreach (string board in BoardData)
        {
            string value = board.Replace(" ", "");
            var intList = value.Select(digit => int.Parse(digit.ToString()));

            BoardIsValid.Add(puzzle.CheckBoard(intList.ToList()));
        }
    }

    private void ResetData()
    {
        StartAlgorithmTime = new DateTime();
        EndAlgorithmTime = new DateTime();
        ElapsedTime = new TimeSpan();
        TimeDifference = new double();
    }
}