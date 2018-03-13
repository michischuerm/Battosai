using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTwoNavMesh : MonoBehaviour {
    private UnityEngine.AI.NavMeshAgent agent;
    private Transform[] targets;                        //All possible transformation positions
    private int currentPosition = 0;
    private float dist;
    void Start () {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        //Find all Possible MovementPositions of the Enemy and store them in a list
        GameObject[] enemyMovementPositions = GameObject.FindGameObjectsWithTag("EnemyMovement");
        targets = new Transform[enemyMovementPositions.Length];
        //make sure the order is correct
        for (int i = 0; i < enemyMovementPositions.Length; i++)
        {
            targets[System.Int32.Parse(enemyMovementPositions[i].name.Split('_')[1])] = enemyMovementPositions[i].transform;
        }

    }
	
	// Update is called once per frame
	void Update () {
        DetectDistance();
    }

    private void MoveEnemy()
    {
        agent.SetDestination(targets[currentPosition].position);
    }

    private void changeTarget()
    {
        if (currentPosition+1 >= targets.Length)
        {
            currentPosition = 2;
        }else
        {
            currentPosition++;
        }
    }

    //calculate distance between enemy and its target
    private void DetectDistance()
    {
        dist = Vector3.Distance(targets[currentPosition].position, transform.position);
        if (dist <= 1)
        {
            changeTarget();
        }
        MoveEnemy();
    }
}
