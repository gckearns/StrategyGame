using UnityEngine;
using System.Collections;
using TileGameResources;

public class TileMap : MonoBehaviour {

	private int numTilesX;
	private int numTilesZ;
	private float tileSize;
	private int tileResolution;
	private Texture2D tileTextures;
	private Texture2D texMap;
	private Vector2 hoverTile = new Vector2(-1,-1);

//	public int numTilesX {get; protected set;}
//	public int numTilesZ {get; protected set;}
//	public float tileSize {get; protected set;}
//	public int tileResolution {get; protected set;}
//	public Texture2D texMap {get; protected set;}
//	public GameObject mapObject {get; protected set;}

	private Tile[,] tiles;

	public TileMap Initialize(int numTilesX, int numTilesZ, float tileSize, int tileResolution, Texture2D tileTex){
		this.numTilesX = numTilesX;
		this.numTilesZ = numTilesZ;
		this.tileSize = tileSize;
		this.tileResolution = tileResolution;
		this.tileTextures = tileTex;
//		this.mapObject = mapObject;
		this.tiles = new Tile[numTilesX,numTilesZ];
		GenerateMap();
		return this;
	}

	public void GenerateMap(){
		BuildMesh();
		CreateTiles();
		GenerateTexture();
	}

	void OnMouseOver() {
		print("mouseover");
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		MeshCollider meshCollider = GetComponent<MeshCollider>();
		RaycastHit hit;
		meshCollider.Raycast(ray, out hit, Mathf.Infinity);
		Vector3 mouseOverPoint = hit.point;
		float xTile = Mathf.Floor(mouseOverPoint.x);
		float zTile = Mathf.Floor(mouseOverPoint.z);
		Vector2 checkTile = new Vector2(xTile,zTile);
		if((hoverTile.x != xTile) || (hoverTile.y != zTile)){
			print("tile changed");
			if((hoverTile.x != -1) && (hoverTile.y != -1)){
				print("didn't exit");
				ClearHighlight(hoverTile);
			}
			hoverTile = checkTile;
			TileHighlight(checkTile);
		}
	}

	void OnMouseExit() {
		print("mouse exit");
		ClearHighlight(hoverTile);
		hoverTile.Set(-1f,-1f);
	}

	void Update(){
	}

	private void TileHighlight(Vector2 tile) {
		tiles[(int)tile.x,(int)tile.y].SetType(TileType.Leaves);
	}

	private void ClearHighlight(Vector2 tile) {
		tiles[(int)tile.x,(int)tile.y].SetType(TileType.Granite);
	}

	private void BuildMesh () {
		// Generate mesh data
		int numTris = (numTilesX * numTilesZ) * 6;
		int numVertsX = numTilesX + 1;
		int numVertsZ = numTilesZ + 1;
		int numVerts = numVertsX * numVertsZ;
		Vector3[] verticies = new Vector3[numVerts];
		Vector3[] normals = new Vector3[numVerts];
		Vector2[] uv = new Vector2[numVerts];
		int[] triangles = new int[numTris];

		int i = 0;
		for(int z = 0; z < numVertsZ; z++){
			for(int x = 0; x < numVertsX; x++){
				verticies[i].x = x * tileSize;
				verticies[i].y = 0;
				verticies[i].z = z * tileSize;
				uv[i].x = (float)x * (1 / (float)numTilesX);
				uv[i].y = (float)z * (1 / (float)numTilesZ);
				normals[i] = Vector3.up;
				i++;
			}
		}

		// Generate triangles
		i=0;
		for(int z = 0; z < numTilesZ; z++){
			for(int x = 0; x < numTilesX; x++){
				triangles[i+0] = x + ( numVertsX * z);
				triangles[i+1] = x + ( numVertsX * (z + 1) ) + 1;
				triangles[i+2] = x + ( numVertsX * z) + 1;
				triangles[i+3] = x + ( numVertsX * z);
				triangles[i+4] = x + ( numVertsX * (z + 1) );
				triangles[i+5] = x + ( numVertsX * (z + 1) ) + 1;
				i+=6;
			}
		}

		// Create new mesh and populate w/ data
		Mesh mesh = new Mesh();
		mesh.name = "TileMap";
		mesh.vertices = verticies;
		mesh.triangles = triangles;
		mesh.normals = normals;
		mesh.uv = uv;

		MeshFilter meshFilter = GetComponent<MeshFilter>();
		MeshCollider meshCollider = GetComponent<MeshCollider>();

		meshFilter.mesh = mesh;
		meshCollider.sharedMesh = mesh;
	}

	private void CreateTiles(){
		for(int z = 0; z < numTilesZ; z++){
			for(int x = 0; x < numTilesX; x++){
				tiles[x,z] = new Tile(x,z,this);
//				tiles[x,z].TileType = randomTileType();
			}
		}
	}

	private Color[] tileColor(TileType tileType){
		Color[] water = tileTextures.GetPixels(0,32,tileResolution,tileResolution);
		Color[] gravel = tileTextures.GetPixels(32,0,tileResolution,tileResolution);
		Color[] leaves = tileTextures.GetPixels(0,0,tileResolution,tileResolution);
		Color[] granite = tileTextures.GetPixels(32,32,tileResolution,tileResolution);

		switch(tileType){
		case TileType.Water:
			return water;
		case TileType.Gravel:
			return gravel;
		case TileType.Leaves:
			return leaves;
		default:
			return granite;
		}
	}

	private TileType randomTileType(){
		int type = Random.Range(0,4);

		switch(type){
		case 0:
			return TileType.Water;
		case 1:
			return TileType.Gravel;
		case 2:
			return TileType.Leaves;
		default:
			return TileType.Granite;
		}
	}

	private void GenerateTexture(){
		int tex_x = tileResolution * numTilesX;
		int tex_z = tileResolution * numTilesZ;
		int widthRes = tileTextures.width / numTilesX;
		int heightRes = tileTextures.height / numTilesZ;

		Texture2D tex = new Texture2D(tex_x,tex_z);
		for(int z = 0; z < numTilesZ; z++){
			for(int x = 0; x < numTilesX; x++){
				tex.SetPixels(x * tileResolution,z * tileResolution,tileResolution,tileResolution,tileColor(tiles[x,z].TileType));
			}
		}
		tex.filterMode = FilterMode.Point;
		tex.wrapMode = TextureWrapMode.Clamp;
		tex.Apply();
		this.texMap = tex;

		MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
		meshRenderer.sharedMaterials[0].mainTexture = tex;
	}


	// redrawing the whole texture is slow.  might be faster to make each tile a game object
	public void UpdateTexture(Vector2 tile){
//		texMap.SetPixels((int)tile.x * tileResolution,(int)tile.y * tileResolution,tileResolution,tileResolution,tileColor(tiles[(int)tile.x,(int)tile.y].TileType));
//		texMap.Apply();
	}
}
