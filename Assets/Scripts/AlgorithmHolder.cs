using TMPro;
using UnityEngine;

/// <summary>
/// Logic for the AlgorithmHolder parent object in the hierarchy
/// </summary>

public class AlgorithmHolder : MonoBehaviour, IHolder
{
    [SerializeField] private GameObject _algorithmSelection;
    private InfoManager _info;
    private int _value;


    private void Start()
    {
        _info = FindObjectOfType<InfoManager>();

        _value = 0;
        DisableAllChilds();
    }

    private void Update()
    {
        int value = _algorithmSelection.GetComponent<TMP_Dropdown>().value;
        CompareValue(value);
    }

    public void DisableAllChilds()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            this.transform.GetChild(i).gameObject.SetActive(false);
            _info.isAlgorithmSet = false;
        }
    }

    public void CompareValue(int value)
    {
        if (_value != value)
        {
            if (_value != 0)
            {
                this.transform.GetChild(_value - 1).gameObject.SetActive(false);
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
                this.transform.GetChild(index - 1).gameObject.SetActive(true); //Bogosort
                _info.isAlgorithmSet = true;
                break;
        }
    }
}
