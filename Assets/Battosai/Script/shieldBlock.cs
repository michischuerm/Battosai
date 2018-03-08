using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldBlock : MonoBehaviour {


    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "BossOneBreathAttack")
        {
            Destroy(other);
        }
    }
}
