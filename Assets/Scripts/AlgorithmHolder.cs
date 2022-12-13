using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AlgorithmHolder : MonoBehaviour, IHolder
{
    [SerializeField] GameObject algorithmSelection;

    public int value;


    private void Start()
    {
        value = 0;
        DisableAllChilds();
    }

    private void Update()
    {
        int value = algorithmSelection.GetComponent<TMP_Dropdown>().value;
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
        if (this.value != value)
        {
            this.transform.GetChild(this.value).gameObject.SetActive(false);
            SwitchChild(value);
            this.value = value;
        }
    }

    public void SwitchChild(int index)
    {
        switch (index)
        {
            case 0:
                this.transform.GetChild(index).gameObject.SetActive(true); //Sudoku
                break;
            case 1:
                this.transform.GetChild(index).gameObject.SetActive(true); //Suguru
                break;
            case 2:
                this.transform.GetChild(index).gameObject.SetActive(true); //Word search
                break;
            case 3:
                this.transform.GetChild(index).gameObject.SetActive(true); //Binary
                break;
        }
    }
}
