using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootBalista : MonoBehaviour
{
	public GameObject arrowFixpoint;
	public GameObject arrowPrefab;
	private GameObject arrow;
	private stationaryControll stationaryControll;
	private bool shotReady = true;

	// Use this for initialization
	void Start ()
	{
		stationaryControll = GetComponent<stationaryControll>();

		if (arrowPrefab != null)
		{
			arrow = (GameObject)Instantiate(arrowPrefab);
		}
		else
		{
			arrow = GameObject.CreatePrimitive(PrimitiveType.Quad);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (arrowShotCommand() && shotReady)
		{
			Debug.Log("Shot fired");
			shotReady = false;
		}
	}

	private bool arrowShotCommand()
	{
		bool shotCommand = false;
        shotCommand = stationaryControll.triggerIsActive();
		
		return shotCommand;
	}
}
