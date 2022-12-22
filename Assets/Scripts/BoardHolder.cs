using UnityEngine;
using TMPro;

/// <summary>
/// Logic for the BoardHolder parent object in the hierarchy
/// </summary>

public class BoardHolder : MonoBehaviour, IHolder
{
    [SerializeField] private GameObject _boardSelection;
    private int _value;

    private void Start()
    {
        _value = 0;
        DisableAllChilds();
    }

    private void Update()
    {
        int value = _boardSelection.GetComponent<TMP_Dropdown>().value;
        CompareValue(value);
    }

    public void DisableAllChilds()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            this.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void CompareValue(int value)
    {
        if(_value != value)
        {
            if(_value != 0) 
            {
                this.transform.GetChild(this._value - 1).gameObject.SetActive(false);
            }
            
            SwitchChild(value);
            _value = value;
        }
    }

    public void SwitchChild(int index)
    {
        switch (index)
        {
            case 0:
                break; //None selected
            case 1:
                this.transform.GetChild(index-1).gameObject.SetActive(true); //Sudoku
                break;
            case 2:
                this.transform.GetChild(index-1).gameObject.SetActive(true); //Suguru
                break;
            case 3:
                this.transform.GetChild(index - 1).gameObject.SetActive(true); //Word search
                break;
            case 4:
                this.transform.GetChild(index - 1).gameObject.SetActive(true); //Binary
                break;
        }
    }
}