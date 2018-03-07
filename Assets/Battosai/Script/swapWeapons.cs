using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swapWeapons : MonoBehaviour
{
	private SteamVR_TrackedObject trackedObj;
	public float blindspot = 0.3f; // from 0 to 1
	public string weaponsTag = "playerWeapons";
	private bool weaponSwaped = false;
	private GameObject[] weapons;
	private IEnumerator weaponEnumerator;

	private SteamVR_Controller.Device Controller
	{
		get { return SteamVR_Controller.Input((int)trackedObj.index); }
	}

	void Awake()
	{
		trackedObj = GetComponent<SteamVR_TrackedObject>();
	}

	private void Start()
	{
		updateGunArray();
	}

	// Update is called once per frame
	void Update ()
	{
		if (Controller.GetAxis() != Vector2.zero)
		{
			Debug.Log(gameObject.name + Controller.GetAxis());
			// For the y coordinate, -1 represents the bottom of the trackpad and 1 represents the top
			// for x, -1=left and 1=right

			float x = Controller.GetAxis().x;
			float y = Controller.GetAxis().y;

			// top right
			if (x > blindspot && y > blindspot)
			{
				Debug.Log("top right");
			}

			// bottom right
			if (x > blindspot && y < (-1 * blindspot))
			{
				Debug.Log("bottom right");
			}

			// top left
			if (x > (-1 * blindspot) && y > (blindspot))
			{
				Debug.Log("top left");
			}

			// bottom left
			if (x > (-1 * blindspot) && y > (-1 * blindspot))
			{
				Debug.Log("bottom left");
			}
		}
	}

	private void swapToGun()
	{
		updateGunArray();
		while(weaponEnumerator.MoveNext())
		{
			GameObject weapon = (GameObject) weaponEnumerator.Current;
			if (weapon.name == "playerGun")
			{
				Debug.Log("playerGun found, activate");
				weapon.active = true;
			}
			else
			{
				weapon.active = false;
			}
		}
	}

	private void swapToSword()
	{
		updateGunArray();
		while (weaponEnumerator.MoveNext())
		{
			GameObject weapon = (GameObject)weaponEnumerator.Current;
			if (weapon.name == "playerSword")
			{
				Debug.Log("playerSword found, activate");
				weapon.active = true;
			}
			else
			{
				weapon.active = false;
			}
		}
	}

	private void swapToShield()
	{
		updateGunArray();
		while (weaponEnumerator.MoveNext())
		{
			GameObject weapon = (GameObject)weaponEnumerator.Current;
			if (weapon.name == "playerShield")
			{
				Debug.Log("playerShield found, activate");
				weapon.active = true;
			}
			else
			{
				weapon.active = false;
			}
		}
	}

	private void updateGunArray()
	{
		weapons = GameObject.FindGameObjectsWithTag(weaponsTag);
		weaponEnumerator = weapons.GetEnumerator();
		Debug.Log("Guns updated " + weapons);
	}
}
