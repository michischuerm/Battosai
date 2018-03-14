using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTwoSound : MonoBehaviour {
    private AudioSource audio;
    public float maxDistanceToThePlayer = 22;
    private GameObject target;

	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();
        target = GameObject.Find("Camera (eye)");
	}
	
	// Update is called once per frame
	void Update () {
        audio.pitch = Map(maxDistanceToThePlayer - Vector3.Distance(target.transform.position, transform.position),0,maxDistanceToThePlayer,1f,1.7f);
    }
    
    private float Map(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
