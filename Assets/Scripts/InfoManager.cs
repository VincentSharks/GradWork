using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System;
using System.IO;

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
    public Vector2 boardSize;

    public List<GameObject> inputFields;

    [Header("PuzzleLogic")]
    public List<int> possibleInputs;

    [Header("Algorithm")]
    [SerializeField] private GameObject _algorithDropDown;
    public string algorithmName;

    [SerializeField] private GameObject _algorithmHolder;
    public int algorithmVersion;

    public int RandomSeed { get; private set; }
    public bool isValuesChanged = false; //Bool to set to true in oter scripts when a parameter is changed

    public bool isPuzzleSelected = false;
    public bool isAlgorithmSelected = false;

    public bool isAlgorithmSet = false;

    //Timer
    public DateTime startAlgorithmTime;
    public DateTime endAlgorithmTime;
    public TimeSpan elapsedTime;

    public bool isExportData = false;

    [SerializeField] private CSVManager _csv;

    private void Awake()
    {
        RandomSeed = DateTime.Now.Millisecond;
        UnityEngine.Random.InitState(DateTime.Now.Millisecond);
        algorithmVersion = 0;
        _csv.gameObject.SetActive(false);
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

            isValuesChanged = false;
        }

        if(isExportData)
        {
            //ExportData();
            _csv.gameObject.SetActive(true);
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

        //If board is changed
        //Deselect algorithm

        var board = _activeBoard;
        
        //Get active board
        _activeBoard = _boardHolder.transform.GetChild(selectedID - 1).gameObject;

        if (board != null)
        {
            if(board != _activeBoard) //See if board has changed
            {
                _algorithDropDown.GetComponent<TMP_Dropdown>().value = 0;
                board = _activeBoard; //redundant?
            }
        }

        boardSize = _activeBoard.GetComponent<IBoard>().Dimensions;

        inputFields = _activeBoard.GetComponent<IBoard>().InputFields;

        isPuzzleSelected = true;

        GetPuzzleLogicInfo();
    }

    private void GetPuzzleLogicInfo()
    {     
        possibleInputs = _activeBoard.GetComponent<IPuzzleLogic>().PossibleInputs;
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
    }

    public void RunCombo() //Run button
    {
        var algorithmComponent = _algorithmHolder.GetComponentInChildren<IAlgorithm>();
        algorithmComponent.Run();
        GetElapsedTime();
    }

    private void GetElapsedTime()
    {
        elapsedTime = endAlgorithmTime - startAlgorithmTime;
    }
}