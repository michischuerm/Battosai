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
        if(newState == 3)
        {
            state = 2;
        }
        if(newState == 0)
        {            
            GetComponent<EnemyHPHandler>().enabled = true;
            GetComponent<BossOneIntro>().enabled = true;
            GetComponent<EnemyMovementAI>().enabled = false;
            GetComponent<EnemyShoot>().enabled = false;
            GetComponent<staticEnemy>().enabled = false;
            GetComponent<BossOneWinder>().enabled = false;
        }
        else if (newState == 1 && state == 0)
        {
            GetComponent<EnemyHPHandler>().enabled = true;
            GetComponent<BossOneIntro>().enabled = false;
            GetComponent<EnemyMovementAI>().enabled = true;
            GetComponent<EnemyShoot>().enabled = true;
            GetComponent<staticEnemy>().enabled = false;
            GetComponent<BossOneWinder>().enabled = false;
        }
        else if(newState == 2 && state == 1)
        { 
            GetComponent<EnemyMovementAI>().MovementSpeed  /= 2;
            GetComponent<EnemyMovementAI>().rotationStrength /= 2;
            GetComponent<EnemyShoot>().minTimeBetweenBursts /= 2;
            GetComponent<EnemyShoot>().maxTimeBetweenBursts /= 2;
            //code below only for testing purposes, delete this when the ballista is implemented
            state = newState;
            Invoke("stater", 10);
        }
        else if(newState == 3 && state == 2)
        {
            //eney is connected with ballista
            GetComponent<EnemyHPHandler>().enabled = true;
            GetComponent<BossOneIntro>().enabled = false;
            GetComponent<EnemyMovementAI>().enabled = false;
            GetComponent<EnemyShoot>().enabled = false;
            GetComponent<staticEnemy>().enabled = false;
            GetComponent<BossOneWinder>().enabled = true;
            GetComponent<Animator>().SetBool("HarpoonHit", true);
        }
        else if(newState == 4 && state == 3)
        {
            Debug.Log("lastState");
            GetComponent<EnemyHPHandler>().enabled = true;
            GetComponent<BossOneIntro>().enabled = false;
            GetComponent<EnemyMovementAI>().enabled = false;
            GetComponent<EnemyShoot>().enabled = false;
            GetComponent<BossOneWinder>().enabled = false;
            GetComponent<staticEnemy>().enabled = true;
        }
        state = newState;
    }

    void stater()
    {
        changeState(3);
    }
}
