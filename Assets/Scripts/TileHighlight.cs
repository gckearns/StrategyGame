using UnityEngine;
using System.Collections;

public class TileHighlight : MonoBehaviour {
    
    public float tileSize { set; get; }

    // Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public TileHighlight InitializeHighlight(float tileSize) {
        this.tileSize = tileSize;
        GenerateHighlightTile ();
        return this;
    }

    void GenerateHighlightTile () {
        GenerateMesh ();
    }

    void GenerateMesh() {
        int numQuadsX = 1;
        int numQuadsZ = 1;
        int numVertsX = 2;
        int numVertsZ = 2;
        int numVerts = numVertsX * numVertsZ;
        int numTris = 2;

        Vector3[] verticies = new Vector3[numVerts];
        Vector3[] normals = new Vector3[numVerts];
        Vector2[] uv = new Vector2[numVerts];
        int[] triangles = new int[numTris * 3];

        int i = 0;
        for (int z = 0; z < numVertsZ; z++) {
            for (int x = 0; x < numVertsX; x++) {
                verticies [i].Set (x * tileSize, 0, z * tileSize);
                normals [i] = Vector3.up;
                uv [i].Set ((float) x / (float) numQuadsX, (float) z / (float) numQuadsZ);
                i++;
            }
        }
        i = 0;
        for (int z = 0; z < numQuadsZ; z++) {
            for (int x = 0; x < numQuadsX; x++) {
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
        mesh.name = "TileHighlight";

        MeshCollider meshCollider = (MeshCollider) GetComponent<MeshCollider>();
        MeshFilter meshFilter = (MeshFilter) GetComponent<MeshFilter>();

        meshFilter.sharedMesh = mesh;
        meshCollider.sharedMesh = mesh;

    }

    public void Destroy(){
        Destroy (this);
    }
}
