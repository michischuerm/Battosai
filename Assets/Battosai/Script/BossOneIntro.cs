using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOneIntro : MonoBehaviour {
    public Transform[] targets;
    private int targetCounter = 0;
    private Quaternion targetRotation;                  //Rotation to face the next position
    private float str;                                  //multiplikation of rotation strength and time
    public float rotationStrength = 0.8f;               //Strength of the rotation
    public float MovementSpeed = 0.5f;                  //Speed of the movement
    public float distanceToReachTarget = 2f;           //min distance the enemy has to reach to his current target, to get a new target
    private float dist;                                 //distance between target and enemy


    private void Update()
    {
        DetectDistance();
    }

    //calculate distance between enemy and its target
    private void DetectDistance()
    {
        dist = Vector3.Distance(targets[targetCounter].position, transform.position);
        if (dist <= distanceToReachTarget)
        {
            targetCounter++;
            if (targetCounter > targets.Length-1)
            {
                GetComponent<BossOneStateHandler>().changeState(1);
                targetCounter = 0;
            }
        }
        MoveEnemy();
    }

    //MoveEnemy is called by the GameManger each turn to tell each Enemy to try to move towards the player.
    private void MoveEnemy()
    {
        //Move to the target
        transform.position = transform.position + -transform.forward * MovementSpeed;
        //Rotate to face the target
        targetRotation = Quaternion.LookRotation(-targets[targetCounter].position + transform.position);
        str = Mathf.Min(rotationStrength * Time.deltaTime, 1);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, str);
    }
}
