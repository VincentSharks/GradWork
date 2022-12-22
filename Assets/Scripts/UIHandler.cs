using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Takes care of representing the stats in the UI
/// take info from infomanager
/// get reference to UI
/// </summary>

public class UIHandler : MonoBehaviour
{
    [SerializeField] private InfoManager _info;

    [Header("UI")]
    [SerializeField] private TMP_Text _selectedPuzzle;
    [SerializeField] private TMP_Text _boardSize;

    [SerializeField] private TMP_Text _selectedAlgorithm;
    [SerializeField] private TMP_Text _randomSeed;
           
    [SerializeField] private GameObject _xSlider;
    [SerializeField] private GameObject _ySlider;
    [SerializeField] private GameObject _runButton;

    private void Awake()
    {
        SetUpSlider(false, false, 0, 1, 0, 0);
    }

    private void Update()
    {
        SetBoardInfoUI();
        SetAlgorithmInfoUI();
        SetOtherInfoUI();
        SetUpRunButton();
    }

    private void SetBoardInfoUI()
    {
        _selectedPuzzle.text = " " + _info.puzzleName;
        _boardSize.text = " [X:" + _info.boardSize.x + ", Y: " + _info.boardSize.y + "]";
    }

    private void SetAlgorithmInfoUI()
    {
        _selectedAlgorithm.text = " " + _info.algorithmName + " " + " v." + _info.algorithmVersion;
    }

    private void SetOtherInfoUI()
    {
        _randomSeed.text = " " + _info.RandomSeed;
    }

    public void SetUpSlider(bool enable, bool enableY, int sliderMin, int sliderMax, int valueX, int valueY)
    {
        _xSlider.SetActive(enable);
        _ySlider.SetActive(enableY);
        
        if (!enable) //sliders disabled
        {           
            return;
        }

        //X Slider
        var x = _xSlider.GetComponent<Slider>();
        x.minValue = sliderMin;
        x.maxValue = sliderMax;
        x.value = valueX;
        
        //Y Slider
        var y = _ySlider.GetComponent<Slider>();
        y.minValue = sliderMin;
        y.maxValue = sliderMax;
        y.value = valueY;
    }

    public int GetXSliderValue()
    {
        return (int)_xSlider.GetComponent<Slider>().value;
    }

    public int GetYSliderValue()
    {
        return (int)_ySlider.GetComponent<Slider>().value;
    }

    private void SetUpRunButton()
    {
        if(!_info.isPuzzleSelected || !_info.isAlgorithmSelected)
        {
            _runButton.SetActive(false);
            return;
        }

        _runButton.SetActive(true);
    }
}
