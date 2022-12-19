using UnityEngine;
using TMPro;

//Holds the logic to spawn the 

public class BoardHolder : MonoBehaviour, IHolder
{
    [SerializeField] GameObject boardSelection;
    int value;

    private void Start()
    {
        value = 0;
        DisableAllChilds();
    }

    private void Update()
    {
        int value = boardSelection.GetComponent<TMP_Dropdown>().value;
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
        if(this.value != value)
        {
            if(this.value != 0) 
            {
                this.transform.GetChild(this.value - 1).gameObject.SetActive(false);
            }
            
            SwitchChild(value);
            this.value = value;
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