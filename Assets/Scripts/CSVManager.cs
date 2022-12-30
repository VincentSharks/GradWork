using System.IO;
using TMPro;
using UnityEngine;

/// <summary>
/// Get all info
/// put it into a CSV file,
/// export/read CSV files
/// </summary>

public class CSVManager : MonoBehaviour
{
    private InfoManager _info;

    private string _statsFilesName = "StatsFile.csv";
    private string _boardFileName = "BoardFile.csv";

    private string _filePath = "Assets/CSVFiles/";

    private string _fieldList;


    private void OnEnable()
    {
        _info = FindObjectOfType<InfoManager>();
    }

    public void WriteDocuments()
    {
        ExportData(false, _statsFilesName);
        ExportData(true, _boardFileName);
    }

    //method for writing both files
    #region Writing Data
    private void ExportData(bool isBoardFile, string fileName)
    {
        var filePath = _filePath + fileName;

        if (isBoardFile)
        {
            WriteBoardData(filePath);
        }
        else
        {
            WriteStatsData(filePath);
        }
    }
    

    #region Board Data
    private void WriteBoardData(string path)
    {
        TextWriter tw = new StreamWriter(path, false);
        tw.WriteLine("Number" + ";" + "Board List");
        tw.Close();

        //Open the file again and write in the data
        tw = new StreamWriter(path, true);

        foreach (string board in _info.boardData)
        {
            tw.WriteLine((_info.boardData.IndexOf(board) + 1) + ";" + $"{board}");
        }

        tw.Close();
    }
    private string BoardData() //Return the board list as a string
    {
        _fieldList = "";

        if (_info.isReadyForData)
        {
            foreach (GameObject go in _info.inputFields)
            {
                _fieldList += go.GetComponent<TMP_InputField>().text;
                _fieldList += " ";
            }

            _info.isReadyForData = false;

            return _fieldList;
        }
        return "";
    }
    public void GetBoardData() //Put the board in 
    {
        if (_info.iterationsAmount > 0)
        {
            if (_info.isReadyForData)
            {
                var fieldAsString = BoardData();

                if (fieldAsString != "")
                {
                    Debug.Log("Succes entry");
                    _info.boardData.Add(fieldAsString);
                }
            }
        }
    }
    #endregion

    #region Stats Data
    private void WriteStatsData(string path)
    {
        var spacer = ";";
        TextWriter tw = new StreamWriter(path, false);

        //Iteration | Puzzle | Algorithm | Random Seed | Start Time | End Time | Elapsed Time | Hoeveel ticks? | Hoeveel garbage?
        tw.WriteLine("Iteration" + spacer + "Puzzle" + spacer + "Algorithm" + spacer + "Random Seed" + spacer + "Start Time" + spacer + "End Time" + spacer + "Elapsed Time");
        tw.Close();

        //Open the file again and write in the data
        tw = new StreamWriter(path, true);

        for (int i = 1; i <= _info.iterationsAmount; i++)
        {
            //1 | Sudoku | Dummy V1 | start time | end time | elapsed time
            tw.WriteLine(i + spacer + _info.puzzleName + spacer + _info.algorithmName + " v." + _info.algorithmVersion + spacer + _info.RandomSeed + spacer + 
                _info.startTimes.ToArray().GetValue(i-1) + spacer + _info.endTimes.ToArray().GetValue(i-1) + spacer + _info.elapsedTimes.ToArray().GetValue(i - 1));
        }

        tw.Close();
    }
    public void GetStatsData()
    {
        if (_info.iterationsAmount > 0)
        {
            if (_info.isReadyForData)
            {
                _info.startTimes.Add(_info.startAlgorithmTime);
                _info.endTimes.Add(_info.endAlgorithmTime);

                _info.elapsedTimes.Add(_info.elapsedTime);
            }
        }
    }
    #endregion
    #endregion

    #region Reading Data

    #endregion
}