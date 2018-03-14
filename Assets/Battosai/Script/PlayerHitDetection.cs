using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitDetection : MonoBehaviour {
    public GameObject prefabSafeSphere;
    private GameObject sphere;
    private float currentSphereSize = .1f;
    private float startSphereSize = .1f;
    public float sphereGrowSpeed = 0.02f;
    private float maxSphereSize = 1f;
    public float timeTillSphereIsDestroyedAfterMaxSize = 1f;
    private float timeTillSphereIsDestroyedCounter = 0f;
    public int hp = 3;
    public bool isHit = false;

    void OnParticleTrigger()
    {
        Debug.Log("trigger with particle");
    }
    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "makeDamage")
        {
            gotHit();
        }
    }

    private void Update()
    {
        if(sphere != null && currentSphereSize <= maxSphereSize)
        {
            currentSphereSize += Time.deltaTime * sphereGrowSpeed;
            sphere.transform.localScale = new Vector3(currentSphereSize, currentSphereSize, currentSphereSize);
        }
        else if(sphere != null && timeTillSphereIsDestroyedCounter < timeTillSphereIsDestroyedAfterMaxSize)
        {
            timeTillSphereIsDestroyedCounter += Time.deltaTime;
        }
        else if(sphere != null)
        {
            timeTillSphereIsDestroyedCounter = 0f;
            isHit = false;
            Destroy(sphere);
            currentSphereSize = startSphereSize;
        }
    }

    public void gotHit()
    {
        hp--;
        if (hp <= 0)
        {
            //Teleport player back where he found the weapons and let him pick them up again to get teleported to the fight
            Debug.Log("you lose");
        }
        else
        {
            isHit = true;
            sphere = Instantiate(prefabSafeSphere, transform.position, new Quaternion(1, 0, 0, 1));
            sphere.transform.parent = this.transform;
            sphere.transform.localScale = new Vector3(currentSphereSize, currentSphereSize, currentSphereSize);
            foreach (GameObject threat in GameObject.FindGameObjectsWithTag("makeDamage"))
            {
                if (!threat.name.Contains("BossT"))
                {
                    Destroy(threat);
                }                
            }
        }
    }
}
