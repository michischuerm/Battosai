using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPHandler : MonoBehaviour {
    public int hp = 100;
    public int stageTwo = 50;
    public int stageThree = 10;

    private bool bossTwoHasToChange = true;
    private void Start()
    {
        
    }
    public void takeDamage(int damage)
    {
        
        //change boss one's boss states
        if (name.Contains("BossOne"))
        {
            if((GetComponent<BossOneStateHandler>().state !=3 && hp > stageTwo) || GetComponent<BossOneStateHandler>().state==4)
            {
                hp -= damage;
            }            
            if (hp <= stageTwo && GetComponent<BossOneStateHandler>().state == 1)
            {
                GetComponent<BossOneStateHandler>().changeState(2);
            }
            if(hp <= stageThree && GetComponent<BossOneStateHandler>().state == 4)
            {
                GetComponent<BossOneStateHandler>().changeState(5);
            }
        }
        //Destroy boss2 illusions
        else if (name.Contains("Illusion"))
        {
            hp -= damage;
            if (hp <= 0)
            {
                Destroy(gameObject);
            }
        }
        //boss 2 starts too spawn illusions
        else if (name == "BossTwo")
        {
            hp -= damage;
            if (hp <= stageTwo && hp > stageThree && bossTwoHasToChange)
            {
                bossTwoHasToChange = false;
                GetComponent<BossTwoNavMesh>().spawnIllusion();
            }
        }
    }
}
