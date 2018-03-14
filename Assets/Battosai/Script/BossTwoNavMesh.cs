using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTwoNavMesh : MonoBehaviour {
    private UnityEngine.AI.NavMeshAgent agent;
    private Transform[] targets;                        //All possible transformation positions
    private int currentPosition = 0;
    private float dist;
    private GameObject player;
    private bool canCharge = true;
    private bool isCharging = false;
    private Vector3 currentTargetPosition;
    public float minTimeTillCharge = 10;
    public float maxTimeTillCharge = 20;
    public float accelerationForCharge = 1;
    private float originalAcceleration;
    private bool movementDirectionForward = true;
    public float minTimeTillDirectionChange = 20;
    public float maxTimeTillDirectionChange = 30;

    void Start () {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        originalAcceleration = agent.acceleration;
        player = GameObject.Find("Camera (eye)");
        //Find all Possible MovementPositions of the Enemy and store them in a list
        GameObject[] enemyMovementPositions = GameObject.FindGameObjectsWithTag("EnemyMovement");
        targets = new Transform[enemyMovementPositions.Length];
        //make sure the order is correct
        for (int i = 0; i < enemyMovementPositions.Length; i++)
        {
            targets[System.Int32.Parse(enemyMovementPositions[i].name.Split('_')[1])] = enemyMovementPositions[i].transform;
        }
        currentTargetPosition = targets[currentPosition].position;
        Invoke("changeDirection", Random.Range(minTimeTillDirectionChange, maxTimeTillDirectionChange));
    }
	
	// Update is called once per frame
	void Update () {
        if (DetectDistance(currentTargetPosition) <= 1)
        {
            if (isCharging)
            {
                canCharge = true;
                isCharging = false;
                agent.acceleration = originalAcceleration;
                changeTargetAfterCharge();
            }
            else
            {
                changeTarget();                
            }
            //set new destination
            currentTargetPosition = targets[currentPosition].position;
        }
        if (canCharge)
        {
            canCharge = false;
            Invoke("chargeToThePlayer",Random.Range(minTimeTillCharge, maxTimeTillCharge));
        }
        MoveEnemy(currentTargetPosition);
    }

    //Move Navagent towards a position
    private void MoveEnemy(Vector3 destination)
    {
        agent.SetDestination(destination);        
    }

    //Find a new target, usually when the old target is reached
    private void changeTarget()
    {
        if (movementDirectionForward)
        {
            if (currentPosition + 1 >= targets.Length)
            {
                currentPosition = 0;
            }
            else
            {
                currentPosition++;
            }
        }
        else
        {
            if (currentPosition - 1 < 0)
            {
                currentPosition = targets.Length-1;
            }
            else
            {
                currentPosition--;
            }
        }
    }

    //Find a new target, usually when the boss charged through the player and reached his destination
    private void changeTargetAfterCharge()
    {
        Transform closest = targets[0];
        float distanceToClosest = DetectDistance(closest.position);
        //Detect the nearest waypoint
        foreach (Transform target in targets)
        {
            if (DetectDistance(target.position) < distanceToClosest)
            {
                closest = target;
                distanceToClosest = DetectDistance(closest.position);
            }
        }
        //set the current position to the nearest waypoint and move to the next
        currentPosition = System.Array.IndexOf(targets,closest);
    }

    //calculate distance between enemy and its target
    private float DetectDistance(Vector3 targetPosition)
    {
        dist = Vector3.Distance(targetPosition, transform.position);        
        return dist;       
    }

    //enemy charges through the player
    private void chargeToThePlayer()
    {
        Vector3 chargeVector = player.transform.position - transform.position;
        chargeVector *= Random.Range(12, 20)/10;
        chargeVector.y = transform.position.y;
        currentTargetPosition = chargeVector;
        agent.velocity = new Vector3(0,0,0);
        agent.SetDestination(currentTargetPosition);
        agent.transform.LookAt(currentTargetPosition);
        agent.acceleration = accelerationForCharge;
        isCharging = true;
    }

    //change the movement Direction of the boss
    private void changeDirection()
    {
        movementDirectionForward = !movementDirectionForward;
        Invoke("changeDirection", Random.Range(minTimeTillDirectionChange, maxTimeTillDirectionChange));
    }
}
