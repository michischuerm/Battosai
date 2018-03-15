using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOneLookAtPlayer : MonoBehaviour {
    private Transform monsterNeck;
    private Transform target;           //object at which the boss should look
    private float oldDistance = 100;
    private float currentDistance = 100;
    private float distanceToPlayer;
    private float lookDirectionTimer = 0;
    private bool startLookDirectionTimer = false;
    public float maxLookDirectionTimer = 4;
    private float correctionTimer = 0;
    private float maxCorrectionTimer = .5f;
    private Transform LookatPositionMonster;
    public bool canLookAtPlayer = false;

    void Start()
    {
        monsterNeck = GameObject.Find("Neck").transform;
        LookatPositionMonster = GameObject.Find("LookStraight").transform;
        target = GameObject.Find("Camera (eye)").transform;
    }
	
	// Update is called once per frame
	void Update () {
        if(startLookDirectionTimer)
        {
            lookDirectionTimer += Time.deltaTime;
        }
    }

    private void LateUpdate()
    {
        if ((lookDirectionTimer <= maxLookDirectionTimer && canLookAtPlayer) || GetComponent<BossOneStateHandler>().state == 5)
        {
            float x = monsterNeck.localRotation.eulerAngles.x;
            float y = monsterNeck.localRotation.eulerAngles.y;
            float z = monsterNeck.localRotation.eulerAngles.z;
            x -= x > 180 ? 360 : 0;
            z -= z > 180 ? 360 : 0;
            if ((Mathf.Abs(y) < 150 && Mathf.Abs(y) > 30
                && Mathf.Abs(x) < 90
                && Mathf.Abs(z) < 90) || GetComponent<BossOneStateHandler>().state == 5)
            {
                startLookDirectionTimer = false;
                lookDirectionTimer = 0;
                //Rotate the head to lookto the player
                monsterNeck.rotation = Quaternion.Lerp(monsterNeck.rotation, Quaternion.LookRotation(-target.position + monsterNeck.position), Time.deltaTime * 2);
            }
            else
            {
                monsterNeck.rotation = Quaternion.Lerp(monsterNeck.rotation, Quaternion.LookRotation(-LookatPositionMonster.position + monsterNeck.position), Time.deltaTime * 2);
                startLookDirectionTimer = true;
            }
        }
        else
        {
            monsterNeck.rotation = Quaternion.Lerp(monsterNeck.rotation, Quaternion.LookRotation(-LookatPositionMonster.position + monsterNeck.position), Time.deltaTime * 2);
            correctionTimer += Time.deltaTime;
            if (correctionTimer >= maxCorrectionTimer)
            {
                startLookDirectionTimer = false;
                lookDirectionTimer = 0f;
                correctionTimer = 0;
            }
        }
    }
}
