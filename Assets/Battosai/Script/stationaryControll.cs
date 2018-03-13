using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stationaryControll : MonoBehaviour
{
	public Transform stationaryFixedHinge;
	private GameObject controllerInteractionBox;
	public GameObject balista;
	private List<SteamVR_TrackedObject> trackedObjs;
	private List<SteamVR_TrackedObject> controllers;
	private List<GameObject> debugSpheres;
	private GameObject debugSphere;
	private GameObject debugRect;
	private int controllersInBox = 0;

	// Use this for initialization
	void Start()
	{
		controllerInteractionBox = gameObject;
		trackedObjs = new List<SteamVR_TrackedObject>();
		controllers = new List<SteamVR_TrackedObject>();
		debugSpheres = new List<GameObject>();
		debugSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		debugRect = GameObject.CreatePrimitive(PrimitiveType.Quad);
	}

	// Update is called once per frame
	void Update()
	{
		countControllers(trackedObjs);

		if (controllersInBox > 0)
		{
			updateTrackedControllers(trackedObjs);
			drawDebugSpheresAtControllers();
			Vector3 middleOfControllers = middlePointBetweenControllers();
			middleOfControllers += stationaryFixedHinge.transform.position;
			middleOfControllers = new Vector3(middleOfControllers.x / 2, middleOfControllers.y / 2, middleOfControllers.z / 2);
			drawDebugsphereAtPosition(middleOfControllers);
			drawDebugRectFromPos(middleOfControllers, stationaryFixedHinge);
			drawBalistaFromPos(middleOfControllers, stationaryFixedHinge, balista);
			//Debug.Log("controllersInBox: " + controllersInBox);
		}

		if (controllersInBox >= 2)
		{
			//Debug.Log("at least two controller active: " + controllers);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		//Debug.Log("Trigger Enter");
		addTrackedObjs(other);
	}

	private void OnTriggerStay(Collider other)
	{
	}

	private void OnTriggerExit(Collider other)
	{
		//Debug.Log("Trigger Exit");
		removeTrackedObjs(other);
	}

	private void addTrackedObjs(Collider collidingObj)
	{
		if (collidingObj.GetComponent<SteamVR_TrackedObject>() != null)
		{
			trackedObjs.Add(collidingObj.GetComponent<SteamVR_TrackedObject>());
		}
	}

	private void removeTrackedObjs(Collider collidingObj)
	{
		if (collidingObj.GetComponent<SteamVR_TrackedObject>() != null)
		{
			trackedObjs.Remove(collidingObj.GetComponent<SteamVR_TrackedObject>());
		}
	}

	private void countControllers(List<SteamVR_TrackedObject> objList)
	{
		controllersInBox = 0;

		objList.ForEach(delegate (SteamVR_TrackedObject obj)
		{
			checkForController(obj);
		});
	}

	private void checkForController(SteamVR_TrackedObject trackedObj)
	{
		if (isController(trackedObj))
		{
			controllersInBox++;
		}
	}

	private bool isController(SteamVR_TrackedObject trackedObj)
	{
		if (SteamVR_Controller.Input((int)trackedObj.index) == null)
		{
			//Debug.Log("no index");
			return false;
		}

		//Debug.Log("tracked obj as controller: " + trackedObj);
		return true;
	}

	private void updateTrackedControllers(List<SteamVR_TrackedObject> trackedObjToCheck)
	{
		List<SteamVR_TrackedObject> controllerList = new List<SteamVR_TrackedObject>();

		trackedObjToCheck.ForEach(delegate (SteamVR_TrackedObject obj)
		{
			if (isController(obj))
			{
				controllerList.Add(obj);
			}
		});

		// overwrite current controllers with the new list
		controllers = controllerList;
	}

	private void drawDebugSpheresAtControllers()
	{
		// create more spheres when there are not enough prepared to display
		while (debugSpheres.Count < controllersInBox)
		{
			Debug.Log("adding more spheres" + debugSpheres.Count + " is less than " + controllersInBox);
			GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			sphere.transform.position = new Vector3(0, 0F, 0);
			sphere.transform.localScale = new Vector3(0.015f, 0.015f, 0.015f);
			debugSpheres.Add(sphere);
		}

		// set as many spheres to controller positions as there are controllers
		IEnumerator sphereEnumerator = debugSpheres.GetEnumerator();
		IEnumerator controllersEnumerator = controllers.GetEnumerator();
		while (controllersEnumerator.MoveNext())
		{
			if (sphereEnumerator.MoveNext())
			{
				GameObject sphere = (GameObject)sphereEnumerator.Current;
				sphere.SetActive(true);
				SteamVR_TrackedObject controller = (SteamVR_TrackedObject) controllersEnumerator.Current;
				sphere.transform.position = controller.transform.position;
			}
			else
			{
				Debug.Log("less spheres then controllers");
			}
		}

		// disable spheres that are not used
		while (sphereEnumerator.MoveNext())
		{
			Debug.Log("too many spheres");
			GameObject sphereToDisable = (GameObject)sphereEnumerator.Current;
			sphereToDisable.SetActive(false);
		}
	}

	private Vector3 middlePointBetweenControllers()
	{
		int controllerCounter = 0;
		Vector3 position = new Vector3();
		IEnumerator controllersEnumerator = controllers.GetEnumerator();
		while (controllersEnumerator.MoveNext())
		{
			controllerCounter++;
			SteamVR_TrackedObject controller = (SteamVR_TrackedObject) controllersEnumerator.Current;
			position += controller.transform.position;
		}

		if (controllerCounter > 0)
		{
			position = new Vector3(position.x / controllerCounter, position.y / controllerCounter, position.z / controllerCounter);
		}
		return position;
	}

	private void drawDebugsphereAtPosition(Vector3 position)
	{
		debugSphere.transform.position = position;
		debugSphere.transform.localScale = new Vector3(0.015f, 0.015f, 0.015f);
	}

	private void drawDebugRectFromPos(Vector3 position, Transform ObjectForDirection)
	{
		debugSphere.transform.position = position;
		debugSphere.transform.localScale = new Vector3(0.015f, 0.015f, 0.1f);
		debugSphere.transform.LookAt(ObjectForDirection);
	}

	private void drawBalistaFromPos(Vector3 position, Transform ObjectForDirection, GameObject balista)
	{
		balista.transform.position = position;
		balista.transform.LookAt(ObjectForDirection);
	}
}