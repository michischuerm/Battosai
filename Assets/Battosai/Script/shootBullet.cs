using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootBullet : MonoBehaviour
{
	public GameObject gameObject;
	private List<GameObject> shotsPool = new List<GameObject>();
	private List<GameObject>.Enumerator shotsEnumerator;
	public Transform bulletSpawnPos;

	// Use this for initialization
	void Start ()
	{
		// prefill Pool
		for (int i = 0; i < 20; i++)
		{
			GameObject shot = (GameObject) Instantiate(gameObject);
			shot.SetActive(false);
			shotsPool.Add(shot);
		}

		shotsEnumerator = shotsPool.GetEnumerator();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetButton("Fire1"))
		{
			fire();
		}
	}

	private void fire()
	{
		if (!shotsEnumerator.MoveNext())
		{
			shotsEnumerator = shotsPool.GetEnumerator();
			shotsEnumerator.MoveNext();
			print("was last");
		}
		if (shotsEnumerator.Current != null)
		{
			GameObject currentShot = shotsEnumerator.Current;
			currentShot.transform.position = bulletSpawnPos.position;
			currentShot.transform.rotation = bulletSpawnPos.rotation;
			currentShot.SetActive(true);

			//Add velocity to the pinsel
			//pinsel.GetComponent<Rigidbody>().velocity = pinsel.transform.forward * 60;
			currentShot.GetComponent<Rigidbody>().velocity = currentShot.transform.forward * 60;

			//Destroy the shot
			//Destroy(shot, 5.0f);
		}
	}
}
