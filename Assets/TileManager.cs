using UnityEngine;
using System.Collections;

public class TileManager : MonoBehaviour {

	public TileMap tileMapPrefab; //prefab
	private TileMap currentMap;
	public int numTilesX;
	public int numTilesZ;
	public float tileSize;
	public int tileResolution;
	public Texture2D tileTex;

	// Use this for initialization
	void Start () {
		generateNewMap();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void generateNewMap(){
//		GameObject newMap = Instantiate <GameObject>(GameObject.Find("TileMap"));
//		tileMap = new TileMap(numTilesX,numTilesZ,tileSize,tileResolution,tileTex,newMap);
//		tileMap.GenerateMap();
		currentMap = Instantiate (tileMapPrefab).Initialize(numTilesX,numTilesZ,tileSize,tileResolution,tileTex);

	}
}
