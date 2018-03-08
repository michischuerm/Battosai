using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOneStateHandler : MonoBehaviour {
    public int state = 0;

	// Use this for initialization
	void Start () {
        changeState(state);
	}
	
    public void changeState(int newState)
    {
        if(newState == 0)
        {            
            GetComponent<EnemyHPHandler>().enabled = true;
            GetComponent<BossOneIntro>().enabled = true;
            GetComponent<EnemyMovementAI>().enabled = false;
            GetComponent<EnemyShoot>().enabled = false;
            GetComponent<staticEnemy>().enabled = false;
        }
        else if (newState == 1 && state == 0)
        {
            GetComponent<EnemyHPHandler>().enabled = true;
            GetComponent<BossOneIntro>().enabled = false;
            GetComponent<EnemyMovementAI>().enabled = true;
            GetComponent<EnemyShoot>().enabled = true;
            GetComponent<staticEnemy>().enabled = false;
        }
        else if(newState == 2 && state == 1)
        {
            GetComponent<EnemyHPHandler>().enabled = true;
            GetComponent<BossOneIntro>().enabled = false;
            GetComponent<EnemyMovementAI>().enabled = false;
            GetComponent<EnemyShoot>().enabled = false;
            GetComponent<staticEnemy>().enabled = true;
        }
        state = newState;
    }
}
