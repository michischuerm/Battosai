using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTwoIllusionShoot : MonoBehaviour {

    public GameObject prefab;
    public float bulletSpeed = 0.6f;
    public int timeBetweenShoots = 2;
    public int minTimeBetweenBursts = 5;
    public int maxTimeBetweenBursts = 10;
    public int minAmountOfShootsInOneBurst = 1;
    public int maxAmountOfShootsInOneBurst = 6;
    private bool canShoot = true;
    private GameObject player;
    private int shootCounter = 0;
    private Animator anim;

    private void Start()
    {
        player = GameObject.Find("Camera (eye)");
        anim = GetComponentInChildren<Animator>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (canShoot && !player.GetComponent<PlayerHitDetection>().isHit)
        {
            canShoot = false;
            int timeToShoot = minTimeBetweenBursts == maxTimeBetweenBursts ?
                minTimeBetweenBursts:Random.Range(minTimeBetweenBursts, maxTimeBetweenBursts);
            shootCounter = Random.Range(minAmountOfShootsInOneBurst, maxAmountOfShootsInOneBurst);
            Invoke("shoot", timeToShoot);
        }
    }

    private void shoot()
    {        
        if (!player.GetComponent<PlayerHitDetection>().isHit)
        {
            anim.SetTrigger("IsAttacking");
            Invoke("spawnShoot", 0.8f);
            if (shootCounter > 0)
            {
                Invoke("shoot", timeBetweenShoots);
            }
            else
            {
                canShoot = true;
            }
        }
        else
        {
            canShoot = true;
        }
    }

    private void spawnShoot()
    {
        GameObject attack = Instantiate(prefab, new Vector3(transform.position.x,transform.position.y+1,transform.position.z), Quaternion.identity);
        attack.GetComponent<Rigidbody>().AddForce(bulletSpeed * (player.transform.position - attack.transform.position), ForceMode.Impulse);
        shootCounter--;
    }

    private void OnDisable()
    {
        CancelInvoke("shoot");
    }
}
