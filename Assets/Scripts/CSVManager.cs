using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEditor.Sprites;
using UnityEngine;

/// <summary>
/// Get all info
/// put it into a CSV file,
/// export/read CSV files
/// </summary>

public class CSVManager : MonoBehaviour
{
    private InfoManager _info;
    private StreamWriter _boardWriter;

    private string _statsFilesName = "StatsFile.csv";
    private string _boardFileName = "BoardFile.csv";

    private string _filePath = "Assets/CVSFiles/";

    private string _fieldList;

    private void OnEnable()
    {
        _info = FindObjectOfType<InfoManager>();

        CheckExistingFile(_boardFileName);
        
    }

    public void WriteDocuments()
    {
        ExportData(false, _statsFilesName);
        //ExportData(true, _boardFileName);
    }

    private void CheckExistingFile(string fileName)
    {
        var filePath = _filePath + fileName;

        if (File.Exists(filePath))
        {
            File.Delete(filePath); //Delete existing file
        }

        //_boardWriter = new StreamWriter(filePath);
    }

    //method for writing both files
    #region Writing Data
    private void ExportData(bool isBoardFile, string fileName)
    {
        var filePath = _filePath + fileName;

        if (isBoardFile)
        {
            _fieldList = "";
            WriteBoardData(filePath);
            //BoardData();
            isBoardFile = false;
        }
        else
        {
            WriteStatsData(filePath);
        }
    }
    private void WriteStatsData(string path)
    {
        var spacer = ";";
        using (StreamWriter file = new StreamWriter(path))
        {
            //Iteration | Puzzle | Algorithm | Random Seed | Start Time | End Time | Elapsed Time | Hoeveel ticks? | Hoeveel garbage?
            file.Write("Iteration" + spacer + "Puzzle" + spacer + "Algorithm" + spacer + "Random Seed" + spacer + "Start Time" + spacer + "End Time" + spacer + "Elapsed Time");
            file.WriteLine(" "); //New Line

            //foreach iteration
            for (int i = 1; i <= _info.iterationsAmount; i++)
            {
                //1 | Sudoku | Dummy V1 | start time | end time | elapsed time
                file.Write(i + spacer + _info.puzzleName + spacer + _info.algorithmName + " v." + _info.algorithmVersion + spacer + _info.RandomSeed + spacer + _info.startAlgorithmTime + spacer + _info.endAlgorithmTime + spacer + _info.elapsedTime);
                file.WriteLine(" "); //New Line
            }
        }
    }
    public void WriteBoardData(string path)
    {
        using (_boardWriter = new StreamWriter(path))
        {
            //Number | board list
            _boardWriter.Write("Number" + ";" + "Board List");
            _boardWriter.WriteLine(" "); //New Line



            for (int i = 1; i <= _info.iterationsAmount; i++)
            {
                if (_info.isReadyForData)
                {
                    var fieldAsString = (BoardData());

                    if (fieldAsString != "")
                    {
                        _boardWriter.WriteLine(i + ";" + fieldAsString);
                        Debug.Log("ENTRY: " + fieldAsString);
                    }
                }  
            }

        }
    }
    private string BoardData()
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
    public void GetBoardData()
    {
        if (_info.iterationsAmount > 0)
        {
            //Create the file and put in the header
            TextWriter tw = new StreamWriter("Assets/CVSFiles/BoardFile.csv", false);
            tw.WriteLine("Number" + ";" + "Board List");
            tw.Close();

            //Open the file again and write in the data
            tw = new StreamWriter("Assets/CVSFiles/BoardFile.csv", true);

            if (_info.isReadyForData)
            {
                var fieldAsString = BoardData();

                if (fieldAsString != "")
                {
                    //tw = new StreamWriter("Assets/CVSFiles/BoardFile" + i + ".csv" , false);

                    Debug.Log("Succes entry");
                    Debug.Log("ENTRY: " + fieldAsString);

                    _info.BoardData.Add(fieldAsString);

                    //tw.WriteLine(fieldAsString);
                    tw.WriteLine($"{fieldAsString}");
                }
            }

            tw.Flush();
            tw.Close();
        }
    }
    public void ExportBoard()
    {
        TextWriter tw = new StreamWriter("Assets/CVSFiles/BoardFileTest.csv", false);
        tw.WriteLine("Number" + ";" + "Board List");
        tw.Close();

        //Open the file again and write in the data
        tw = new StreamWriter("Assets/CVSFiles/BoardFileTest.csv", true);

        foreach (string board in _info.BoardData)
        {
            tw.WriteLine((_info.BoardData.IndexOf(board) + 1) + ";" + $"{board}");
        }

        tw.Close();
    }
    #endregion

    #region Reading Data

    #endregion
}
