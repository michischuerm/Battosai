using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnvironmentCollision : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Arena" || (other.tag =="PlayerWeapons" && other.name =="playerShield"))
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Arena" || (collision.collider.tag == "PlayerWeapons" && collision.collider.name == "playerShield"))
        {
            Destroy(gameObject);
        }
    }
}
