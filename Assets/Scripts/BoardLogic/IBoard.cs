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
    Vector2 Dimension { get; set; } //Dimensions
    bool IsBoardSet { get; set; } //True when board is placed (stops loops)
    int LineThiccWidth { get; set; }
    int LineThinWidth { get; set; }


    //Create Board function
    void CompareValues();
    void CreateBoard();
    void DeleteBoard();

    void SpawnInputFields();
    void SpawnLines();

    //Fill Board function

    //Begin Event
    //End Event
}
