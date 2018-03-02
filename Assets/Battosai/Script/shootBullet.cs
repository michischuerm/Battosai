using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootBullet : MonoBehaviour
{
	public GameObject gameObject;
	public Transform bulletSpawnPos;
	private List<GameObject> shotsPool = new List<GameObject>();
	private List<GameObject>.Enumerator shotsEnumerator;
	public float shootCooldownSeconds = 0.5f;
	private float lastShot = 0.0f;
	private bool canFire = true;

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
		lastShot = Time.realtimeSinceStartup;

    }
	
	// Update is called once per frame
	void Update ()
	{
		float timeDelta = (Time.realtimeSinceStartup - lastShot - shootCooldownSeconds);
		if (timeDelta > 0.0f)
		{
			canFire = true;
		}

		//if ((SteamVR_Controller.Input () || Input.GetButton("Fire1")) && canFire)
		if (Input.GetButton("Fire1") && canFire)
		{
			fire();
		}

        //bulletSpawnPos.position = bulletSpawnPos.parent.position;
        Debug.DrawLine(transform.localPosition, new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + 60.0f), Color.blue);
        //Debug.DrawLine(new Vector3(0, 0, 0), new Vector3(10.0f, 10.0f, 10.0f), Color.blue);

        //Debug.DrawLine(new Vector3(0, 0, 0), bulletSpawnPos.parent.position, Color.red);
    }

	private void fire()
	{
		canFire = false;
		lastShot = Time.realtimeSinceStartup;

		if (!shotsEnumerator.MoveNext())
		{
			shotsEnumerator = shotsPool.GetEnumerator();
			shotsEnumerator.MoveNext();
			print("was last");
		}
		if (shotsEnumerator.Current != null)
		{
			GameObject currentShot = shotsEnumerator.Current;
			currentShot.transform.position = bulletSpawnPos.localPosition;
			currentShot.transform.rotation = bulletSpawnPos.localRotation;
            /*
            currentShot.transform.eulerAngles = new Vector3(
                currentShot.transform.eulerAngles.x,
                currentShot.transform.eulerAngles.y - 110.0f,
                currentShot.transform.eulerAngles.z - 70.0f);
            Debug.Log("x: " + currentShot.transform.eulerAngles.x);
            Debug.Log("y: " + currentShot.transform.eulerAngles.y);
            Debug.Log("z: " + currentShot.transform.eulerAngles.z);
            */

            currentShot.SetActive(true);

            //Add velocity to the pinsel
            //pinsel.GetComponent<Rigidbody>().velocity = pinsel.transform.forward * 60;
            Debug.Log("forwards: " + bulletSpawnPos.forward);
			currentShot.GetComponent<Rigidbody>().velocity = bulletSpawnPos.forward * 60;

			//Destroy the shot
			//Destroy(shot, 5.0f);
		}

    }
}
