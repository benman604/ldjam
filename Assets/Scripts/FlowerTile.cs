using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "NewFlowerTile", menuName = "Tiles/FlowerTile")]
public class FlowerTile : Tile
{
    public void OnPlayerEnter(Player player)
    {
        // Logic for when the player interacts with the flower tile
        player.IncreaseFlowerCount();
        // Optionally, you can set this tile to null to remove it
        // or replace it with another tile.
    }
}
