using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class staticEnemy : MonoBehaviour {
    public GameObject prefab;
    public int attackLength = 4;
    public int minTimeBetweenAttacks = 10;
    public int maxTimeBetweenAttacks = 15;
    public float timeTillAttackStopsAfterHit = 1.5f;
    private bool canShoot = true;
    private GameObject player;
    private GameObject attackEffect;
    private Animator animator;

    private Transform target;
    private Quaternion targetRotation;                  //Rotation to face the player
    private float str;                                  //multiplikation of rotation strength and time
    public float rotationStrength = 0.8f;               //Strength of the rotation
    private bool stopRotation = false;

    private Transform monsterHead;

    // Use this for initialization
    void Start () {
        target = GameObject.Find("Camera (eye)").transform;
        animator = GetComponent<Animator>();
        //change animation later, for now only change the speed for testing purposes==========
        //animator.speed = 0.01f;
        //=====================================================================================
        GetComponent<EnemyMovementAI>().enabled = false;
        GetComponent<EnemyShoot>().enabled = false;
        //change Animation to Exhausted
        player = GameObject.Find("Camera (eye)");
        monsterHead = GameObject.Find("BossOne/Rig/WingPart/Neck/Head").transform;
        attackEffect = Instantiate(prefab, monsterHead.position, new Quaternion(0,0,0,0));
        attackEffect.transform.position += new Vector3(0, -0.5f, 0);
        attackEffect.transform.SetParent(monsterHead,true);
       // attackEffect.transform.rotation = Quaternion.LookRotation(target.position);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!stopRotation)
        {
            //Rotate to face the target
            targetRotation = Quaternion.LookRotation(-target.position + transform.position);
            str = Mathf.Min(rotationStrength * Time.deltaTime, 1);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, str);
            Debug.Log(targetRotation == transform.rotation);
            if (targetRotation == transform.rotation) stopRotation = true;
        }
        if (canShoot && !player.GetComponent<PlayerHitDetection>().isHit)
        {
            canShoot = false;
            Invoke("attack", Random.Range(minTimeBetweenAttacks, maxTimeBetweenAttacks));
        }
        //Monster damaged the player and is still attacking, stop monster attack after delay
        else if (!canShoot && player.GetComponent<PlayerHitDetection>().isHit)
        {
            Invoke("finishAttack", timeTillAttackStopsAfterHit);
        }        
    }

    private void attack()
    {
        attackEffect.SetActive(true);
        attackEffect.transform.LookAt(target.position);
        Invoke("finishAttack", attackLength);
    }

    private void finishAttack()
    {
        canShoot = true;
        attackEffect.SetActive(false);
        //change Animation to exhausted
    }
    private void OnDisable()
    {
        CancelInvoke("attack");
    }
}
