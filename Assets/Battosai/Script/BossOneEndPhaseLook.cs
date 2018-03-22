using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOneEndPhaseLook : MonoBehaviour {
    private Transform monsterNeck;
    public Transform target;           //object at which the boss should look
    void Start()
    {
        monsterNeck = GameObject.Find("BossOne/Rig/WingPart/Neck/Head").transform;
        target = GameObject.Find("Camera (eye)").transform;
    }
    
    private void LateUpdate()
    {
        //Rotate the head to lookto the player
        //monsterNeck.rotation = /*Quaternion.Lerp(monsterNeck.rotation,*/ Quaternion.LookRotation(target.position + monsterNeck.position)/*, Time.deltaTime * 2)*/;
        monsterNeck.transform.LookAt(new Vector3(target.position.x,target.position.y-3,target.position.z+2));
        monsterNeck.forward *= -1;
    }
}
