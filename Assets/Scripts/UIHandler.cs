using TMPro;
using UnityEngine;
using UnityEngine.UI;

//
//Takes care of representing the stats in the UI

public class UIHandler : MonoBehaviour
{
    //take info from infomanager
    //get reference to UI
    //

    [SerializeField] private InfoManager info;

    [SerializeField] private TMP_Text selectedPuzzle;
    [SerializeField] private TMP_Text boardSize;

    private void Update()
    {
        SetBoardInfoUI();   
    }

    private void SetBoardInfoUI()
    {
        selectedPuzzle.text = info.puzzleName;
        boardSize.text = "[X:" + info.boardSize.x + ", Y: " + info.boardSize.y + "]";
    }
}
