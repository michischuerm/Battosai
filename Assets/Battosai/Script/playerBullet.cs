using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBullet : MonoBehaviour {

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "enemy")
        {

        }
        else
        {
            Destroy(gameObject);
        }
    }
}
