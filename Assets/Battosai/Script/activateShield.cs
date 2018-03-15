using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// shield will only go up for the moment
public class activateShield : MonoBehaviour
{
	// lifespan includes animationtime
	public float lifespan = 5.0f;
	public Material almostFinishedMaterial;
	private float signalStartingAfterPercentLifespan = 0.8f;
	private float animationTime = 0.5f;
	private float activationTime = 0.0f;
	private bool shieldActivated = false;
	private Vector3 startPos;
	private Vector3 endPos;
	private Material normalMaterial;

	// Use this for initialization
	void Start ()
	{
		activationTime = Time.realtimeSinceStartup;
		startPos = transform.parent.position;
		normalMaterial = GetComponent<Renderer>().material;

		endPos = startPos;
		Vector3 size = new Vector3();
		Renderer[] childRenderers = transform.parent.GetComponentsInChildren<Renderer>();
		foreach(Renderer child in childRenderers)
		{
			size += child.bounds.size;
		}
		endPos.y = startPos.y + size.y;
	}

	// Update is called once per frame
	void Update ()
	{
		if (shieldActivated)
		{
			// shield should go to endPos
			if (transform.parent.position != endPos)
			{
				// max position
				float endPosRelative = endPos.y - startPos.y;

				float currentPosRelativeToEnd = 0.0f;
				// current position in percent
				if ((Time.realtimeSinceStartup - this.activationTime) < this.animationTime)
				{
					currentPosRelativeToEnd = (Time.realtimeSinceStartup - this.activationTime) / this.animationTime;
				}
				else
				{
					currentPosRelativeToEnd = 1.0f;
					shieldActivated = false;
				}

				// set position
				shieldPosition(startPos, endPos, currentPosRelativeToEnd);
			}
		}
		else
		{
			// shield should go to startPos
			if (transform.parent.position != startPos)
			{
				float currentPosRelativeToEnd = 1.0f;

				if ((this.activationTime + this.lifespan) > Time.realtimeSinceStartup)
				{
					currentPosRelativeToEnd = (this.activationTime + this.lifespan - Time.realtimeSinceStartup) / this.animationTime;
				}
				else
				{
					currentPosRelativeToEnd = 0.0f;
					shieldActivated = false;
				}

				shieldPosition(startPos, endPos, currentPosRelativeToEnd);
			}
		}

		signalShieldGone();
	}

	private void OnTriggerEnter(Collider other)
	{
		//Debug.Log("Shield activated");
		startShield();
	}

	private void startShield()
	{
		this.activationTime = Time.realtimeSinceStartup;
		shieldActivated = true;
		// todo set position when triggered to activate again mid move
	}

	private void shieldPosition(Vector3 startPosition, Vector3 endPosition, float currentPosPercent)
	{
		transform.parent.position = Vector3.Lerp(startPos, endPosition, currentPosPercent);
	}

	private void signalShieldGone()
	{
		if (Time.realtimeSinceStartup > this.activationTime + this.lifespan * this.signalStartingAfterPercentLifespan
			&& Time.realtimeSinceStartup < (this.activationTime + this.lifespan))
		{
			GetComponent<Renderer>().material = almostFinishedMaterial;
		}
		else
		{
			GetComponent<Renderer>().material = normalMaterial;
		}
	}

}
