using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Logic for spawning an empty binary board
/// - Grid based input fields
/// - Can resize dimensions (only even numbers)
/// </summary>

public class BinaryBoard : MonoBehaviour, IBoard
{
    int valueMin = 2;
    int valueMax = 14;

    int sizeX;
    int sizeY;

    public Vector2 Dimension { get { return new Vector2(sizeX, sizeY); } set { value = new Vector2(sizeX, sizeY); } }

    int dim;

    public bool IsBoardSet { get { return false; } set { value = false; } } //false

    float spacing;

    UIHandler uiHand;
    InfoManager info;

    [Header("Fields")]
    [SerializeField] GameObject inputBox;
    private List<GameObject> inputFields = new List<GameObject>();
    public List<GameObject> InputFields { get { return inputFields; } set { value = inputFields; } }
    [SerializeField] GameObject fieldHolder;
    private GridLayoutGroup fieldGrid;

    int totalWidth = 700;

    [Header("Lines")]
    [SerializeField] GameObject linePixel;
    List<GameObject> lines = new List<GameObject>();
    [SerializeField] GameObject lineHolder;

    public int LineThinWidth { get { return 5; } set { value = 5; } }
    public int LineThiccWidth { get { return 10; } set { value = 10; } }


    private void OnEnable()
    {
        fieldGrid = fieldHolder.GetComponent<GridLayoutGroup>();

        sizeX = 2;
        sizeY = 2;

        dim = 0;

        info = FindObjectOfType<InfoManager>();

        uiHand = FindObjectOfType<UIHandler>();
        uiHand.SetUpSlider(true, false, valueMin, valueMax, sizeX, sizeY);

        if (!IsBoardSet)
        {
            CreateBoard();
        }        
    }

    private void Update()
    {        
        CompareValues();
    }

    private void CheckEven()
    {
        if (sizeX % 2 != 0) //Dimensions have to be %2
        {
            sizeX++;            
        }
    }

    public void CompareValues()
    {
        //Get slider value
        sizeX = uiHand.GetXSliderValue();
        sizeY = uiHand.GetYSliderValue();
        
        CheckEven();

        Dimension = new Vector2(sizeX, sizeY);

        if (dim != Dimension.x) //Dimension changed
        {
            DeleteBoard();

            sizeY = sizeX;

            info.isValuesChanged = true;

            fieldGrid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            fieldGrid.constraintCount = (int)Dimension.x;            

            CreateBoard();
            dim = (int)Dimension.x;
        }
    }

    public void CreateBoard()
    {
        spacing = totalWidth / Dimension.x;
        fieldGrid.cellSize = new Vector2(spacing, spacing);

        this.GetComponent<Image>().color = new Color(255, 255, 255, 255); //Switch to a white colour BG colour

        SpawnInputFields();
        SpawnLines();
        IsBoardSet = true;
    }

    public void DeleteBoard()
    {
        foreach(GameObject go in inputFields) //Remove all input fields
        {
            Destroy(go);
        }

        foreach(GameObject go in lines) //Remove all lines
        {
            Destroy(go);
        }

        inputFields.Clear();
        lines.Clear();
        IsBoardSet = false;
    }
    
    public void SpawnInputFields()
    {
        for (int row = 0; row < Dimension.x; row++)
        {
            for (int column = 0; column < this.Dimension.y; column++)
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