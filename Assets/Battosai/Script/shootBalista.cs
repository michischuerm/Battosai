using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootBalista : MonoBehaviour
{
	public GameObject arrowFixpoint;
	public GameObject arrowPrefab;
	public float shotCooldown = 2.0f;
	public float arrowSpeedMS = 40.0f;
	private GameObject arrow;
	private stationaryControll stationaryControll;
	private bool shotReady = true;
	private float lastShotTime = 0.0f;

	// Use this for initialization
	void Start ()
	{
		stationaryControll = GetComponent<stationaryControll>();
		lastShotTime = Time.realtimeSinceStartup;

		if (arrowPrefab != null)
		{
			arrow = (GameObject)Instantiate(arrowPrefab);
			arrow.GetComponent<Rigidbody>().useGravity = false;
		}
		else
		{
			arrow = GameObject.CreatePrimitive(PrimitiveType.Quad);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if ((lastShotTime + shotCooldown) < Time.realtimeSinceStartup)
		{
			shotReady = true;
			resetArrow();
		}

		if (arrowShotCommand() && shotReady)
		{
			Debug.Log("Shot fired");
			shotReady = false;
			this.lastShotTime = Time.realtimeSinceStartup;
			shootArrow();
		}
	}

	private bool arrowShotCommand()
	{
		bool shotCommand = false;
        shotCommand = stationaryControll.triggerIsActive();
		
		return shotCommand;
	}

	private void resetArrow()
	{
		arrow.GetComponent<Rigidbody>().useGravity = false;
		arrow.transform.position = arrowFixpoint.transform.position;
		arrow.transform.parent = stationaryControll.balista.transform;
		arrow.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
	}

	public void shootArrow()
	{
		arrow.transform.parent = null;
		arrow.GetComponent<Rigidbody>().useGravity = true;
		arrow.GetComponent<Rigidbody>().velocity = Vector3.Normalize(arrow.transform.position ) * arrowSpeedMS;
	}
}
