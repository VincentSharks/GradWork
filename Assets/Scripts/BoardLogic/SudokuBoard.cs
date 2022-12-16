using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Logic for spawning an empty binary board
/// - 
/// </summary>

public class SudokuBoard : MonoBehaviour
{
    [SerializeField] GameObject linePixel;
    [SerializeField] GameObject inputBox;

    int totalWidth = 700;

    int lineThiccWidth = 10;
    int lineThinWidth = 5;

    int inputWidth = 50;

    float spacing;

    List<GameObject> inputFields = new List<GameObject>();

    bool isBoardSet = false;

    private void OnEnable()
    {
        if(!isBoardSet)
        {
            CreateBoard();
        }        
    }

    public void CreateBoard()
    {
        spacing = totalWidth / 9.0f;

        this.GetComponent<Image>().color = new Color(255, 255, 255, 255); //Switch to a white colour BG colour? //get lines?

        SpawnInputFields();
        SpawnLines();
        isBoardSet = true;
    }

    private void SpawnInputFields()
    {
        for (int column = 0; column < 9; column++)
        {
            for (int row = 0; row < 9; row++)
            {
                var input = Instantiate(inputBox, this.transform);
                var rectTrans = input.GetComponent<RectTransform>();

                rectTrans.localScale = new Vector3(spacing / inputWidth, spacing / inputWidth, 1);

                var xPos = (row * spacing) + inputWidth - lineThiccWidth - lineThinWidth;
                var yPos = ((8-column) * spacing) + inputWidth - lineThiccWidth - lineThinWidth; //Start at the top one and build down
                
                rectTrans.localPosition = new Vector3(xPos, yPos, 0);

                inputFields.Add(input);
            }
        }
    }

    private void SpawnLines()
    {
        var outerLineAddition = 10; //The outer lines don't connect in the corners, with this they connect

        for (int index = 0; index < 10; index++)
        {
            //Spawn as child
            var lineVertical = Instantiate(linePixel, this.transform);
            var lineHorizontal = Instantiate(linePixel, this.transform);            

            if (index % 3 == 0)
            {
                //Thicc
                lineVertical.GetComponent<RectTransform>().localScale = new Vector3(lineThiccWidth, totalWidth + outerLineAddition, 1);
                lineHorizontal.GetComponent<RectTransform>().localScale = new Vector3(totalWidth + outerLineAddition, lineThiccWidth, 1);
                lineVertical.GetComponent<RectTransform>().localPosition = new Vector3(index * spacing - (lineThiccWidth / 2.0f), (totalWidth / 2.0f) - (lineThiccWidth / 2.0f), 0);
                lineHorizontal.GetComponent<RectTransform>().localPosition = new Vector3((totalWidth / 2.0f) - (lineThiccWidth / 2.0f), index * spacing - (lineThiccWidth / 2.0f), 0);
            }
            else
            {
                //Thin
                lineVertical.GetComponent<RectTransform>().localScale = new Vector3(lineThinWidth, totalWidth, 1);
                lineHorizontal.GetComponent<RectTransform>().localScale = new Vector3(totalWidth, lineThinWidth, 1);
                lineVertical.GetComponent<RectTransform>().localPosition = new Vector3(index * spacing - (lineThinWidth / 2.0f), (totalWidth / 2.0f) - (lineThinWidth / 2.0f), 0);
                lineHorizontal.GetComponent<RectTransform>().localPosition = new Vector3((totalWidth / 2.0f) - (lineThinWidth / 2.0f), index * spacing - (lineThinWidth / 2.0f), 0);
            }
        }
    }
}