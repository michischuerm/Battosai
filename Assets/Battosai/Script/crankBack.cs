using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crankBack : MonoBehaviour
{
	public float distanceToStartCranked = 0.0f;
	public float crankToDistanceFactor = 1.0f;
	public float fixedUpdateDistanceDelta = 0.0f;
	private HingeJoint hingeJoint;
	private float radius;
	private float lastFixedUpdateAngle = 0.0f;

	// Use this for initialization
	void Start ()
	{
		hingeJoint = GetComponent<HingeJoint>();
		
		if (hingeJoint != null)
		{
			radius = Vector3.Magnitude(hingeJoint.anchor);
		}
		else
		{
			Debug.Log("No Hinge Joint on this Object");
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	private void FixedUpdate()
	{
		// distance travelled this update. calulating the arc and set the sign for the direction
		float angle = hingeJoint.angle + 180;
		//Debug.Log("velocity: " + hingeJoint.velocity);
		//Debug.Log("angle: " + angle);
		float deltaAngle = deltaDegree(angle, lastFixedUpdateAngle, hingeJoint.velocity);
		//Debug.Log("angle: " + angle + " lastFixedUpdateAngle:" + lastFixedUpdateAngle + " deltaAngle: " + deltaAngle + " velocity: " + hingeJoint.velocity);
		this.lastFixedUpdateAngle = angle;
		//this.fixedUpdateDistanceDelta = hingeJoint.angle / 360 * Mathf.PI * 2 * radius * Mathf.Sign(hingeJoint.velocity);
		this.fixedUpdateDistanceDelta = angle / 360 * Mathf.PI * 2 * radius * Mathf.Sign(hingeJoint.velocity);
		// calculated distance travelled with factor. Relative to the start.
		this.distanceToStartCranked += crankToDistanceFactor * fixedUpdateDistanceDelta;
		//Debug.Log("distanceCranked: " + distanceToStartCranked);
		//Debug.Log("fixedUpdateDistanceDelta: " + fixedUpdateDistanceDelta);
	}

	// takes values from 0 to 360
	private float deltaDegree(float angle, float previousAngle, float velocity)
	{
		float deltaDegree = 0.0f;
		// two special cases:
		// velocity is positive, previousAngle greater then new
		// then 360 - previousAngle + new angle
		if (velocity > 0.0f && previousAngle > angle)
		{
			deltaDegree = previousAngle + angle - 360;
		}
		// case two:
		// velocity is negative, previousAngle less then new
		// then previousAngle + 360 - new angle
		else if (velocity < 0.0f && previousAngle < angle)
		{
			deltaDegree = previousAngle - angle + 360;
		}
		// all other cases:
		// when velocity positive
		// new angle - previous
		else if (velocity > 0.0f)
		{
			deltaDegree = angle - previousAngle;
		}
		// velo negative
		// previous - new angle
		else if (velocity < 0.0f)
		{
			deltaDegree = previousAngle - angle;
		}

		return deltaDegree;
	}
}
