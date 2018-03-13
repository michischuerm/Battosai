using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTwoVisibility : MonoBehaviour {
    private Renderer renderer;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
        renderer.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "makeVisible")
        {
            renderer.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "makeVisible")
        {
            renderer.enabled = false;
        }
    }
}
