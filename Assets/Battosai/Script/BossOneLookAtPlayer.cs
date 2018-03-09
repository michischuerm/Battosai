using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOneLookAtPlayer : MonoBehaviour {
    private Transform monsterNeck;
    private Transform target;           //object at which the boss should look

    void Start()
    {
        monsterNeck = GameObject.Find("Neck").transform;
        target = GameObject.Find("Camera (eye)").transform;
    }
	
	// Update is called once per frame
	void Update () {
        //Rotate the head to allways lookto the player
        // complete guess. Math to apply overall model tilt to the spine:
        monsterNeck.rotation = Quaternion.LookRotation(-target.position + monsterNeck.position);
    }
}
