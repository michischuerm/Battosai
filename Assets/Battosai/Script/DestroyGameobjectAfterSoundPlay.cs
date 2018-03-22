using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameobjectAfterSoundPlay : MonoBehaviour {

    private float totalTitmeBeforeDestroy;


	// Use this for initialization
	void Start () {
        var sound = this.GetComponent<AudioSource>();
        totalTitmeBeforeDestroy = sound.clip.length;
	}
	
	// Update is called once per frame
	void Update () {

        totalTitmeBeforeDestroy -= Time.deltaTime;
        if (totalTitmeBeforeDestroy <= 0f)
            Destroy(this.gameObject);
		
	}
}
