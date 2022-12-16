using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Logic for spawning an empty binary board
/// - 
/// </summary>

public class SudokuBoard : MonoBehaviour, IBoard
{
    private int sizeX = 9;
    private int sizeY = 9;

    public Vector2 dimension { get { return new Vector2(sizeX, sizeY); } set { value = new Vector2(sizeX, sizeY); } }

    private int dimX;
    private int dimY;

    public bool isBoardSet { get { return false; } set { value = false; } } //false

    private float spacing;

    [Header("Fields")]
    [SerializeField] private GameObject inputBox;
    private List<GameObject> inputFields = new List<GameObject>();
    [SerializeField] private GameObject fieldHolder;
    private GridLayoutGroup fieldGrid;

    private int totalWidth = 700;
    private int inputWidth = 50;

    [Header("Lines")]
    [SerializeField] private GameObject linePixel;
    private List<GameObject> lines = new List<GameObject>();
    [SerializeField] private GameObject lineHolder;


    public int lineThinWidth { get { return 5; } set { value = 5; } }
    public int lineThiccWidth { get { return 10; } set { value = 10; } }


    private void OnEnable()
    {
        sizeX = 9;
        sizeY = 9;

        dimX = 0;
        dimY = 0;

        fieldGrid = fieldHolder.GetComponent<GridLayoutGroup>();

        if (!isBoardSet)
        {
            CreateBoard();
        }        
    }

    private void Update()
    {
        dimension = new Vector2(sizeX, sizeY);

        CompareValues();
    }

    private void CompareValues()
    {
        if (dimX != dimension.x || dimY != dimension.y) //Dimension changed
        {
            DeleteBoard();

            if (dimension.y > dimension.x) //Change to fixed row
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
        isBoardSet = true;
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
        isBoardSet = false;
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

        for (int index = 0; index < 10; index++)
        {
            //Spawn as child
            var lineVertical = Instantiate(linePixel, lineHolder.transform);
            var lineHorizontal = Instantiate(linePixel, lineHolder.transform);

            var position = (index * spacing + (lineThiccWidth / 2)) - (totalWidth / 2);

            if (index % 3 == 0)
            {
                //Thicc
                lineVertical.GetComponent<RectTransform>().localScale = new Vector3(lineThiccWidth, totalWidth, 1);
                lineHorizontal.GetComponent<RectTransform>().localScale = new Vector3(totalWidth, lineThiccWidth, 1);
                lineVertical.GetComponent<RectTransform>().localPosition = new Vector3(position, 0, 0);
                lineHorizontal.GetComponent<RectTransform>().localPosition = new Vector3(0, position, 0);
            }
            else
            {
                //Thin
                lineVertical.GetComponent<RectTransform>().localScale = new Vector3(lineThinWidth, totalWidth, 1);
                lineHorizontal.GetComponent<RectTransform>().localScale = new Vector3(totalWidth, lineThinWidth, 1);
                lineVertical.GetComponent<RectTransform>().localPosition = new Vector3(position, 0, 0);
                lineHorizontal.GetComponent<RectTransform>().localPosition = new Vector3(0, position, 0);
            }
        }
    }
}