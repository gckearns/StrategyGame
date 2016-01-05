using UnityEngine;
using System.Collections;

public class WorldController : MonoBehaviour {

    public TileMap terrainMap;
	public GameManager gameManager;

	private TileMap currentMap;


	// Use this for initialization
	void Start () {
		gameManager = (GameManager) FindObjectOfType<GameManager>();
        NewMap ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void NewMap () {
        currentMap = (TileMap)Instantiate (terrainMap);
        currentMap.transform.SetParent (this.transform);
    }
}
