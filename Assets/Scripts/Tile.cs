using UnityEngine;
using System.Collections;
using StrategyResources;

public class Tile {

    public TileMap map { get; set; }
    public Vector2 coords { get; set; }
    public TileTerrainType terrainType { get; set; }
    public TileState state { get; set; }
  
    public Tile (TileMap map, Vector2 coords) {
        this.map = map;
        this.coords = coords;
        this.terrainType = TileTerrainType.Default;
        this.state = TileState.Default;
    }
}
