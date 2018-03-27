using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swapWeapons : MonoBehaviour
{
	private SteamVR_TrackedObject trackedObj;
	public float blindspot = 0.2f; // from 0 to 1
	public static string weaponsTag = "playerWeapons";
	private bool shouldSwap = false;
	// the static is for scripts to not have separate incomplete weaponlists.
	// first call to updateGunArray will fill the weapons array
	private static GameObject[] weapons;
	private IEnumerator weaponEnumerator;
	public allWeapons selectedWeapon = allWeapons.playerShield;

	// sounds
	public AudioClip weaponSwapSound;
	private AudioSource soundEmitter;

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
		print("This is the Start() function");
		updateGunArray();
        resetEnumerator();
		soundEmitter = GetComponent<AudioSource>();
		while (weaponEnumerator.MoveNext())
        {
            GameObject weapon = (GameObject)weaponEnumerator.Current;
			string selectedWepString;
			switch (selectedWeapon)
			{
				case allWeapons.playerGun:
					selectedWepString = "playerGun";
					break;
				case allWeapons.playerSword:
					selectedWepString = "playerSword";
					break;
				case allWeapons.playerShield:
					selectedWepString = "playerShield";
					break;
				default:
					selectedWepString = "playerShield";
					break;
			}

			if (weapon.name == selectedWepString)
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
		}
		else
		{
			// all new Weps + the active wep in the other hand(s)
			GameObject[] allActiveWeps = GameObject.FindGameObjectsWithTag(weaponsTag);
			List<GameObject> allWeps = new List<GameObject>(weapons);
			List<GameObject> allActiveWepsList = new List<GameObject>(allActiveWeps);

			for (int i = allActiveWepsList.Count - 1; i >= 0; i--)
			{
				bool isInArray = false;

				foreach (GameObject savedWep in weapons)
				{
					if (GameObject.ReferenceEquals(allActiveWepsList[i], savedWep))
					{
						// weapon allready in array
						allActiveWepsList.RemoveAt(i);
						isInArray = true;
					}
				}

				if(!isInArray)
				{
					allWeps.Add(allActiveWepsList[i]);
				}
			}

			weapons = allWeps.ToArray();
		}

		//if (weapons == null)
		//{
		//	weapons = GameObject.FindGameObjectsWithTag(weaponsTag);
		/*
		for (int i = 0; i < weapons.Length; i++)
		{
			Debug.Log("weapons found: " + weapons[i].name);
		}
		*/
		//}
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
		Vector2 axis = new Vector2(x, y);
		Vector2 zeroPoint = new Vector2(0, 0);
		Debug.Log("touch angle from right side: " + Vector2.Angle(zeroPoint, axis));

		// cross straight
		// top
		if (Vector2.Angle(zeroPoint, axis) >= 35f && Vector2.Angle(zeroPoint, axis) < 125f)
		{
			swapToGun();
			soundEmitter.clip = weaponSwapSound;
			soundEmitter.Play();
		}

		// left
		if (Vector2.Angle(zeroPoint, axis) >= 125f && Vector2.Angle(zeroPoint, axis) < 215f)
		{
			swapToShield();
			soundEmitter.clip = weaponSwapSound;
			soundEmitter.Play();
		}

		// bottom
		if (Vector2.Angle(zeroPoint, axis) >= 215f && Vector2.Angle(zeroPoint, axis) < 305f)
		{
			swapToSword();
			soundEmitter.clip = weaponSwapSound;
			soundEmitter.Play();
		}

		// right
		if (Vector2.Angle(zeroPoint, axis) >= 305f && Vector2.Angle(zeroPoint, axis) < 35f)
		{

		}

		/*
		//cross diagonal
		// top right
		if (x > blindspot && y > blindspot)
		{
			//Debug.Log("top right");
			swapToGun();
			soundEmitter.clip = weaponSwapSound;
			soundEmitter.Play();
		}

		// bottom right
		if (x > blindspot && y < (-1 * blindspot))
		{
			//Debug.Log("bottom right");
			swapToShield();
			soundEmitter.clip = weaponSwapSound;
			soundEmitter.Play();
		}

		// top left
		if (x < (-1 * blindspot) && y > (blindspot))
		{
			//Debug.Log("top left");
			swapToSword();
			soundEmitter.clip = weaponSwapSound;
			soundEmitter.Play();
		}

		// bottom left
		if (x < (-1 * blindspot) && y < (-1 * blindspot))
		{
			//Debug.Log("bottom left");
		}
		*/
	}

	public enum allWeapons
	{
		playerShield = 1,
		playerSword,
		playerGun
	}
}
