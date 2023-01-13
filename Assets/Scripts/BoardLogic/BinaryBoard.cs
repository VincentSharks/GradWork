using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Logic for spawning an empty binary board (Input fields & Lines).
/// Grid based input fields,
/// Can resize dimensions (only even numbers).
/// </summary>

public class BinaryBoard : MonoBehaviour, IBoard
{
    [Header("Other Info")]
    private UIHandler _uiHand;
    private InfoManager _info;
    private bool _isBoardSet = false;
    public bool IsBoardSet { get { return _isBoardSet; } }
    
    [Header("Board Dimensions")]
    private int _valueMin = 2;
    private int _valueMax = 14;

    private int _sizeX = 2;
    private int _sizeY = 2;

    public Vector2 Dimensions { get { return new Vector2(_sizeX, _sizeY); } }

    private Vector2 _dimensions;
    private float _spacing;
    private int _totalWidth = 700;
         
    [Header("Fields")]
    [SerializeField] private GameObject _inputBox;
    private List<GameObject> _inputFields = new List<GameObject>();
    public List<GameObject> InputFields { get { return _inputFields; } }
    [SerializeField] private GameObject _fieldHolder;
    private GridLayoutGroup _fieldGrid;


    [Header("Lines")]
    [SerializeField] private GameObject _linePixel;
    private List<GameObject> _lines = new List<GameObject>();
    [SerializeField] private GameObject _lineHolder;

    public int LineThinWidth { get { return 5; } }
    public int LineThickWidth { get { return 10; } }


    private void OnEnable()
    {
        _fieldGrid = _fieldHolder.GetComponent<GridLayoutGroup>();
        
        _dimensions = new Vector2(0,0);

        _info = FindObjectOfType<InfoManager>();

        _uiHand = FindObjectOfType<UIHandler>();
        _uiHand.SetUpSlider(true, false, _valueMin, _valueMax, _sizeX, _sizeY);

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
        if (_sizeX % 2 != 0) //Dimensions have to be %2
        {
            _sizeX++;            
        }
    }

    public void CompareValues()
    {
        //Get slider value
        _sizeX = _uiHand.GetXSliderValue();
        _sizeY = _uiHand.GetYSliderValue();
        
        CheckEven();

        _sizeY = _sizeX;


        if (_dimensions.x != Dimensions.x) //Dimension changed
        {
            DeleteBoard();

            _info.IsValuesChanged = true;

            _fieldGrid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            _fieldGrid.constraintCount = (int)Dimensions.x;            

            CreateBoard();
            _dimensions = Dimensions;
        }
    }

    public void CreateBoard()
    {
        _spacing = _totalWidth / Dimensions.x;
        _fieldGrid.cellSize = new Vector2(_spacing, _spacing);
        
        SpawnInputFields();
        SpawnLines();
        _isBoardSet = true;
    }

    public void DeleteBoard()
    {
        foreach (GameObject field in _inputFields) //Remove all input fields
        {
            Destroy(field);
        }

        foreach (GameObject line in _lines) //Remove all lines
        {
            Destroy(line);
        }

        _inputFields.Clear();
        _lines.Clear();
        _isBoardSet = false;
    }
    
    public void SpawnInputFields()
    {
        for (int row = 0; row < Dimensions.x; row++)
        {
            for (int column = 0; column < Dimensions.y; column++)
            {
                var input = Instantiate(_inputBox, _fieldHolder.transform);
                _inputFields.Add(input);
            }
        }
    }

    public void SpawnLines()
    {
        var outerLineAddition = 10; //The outer lines don't connect in the corners, with this they connect

        for (int indexX = 0; indexX <= Dimensions.x; indexX++) //X
        {
            var verticalLine = Instantiate(_linePixel, _lineHolder.transform);

            for (int indexY = 0; indexY <= Dimensions.y; indexY++)
            {
                var horizontalLine = Instantiate(_linePixel, _lineHolder.transform);

                var totalWidth = _spacing * _sizeX;
                var totalHeight = _spacing * _sizeY;

                if (indexX == 0 || indexY == 0) //Outer lines left & bottom
                {
                    verticalLine.GetComponent<RectTransform>().localScale = new Vector3(LineThickWidth, totalHeight + outerLineAddition, 1);
                    verticalLine.GetComponent<RectTransform>().localPosition = new Vector3(-1 * (totalWidth / 2.0f), 0, 0);

                    horizontalLine.GetComponent<RectTransform>().localScale = new Vector3(totalWidth + outerLineAddition, LineThickWidth, 1);
                    horizontalLine.GetComponent<RectTransform>().localPosition = new Vector3(0, -1 * (totalHeight / 2.0f), 0);

                }
                else if (indexX % (Dimensions.x) == 1 || indexY % (Dimensions.y) == 1) //Outer lines right & top
                {
                    verticalLine.GetComponent<RectTransform>().localScale = new Vector3(LineThickWidth, totalHeight + outerLineAddition, 1);
                    verticalLine.GetComponent<RectTransform>().localPosition = new Vector3((totalWidth / 2.0f), 0, 0);

                    horizontalLine.GetComponent<RectTransform>().localScale = new Vector3(totalWidth + outerLineAddition, LineThickWidth, 1);
                    horizontalLine.GetComponent<RectTransform>().localPosition = new Vector3(0, (totalHeight / 2.0f), 0);
                }
                else //Inner lines
                {
                    var positionX = ((((Dimensions.x / 2) - 1) + indexX) * _spacing) - totalWidth;
                    var positionY = ((((Dimensions.y / 2) - 1) + indexY) * _spacing) - totalHeight;

                    verticalLine.GetComponent<RectTransform>().localScale = new Vector3(LineThinWidth, totalHeight + outerLineAddition, 1);
                    verticalLine.GetComponent<RectTransform>().localPosition = new Vector3(positionX, 0, 0);

                    horizontalLine.GetComponent<RectTransform>().localScale = new Vector3(totalWidth + outerLineAddition, LineThinWidth, 1);
                    horizontalLine.GetComponent<RectTransform>().localPosition = new Vector3(0, positionY, 0);
                }

                _lines.Add(verticalLine);
                _lines.Add(horizontalLine);
            }
        }
    }
}