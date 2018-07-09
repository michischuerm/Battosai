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
        bool displayDebugPoints = true;
        print("tileCountX " + tileCountX + " tileCountY " + tileCountY);
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
        int triangleAmount = tileCountX * tileCountY * 2;
        int vertexPointsX = tileCountX + 1;
        int vertexPointsY = tileCountY + 1;
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
                trianglePoints[i + 2] = vertexPointsX + counterForTopTwoPoints;
                print("triangleTop " + (i / 2) + " " + ((i / 2) + 1) + " " + (vertexPointsX + counterForTopTwoPoints));
                counterForTopTwoPoints++;
            }
            else
            {
                trianglePoints[i] = counterForBottomTwoPoints + 1;
                trianglePoints[i + 1] = vertexPointsX + counterForBottomTwoPoints + 1;
                trianglePoints[i + 2] = vertexPointsX + counterForBottomTwoPoints;
                print("triangleBottom " + (counterForBottomTwoPoints + 1) + " " + (vertexPointsX + counterForBottomTwoPoints + 1) + " " + (vertexPointsX + counterForBottomTwoPoints));
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
