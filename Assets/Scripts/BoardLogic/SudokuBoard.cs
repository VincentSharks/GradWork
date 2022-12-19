using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Logic for spawning an empty binary board
/// </summary>

public class SudokuBoard : MonoBehaviour, IBoard
{
    private int sizeX = 9;
    private int sizeY = 9;

    public Vector2 Dimension { get { return new Vector2(sizeX, sizeY); } set { value = new Vector2(sizeX, sizeY); } }

    private int dimX;
    private int dimY;

    public bool IsBoardSet { get { return false; } set { value = false; } } //false

    private float spacing;

    UIHandler uiHand;
    InfoManager info;

    [Header("Fields")]
    [SerializeField] private GameObject inputBox;
    private List<GameObject> inputFields = new List<GameObject>();
    public List<GameObject> InputFields { get { return inputFields; } set { value = inputFields; } }
    [SerializeField] private GameObject fieldHolder;
    private GridLayoutGroup fieldGrid;

    private int totalWidth = 700;

    [Header("Lines")]
    [SerializeField] private GameObject linePixel;
    private List<GameObject> lines = new List<GameObject>();
    [SerializeField] private GameObject lineHolder;


    public int LineThinWidth { get { return 5; } set { value = 5; } }
    public int LineThiccWidth { get { return 10; } set { value = 10; } }


    private void OnEnable()
    {
        sizeX = 9;
        sizeY = 9;

        dimX = 0;
        dimY = 0;

        fieldGrid = fieldHolder.GetComponent<GridLayoutGroup>();

        uiHand = FindObjectOfType<UIHandler>();
        uiHand.SetUpSlider(false, false, 0, 1, sizeX, sizeY);

        if (!IsBoardSet)
        {
            CreateBoard();
        }        
    }

    private void Update()
    {
        Dimension = new Vector2(sizeX, sizeY);
        CompareValues();
    }

    public void CompareValues()
    {
        if (dimX != Dimension.x || dimY != Dimension.y) //Dimension changed
        {
            DeleteBoard();
            
            fieldGrid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            fieldGrid.constraintCount = sizeX;

            CreateBoard();

            fieldHolder.gameObject.SetActive(true);
            dimX = sizeX;
            dimY = sizeY;
        }
    }

    public void CreateBoard()
    {
        fieldHolder.gameObject.SetActive(false); //Disable input field parent, so that the spawn in bug is fixed
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
        for (int index = 0; index < 10; index++)
        {
            //Spawn as child
            var lineVertical = Instantiate(linePixel, lineHolder.transform);
            var lineHorizontal = Instantiate(linePixel, lineHolder.transform);

            var position = (index * spacing + (LineThiccWidth / 2)) - (totalWidth / 2);

            if (index % 3 == 0)
            {
                //Thicc
                lineVertical.GetComponent<RectTransform>().localScale = new Vector3(LineThiccWidth, totalWidth, 1);
                lineHorizontal.GetComponent<RectTransform>().localScale = new Vector3(totalWidth, LineThiccWidth, 1);
                
                lineVertical.GetComponent<RectTransform>().localPosition = new Vector3(position, 0, 0);
                lineHorizontal.GetComponent<RectTransform>().localPosition = new Vector3(0, position, 0);
            }
            else
            {
                //Thin
                lineVertical.GetComponent<RectTransform>().localScale = new Vector3(LineThinWidth, totalWidth, 1);
                lineHorizontal.GetComponent<RectTransform>().localScale = new Vector3(totalWidth, LineThinWidth, 1);
                
                lineVertical.GetComponent<RectTransform>().localPosition = new Vector3(position, 0, 0);
                lineHorizontal.GetComponent<RectTransform>().localPosition = new Vector3(0, position, 0);
            }

            lines.Add(lineVertical);
            lines.Add(lineHorizontal);
        }
    }
}