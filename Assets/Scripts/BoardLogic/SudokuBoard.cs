using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Logic for spawning an empty Sudoku board
/// </summary>

public class SudokuBoard : MonoBehaviour, IBoard
{
    [Header("Other Info")]
    private UIHandler _uiHand;
    private InfoManager _info;
    private bool _isBoardSet = false;
    public bool IsBoardSet { get { return _isBoardSet; } }

    [Header("Board Dimensions")]
    private int _sizeX = 9;
    private int _sizeY = 9;

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

        _dimensions = new Vector2(0, 0);

        _info = FindObjectOfType<InfoManager>();

        _uiHand = FindObjectOfType<UIHandler>();
        _uiHand.SetUpSlider(false, false, 0, 1, _sizeX, _sizeY);

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
        if (_dimensions.x != Dimensions.x || _dimensions.y != Dimensions.y) //Dimension changed
        {
            DeleteBoard();

            _info.IsValuesChanged = true;

            _fieldGrid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            _fieldGrid.constraintCount = (int)Dimensions.x;

            CreateBoard();
            _fieldHolder.gameObject.SetActive(true);
            _dimensions = Dimensions;
        }
    }

    public void CreateBoard()
    {
        _fieldHolder.gameObject.SetActive(false); //Disable input field parent, so that the spawn in bug is fixed

        _spacing = _totalWidth / Dimensions.x;
        _fieldGrid.cellSize = new Vector2(_spacing, _spacing);

        SpawnInputFields();
        SpawnLines();
        _isBoardSet = true;
    }

    public void DeleteBoard()
    {
        foreach (GameObject go in _inputFields) //Remove all input fields
        {
            Destroy(go);
        }

        foreach (GameObject go in _lines) //Remove all lines
        {
            Destroy(go);
        }

        _inputFields.Clear();
        _lines.Clear();
        _isBoardSet = false;
    }

    public void SpawnInputFields()
    {
        for (int column = 0; column < _sizeX; column++)
        {
            for (int row = 0; row < _sizeY; row++)
            {
                var input = Instantiate(_inputBox, _fieldHolder.transform);
                _inputFields.Add(input);
            }
        }
    }

    public void SpawnLines()
    {
        var outerLineAddition = 10; //The outer lines don't connect in the corners, with this they connect
        
        for (int index = 0; index < 10; index++)
        {
            //Spawn as child
            var lineVertical = Instantiate(_linePixel, _lineHolder.transform);
            var lineHorizontal = Instantiate(_linePixel, _lineHolder.transform);
            
            var totalWidth = _spacing * _sizeX;
            var totalHeight = _spacing * _sizeY;

            var position = (index * _spacing + (LineThickWidth / 2)) - (_totalWidth / 2);
            var otherAxisPositionOffset = outerLineAddition / 2;

            if (index % 3 == 0)
            {
                //Thicc
                lineVertical.GetComponent<RectTransform>().localScale = new Vector3(LineThickWidth, totalWidth + outerLineAddition, 1);
                lineVertical.GetComponent<RectTransform>().localPosition = new Vector3(position, otherAxisPositionOffset, 0);
                
                lineHorizontal.GetComponent<RectTransform>().localScale = new Vector3(totalHeight + outerLineAddition, LineThickWidth, 1);
                lineHorizontal.GetComponent<RectTransform>().localPosition = new Vector3(otherAxisPositionOffset, position, 0);
            }
            else
            {
                //Thin
                lineVertical.GetComponent<RectTransform>().localScale = new Vector3(LineThinWidth, _totalWidth, 1);
                lineHorizontal.GetComponent<RectTransform>().localScale = new Vector3(_totalWidth, LineThinWidth, 1);

                lineVertical.GetComponent<RectTransform>().localPosition = new Vector3(position, otherAxisPositionOffset, 0);
                lineHorizontal.GetComponent<RectTransform>().localPosition = new Vector3(otherAxisPositionOffset, position, 0);
            }

            _lines.Add(lineVertical);
            _lines.Add(lineHorizontal);
        }
    }
}