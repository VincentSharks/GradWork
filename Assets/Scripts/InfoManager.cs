using UnityEngine;
using TMPro;

/// <summary>
/// Gather all the info from other scripts
/// Display this info in the UI
/// use this info to write to a JSON
/// </summary>

public class InfoManager : MonoBehaviour
{
    //Gather all info

    //Board/Puzzle name (Get from drop down)
    [SerializeField] GameObject PuzzleDropDown;
    string puzzleName;
    
    [SerializeField] GameObject boardHolder;
    GameObject activeBoard;

    //board size
    //board dimensions

    //Algorithm name (Get from drop down)
    //algorithm version number


    public bool isValuesChanged = false; //Bool to set to true in oter scripts when a parameter is changed


    //puzzle name UI
    //





    //UI
    [SerializeField] GameObject infoUI;
    [SerializeField] GameObject AlgorithmDropDown;


    //Puzzle
    //Get puzzle name
    //get board size
    //get sliders 

    string boardName;

    private void Update()
    {
        GetActiveBoard(); //
        SetInfoUI();
    }

    private void GetActiveBoard()
    {
        for (int i = 0; i < boardHolder.transform.childCount; i++)
        {
            if (boardHolder.transform.GetChild(i).gameObject.activeSelf == true)
            {
                activeBoard = boardHolder.transform.GetChild(i).gameObject;
                boardName = activeBoard.name.Replace("Board", "");

                //int value = PuzzleDropDown.GetComponent<TMP_Dropdown>().value;
                //boardName = PuzzleDropDown.GetComponent<TMP_Dropdown>().options[value].ToString();
            }
        }
    }

    private void SetInfoUI()
    {
        //Get the info child needed
        //set the info 

        for (int i = 0; i < boardHolder.transform.childCount; i++)
        {
            if (boardHolder.transform.GetChild(i).gameObject.name == "PuzzleInfo")
            {
                Debug.Log("Found");
            }
        }
    }
}
