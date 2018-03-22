using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOneStateHandler : MonoBehaviour {
    public int state = 0;
    public GameObject prefab;
    private GameObject attackEffect;

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
            GetComponent<BossOneWinder>().enabled = false;
        }
        else if (newState == 1)
        {
            GetComponent<EnemyHPHandler>().enabled = true;
            GetComponent<BossOneIntro>().enabled = false;
            GetComponent<EnemyMovementAI>().enabled = true;
            GetComponent<EnemyShoot>().enabled = true;
            GetComponent<staticEnemy>().enabled = false;
            GetComponent<BossOneWinder>().enabled = false;
        }
        else if(newState == 2 && state != 2)
        {
            /* GetComponent<EnemyMovementAI>().MovementSpeed  /= 2;
             GetComponent<EnemyMovementAI>().rotationStrength /= 2;*/
            Transform spawnPoint = GameObject.Find("BossOne/Rig/WingPart/Neck/Head").transform;

            GameObject attackEffect = Instantiate(prefab, spawnPoint.position, new Quaternion(0, 0, 0, 0));
            attackEffect.transform.position += new Vector3(0, -0.5f, 0);
            attackEffect.transform.SetParent(spawnPoint, true);
            attackEffect.transform.LookAt(GameObject.Find("LookStraight").transform.position);
            attackEffect.SetActive(true);
            Invoke("destroyEffect", 3);

            GetComponent<EnemyMovementAI>().movementSpeed  /= 10;
            GetComponent<EnemyMovementAI>().rotationStrength /= 10;

            GetComponent<EnemyShoot>().minTimeBetweenBursts /= 2;
            GetComponent<EnemyShoot>().maxTimeBetweenBursts /= 2;
            GameObject.Find("Balista").SetActive(true);
            
        }
        else if(newState == 3)
        {
            //enemy is connected with ballista
            GetComponent<EnemyHPHandler>().enabled = true;
            GetComponent<BossOneIntro>().enabled = false;
            GetComponent<EnemyMovementAI>().enabled = false;
            GetComponent<EnemyShoot>().enabled = false;
            GetComponent<staticEnemy>().enabled = false;
            GetComponent<BossOneWinder>().enabled = true;
            GetComponent<BossOneLookAtPlayer>().enabled = false;
            GetComponent<BossOneEndPhaseLook>().enabled = true;
            GetComponent<Animator>().SetBool("HarpoonHit", true);
            GameObject.Find("Balista").SetActive(false);
        }
        else if(newState == 4)
        {
            Debug.Log("lastState");
            GetComponent<EnemyHPHandler>().enabled = true;
            GetComponent<BossOneIntro>().enabled = false;
            GetComponent<EnemyMovementAI>().enabled = false;
            GetComponent<EnemyShoot>().enabled = false;
            GetComponent<BossOneWinder>().enabled = false;
            GetComponent<staticEnemy>().enabled = true;
            GetComponent<BossOneLookAtPlayer>().enabled = false;
            GetComponent<BossOneEndPhaseLook>().enabled = true;
            GetComponent<Animator>().SetBool("Grounded", true);
        }
        else if(newState == 5)
        {
            GetComponent<EnemyHPHandler>().enabled = false;
            GetComponent<BossOneIntro>().enabled = false;
            GetComponent<EnemyMovementAI>().enabled = false;
            GetComponent<EnemyShoot>().enabled = false;
            GetComponent<BossOneWinder>().enabled = false;
            GetComponent<staticEnemy>().enabled = false;
            GetComponent<BossOneLookAtPlayer>().enabled = false;
            GetComponent<Animator>().SetBool("Dead", true);
        }
        state = newState;
    }

    void destroyEffect()
    {
        GetComponent<EnemyMovementAI>().movementSpeed *= 10;
        GetComponent<EnemyMovementAI>().rotationStrength *= 10;
        Destroy(attackEffect);
    }
}
