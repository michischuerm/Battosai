using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTwoVisibility : MonoBehaviour {
    public string makeVisibleObjectParent;
    private List<SkinnedMeshRenderer> visibilityToggleObjects = new List<SkinnedMeshRenderer>();

    private void Start()
    {        
        foreach(SkinnedMeshRenderer renderer in GameObject.Find(makeVisibleObjectParent).GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            visibilityToggleObjects.Add(renderer);
            renderer.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "makeVisible")
        {
            foreach (SkinnedMeshRenderer renderer in visibilityToggleObjects)
            {
                renderer.enabled = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "makeVisible")
        {
            foreach (SkinnedMeshRenderer renderer in visibilityToggleObjects)
            {
                renderer.enabled = false;
            }
        }
    }
}
