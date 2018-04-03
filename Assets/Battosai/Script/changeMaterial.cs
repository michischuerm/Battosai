using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeMaterial : MonoBehaviour {
    public Material oldMaterial;
    public Material newMaterial;
    public string changeChilds;
    public float timerTillChangeBack = 2f;
    public float blinkTime = 0.2f;
    private float stopTimer = 0;
    private float blinkTimer = 0;
    private bool changeBack = false;
    private bool changeTime = false;
    private GameObject parent;


    private void Start()
    {
        parent = GameObject.Find(name+"/"+changeChilds);
    }
    // Update is called once per frame
    void Update () {
        if (changeTime)
        {
            stopTimer += Time.deltaTime;
            blinkTimer += Time.deltaTime;
            if (stopTimer >= timerTillChangeBack)
            {                
                endSwap();
            }
            else if (blinkTimer >= blinkTime)
            {
                blinkTimer = 0f;
                if (changeBack)
                {
                    swapBack();
                }
                else
                {
                    swap();
                }
            }
        }
	}

    public void startSwap()
    {
        if (changeTime == false)
        {
            changeTime = true;
            changeBack = true;
            swap();
        }
    }

    private void swap()
    {
        changeBack = true;
        foreach (Renderer renderer in parent.GetComponentsInChildren<Renderer>())
        {
            renderer.material = newMaterial;
        }
    }

    private void endSwap()
    {
        swapBack();
        changeTime = false;
        stopTimer = 0;
    }

    private void swapBack()
    {
        changeBack = false;
        foreach (Renderer renderer in parent.GetComponentsInChildren<Renderer>())
        {
            renderer.material = oldMaterial;
        }
    }
}