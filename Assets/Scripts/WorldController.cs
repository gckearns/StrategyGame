using UnityEngine;
using System.Collections;

public class WorldController : MonoBehaviour {

    public TileMap terrainMap;
    public GameObject tileHighlight;
    public TileManager tileManager;
    public BuildingType[] buildingTypes;

	private TileMap currentMap;
    private static WorldController worldController;

    public static WorldController Instance () {
        if (!worldController) {
            worldController = FindObjectOfType(typeof (WorldController)) as WorldController;
            if (!worldController)
                Debug.LogError ("There needs to be one active WorldController script on a GameObject in your scene.");
        }

        return worldController;
    }

	// Use this for initialization
	void Start () {
        NewMap ();
	}

    void NewMap () {
        currentMap = (TileMap)Instantiate (terrainMap);
        currentMap.transform.SetParent (this.transform);
        tileManager = tileManager.Initialize (this, currentMap, currentMap.tiles);
        ClearTileHighlight ();
    }

    public void TileHighlight (Tile tile) {
        tileHighlight.transform.position = new Vector3 (tile.worldCoords.x, 0.01f, tile.worldCoords.z);
        tileHighlight.SetActive (true);
    }

    public void ClearTileHighlight (){
        tileHighlight.SetActive (false);
    }
}
