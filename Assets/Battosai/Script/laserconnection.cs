using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserconnection : MonoBehaviour
{
	public Transform laserPoint1;
	public Transform laserPoint2;
	// if maxDistanceMeters is 0 the maxDistance is disabled
	public float maxDistanceMeters = 100.0f;
	public bool isActive = true;
	public GameObject laserray;
	private Material realMaterial;
	public Material connectionBrokenMaterial;
	private bool isActiveDistanceRelated = true;

	// Use this for initialization
	void Start ()
	{
		//laserray = Instantiate(Resources.Load("Laserline", typeof(GameObject))) as GameObject;
		//laserray = (GameObject)Instantiate(Resources.Load("Laserline"));
		laserray = (GameObject)Instantiate(laserray);
		realMaterial = laserray.GetComponent<MeshRenderer>().material;
		//Debug.Log("realMaterial" + realMaterial);
		//connectionBrokenMaterial = Resources.Load("shaderMaterial_3", typeof(Material)) as Material;
		//Debug.Log("connection Mat: " + connectionBrokenMaterial);
	}

	// Update is called once per frame
	void Update ()
	{
		RaycastHit hitInfo;
		Vector3 centerPos = (laserPoint1.position + laserPoint2.position) / 2.0f;

		float scale = Vector3.Distance(laserPoint1.position, laserPoint2.position);
		bool lineHadCollision = Physics.Linecast(laserPoint1.position, laserPoint2.position, out hitInfo);

		if (maxDistanceMeters == 0 || scale < maxDistanceMeters)
		{
			isActiveDistanceRelated = true;
		}
		else
		{
			isActiveDistanceRelated = false;
		}

		if (isActive && isActiveDistanceRelated)
		{
			laserray.SetActive(true);
		}
		else
		{
			laserray.SetActive(false);
		}

		if (!lineHadCollision)
		{
			//Debug.DrawLine(laserPoint1.position, laserPoint2.position);
			laserray.GetComponent<MeshRenderer>().material = realMaterial;
		}
		else
		{
			//Debug.DrawLine(laserPoint1.position, hitInfo.point);
			scale = Vector3.Distance(laserPoint1.position, hitInfo.point);
			centerPos = (laserPoint1.position + hitInfo.point) / 2.0f;
			laserray.GetComponent<MeshRenderer>().material = connectionBrokenMaterial;
		}
		
		laserray.transform.position = centerPos;
		laserray.transform.LookAt(laserPoint1);
		laserray.transform.localScale = new Vector3(0.1f, 0.1f, scale);
	}
}
