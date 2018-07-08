using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public int tileCountX = 30;
    public int tileCountY = 30;
    public float width = 10.0f;
    public float height = 10.0f;


    // Use this for initialization
    void Start ()
    {
        MeshFilter meshFilter = null;
        Mesh mesh = new Mesh();
        meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = mesh;

        // generate the vertices
        int vertexAmount = (tileCountX + tileCountY + 2) * (tileCountX + tileCountY + 2);
        Vector3[] vertices = new Vector3[vertexAmount];
        Vector2[] uv = new Vector2[vertexAmount];
        int xVertexNumber = 0;
        int zVertexNumber = 0;

        for (int i = 0; i < vertexAmount; i++)
        {
            float xPosition = xVertexNumber * (width / (tileCountX + 1));
            float zPosition = zVertexNumber * (height / (tileCountY + 1));
            vertices[i] = new Vector3(xPosition, 0, zPosition);
            uv[i] = new Vector2(xPosition, zPosition);

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
        // every tile is built by 2 triangles
        int triangleAmount = tileCountX * tileCountY * 2;
        int[] trianglePoints = new int[triangleAmount * 3];
        int counterForTopTwoPoints = 0;
        int counterForBottomTwoPoints = 0;
        for (int i = 0; i < triangleAmount; i++)
        {
            if (i % 2 == 0)
            {
                // top 2 points are the number half the triangle count and +1
                trianglePoints[i] = (i / 2);
                trianglePoints[i + 1] = (i / 2) + 1;
                trianglePoints[i + 2] = tileCountX + 1 + counterForTopTwoPoints;
                counterForTopTwoPoints++;
            }
            else
            {
                trianglePoints[i] = i + counterForBottomTwoPoints;
                trianglePoints[i + 1] = tileCountX + counterForBottomTwoPoints;
                trianglePoints[i + 2] = tileCountX + counterForBottomTwoPoints + 1;
                counterForBottomTwoPoints++;
            }
        }

        mesh.triangles = trianglePoints;

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
}
