using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBullet : MonoBehaviour {
    public int damage = 2;
    public int weakSpotDamage = 4;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemy")
        {
            other.transform.root.GetComponent<EnemyHPHandler>().takeDamage(damage);
            gameObject.SetActive(false);
        }
        else if(other.tag == "enemyWeakSpot")
        {
            other.transform.root.GetComponent<EnemyHPHandler>().takeDamage(weakSpotDamage);
            gameObject.SetActive(false);
        }      
    }

    private void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
    }
}
