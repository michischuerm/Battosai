using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VRButtons : MonoBehaviour
{
	public Material hitMaterial;
	private Material normalMaterial;
	public Button thisButton;

	// Use this for initialization
	void Start ()
	{
		normalMaterial = GetComponent<Renderer>().material;
	}

	// Update is called once per frame
	void Update ()
	{
		
	}

	private void OnTriggerEnter(Collider other)
	{
		GetComponent<Renderer>().material = hitMaterial;

		switch(thisButton)
		{
			case Button.Back:
				SceneManager.LoadScene("startScene", LoadSceneMode.Single);
				break;
			case Button.Start:
				SceneManager.LoadScene("level_1", LoadSceneMode.Single);
				break;
			case Button.Exit:
				Application.Quit();
				break;
			case Button.Tutorial:
				SceneManager.LoadScene("level_tutorial", LoadSceneMode.Single);
				break;
			case Button.Level1:
				SceneManager.LoadScene("level_1", LoadSceneMode.Single);
				break;
			case Button.Level2:
				SceneManager.LoadScene("level_2", LoadSceneMode.Single);
				break;
			case Button.shootingRange:
				SceneManager.LoadScene("shootingRange", LoadSceneMode.Single);
				break;
			default:
				break;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		GetComponent<Renderer>().material = normalMaterial;
	}

	public enum Button
	{
		Start,
		Exit,
		Tutorial,
		Level1,
		Level2,
		shootingRange,
		Back
	};
}
