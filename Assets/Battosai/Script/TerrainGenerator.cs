using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public int tileCountX = 30;
    public int tileCountZ = 30;
    public float width = 10.0f;
    public float length = 10.0f;
    public float height = 5.0f;
    private System.Random worldSeedGenerator = new System.Random();
    public int worldSeed;

    // Use this for initialization
    void Start ()
    {
        bool displayDebug = false;
        if (worldSeed == 0)
        {
            worldSeed = Random.Range(1, int.MaxValue);
        }
        if (displayDebug)
        {
            print("worldSeed " + worldSeed);
        }
        worldSeedGenerator = new System.Random(worldSeed);

        if (displayDebug)
        {
            print("tileCountX " + tileCountX + " tileCountY " + tileCountZ);
        }
        MeshFilter meshFilter = null;
        Mesh mesh = new Mesh();
        meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = mesh;

        // generate the vertices and uv maps
        int vertexAmount = (tileCountX + 1) * (tileCountZ + 1);
        Vector3[] vertices = new Vector3[vertexAmount];
        Vector2[] uv = new Vector2[vertexAmount];
        int xVertexNumber = 0;
        int zVertexNumber = 0;

        for (int i = 0; i < vertexAmount; i++)
        {
            float xPosition = xVertexNumber * (width / (tileCountX + 1));
            float zPosition = zVertexNumber * (length / (tileCountZ + 1));
            Random rnd = new Random();
            
            float yPosition = Mathf.PerlinNoise((
                (1.0f * worldSeedGenerator.Next(1, int.MaxValue)) / int.MaxValue), 
                ((1.0f * worldSeedGenerator.Next(1, int.MaxValue)) / int.MaxValue)) * height;
            print(yPosition);
            vertices[i] = new Vector3(xPosition, yPosition, zPosition);
            uv[i] = new Vector2(xPosition, zPosition);
            if (displayDebug)
            {
                GameObject debugSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                debugSphere.name = "debugSphere";
                debugSphere.GetComponent<Transform>().position = 
                    new Vector3(
                        transform.position.x + xPosition, 
                        transform.position.y + 0, 
                        transform.position.z + zPosition);
                debugSphere.GetComponent<Transform>().localScale = new Vector3(0.2f, 0.2f, 0.2f);
                Material debugMaterial = new Material(Shader.Find("Standard"));
                debugMaterial.color = new Color(1.0f, 0.7f, 0.0f);
                debugSphere.GetComponent<MeshRenderer>().material = debugMaterial;
            }

            xVertexNumber++;
            if (xVertexNumber > tileCountX)
            {
                xVertexNumber = 0;
                zVertexNumber++;
            }
        }

        mesh.vertices = vertices;
        mesh.uv = uv;

        // setup the triangles from the vertices
        mesh.triangles = trianglesFromTiles(tileCountX, tileCountZ, displayDebug);

        Vector3[] normals = new Vector3[vertexAmount];
        for (int i = 0; i < vertexAmount; i++)
        {
            normals[i] = -Vector3.forward;
        }

        mesh.normals = normals;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    int[] trianglesFromTiles(int tileCountX, int tileCountZ, bool showDebugLogs = false)
    {
        // every tile is built by 2 triangles
        int triangleAmount = tileCountX * tileCountZ * 2;
        if (showDebugLogs)
        {
            print("triangleAmount " + triangleAmount);
        }
        int vertexPointsX = tileCountX + 1;
        int[] trianglePoints = new int[triangleAmount * 3];
        int trianglePointIndex = 0;
        int xCounter = 0;
        int yCounter = 0;

        for (int i = 0; i < triangleAmount; i++)
        {
            if (showDebugLogs)
            {
                print("trianglePointIndex " + trianglePointIndex);
            }

            if (i % 2 == 0)
            {
                trianglePoints[trianglePointIndex] = xCounter + yCounter * vertexPointsX; // x0y0
                trianglePoints[trianglePointIndex + 1] = xCounter + (yCounter + 1) * vertexPointsX; // x0y1
                trianglePoints[trianglePointIndex + 2] = (xCounter + 1) + yCounter * vertexPointsX; // x1y0
                if (showDebugLogs)
                {
                    print("triangleTop " + (xCounter + yCounter * vertexPointsX)
                        + " " + (xCounter + (yCounter + 1) * vertexPointsX)
                        + " " + ((xCounter + 1) + yCounter * vertexPointsX));
                }
            }
            else
            {
                trianglePoints[trianglePointIndex] = (xCounter + 1) + yCounter * vertexPointsX; // x1y0
                trianglePoints[trianglePointIndex + 1] = xCounter + (yCounter + 1) * vertexPointsX; // x0y1
                trianglePoints[trianglePointIndex + 2] = (xCounter + 1) + (yCounter + 1) * vertexPointsX; // x1y1
                if (showDebugLogs)
                {
                    print("triangleBottom " + ((xCounter + 1) + yCounter * vertexPointsX)
                        + " " + (xCounter + (yCounter + 1) * vertexPointsX)
                        + " " + ((xCounter + 1) + (yCounter + 1) * vertexPointsX));
                }

                xCounter++;
                if (xCounter + 1 >= vertexPointsX)
                {
                    yCounter++;
                    xCounter = 0;
                }
            }

            trianglePointIndex = trianglePointIndex + 3;
        }

        return trianglePoints;
    }
}
