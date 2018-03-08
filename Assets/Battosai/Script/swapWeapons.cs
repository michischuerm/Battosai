using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swapWeapons : MonoBehaviour
{
	private SteamVR_TrackedObject trackedObj;
	public float blindspot = 0.2f; // from 0 to 1
	public static string weaponsTag = "playerWeapons";
	private bool shouldSwap = false;
	private static GameObject[] weapons;
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
        resetEnumerator();
        while (weaponEnumerator.MoveNext())
        {
            GameObject weapon = (GameObject)weaponEnumerator.Current;
            if (weapon.name == "playerShield")
            {
                weapon.SetActive(true);
            }
            else
            {
                weapon.SetActive(false);
            }
        }
    }

	// Update is called once per frame
	void Update ()
	{
        updateTouchBool();

        if (Controller.GetAxis() != Vector2.zero)
		{
			//Debug.Log(gameObject.name + Controller.GetAxis());
			// For the y coordinate, -1 represents the bottom of the trackpad and 1 represents the top
			// for x, -1=left and 1=right

			float x = Controller.GetAxis().x;
			float y = Controller.GetAxis().y;

			if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad) && shouldSwap)
			{
				//Debug.Log("Swapping Weapon");
				swapWeapon(x, y);
			}
		}
	}

	private void swapToGun()
	{
		resetEnumerator();
        while (weaponEnumerator.MoveNext())
		{
			GameObject weapon = (GameObject) weaponEnumerator.Current;
			if (weapon.name == "playerGun")
			{
                weapon.SetActive(true);
            }
			else
			{
                weapon.SetActive(false);
            }
		}
	}

	private void swapToSword()
	{
        //Debug.Log("searching for playerSword");
        resetEnumerator();
        while (weaponEnumerator.MoveNext())
		{
			GameObject weapon = (GameObject)weaponEnumerator.Current;
            //Debug.Log("Listing: " + weapon.name);
			if (weapon.name == "playerSword")
			{
                weapon.SetActive(true);
			}
			else
			{
				weapon.SetActive(false);
            }
		}
	}

	private void swapToShield()
	{
		resetEnumerator();
        while (weaponEnumerator.MoveNext())
		{
			GameObject weapon = (GameObject)weaponEnumerator.Current;
			if (weapon.name == "playerShield")
			{
				weapon.SetActive(true);
			}
			else
			{
				weapon.SetActive(false);
			}
		}
	}

	private static void updateGunArray()
	{
		if (weapons == null)
		{
			weapons = GameObject.FindGameObjectsWithTag(weaponsTag);
			Debug.Log("weapons updated: " + weapons);
		}
	}

    private void resetEnumerator()
    {
        weaponEnumerator = weapons.GetEnumerator();
    }

	private void updateTouchBool()
	{
		if (Controller.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
		{
			shouldSwap = true;
		}
		else
		{
			shouldSwap = false;
		}
	}

	private void swapWeapon(float x, float y)
	{
        //Debug.Log("swap Weapon");
		// top right
		if (x > blindspot && y > blindspot)
		{
			//Debug.Log("top right");
			swapToGun();
		}

		// bottom right
		if (x > blindspot && y < (-1 * blindspot))
		{
			//Debug.Log("bottom right");
			swapToShield();
		}

		// top left
		if (x < (-1 * blindspot) && y > (blindspot))
		{
			//Debug.Log("top left");
			swapToSword();
		}

		// bottom left
		if (x < (-1 * blindspot) && y < (-1 * blindspot))
		{
			//Debug.Log("bottom left");
		}
	}
}
