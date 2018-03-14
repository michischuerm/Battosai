using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crankBack : MonoBehaviour
{
	public float distanceCranked = 0.0f;
	public float crankToDistanceFactor = 2.0f;
	private HingeJoint hingeJoint;

	// Use this for initialization
	void Start ()
	{
		hingeJoint = GetComponent<HingeJoint>();
		
		if (hingeJoint == null)
		{
			Debug.Log("No Hinge Joint on this Object");
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		Debug.Log("velocity: " + hingeJoint.velocity);
		Debug.Log("angle: " + hingeJoint.angle);
		distanceCranked += crankToDistanceFactor * hingeJoint.velocity;
		Debug.Log("distanceCranked: " + distanceCranked);
	}
}
