using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPHandler : MonoBehaviour {
    public int hp = 100;
    public int stageTwo = 50;
    public int stageThree = 10;

    private int startHp;
    private bool bossTwoHasToChange = true;
    private void Start()
    {
        startHp = hp;
    }
    public void takeDamage(int damage)
    {

        //change boss one's boss states
        if (name.Contains("BossOne"))
        {
            BossOneStateHandler stateHandler = GetComponent<BossOneStateHandler>();
            if ((stateHandler.state != 3 && stateHandler.state != 0 && hp > stageTwo) || stateHandler.state == 4)
            {
                hp -= damage;
                GetComponent<changeMaterial>().startSwap();
            }
            if (hp <= stageTwo && stateHandler.state == 1)
            {
                stateHandler.changeState(2);
            }
            if (hp <= stageThree && stateHandler.state == 4)
            {
                stateHandler.changeState(5);
            }
        }
        //Destroy boss2 illusions
        else if (name.Contains("Illusion"))
        {
            hp -= damage;
            if (hp <= 0)
            {
                GetComponent<BossTwoIllusionShoot>().enabled = false;
                Animator anim = GetComponentInChildren<Animator>();
                anim.SetTrigger("IsTransitioning");
                anim.SetTrigger("IsDying");
                Invoke("destroyThisGameobject", 5);
            }
        }
        //boss 2 starts too spawn illusions
        else if (name == "BossTwoPrefab")
        {
            if (hp != startHp) { 
                hp -= damage;
                if (hp <= stageTwo && hp > stageThree && bossTwoHasToChange)
                {
                    bossTwoHasToChange = false;
                    GetComponent<BossTwoNavMesh>().spawnIllusion();
                }
                if (hp <= 0)
                {
                    Animator anim = GetComponentInChildren<Animator>();
                    anim.SetTrigger("IsTransitioning");
                    anim.SetTrigger("IsDying");
                    Debug.Log("bossIsDead");
                }
            }
            else if (GetComponent<BossTwoNavMesh>().getIntroIsFinished())
            {
                hp-= damage;
            }
        }
    }

    private void destroyThisGameobject()
    {
        Destroy(gameObject);
    }
}
