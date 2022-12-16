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
    bool isBoardSet { get; set; } //True when board is placed (stops loops)
    int lineThiccWidth { get; set; }
    int lineThinWidth { get; set; }



    //Begin event


    //End event


    //CreateBoard function
    void CreateBoard();

    void DeleteBoard();
    void SpawnInputFields();
    void SpawnLines();
}
