using TMPro;
using UnityEngine;

public class AlgorithmHolder : MonoBehaviour, IHolder
{
    [SerializeField] GameObject algorithmSelection;
    InfoManager info;
    int value;


    private void Start()
    {
        info = FindObjectOfType<InfoManager>();

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
            info.isAlgorithmSet = false;
        }
    }

    public void CompareValue(int value)
    {
        if (this.value != value)
        {
            if (this.value != 0)
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
                this.transform.GetChild(index - 1).gameObject.SetActive(true); //Bogosort
                info.isAlgorithmSet = true;
                break;
        }
    }
}
