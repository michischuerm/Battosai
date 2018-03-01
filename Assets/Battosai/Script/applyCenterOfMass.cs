using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class applyCenterOfMass : MonoBehaviour
{
    public Rigidbody rigidBody;
    public GameObject centerOfMassObj;

    // Use this for initialization
    void Start ()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.centerOfMass = centerOfMassObj.GetComponent<Transform>().position;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
