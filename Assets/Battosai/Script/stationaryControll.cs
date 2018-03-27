using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stationaryControll : MonoBehaviour
{
	public Transform stationaryFixedHinge;
	public GameObject balista;
	public GameObject balistaHingePoint;
	private bool shotTriggered = false;
	private GameObject controllerInteractionBox;
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
		debugSphere.SetActive(false);
		debugRect.SetActive(false);
	}

	// Update is called once per frame
	void Update()
	{
		countControllers(trackedObjs);

		if (controllersInBox > 0)
		{
			updateTrackedControllers(trackedObjs);
			checkTriggers(trackedObjs);

			//drawDebugSpheresAtControllers();
			Vector3 middleOfControllers = middlePointBetweenControllers();
            Vector3 middleOfControlAndFixedHinge = middleOfControllers + stationaryFixedHinge.transform.position;
            middleOfControlAndFixedHinge = new Vector3(middleOfControlAndFixedHinge.x / 2, middleOfControlAndFixedHinge.y / 2, middleOfControlAndFixedHinge.z / 2);

            //drawDebugsphereAtPosition(middleOfControllers);
			//drawDebugRectFromPos(middleOfControllers, stationaryFixedHinge);
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
		debugSphere.SetActive(true);
		debugSphere.transform.position = position;
		debugSphere.transform.localScale = new Vector3(0.015f, 0.015f, 0.015f);
	}

	private void drawDebugRectFromPos(Vector3 position, Transform ObjectForDirection)
	{
		debugRect.SetActive(true);
		debugRect.transform.position = position;
		debugRect.transform.localScale = new Vector3(0.015f, 0.015f, 0.1f);
		debugRect.transform.LookAt(ObjectForDirection);
	}

	private void drawBalistaFromPos(Vector3 middleOfControllers, Transform ObjectForDirectionAndHeadposition, GameObject balista)
	{
		// update front pivot point, put balista head on fixedPoint
		Vector3 balistaPivotPointOffset = balistaHingePoint.transform.localPosition - balista.transform.localPosition;
        balista.transform.position = ObjectForDirectionAndHeadposition.transform.position;

        // "set" balista rear into hands direction
        balista.transform.LookAt(middleOfControllers);

        // rotate 180 and for the right direction
        balista.transform.Rotate(new Vector3(180.0f, 90f, 0.0f));
	}

	private void checkTriggers(List<SteamVR_TrackedObject> vrControllers)
	{
		this.shotTriggered = false;

		IEnumerator controllersEnumerator = vrControllers.GetEnumerator();
		while (controllersEnumerator.MoveNext())
		{
			SteamVR_TrackedObject vrtrackedObj = (SteamVR_TrackedObject)controllersEnumerator.Current;
			SteamVR_Controller.Device vrController = SteamVR_Controller.Input((int)vrtrackedObj.index);
			
			if (vrController.GetHairTriggerDown())
			{
				this.shotTriggered = true;
			}
		}
	}

	public bool triggerIsActive()
	{
		return this.shotTriggered;
	}
}