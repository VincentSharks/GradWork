using TMPro;
using UnityEngine;
using UnityEngine.UI;

//
//Takes care of representing the stats in the UI

public class UIHandler : MonoBehaviour
{
    //take info from infomanager
    //get reference to UI
    //

    [SerializeField] private InfoManager info;

    [SerializeField] private TMP_Text selectedPuzzle;
    [SerializeField] private TMP_Text boardSize;

    [Header("UI")]
    [SerializeField] private GameObject xSlider;
    [SerializeField] private GameObject ySlider;

    private void Awake()
    {
        SetUpSlider(false, false, 0, 1, 0, 0);
    }

    private void Update()
    {
        SetBoardInfoUI();   
    }

    private void SetBoardInfoUI()
    {
        selectedPuzzle.text = " " + info.puzzleName;
        boardSize.text = " [X:" + info.boardSize.x + ", Y: " + info.boardSize.y + "]";
    }

    public void SetUpSlider(bool enable, bool enableY, int sliderMin, int sliderMax, int valueX, int valueY)
    {
        xSlider.SetActive(enable);
        ySlider.SetActive(enableY);
        
        if (!enable) //sliders disabled
        {           
            return;
        }

        //X Slider
        var x = xSlider.GetComponent<Slider>();
        x.minValue = sliderMin;
        x.maxValue = sliderMax;
        x.value = valueX;
        
        //Y Slider
        var y = ySlider.GetComponent<Slider>();
        y.minValue = sliderMin;
        y.maxValue = sliderMax;
        y.value = valueY;
    }

    public int GetXSliderValue()
    {
        return (int)xSlider.GetComponent<Slider>().value;
    }

    public int GetYSliderValue()
    {
        return (int)ySlider.GetComponent<Slider>().value;
    }
}
