using UnityEngine;
using TMPro;
using System.Collections.Generic;

/// <summary>
/// Gather all the info from other scripts
/// </summary>

public class InfoManager : MonoBehaviour
{
    [Header("Board")]
    [SerializeField] GameObject PuzzleDropDown;
    public string puzzleName;
    
    [SerializeField] GameObject boardHolder;
    GameObject activeBoard;
    public Vector2 boardSize;

    public List<GameObject> inputFields;

    [Header("PuzzleLogic")]
    public List<int> possibleInputs;

    [Header("Algorithm")]
    [SerializeField] GameObject algorithDropDown;
    public string algorithmName;

    [SerializeField] GameObject algorithmHolder;
    GameObject activeAlgorithm;
    public int algorithmVersion;

    public int RandomSeed { get; private set; }
    public bool isValuesChanged = false; //Bool to set to true in oter scripts when a parameter is changed

    public bool isPuzzleSelected = false;
    public bool isAlgorithmSelected = false;


    private void Awake()
    {
        RandomSeed = Random.seed = System.DateTime.Now.Millisecond;
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
    }

    private void GetBoardInfo()
    {
        //Get board name
        var dropdown = PuzzleDropDown.GetComponent<TMP_Dropdown>();
        var selectedID = dropdown.value;
        
        if (selectedID <= 0)
        {
            isPuzzleSelected = false;
            return;
        }

        puzzleName = dropdown.options[selectedID].text;

        //Get active board
        activeBoard = boardHolder.transform.GetChild(selectedID - 1).gameObject;
        boardSize = activeBoard.GetComponent<IBoard>().Dimension;

        inputFields = activeBoard.GetComponent<IBoard>().InputFields;

        isPuzzleSelected = true;

        GetPuzzleLogicInfo();
    }

    private void GetPuzzleLogicInfo()
    {     
        possibleInputs = activeBoard.GetComponent<IPuzzleLogic>().PossibleInputs;
    }

    private void GetAlgorithmInfo()
    {
        var dropdown = algorithDropDown.GetComponent<TMP_Dropdown>();
        var selectedID = dropdown.value;
        
        if(selectedID <= 0)
        {
            isAlgorithmSelected = false;
            return;
        }

        algorithmName = dropdown.options[selectedID].text;

        //Get active algorithm
        activeAlgorithm = algorithmHolder.transform.GetChild(selectedID - 1).gameObject;
        algorithmVersion = activeAlgorithm.GetComponent<IAlgorithm>().AlgorithmVersion;

        isAlgorithmSelected = true;
    }

    public void RunCombo() //Run button
    {
        Debug.Log("Run Combo: " + puzzleName + " + " + algorithmName);
        activeAlgorithm.GetComponent<IAlgorithm>().Run();
    }
}
