using System.Collections.Generic;
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
    public Vector2 dimensions { get; } //Dimensions
    public bool isBoardSet { get; } //True when board is placed (stops loops)
    public int lineThiccWidth { get; }
    public int lineThinWidth { get; }

    public List<GameObject> inputFields { get; }

    //Create Board function
    public void CompareValues();
    public void CreateBoard();
    public void DeleteBoard();

    public void SpawnInputFields();
    public void SpawnLines();

    //Fill Board function
}
