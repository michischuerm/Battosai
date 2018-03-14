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
		this.fixedUpdateDistanceDelta = hingeJoint.angle / 360 * Mathf.PI * 2 * radius * Mathf.Sign(hingeJoint.velocity);
		// calculated distance travelled with factor. Relative to the start.
		this.distanceToStartCranked += crankToDistanceFactor * fixedUpdateDistanceDelta;
		//Debug.Log("distanceCranked: " + distanceToStartCranked);
		//Debug.Log("fixedUpdateDistanceDelta: " + fixedUpdateDistanceDelta);
	}
}
