using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class relativeMovementTest : MonoBehaviour {

	private Transform objectTransform;
	public float deltaMetersPerSecond = 2.0f;
	private bool positiveDir = false;

	// Use this for initialization
	void Start()
	{
		objectTransform = GetComponent<Transform>();
	}

	// Update is called once per frame
	void Update()
	{
		Vector3 newPos = objectTransform.position;

		if (positiveDir)
		{
			newPos = newPos + objectTransform.up * deltaMetersPerSecond * Time.deltaTime;
		}
		else
		{
			newPos = newPos - objectTransform.up * deltaMetersPerSecond * Time.deltaTime;
		}

		if (Vector3.Magnitude(newPos) > 100.0f)
		{
			positiveDir = !positiveDir;
		}

		objectTransform.position = newPos;
	}
}
