using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swapWeapons : MonoBehaviour
{
	private SteamVR_TrackedObject trackedObj;
	public float blindspot = 0.3f; // from 0 to 1
	public string weaponsTag = "playerWeapons";
	private bool weaponSwaped = false;
	private bool touchPressed = false;
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
            //Debug.Log(gameObject.name + Controller.GetAxis());
            // For the y coordinate, -1 represents the bottom of the trackpad and 1 represents the top
            // for x, -1=left and 1=right

            float x = Controller.GetAxis().x;
			float y = Controller.GetAxis().y;

			// top right
			if (x > blindspot && y > blindspot)
			{
				//Debug.Log("top right");
				if (touchPressed && !weaponSwaped)
				{
					weaponSwaped = true;
					swapToGun();
				}

			}

            // bottom right
            if (x > blindspot && y < (-1 * blindspot))
			{
				if (touchPressed && !weaponSwaped)
				{
					weaponSwaped = true;
					swapToShield();
				}
				//Debug.Log("bottom right");
			}

			// top left
			if (x < (-1 * blindspot) && y > (blindspot))
			{
				if (touchPressed && !weaponSwaped)
				{
					weaponSwaped = true;
					swapToSword();
				}
				//Debug.Log("top left");
			}

			// bottom left
			if (x < (-1 * blindspot) && y < (-1 * blindspot))
			{
				if (touchPressed && !weaponSwaped)
				{
					weaponSwaped = true;

				}
				//Debug.Log("bottom left");
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
        resetEnumerator();
        while (weaponEnumerator.MoveNext())
		{
			GameObject weapon = (GameObject)weaponEnumerator.Current;
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

	private void updateGunArray()
	{
		weapons = GameObject.FindGameObjectsWithTag(weaponsTag);
	}

    private void resetEnumerator()
    {
        weaponEnumerator = weapons.GetEnumerator();
    }

	private void updateTouchBool()
	{
		if (Controller.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
		{
			touchPressed = true;
		}

		if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
		{
			weaponSwaped = false;
			touchPressed = false;
		}
	}
}
