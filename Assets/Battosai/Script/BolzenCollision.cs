using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolzenCollision : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "enemy" || other.tag == "enemyWeakSpot")
        {
            other.GetComponent<BossOneStateHandler>().changeState(3);
        }
    }
}
