using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BinaryBoard : MonoBehaviour
{
    [SerializeField, Range(2,10)] int dimension;
    int dim;

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


    private void OnEnable()
    {
        dimension = 4;
        dim = 0;

        fieldGrid = fieldHolder.GetComponent<GridLayoutGroup>();

        if (!isBoardSet)
        {
            CreateBoard();
        }        
    }

    private void Update()
    {
        CheckEven();
        CompareValues();
    }

    private void DeleteExistingBoard()
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
        isBoardSet = false;
    }

    private void CompareValues()
    {
        if(dim != dimension) //Dimension changed
        {            
            DeleteExistingBoard();
            fieldGrid.constraintCount = dimension;
            CreateBoard();
            dim = dimension;
        }
    }

    private void CheckEven()
    {
        if (dimension % 2 != 0) //dimensions have to be %2
        {
            dimension++;
        }
    }

    public void CreateBoard()
    {
        spacing = totalWidth / dimension;
        fieldGrid.cellSize = new Vector2(spacing, spacing);

        this.GetComponent<Image>().color = new Color(255, 255, 255, 255); //Switch to a white colour BG colour

        SpawnInputFields();
        SpawnLines();
        isBoardSet = true;
    }

    private void SpawnInputFields()
    {
        for (int row = 0; row < dimension; row++)
        {
            for (int column = 0; column < this.dimension; column++)
            {
                var input = Instantiate(inputBox, fieldHolder.transform);
                inputFields.Add(input);
            }
        }
    }

    private void SpawnLines()
    {
        var outerLineAddition = 10; //The outer lines don't connect in the corners, with this they connect

        for (int index = 0; index <= dimension; index++)
        {
            var verticalLine = Instantiate(linePixel, lineHolder.transform);
            var horizontalLine = Instantiate(linePixel, lineHolder.transform);

            if (index == 0) //Outer edge left & bottom
            {
                verticalLine.GetComponent<RectTransform>().localScale = new Vector3(lineThiccWidth, totalWidth + outerLineAddition, 1);
                verticalLine.GetComponent<RectTransform>().localPosition = new Vector3(-1 * (totalWidth / 2.0f), 0, 0);

                horizontalLine.GetComponent<RectTransform>().localScale = new Vector3(totalWidth + outerLineAddition, lineThiccWidth, 1);
                horizontalLine.GetComponent<RectTransform>().localPosition = new Vector3(0, -1 * (totalWidth / 2.0f), 0);
            }
            else if (index % (dimension) == 1) //Outer edge right & top
            {
                verticalLine.GetComponent<RectTransform>().localScale = new Vector3(lineThiccWidth, totalWidth + outerLineAddition, 1);
                verticalLine.GetComponent<RectTransform>().localPosition = new Vector3((totalWidth / 2.0f), 0, 0);

                horizontalLine.GetComponent<RectTransform>().localScale = new Vector3(totalWidth + outerLineAddition, lineThiccWidth, 1);
                horizontalLine.GetComponent<RectTransform>().localPosition = new Vector3(0, (totalWidth / 2.0f), 0);
            }
            else //Thin lines
            {
                verticalLine.GetComponent<RectTransform>().localScale = new Vector3(lineThinWidth, totalWidth + outerLineAddition, 1);
                verticalLine.GetComponent<RectTransform>().localPosition = new Vector3(((((dimension / 2) - 1) + index) * spacing) - totalWidth, 0, 0);

                horizontalLine.GetComponent<RectTransform>().localScale = new Vector3(totalWidth + outerLineAddition, lineThinWidth, 1);
                horizontalLine.GetComponent<RectTransform>().localPosition = new Vector3(0, (( ( (dimension / 2) - 1) + index) * spacing) - totalWidth, 0);
            }

            lines.Add(verticalLine);
            lines.Add(horizontalLine);
        }
    }
}