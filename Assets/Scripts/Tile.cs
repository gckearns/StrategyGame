using UnityEngine;
using System.Collections;
using StrategyGame;

public class Tile {

    public TileMap map { get; set; }
    public Vector2 mapCoords { get; set; }
    public Vector3 worldCoords { get; set; }
    public TileTerrainType terrainType { get; set; }
  
    public Tile (TileMap map, Vector2 mapCoords, Vector3 worldCoords) {
        this.map = map;
        this.mapCoords = mapCoords;
        this.worldCoords = worldCoords;
        this.terrainType = TileTerrainType.Default;
    }

    public override string ToString ()
    {
        return string.Format ("[Tile: {0}, WorldCoords={1}, Type={2}]", this.mapCoords.ToString (), this.worldCoords.ToString (), terrainType);
    }
}
