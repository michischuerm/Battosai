using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserconnection : MonoBehaviour
{
	public Transform laserPoint1;
	public Transform laserPoint2;
	public float maxDistanceMeters = 100.0f;
	public bool isActive = true;
	public GameObject laserray;
	private Material realMaterial;
	public Material connectionBrokenMaterial;

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
		//Physics.Linecast(laserPoint1.position, laserPoint2.position);
		//Debug.DrawRay(laserPoint1.position, laserPoint2.position, Color.red);
		//Debug.Log(laserPoint2);
		RaycastHit hitInfo;
		/*
		Vector3 centerPos = new Vector3(
				laserPoint1.position.x + laserPoint2.position.x,
				laserPoint1.position.y + laserPoint2.position.y) / 2f;
		*/
		Vector3 centerPos = (laserPoint1.position + laserPoint2.position) / 2.0f;

		/*
		float scaleX = Mathf.Abs(laserPoint1.position.x - laserPoint2.position.x);
		float scaleY = Mathf.Abs(laserPoint1.position.y - laserPoint2.position.y);
		float scaleZ = Mathf.Abs(laserPoint1.position.z - laserPoint2.position.z);
		*/
		float scale = Vector3.Distance(laserPoint1.position, laserPoint2.position);
		//Vector3 distance = laserPoint1.position - laserPoint2.position;
		bool lineHadCollision = Physics.Linecast(laserPoint1.position, laserPoint2.position, out hitInfo);

		if (isActive && !lineHadCollision)
		{
			//Debug.DrawLine(laserPoint1.position, laserPoint2.position);
			//laserray.SetActive(true);
			//laserray.GetComponent<MeshRenderer>().material = realMaterial;
		}
		else
		{
			//Debug.DrawLine(laserPoint1.position, hitInfo.point);
			//laserray.SetActive(false);
			/*
			scaleX = Mathf.Abs(laserPoint1.position.x - hitInfo.point.x);
			scaleY = Mathf.Abs(laserPoint1.position.y - hitInfo.point.y);
			scaleZ = Mathf.Abs(laserPoint1.position.z - hitInfo.point.z);
			*/
			//distance = laserPoint1.position - hitInfo.point;
			scale = Vector3.Distance(laserPoint1.position, hitInfo.point);
			/*
			centerPos = new Vector3(
				laserPoint1.position.x + hitInfo.point.x,
				laserPoint1.position.y + hitInfo.point.y,
				laserPoint1.position.z + hitInfo.point.z) / 2.0f;
				*/
			centerPos = (laserPoint1.position + hitInfo.point) / 2.0f;
			//laserray.GetComponent<MeshRenderer>().material = connectionBrokenMaterial;
		}
		
		laserray.transform.position = centerPos;
		//float scale = Mathf.Sqrt(distance.x * distance.x + distance.y * distance.y + distance.z * distance.z);
		//laserray.transform.localScale = new Vector3(scaleX * laserray.transform.forward.x, scaleY * laserray.transform.forward.y, scaleZ * laserray.transform.forward.z);
		//laserray.transform.localScale = new Vector3(scaleX * laserray.transform.forward.x, 1, scaleZ * laserray.transform.forward.z);
		//laserray.transform.localScale = new Vector3(1, 1, scaleZ * laserray.transform.forward.z);
		laserray.transform.LookAt(laserPoint1);
		//laserray.transform.localScale = new Vector3(0.1f, 0.1f, scale * laserray.transform.forward.z);
		laserray.transform.localScale = new Vector3(0.1f, 0.1f, scale);
		//Debug.Log(laserray.transform.localScale);
		//Debug.Log(scale);
	}
}
