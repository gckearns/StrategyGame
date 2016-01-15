using UnityEngine;
using System.Collections;
using StrategyGame;

public class TileHighlight : MonoBehaviour {
    
    private float tileSide = StrategyGame.GameResources.TileSide;

    // Use this for initialization
	void Start () {
        GenerateMesh ();
        OffsetTransform ();
	}

    void GenerateMesh() {
        int numMeshTilesX = 1;
        int numMeshTilesZ = 1;
        int numVertsX = (numMeshTilesX * 2);
        int numVertsZ = (numMeshTilesZ * 2);
        int numVerts = numVertsX * numVertsZ;
        int numTris = (numMeshTilesX * numMeshTilesZ) * 2;
        float dimX = numMeshTilesX * tileSide;
        float dimZ = numMeshTilesZ * tileSide;
        float halfX = dimX / 2;
        float halfZ = dimZ / 2;

        Vector3[] verticies = new Vector3[numVerts];
        Vector3[] normals = new Vector3[numVerts];
        Vector2[] uv = new Vector2[numVerts];
        int[] triangles = new int[numTris * 3];

        int i = 0;
        for (int z = 0; z < numVertsZ; z++) {
            for (int x = 0; x < numVertsX; x++) {
                verticies [i].Set ((x * tileSide) - halfX, 0, (z * tileSide) - halfZ);
                normals [i] = Vector3.up;
                uv [i].Set ((float) x / (float) numMeshTilesX, (float) z / (float) numMeshTilesZ);
                i++;
            }
        }
        i = 0;
        for (int z = 0; z < numMeshTilesZ; z++) {
            for (int x = 0; x < numMeshTilesX; x++) {
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

    void OffsetTransform (){
        
    }
}
