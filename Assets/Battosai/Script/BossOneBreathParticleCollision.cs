using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOneBreathParticleCollision : MonoBehaviour {
    ParticleSystem ps;
    // these lists are used to contain the particles which match
    // the trigger conditions each frame.
    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
    private int numEnter = 0;
    private GameObject player;
    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
        player = GameObject.Find("Camera (eye)");
        //tells the particles they shall react, when triggered with the player
        ps.trigger.SetCollider(0, player.GetComponent<Collider>());
          
    }
    void OnParticleTrigger()
    {
        numEnter += ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        Debug.Log("numEnter " + numEnter);
        if(numEnter >= 6)
        {
            player.GetComponent<PlayerHitDetection>().gotHit();
            numEnter = 0;
        }
    }

    private void OnEnable()
    {
        numEnter = 0;
    }
}
