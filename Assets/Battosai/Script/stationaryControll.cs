using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stationaryControll : MonoBehaviour
{
	public Transform stationaryFixedHinge;
	private GameObject controllerInteractionBox;
	private List<SteamVR_TrackedObject> trackedObjs;
	private int controllersInBox = 0;

	// Use this for initialization
	void Start ()
	{
		controllerInteractionBox = gameObject;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (trackedObjs != null && trackedObjs.Count >= 2)
		{
			trackedObjs.ForEach(delegate (SteamVR_TrackedObject obj)
			{
				Debug.Log(obj.name);
			});
			//trackedObjs.ForEach(checkForController);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		Debug.Log("Trigger Enter");
		addTrackedObjs(other);
	}

	private void OnTriggerStay(Collider other)
	{
		// updateTrackedObjs(other);
	}

	private void OnTriggerExit(Collider other)
	{
		Debug.Log("Trigger Exit");
		removeTrackedObjs(other);
	}

	private void addTrackedObjs(Collider collidingObj)
	{
		Debug.Log(collidingObj.name);
		Debug.Log(collidingObj.GetComponent<SteamVR_TrackedObject>());
		/*
		if (collidingObj.GetComponent<SteamVR_TrackedObject>() != null)
		{
			trackedObjs.Add(collidingObj.GetComponent<SteamVR_TrackedObject>());
		}
		*/
	}

	private void removeTrackedObjs(Collider collidingObj)
	{
		/*
		if (collidingObj.GetComponent<SteamVR_TrackedObject>() != null)
		{
			trackedObjs.Remove(collidingObj.GetComponent<SteamVR_TrackedObject>());
		}
		*/
	}

	private void checkForController(SteamVR_TrackedObject trackedObj)
	{
		if (isController(trackedObj))
		{
			controllersInBox++;
		}
	}

	private bool isController (SteamVR_TrackedObject trackedObj)
	{
		if (SteamVR_Controller.Input((int)trackedObj.index) == null)
		{
			Debug.Log("no index");
		}

		Debug.Log("tracked obj as controller: " + trackedObj);
		return true;
	}
}
