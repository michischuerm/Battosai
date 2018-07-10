using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public int tileCountX = 30;
    public int tileCountZ = 30;
    public float width = 10.0f;
    public float height = 10.0f;

    // Use this for initialization
    void Start ()
    {
        bool displayDebugPoints = true;

        if (displayDebugPoints)
        {
            print("tileCountX " + tileCountX + " tileCountY " + tileCountZ);
        }
        MeshFilter meshFilter = null;
        Mesh mesh = new Mesh();
        meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = mesh;

        // generate the vertices
        int vertexAmount = (tileCountX + 1) * (tileCountZ + 1);
        Vector3[] vertices = new Vector3[vertexAmount];
        Vector2[] uv = new Vector2[vertexAmount];
        int xVertexNumber = 0;
        int zVertexNumber = 0;

        for (int i = 0; i < vertexAmount; i++)
        {
            float xPosition = xVertexNumber * (width / (tileCountX + 1));
            float zPosition = zVertexNumber * (height / (tileCountZ + 1));
            vertices[i] = new Vector3(xPosition, 0, zPosition);
            uv[i] = new Vector2(xPosition, zPosition);
            if (displayDebugPoints)
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
        // every tile is built by 2 triangles
        int triangleAmount = tileCountX * tileCountZ * 2;
        print("triangleAmount " + triangleAmount);
        int vertexPointsX = tileCountX + 1;
        int vertexPointsY = tileCountZ + 1;
        int[] trianglePoints = new int[triangleAmount * 3];
        int counterForTopTwoPoints = 0;
        int counterForBottomTwoPoints = 0;
        int trianglePointIndex = 0;
        for (int i = 0; i < triangleAmount; i++)
        {
            if (displayDebugPoints)
            {
                print("trianglePointIndex " + trianglePointIndex);
            }
            
            if (i % 2 == 0)
            {
                // top 2 points are the number half the triangle count and +1
                trianglePoints[trianglePointIndex] = (i / 2);
                trianglePoints[trianglePointIndex + 1] = vertexPointsX + counterForTopTwoPoints;
                trianglePoints[trianglePointIndex + 2] = (i / 2) + 1;
                if (displayDebugPoints)
                {
                    print("triangleTop " + (i / 2)
                        + " " + (vertexPointsX + counterForTopTwoPoints)
                        + " " + ((i / 2) + 1));
                }
                counterForTopTwoPoints++;
            }
            else
            {
                trianglePoints[trianglePointIndex] = counterForBottomTwoPoints + 1;
                trianglePoints[trianglePointIndex + 1] = vertexPointsX + counterForBottomTwoPoints;
                trianglePoints[trianglePointIndex + 2] = vertexPointsX + counterForBottomTwoPoints + 1;
                if (displayDebugPoints)
                {
                    print("triangleBottom " + (counterForBottomTwoPoints + 1)
                        + " " + (vertexPointsX + counterForBottomTwoPoints)
                        + " " + (vertexPointsX + counterForBottomTwoPoints + 1));
                }
                counterForBottomTwoPoints++;
            }
            
            trianglePointIndex = trianglePointIndex + 3;
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
