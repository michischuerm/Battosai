using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject prefab;
    public float bulletSpeed = 0.6f;
    public int timeBetweenShoots = 2;
    public int minTimeBetweenBursts = 5;
    public int maxTimeBetweenBursts = 10;
    public int minAmountOfShootsInOneBurst = 1;
    public int maxAmountOfShootsInOneBurst = 6;
    public bool canShoot = true;
    private GameObject player;
    private int shootCounter = 0;
    private BossOneLookAtPlayer lookScript;
    private Animator animator;
    private EnemyMovementAI movement;
    private void Start()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<EnemyMovementAI>();
        player = GameObject.Find("Camera (eye)");
        lookScript = GetComponent<BossOneLookAtPlayer>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (canShoot && !player.GetComponent<PlayerHitDetection>().isHit)
        {            
            canShoot = false;
            int timeToLook = Random.Range(minTimeBetweenBursts, maxTimeBetweenBursts);
            shootCounter = Random.Range(minAmountOfShootsInOneBurst, maxAmountOfShootsInOneBurst);
            Invoke("shoot", timeToLook);
            Invoke("activateLookAtPlayer", timeToLook>0?timeToLook-1:timeToLook);
        }
    }

    private void shoot()
    {
        if (!player.GetComponent<PlayerHitDetection>().isHit)
        {
            movement.movementSpeed /= 1.5f;
            movement.rotationStrength /= 1.5f;
            animator.SetTrigger("Attacking");
            GameObject attack = Instantiate(prefab, GameObject.Find("Head").transform.position, Quaternion.identity);
            attack.GetComponent<Rigidbody>().AddForce(bulletSpeed * (player.transform.position - attack.transform.position), ForceMode.Impulse);
            shootCounter--;
            if (shootCounter >= 0)
            {
                animator.ResetTrigger("Attacking");
                movement.movementSpeed = movement.originalMovementSpeed;
                movement.rotationStrength = movement.originalRotationStrength;
                Invoke("shoot", timeBetweenShoots);
            }
            else
            {
                deactivateLookAtPlayer();
                canShoot = true;
            }
        }
        else
        {
            deactivateLookAtPlayer();
            canShoot = true;
        }
    }

    private void OnDisable()
    {
        activateLookAtPlayer();
        CancelInvoke("shoot");
        canShoot = true;
    }

    private void activateLookAtPlayer()
    {
        lookScript.canLookAtPlayer = true;
    }
    private void deactivateLookAtPlayer()
    {
        lookScript.canLookAtPlayer = false;
    }
}
