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
    Vector2 dimension { get; set; } //Dimensions
    bool isBoardSet { get; set; } //True when board is placed (stops loops)
    int lineThiccWidth { get; set; }
    int lineThinWidth { get; set; }


    //Create Board function
    void CreateBoard();
    void DeleteBoard();

    void SpawnInputFields();
    void SpawnLines();

    //Fill Board function

    //Begin Event
    //End Event
}
