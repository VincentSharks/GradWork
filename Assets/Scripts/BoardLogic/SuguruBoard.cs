using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Temp script, currently a copy of word search
/// </summary>

public class SuguruBoard : MonoBehaviour, IBoard
{
    int valueMin = 2;
    int valueMax = 12;

    int sizeX;
    int sizeY;

    public Vector2 Dimension { get { return new Vector2(sizeX, sizeY); } set { value = new Vector2(sizeX, sizeY); } }

    int dimX;
    int dimY;

    public bool IsBoardSet { get { return false; } set { value = false; } } //false

    float spacing;

    UIHandler uiHand;
    InfoManager info;

    [Header("Fields")]
    [SerializeField] GameObject inputBox;
    List<GameObject> inputFields = new List<GameObject>();
    [SerializeField] GameObject fieldHolder;
    private GridLayoutGroup fieldGrid;

    int totalWidth = 700;

    [Header("Lines")]
    [SerializeField] GameObject linePixel;
    List<GameObject> lines = new List<GameObject>();
    [SerializeField] GameObject lineHolder;

    public int LineThinWidth { get { return 5; } set { value = 5; } }
    public int LineThiccWidth { get { return 10; } set { value = 10; } }

    [Header("Words")]
    int test = 0;

    private void OnEnable()
    {
        fieldGrid = fieldHolder.GetComponent<GridLayoutGroup>();

        sizeX = 2;
        sizeY = 2;

        dimX = 0;
        dimY = 0;

        info = FindObjectOfType<InfoManager>();

        uiHand = FindObjectOfType<UIHandler>();
        uiHand.SetUpSlider(true, true, valueMin, valueMax, sizeX, sizeY);

        if (!IsBoardSet)
        {
            CreateBoard();
        }
    }

    private void Update()
    {
        CompareValues();
    }

    public void CompareValues()
    {
        //Get slider value
        sizeX = uiHand.GetXSliderValue();
        sizeY = uiHand.GetYSliderValue();

        Dimension = new Vector2(sizeX, sizeY);

        if (dimX != Dimension.x || dimY != Dimension.y) //Dimension changed
        {
            DeleteBoard();

            info.isValuesChanged = true;

            if (Dimension.y > Dimension.x) //Change to fixed row
            {
                fieldGrid.constraint = GridLayoutGroup.Constraint.FixedRowCount;
                fieldGrid.constraintCount = sizeY;
            }
            else
            {
                fieldGrid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
                fieldGrid.constraintCount = sizeX;
            }

            CreateBoard();
            dimX = sizeX;
            dimY = sizeY;
        }
    }

    public void CreateBoard()
    {
        if (sizeX > sizeY)
        {
            spacing = totalWidth / sizeX;
        }
        else
        {
            spacing = totalWidth / sizeY;
        }

        fieldGrid.cellSize = new Vector2(spacing, spacing);

        this.GetComponent<Image>().color = new Color(255, 255, 255, 255); //Switch to a white colour BG colour

        SpawnInputFields();
        SpawnLines();
        IsBoardSet = true;
    }

    public void DeleteBoard()
    {
        foreach (GameObject go in inputFields) //Remove all input fields
        {
            Destroy(go);
        }

        foreach (GameObject go in lines) //Remove all lines
        {
            Destroy(go);
        }

        inputFields.Clear();
        lines.Clear();
        IsBoardSet = false;
    }

    public void SpawnInputFields()
    {
        for (int column = 0; column < sizeX; column++)
        {
            for (int row = 0; row < sizeY; row++)
            {
                var input = Instantiate(inputBox, fieldHolder.transform);
                inputFields.Add(input);
            }
        }
    }

    public void SpawnLines()
    {
        var outerLineAddition = 10; //The outer lines don't connect in the corners, with this they connect
        
        for (int indexX = 0; indexX <= Dimension.x; indexX++) //X
        {
            var verticalLine = Instantiate(linePixel, lineHolder.transform);

            for (int indexY = 0; indexY <= Dimension.y; indexY++)
            {
                var horizontalLine = Instantiate(linePixel, lineHolder.transform);

                var totalWidth = spacing * sizeX;
                var totalHeight = spacing * sizeY;

                if (indexX == 0 || indexY == 0) //Outer edge left & bottom
                {
                    verticalLine.GetComponent<RectTransform>().localScale = new Vector3(LineThiccWidth, totalHeight + outerLineAddition, 1);
                    verticalLine.GetComponent<RectTransform>().localPosition = new Vector3(-1 * (totalWidth / 2.0f), 0, 0);

                    horizontalLine.GetComponent<RectTransform>().localScale = new Vector3(totalWidth + outerLineAddition, LineThiccWidth, 1);
                    horizontalLine.GetComponent<RectTransform>().localPosition = new Vector3(0, -1 * (totalHeight / 2.0f), 0);

                }
                else if (indexX % (Dimension.x) == 1 || indexY % (Dimension.y) == 1)
                {
                    verticalLine.GetComponent<RectTransform>().localScale = new Vector3(LineThiccWidth, totalHeight + outerLineAddition, 1);
                    verticalLine.GetComponent<RectTransform>().localPosition = new Vector3((totalWidth / 2.0f), 0, 0);

                    horizontalLine.GetComponent<RectTransform>().localScale = new Vector3(totalWidth + outerLineAddition, LineThiccWidth, 1);
                    horizontalLine.GetComponent<RectTransform>().localPosition = new Vector3(0, (totalHeight / 2.0f), 0);
                }
                else
                {
                    var positionX = ((((Dimension.x / 2) - 1) + indexX) * spacing) - totalWidth;
                    var positionY = ((((Dimension.y / 2) - 1) + indexY) * spacing) - totalHeight;

                    verticalLine.GetComponent<RectTransform>().localScale = new Vector3(LineThinWidth, totalHeight + outerLineAddition, 1);
                    verticalLine.GetComponent<RectTransform>().localPosition = new Vector3(positionX, 0, 0);

                    horizontalLine.GetComponent<RectTransform>().localScale = new Vector3(totalWidth + outerLineAddition, LineThinWidth, 1);
                    horizontalLine.GetComponent<RectTransform>().localPosition = new Vector3(0, positionY, 0);
                }

                lines.Add(verticalLine);
                lines.Add(horizontalLine);
            }
        }
    }
}