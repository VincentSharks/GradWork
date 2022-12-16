using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordSearchBoard : MonoBehaviour
{
    //[SerializeField, Range(2, 14)] int dimension;

    [SerializeField, Range(2, 12)] int sizeX;
    [SerializeField, Range(2, 12)] int sizeY;

    Vector2 dimensions;

    int dimX;
    int dimY;

    bool isBoardSet = false;

    float spacing;

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

    int lineThiccWidth = 10;
    int lineThinWidth = 5;

    [Header("Words")]
    int test = 0;


    private void OnEnable()
    {
        sizeX = 2;
        sizeY = 2;
        
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
        dimensions = new Vector2(sizeX, sizeY);

        CompareValues();
    }

    private void DeleteExistingBoard()
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

    private void CompareValues()
    {
        if (dimX != dimensions.x || dimY != dimensions.y) //Dimension changed
        {
            DeleteExistingBoard();

            if(dimensions.y > dimensions.x) //Change to fixed row
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
        if(sizeX > sizeY)
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

    private void SpawnInputFields()
    {
        //first x = column
        //then Y = row

        for (int column = 0; column < sizeX; column++)
        {
            for (int row = 0; row < sizeY; row++)
            {
                var input = Instantiate(inputBox, fieldHolder.transform);
                inputFields.Add(input);
            }
        }
    }

    private void SpawnLines()
    {
        var outerLineAddition = 10; //The outer lines don't connect in the corners, with this they connect


        for (int indexX = 0; indexX <= dimensions.x; indexX++) //X
        {
            var verticalLine = Instantiate(linePixel, lineHolder.transform);

            for(int indexY = 0; indexY <= dimensions.y; indexY++)
            {
                var horizontalLine = Instantiate(linePixel, lineHolder.transform);
                
                var totalWidth = spacing * sizeX;
                var totalHeight = spacing * sizeY;


                if (indexX == 0 || indexY == 0) //Outer edge left & bottom
                {
                    verticalLine.GetComponent<RectTransform>().localScale = new Vector3(lineThiccWidth, totalHeight + outerLineAddition, 1);
                    verticalLine.GetComponent<RectTransform>().localPosition = new Vector3(-1 * (totalWidth / 2.0f), 0, 0);

                    horizontalLine.GetComponent<RectTransform>().localScale = new Vector3(totalWidth + outerLineAddition, lineThiccWidth, 1);
                    horizontalLine.GetComponent<RectTransform>().localPosition = new Vector3(0, -1 * (totalHeight / 2.0f), 0);
                    
                }
                else if(indexX % (dimensions.x) == 1 || indexY % (dimensions.y) == 1)
                {
                    verticalLine.GetComponent<RectTransform>().localScale = new Vector3(lineThiccWidth, totalHeight + outerLineAddition, 1);
                    verticalLine.GetComponent<RectTransform>().localPosition = new Vector3((totalWidth / 2.0f), 0, 0);

                    horizontalLine.GetComponent<RectTransform>().localScale = new Vector3(totalWidth + outerLineAddition, lineThiccWidth, 1);
                    horizontalLine.GetComponent<RectTransform>().localPosition = new Vector3(0, (totalHeight / 2.0f), 0);
                }
                else
                {
                    var positionX = ((((dimensions.x / 2) - 1) + indexX) * spacing) - totalWidth;
                    var positionY = ((((dimensions.y / 2) - 1) + indexY) * spacing) - totalHeight;

                    verticalLine.GetComponent<RectTransform>().localScale = new Vector3(lineThinWidth, totalHeight + outerLineAddition, 1);
                    verticalLine.GetComponent<RectTransform>().localPosition = new Vector3(positionX, 0, 0);

                    horizontalLine.GetComponent<RectTransform>().localScale = new Vector3(totalWidth + outerLineAddition, lineThinWidth, 1);
                    horizontalLine.GetComponent<RectTransform>().localPosition = new Vector3(0, positionY, 0);
                }

                lines.Add(verticalLine);
                lines.Add(horizontalLine);                
            }
        }
    }
}