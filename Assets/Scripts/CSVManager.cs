using System.Collections;
using System.Collections.Generic;
using System.IO;
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

    private string _filePath = "Assets/";



    private void OnEnable()
    {
        _info = FindObjectOfType<InfoManager>();

        ExportData(_statsFilesName);
    }

    //method for writing both files
    //method for reading both files



    private void ExportData(string fileName)
    {
        var filePath = "Assets/" + fileName;

        if (File.Exists(filePath))
        {
            File.Delete(filePath); //Delete existing file
        }

        WriteData(filePath);        
    }

    private void WriteData(string path)
    {
        var spacer = ",";
        using (StreamWriter file = new StreamWriter(path))
        {
            //Iteration | Puzzle | Algorithm | Random Seed | Start Time | End Time | Elapsed Time | Hoeveel ticks? | Hoeveel garbage?
            file.Write("Iteration" + spacer + "Puzzle" + spacer + "Algorithm" + spacer + "Random Seed" + spacer + "Start Time" + spacer + "End Time" + spacer + "Elapsed Time");
            file.WriteLine(" "); //New Line

            //foreach iteration

            //1 | Sudoku | Dummy V1 | start time | end time | elapsed time
            file.Write(0 + spacer + _info.puzzleName + spacer + _info.algorithmName + " " + _info.algorithmVersion + spacer + _info.RandomSeed + spacer + _info.startAlgorithmTime + spacer + _info.endAlgorithmTime + spacer + _info.elapsedTime);
            file.WriteLine(" "); //New Line

            

            file.Write("test 4");
        }
    }
}
