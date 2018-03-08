using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPHandler : MonoBehaviour {
    public int hp = 100;
    public int stageTwo = 50;
    public int stageThree = 10;
    private void Start()
    {
        
    }
    public void takeDamage(int damage)
    {
        hp-=damage;
        if(hp <= stageTwo && hp > stageThree && GetComponent<BossOneStateHandler>().state != 2)
        {
            GetComponent<BossOneStateHandler>().changeState(2);
        }
    }

}
