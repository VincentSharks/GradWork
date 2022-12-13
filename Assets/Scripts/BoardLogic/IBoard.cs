using UnityEngine;

/// <summary>
/// Holds information about the size of the board (X & Y)
/// Begin & end event
/// Welke puzzle? Welk algorihtme?
/// Algorithme versie ondersteuning?
/// Runs met hetzelfde algorithme met andere data (vermeld deze data in de output)
/// </summary>

public interface IBoard
{
    int SizeX { get; }
    int SizeY { get; }

    //Begin event


    //End event


    //CreateBoard function
    void CreateBoard();
}
