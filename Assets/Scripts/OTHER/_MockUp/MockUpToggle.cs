using UnityEngine;

public class MockUpToggle : MonoBehaviour
{
    [SerializeField] GameObject loadBoardButton;
    [SerializeField] GameObject exportBoardButton;
    [SerializeField] GameObject loadStats;
    [SerializeField] GameObject exportStats;

    bool isToggled = false;
    
    private void Update()
    {
        //In non prototype version -> dirty flag?

        if(isToggled)
        {
            loadBoardButton.SetActive(true);
            exportBoardButton.SetActive(true);
            loadStats.SetActive(true);
            exportStats.SetActive(true);
        }
        else
        {
            loadBoardButton.SetActive(false);
            exportBoardButton.SetActive(false);
            loadStats.SetActive(false);
            exportStats.SetActive(false);
        }
    }

    public void ChangeToggle()
    {
        isToggled = !isToggled;
    }
}
