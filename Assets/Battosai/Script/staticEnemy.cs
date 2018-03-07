using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class staticEnemy : MonoBehaviour {
    public GameObject prefab;
    public int attackLength = 4;
    public int minTimeBetweenAttacks = 10;
    public int maxTimeBetweenAttacks = 15;
    private bool canShoot = true;
    private GameObject player;
    private GameObject attackEffect;
    // Use this for initialization
    void Start () {
        GetComponent<EnemyMovementAI>().enabled = false;
        GetComponent<EnemyShoot>().enabled = false;
        //change Animation to Exhausted
        player = GameObject.Find("Camera (eye)");
        attackEffect = Instantiate(prefab, GameObject.Find("Root").transform.position, new Quaternion(0, 1, 0, 0));
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (canShoot && !player.GetComponent<PlayerHitDetection>().isHit)
        {
            canShoot = false;
            Invoke("attack", Random.Range(minTimeBetweenAttacks, maxTimeBetweenAttacks));
        }else if (!canShoot && player.GetComponent<PlayerHitDetection>().isHit)
        {
            Invoke("finishAttack", 2);
        }
    }

    private void attack()
    {
        attackEffect.SetActive(true);
        Invoke("finishAttack", attackLength);
    }

    private void finishAttack()
    {
        canShoot = true;
        attackEffect.SetActive(false);
        //change Animation to exhausted
    }
}
