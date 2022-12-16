using UnityEngine;
using TMPro;

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

    [Header("Algorithm")]
    [SerializeField] GameObject algorithDropDown;
    string algorithmName;

    [SerializeField] GameObject algorithmHolder;
    GameObject activeAlgorithm;
    float algorithmVersion;

    float randomSeed;


    public bool isValuesChanged = false; //Bool to set to true in oter scripts when a parameter is changed


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

        if(selectedID != 0) //isn't empty
        {
            puzzleName = dropdown.options[selectedID].text; //Sudoku
        }


        //Get active board
        activeBoard = boardHolder.transform.GetChild(selectedID - 1).gameObject;
        //var board = boardHolder.transform.GetChild(selectedID - 1).name.ToUpper(); //SUDOKUBOARD
        boardSize = activeBoard.GetComponent<IBoard>().dimension;
    }

    private void GetAlgorithmInfo()
    {

    }
}
