using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Temp script, currently a copy of word search
/// </summary>

public class SuguruBoard : MonoBehaviour, IBoard
{
    //Spawn fields like in word search board - DONE

    //pick a random field (that doesn't have an ID yet) - 
    //generate a random number (1-4)
    //try to grow in that direction
    //else regenerate a number

    //grow in that direction
    //add the ID to this field, 

    //max go to 5 fields with a single ID

    //repeat until the field is filled






    private int _valueMin = 2;
    private int _valueMax = 12;

    private int _sizeX;
    private int _sizeY;

    public Vector2 dimensions { get { return new Vector2(_sizeX, _sizeY); } set { value = new Vector2(_sizeX, _sizeY); } }

    private int _dimX;
    private int _dimY;

    public bool isBoardSet { get { return false; } set { value = false; } } //false

    private float _spacing;

    private UIHandler _uiHand;
    private InfoManager _info;

    [Header("Fields")]
    [SerializeField] private GameObject _inputBox;
    private List<GameObject> _inputFields = new List<GameObject>();
    public List<GameObject> inputFields { get { return _inputFields; } }
    private List<GameObject> _fieldsForIDs = new List<GameObject>();
    [SerializeField] private GameObject _fieldHolder;
    private GridLayoutGroup _fieldGrid;

    private int _totalWidth = 700;

    [Header("Lines")]
    [SerializeField] private GameObject _linePixel;
    private List<GameObject> _lines = new List<GameObject>();
    [SerializeField] private GameObject _lineHolder;

    public int lineThinWidth { get { return 5; } set { value = 5; } }
    public int lineThiccWidth { get { return 10; } set { value = 10; } }

    private int _fieldID = 1;


    private void OnEnable()
    {
        _fieldGrid = _fieldHolder.GetComponent<GridLayoutGroup>();

        _sizeX = 2;
        _sizeY = 2;

        _dimX = 0;
        _dimY = 0;

        _info = FindObjectOfType<InfoManager>();

        _uiHand = FindObjectOfType<UIHandler>();
        _uiHand.SetUpSlider(true, true, _valueMin, _valueMax, _sizeX, _sizeY);

        if (!isBoardSet)
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
        _sizeX = _uiHand.GetXSliderValue();
        _sizeY = _uiHand.GetYSliderValue();

        dimensions = new Vector2(_sizeX, _sizeY);

        if (_dimX != dimensions.x || _dimY != dimensions.y) //Dimension changed
        {
            DeleteBoard();

            //info manager value change
            _info.isValuesChanged = true;

            if (dimensions.y > dimensions.x) //Change to fixed row
            {
                _fieldGrid.constraint = GridLayoutGroup.Constraint.FixedRowCount;
                _fieldGrid.constraintCount = _sizeY;
            }
            else
            {
                _fieldGrid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
                _fieldGrid.constraintCount = _sizeX;
            }

            CreateBoard();
            _dimX = _sizeX;
            _dimY = _sizeY;
        }
    }

    public void CreateBoard()
    {
        if (_sizeX > _sizeY)
        {
            _spacing = _totalWidth / _sizeX;
        }
        else
        {
            _spacing = _totalWidth / _sizeY;
        }

        _fieldGrid.cellSize = new Vector2(_spacing, _spacing);

        //this.GetComponent<Image>().color = new Color(255, 255, 255, 255); //Switch to a white colour BG colour

        SpawnInputFields();
        SpawnLines();
        isBoardSet = true;
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
        _fieldsForIDs.Clear();
        _lines.Clear();
        isBoardSet = false;
    }

    public void SpawnInputFields()
    {
        for (int column = 0; column < _sizeX; column++)
        {
            for (int row = 0; row < _sizeY; row++)
            {
                var input = Instantiate(_inputBox, _fieldHolder.transform);
                _inputFields.Add(input);
                _fieldsForIDs.Add(input);
                //_fieldsForIDs.Add(input);
            }
        }
    }

    public void SpawnLines()
    {
        var outerLineAddition = 10; //The outer lines don't connect in the corners, with this they connect

        for (int indexX = 0; indexX <= dimensions.x; indexX++) //X
        {
            var verticalLine = Instantiate(_linePixel, _lineHolder.transform);

            for (int indexY = 0; indexY <= dimensions.y; indexY++)
            {
                var horizontalLine = Instantiate(_linePixel, _lineHolder.transform);

                var totalWidth = _spacing * _sizeX;
                var totalHeight = _spacing * _sizeY;

                if (indexX == 0 || indexY == 0) //Outer edge left & bottom
                {
                    verticalLine.GetComponent<RectTransform>().localScale = new Vector3(lineThiccWidth, totalHeight + outerLineAddition, 1);
                    verticalLine.GetComponent<RectTransform>().localPosition = new Vector3(-1 * (totalWidth / 2.0f), 0, 0);

                    horizontalLine.GetComponent<RectTransform>().localScale = new Vector3(totalWidth + outerLineAddition, lineThiccWidth, 1);
                    horizontalLine.GetComponent<RectTransform>().localPosition = new Vector3(0, -1 * (totalHeight / 2.0f), 0);

                }
                else if (indexX % (dimensions.x) == 1 || indexY % (dimensions.y) == 1)
                {
                    verticalLine.GetComponent<RectTransform>().localScale = new Vector3(lineThiccWidth, totalHeight + outerLineAddition, 1);
                    verticalLine.GetComponent<RectTransform>().localPosition = new Vector3((totalWidth / 2.0f), 0, 0);

                    horizontalLine.GetComponent<RectTransform>().localScale = new Vector3(totalWidth + outerLineAddition, lineThiccWidth, 1);
                    horizontalLine.GetComponent<RectTransform>().localPosition = new Vector3(0, (totalHeight / 2.0f), 0);
                }
                else
                {
                    
                }

                _lines.Add(verticalLine);
                _lines.Add(horizontalLine);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>

    //
    private GameObject _startField;
    private GameObject _currentField;
    private GameObject _neighbourField;
    private bool _isGrow = false;
    private List<GameObject> _localFieldList = new List<GameObject>();

    public void SuguruBoardPieces() //Logic for the grouping of fields
    {
        if (_fieldsForIDs.Count <= 0) //No more available tiles
        {
            Debug.Log("Board should be completely done");
            return;
        }

        //Start field
        _currentField = _startField = GetRandomField(); //Random field to start
        CheckFieldAvailability(_currentField, _localFieldList); //Check if file isn't already taken
        _currentField.GetComponent<Image>().color = new Color(0.2f, 0.2f, .8f);

        //Neightbour
        _isGrow = true;

        while (_isGrow)
        {
            Debug.Log("RUN");

            //Get direction from current field
            //Check direction
            GetNeighbour();
            CheckFieldAvailability(_neighbourField, _localFieldList);
            _neighbourField.GetComponent<Image>().color = new Color(1f, .2f, .2f);
            _currentField = _neighbourField;

            //Check field in direction
            //Grow in that direction
            //if can't grow anymore (all directions blocked or list count >= 5)






            //TryGrowNeighbour(_localFieldList);

            if(_localFieldList.Count >=5)
            {
                Debug.Log("STOP");
                _isGrow = false;
            }

           
        }
    }

    private GameObject GetRandomField()
    {
        var index = Random.Range(0, _fieldsForIDs.Count);
        return _fieldsForIDs[index];
    }

    private void CheckFieldAvailability(GameObject field, List<GameObject> localFieldList)
    {
        var fieldID = field.GetComponent<Field>().fieldID;
        if (fieldID == 0)
        {
            fieldID = _fieldID;
            localFieldList.Add(field);
            _fieldsForIDs.Remove(field);
        }
    }

    private void GetNeighbour()
    {
        var index = GetDirection();

        if (index < 0 || index > inputFields.Count) //out of board
        {
            index = GetDirection();
            return;
        }

        _neighbourField = inputFields[index];
        //_localFieldList.Add(_neighbourField);
    }

    private int GetDirection()
    {
        var direction = Random.Range(1, 5); //Direction to go
        var currentID = inputFields.IndexOf(_currentField);

        switch (direction)
        {
            case 1: //North
                return currentID - _sizeX;
            case 2: //Right
                return currentID + 1;
            case 3: //Down
                return currentID + _sizeX;
            case 4: //Left
                return currentID - 1;
        }
        return 0;
    }

    private void TryGrowNeighbour(List<GameObject> localFieldList)
    {
        if(localFieldList.Count < 5)
        {
            var neighbourID = _neighbourField.GetComponent<Field>().fieldID;
            if (neighbourID == 0)
            {
                neighbourID = _fieldID;
                localFieldList.Add(_neighbourField);
                _fieldsForIDs.Remove(_neighbourField);
            }
        }
        else
        {
            _isGrow = false;
        }
    }
}