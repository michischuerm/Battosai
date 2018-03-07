using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{
	private SteamVR_TrackedObject trackedObj;

	private SteamVR_Controller.Device Controller
	{
		get { return SteamVR_Controller.Input((int)trackedObj.index); }
	}

	void Awake()
	{
		trackedObj = GetComponent<SteamVR_TrackedObject>();
	}

	public GameObject laserPrefab;
	public GameObject bulletPrefab;
	public float shootCooldownSeconds = 0.2f;
	public float shotSpeedMS = 40.0f;

	private GameObject laser;
	private Transform laserTransform;
	private Vector3 hitPoint;
	private List<GameObject> bulletPool = new List<GameObject>();
	private List<GameObject>.Enumerator bulletEnumerator;
	private float lastShot = 0.0f;
	private bool canFire = true;
	private bool hairTriggerPressed = false;

	private void ShowLaser(RaycastHit hit)
	{
		laser.SetActive(true);
		laserTransform.position = Vector3.Lerp(trackedObj.transform.position, hitPoint, .5f);
		laserTransform.LookAt(hitPoint);
		laserTransform.localScale = new Vector3(
			laserTransform.localScale.x,
			laserTransform.localScale.y,
			hit.distance);
	}

	// Use this for initialization
	void Start()
	{
		laser = Instantiate(laserPrefab);
		laserTransform = laser.transform;

		lastShot = Time.realtimeSinceStartup;

		// prefill Pool
		for (int i = 0; i < 20; i++)
		{
			GameObject bullet = (GameObject)Instantiate(bulletPrefab);
			bullet.SetActive(false);
			bulletPool.Add(bullet);
		}

		bulletEnumerator = bulletPool.GetEnumerator();
	}

	// Update is called once per frame
	void Update()
	{
		float timeDelta = (Time.realtimeSinceStartup - lastShot - shootCooldownSeconds);
		if (timeDelta > 0.0f)
		{
			canFire = true;
		}

		if (Controller.GetHairTriggerDown())
		{
			hairTriggerPressed = true;
		}
		if (Controller.GetHairTriggerUp())
		{
			hairTriggerPressed = false;
		}

		if (hairTriggerPressed)
		{
			RaycastHit hit;

			if (Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, 100))
			{
				hitPoint = hit.point;
				ShowLaser(hit);
			}

			if (canFire)
			{
				fire();
			}
		}
		else
		{
			laser.SetActive(false);
		}
	}

	private void fire()
	{
		Controller.TriggerHapticPulse(1000);
		canFire = false;
		lastShot = Time.realtimeSinceStartup;

		if (!bulletEnumerator.MoveNext())
		{
			bulletEnumerator = bulletPool.GetEnumerator();
			bulletEnumerator.MoveNext();
		}
		if (bulletEnumerator.Current != null)
		{
			GameObject currentBullet = bulletEnumerator.Current;
			currentBullet.transform.position = transform.position;
			currentBullet.transform.rotation = transform.rotation;
			currentBullet.SetActive(true);

			//Add velocity to the pinsel
			currentBullet.GetComponent<Rigidbody>().velocity = currentBullet.transform.forward * shotSpeedMS;
		}
	}
}
