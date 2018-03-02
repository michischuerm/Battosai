using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeMoveTest : MonoBehaviour
{
	private Transform objectTransform;
	public float deltaMetersPerSecond = 2.0f;
	private bool positiveDir = true;

	// Use this for initialization
	void Start ()
	{
		objectTransform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		float zVal = objectTransform.position.z;

		if (positiveDir)
		{
			zVal = zVal + deltaMetersPerSecond * Time.deltaTime;
		}
		else
		{
			zVal = zVal - deltaMetersPerSecond * Time.deltaTime;
		}

		if (zVal > 3.0f)
		{
			zVal = 3.0f;
			positiveDir = !positiveDir;
		}
		else if (zVal < -3.0f)
		{
			zVal = -3.0f;
			positiveDir = !positiveDir;
		}

		Vector3 newVec = new Vector3(objectTransform.position.x, objectTransform.position.y, zVal);
		objectTransform.position = newVec;
	}
}
