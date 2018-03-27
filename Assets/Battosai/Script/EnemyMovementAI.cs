using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementAI : MonoBehaviour {
    private Transform[] targets;                        //All possible transformation positions
    private Transform target;                           //Transform to attempt to move toward each turn.
    private Quaternion targetRotation;                  //Rotation to face the next position
    private float str;                                  //multiplikation of rotation strength and time
    public float rotationStrength = 0.8f;               //Strength of the rotation
    public float originalRotationStrength = 0.8f;       //original strength of the rotation
    public float movementSpeed = 0.5f;                  //Speed of the movement
    public float originalMovementSpeed = 0.5f;          //original Speed of the movement
    private int lastIndex;                              //used to detect if the target location is two times the same
    private float safetyTargetChangeTime = 0f;          //If the Enemy tries for to long, to get to a Target and can't reach it, the target gets changed
    public float maxTimeBeforeTargetChange = 15f;       //max time the enemy follows one target, before changing targets
    public float distanceToReachTarget = 15f;           //min distance the enemy has to reach to his current target, to get a new target
    private float dist;                                 //distance between target and enemy
    private BossOneStateHandler stateHandler;           //statehandler of the boss
    private int slowTargetIndex;                        //phase two where the boss moves slower
    private bool isSlow = false;                        //if the boss currently moves slower than normal
    public float distanceToSlowDown = 100; 
    void Start()
    {
        stateHandler = GetComponent<BossOneStateHandler>();
        originalMovementSpeed = movementSpeed;
        originalRotationStrength = rotationStrength;
        //Find all Possible MovementPositions of the Enemy and store them in a list
        GameObject[] enemyMovementPositions = GameObject.FindGameObjectsWithTag("EnemyMovement");
        targets = new Transform[enemyMovementPositions.Length];
        for(int i = 0; i < enemyMovementPositions.Length; i++)
        {
            targets[i] = enemyMovementPositions[i].transform;
            if (enemyMovementPositions[i].name.Contains("Slow"))
            {
                slowTargetIndex = i;
            }
        }
        lastIndex = Random.Range(0, targets.Length);
        target = targets[lastIndex];
    }

    private void Update()
    {
        DetectDistance();
    }

    //calculate distance between enemy and its target
    private void DetectDistance () {
        dist = Vector3.Distance(target.position, transform.position);
        if (dist <= distanceToReachTarget || safetyTargetChangeTime >= maxTimeBeforeTargetChange)
        {
            safetyTargetChangeTime = 0;
            changeTargetRandom();
        }
        //Debug.Log(Vector3.Distance(targets[slowTargetIndex].position, transform.position)+" < "+distanceToSlowDown+" :"+isSlow+" , "+movementSpeed);
        if(Vector3.Distance(targets[slowTargetIndex].position, transform.position) < distanceToSlowDown && stateHandler.state == 2)
        {
            if (!isSlow || movementSpeed != originalMovementSpeed/2)
            {
                isSlow = true;
                slowDown();
            }
        }
        else if (isSlow)
        {
            isSlow = false;
            resetSpeed();
        }
        MoveEnemy();
    }

    //Changes the current target to a random target of the targets array
    private void changeTargetRandom()
    {
        int randomIndex = Random.Range(0, targets.Length);
        if (stateHandler.state == 2)
        {
            if (randomIndex >= targets.Length / 2)
            {
                randomIndex = slowTargetIndex;
            }
        }
        if (randomIndex == lastIndex)
        {
            randomIndex = (randomIndex != 0 ? randomIndex - 1 : 1);
        }
        lastIndex = randomIndex;
        target = targets[randomIndex];
    }

    //MoveEnemy is called by the GameManger each turn to tell each Enemy to try to move towards the player.
    private void MoveEnemy()
    {
        safetyTargetChangeTime += Time.deltaTime;
        //Move to the target
        transform.position = transform.position+-transform.forward*movementSpeed;
        //Rotate to face the target
        targetRotation = Quaternion.LookRotation(-target.position + transform.position);
        str = Mathf.Min(rotationStrength * Time.deltaTime, 1);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, str);
    }

    private void slowDown()
    {
        movementSpeed = originalMovementSpeed/2;
        rotationStrength = originalRotationStrength/2;
    }

    private void resetSpeed()
    {
        movementSpeed = originalMovementSpeed;
        rotationStrength = originalRotationStrength;
    }
}
