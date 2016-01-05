using UnityEngine;
using System.Collections;
using StrategyResources;

public class TileMap : MonoBehaviour {

    private int playAreaX = 10;
    private int playAreaZ = 10;
    private int numTilesX;
    private int numTilesZ;
    private float tileSize = 1.0f;
	private Tile[,] tiles;
    private Color[] colorsDefault;
    private Color[] colorsCrater;
    private Color[] colorsBorder;
    private Tile selectedTile;
    private bool hasSelectedTile = false;
    private TileHighlight currentTileHighlight;
	private WorldController worldController;
	private GameManager gameManager;
	private UICanvas uiCanvas;

    public int tileResolution = 32;
    public Texture2D textureAtlas;
    public int numTextureTiles = 3;
    public TileHighlight tileHighlight;


	// Use this for initialization
	void Start () {
        InitializeTileHighlights ();
		worldController = (WorldController) FindObjectOfType<WorldController>();
		gameManager = worldController.gameManager;
		uiCanvas = gameManager.uiCanvas;
        GenerateMap ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown() {
        Tile clickedTile = PointToTile (GetClickPoint ());
        if (clickedTile.terrainType != TileTerrainType.Border) {
            SelectTile (clickedTile);
        }
    }

    GameObject GetClickObject () {
        Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast (ray, out hit);
        return hit.transform.gameObject;
    }


    Vector3 GetClickPoint () {
        Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast (ray, out hit);
        return hit.point;
    }

    Tile PointToTile (Vector3 point) {
        return tiles [(int) point.x, (int) point.z];
    }

    void SelectTile (Tile tile) {
        ClearSelectedTile ();
        selectedTile = tile;
        tile.state = TileState.Selected;
        hasSelectedTile = true;
        TileHighlight (tile);
		uiCanvas.ShowContextMenu(tile);
		uiCanvas.gameObject.SetActive(true);
    }

    void ClearSelectedTile (){
        if (hasSelectedTile) {
            ClearTileHighlight ();
        }
        hasSelectedTile = false;
		uiCanvas.gameObject.SetActive(false);
    }

    void TileHighlight (Tile tile) {
        currentTileHighlight = (TileHighlight) Instantiate (tileHighlight);
        currentTileHighlight.transform.SetParent (transform);
        currentTileHighlight.transform.position = new Vector3 (tile.coords.x, 0.01f, tile.coords.y);
//		GameObject building = GameObject.Find("Building");
//		building.transform.position = new Vector3 (tile.coords.x + 0.5f, 0.5f, tile.coords.y + 0.5f);
    }

    void ClearTileHighlight (){
		Destroy (currentTileHighlight.gameObject);
    }

    void InitializeTileHighlights(){
        tileHighlight.InitializeHighlight (tileSize);
        Texture2D highlightTexture = new Texture2D (tileResolution, tileResolution);
        Color[] highlightColors = textureAtlas.GetPixels(3 * tileResolution, 0 ,tileResolution,tileResolution);
        highlightTexture.SetPixels(highlightColors);

        MeshRenderer meshRenderer = (MeshRenderer) tileHighlight.GetComponent<MeshRenderer> ();
        meshRenderer.sharedMaterials[0].mainTexture = highlightTexture;
        highlightTexture.Apply ();

    }

	void GenerateMap () {
		GenerateMesh ();
        GenerateTiles ();
        GenerateTexture ();
	}

	void GenerateMesh(){
        numTilesX = (playAreaX * 2) - 1;
        numTilesZ = (playAreaZ * 2) - 1;

        int numVertsX = numTilesX + 1;
        int numVertsZ = numTilesZ + 1;
		int numVerts = numVertsX * numVertsZ;
		int numTris = (numTilesX * numTilesZ) * 2;

		Vector3[] verticies = new Vector3[numVerts];
		Vector3[] normals = new Vector3[numVerts];
        Vector2[] uv = new Vector2[numVerts];
		int[] triangles = new int[numTris * 3];

		int i = 0;
		for (int z = 0; z < numVertsZ; z++) {
			for (int x = 0; x < numVertsX; x++) {
                verticies [i].Set (x * tileSize, 0, z * tileSize);
				normals [i] = Vector3.up;
                uv [i].Set ((float) x / (float) numTilesX, (float) z / (float) numTilesZ);
				i++;
			}
		}
        i = 0;
        for (int z = 0; z < numTilesZ; z++) {
            for (int x = 0; x < numTilesX; x++) {
                triangles [i + 0] = x + (z * numVertsX);
                triangles [i + 1] = x + (z * numVertsX) + numVertsX + 1 ;
                triangles [i + 2] = x + (z * numVertsX) + 1;
                triangles [i + 3] = x + (z * numVertsX);
                triangles [i + 4] = x + (z * numVertsX) + numVertsX;
                triangles [i + 5] = x + (z * numVertsX) + numVertsX + 1;
                i += 6;
            }
        }

		Mesh mesh = new Mesh ();
		mesh.vertices = verticies;
		mesh.normals = normals;
        mesh.triangles = triangles;
        mesh.uv = uv;
        mesh.name = "TileMap";

        MeshCollider meshCollider = (MeshCollider) GetComponent<MeshCollider>();
        MeshFilter meshFilter = (MeshFilter) GetComponent<MeshFilter>();

        meshFilter.sharedMesh = mesh;
        meshCollider.sharedMesh = mesh;
	}

    void GenerateTiles () {
        tiles = new Tile[numTilesX,numTilesZ];
        for (int z = 0; z < numTilesZ; z++) {
            for (int x = 0; x < numTilesX; x++) {
                tiles [x, z] = new Tile (this, new Vector2 (x, z));
            }
        }
        for (int z = 0; z < playAreaZ; z++) {
            for (int x = 0; x < playAreaX; x++) {
                if ((x + z) < (playAreaX - 1)) {
                    tiles [x, z].terrainType = TileTerrainType.Border; // Bottom left
                    tiles [(numTilesX - 1) - x, z].terrainType = TileTerrainType.Border; // Bottom right
                    tiles [x, (numTilesZ - 1) - z].terrainType = TileTerrainType.Border; // Top left
                    tiles [(numTilesX - 1) - x, (numTilesZ - 1) - z].terrainType = TileTerrainType.Border; // Top right
                }
            }        
        }
        int addNumCraters = 2;
        while (addNumCraters > 0) {
            Vector2 randCoords = new Vector2 (Random.Range (0, numTilesX), Random.Range (0, numTilesZ));
            Tile selectedTile = tiles [(int) randCoords.x, (int) randCoords.y];
            if (selectedTile.terrainType == TileTerrainType.Default) {
                selectedTile.terrainType = TileTerrainType.Crater;
                addNumCraters --;
            }
        }
    }

    void GenerateTexture () {
        this.colorsDefault = textureAtlas.GetPixels(0 * tileResolution, 0, tileResolution,tileResolution);
        this.colorsCrater = textureAtlas.GetPixels(1 * tileResolution, 0 ,tileResolution,tileResolution);
        this.colorsBorder = textureAtlas.GetPixels(2 * tileResolution, 0 ,tileResolution,tileResolution);

        Texture2D mapTexture = new Texture2D(numTilesX * tileResolution, numTilesZ * tileResolution);
        for (int z = 0; z < numTilesZ; z++) {
            for (int x = 0; x < numTilesX; x++) {
                mapTexture.SetPixels (x * tileResolution, z * tileResolution, tileResolution, tileResolution, getTileColors (tiles [x, z]));
            }
        }
        MeshRenderer meshRenderer = (MeshRenderer)GetComponent<MeshRenderer> ();
        meshRenderer.sharedMaterials[0].mainTexture = mapTexture;
        mapTexture.Apply ();
    }

    Color[] getTileColors(Tile tile) {
        switch (tile.terrainType) {
        case TileTerrainType.Border:
            return this.colorsBorder;
        case TileTerrainType.Crater:
            return this.colorsCrater;
        default:
            return this.colorsDefault;
        }
    }
}
