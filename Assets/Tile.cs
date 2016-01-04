using UnityEngine;
using System.Collections;
using TileGameResources;

public class Tile {

	public int X { get; protected set; }
	public int Z { get; protected set; }
	public TileMap Map { get; protected set; }
	public TileType TileType {get; set; }

	public Tile(int x, int z){
		X = x;
		Z = z;
		TileType = TileType.Granite;
	}

	public Tile(int x, int z, TileMap map){
		X = x;
		Z = z;
		Map = map;
		TileType = TileType.Granite;
	}

	public void SetType(TileType type){
		this.TileType = type;
		Map.UpdateTexture(new Vector2(X,Z));
	}
}
