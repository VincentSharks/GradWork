using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordSearchBoard : MonoBehaviour, IBoard
{
    //put the sudoku sizes in here

    public int SizeX { get => throw new System.NotImplementedException(); }
    public int SizeY { get => throw new System.NotImplementedException(); }

    [SerializeField] GameObject boardHolder;
    [SerializeField] GameObject linePixel;

    [SerializeField] GameObject inputBox;

    int totalWidth = 700;

    int lineThiccWidth = 10;
    int lineThinWidth = 5;

    int inputWidth = 50;

    float spacing;

    int rows = 4; //Y
    int columns = 9; //X

    List<GameObject> inputFields = new List<GameObject>();

    bool isBoardSet = false;


    private void OnEnable()
    {
        if (!isBoardSet)
        {
            CreateBoard();
        }
    }

    public void CreateBoard()
    {
        spacing = totalWidth / 9.0f;

        this.GetComponent<Image>().color = new Color(255, 255, 255, 255); //Switch to a white colour BG colour? //get lines?

        SpawnInputFields();
        isBoardSet = true;
    }

    private void SpawnInputFields()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < this.columns; column++)
            {
                var input = Instantiate(inputBox, boardHolder.transform);
                var rectTrans = input.GetComponent<RectTransform>();

                rectTrans.localScale = new Vector3(spacing / inputWidth, spacing / inputWidth, 1);

                var xPos = (column * spacing) + inputWidth - lineThiccWidth - lineThinWidth;
                var yPos = ((8 - row) * spacing) + inputWidth - lineThiccWidth - lineThinWidth; //Start at the top one and build down

                rectTrans.localPosition = new Vector3(xPos, yPos, 0);

                inputFields.Add(input);
            }
        }
    }
}
