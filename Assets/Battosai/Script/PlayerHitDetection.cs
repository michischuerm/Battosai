using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitDetection : MonoBehaviour {
    public GameObject prefabSafeSphere;
    private GameObject sphere;
    private float currentSphereSize = 0.25f;
    public int hp = 3;

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "makeDamage")
        {
            hp--;
            if (hp <= 0)
            {
                Debug.Log("you lose");
            }else
            {
                sphere = Instantiate(prefabSafeSphere);
                sphere.transform.localScale = new Vector3(currentSphereSize, currentSphereSize, currentSphereSize);
            }
        }
    }

    private void Update()
    {
        if(sphere != null && currentSphereSize <=2)
        {
            currentSphereSize += Time.deltaTime*0.3f;
            sphere.transform.localScale = new Vector3(currentSphereSize, currentSphereSize, currentSphereSize);
        }
        else if(sphere != null)
        {
            Destroy(sphere);
            currentSphereSize = 0.25f;
        }
    }
}
