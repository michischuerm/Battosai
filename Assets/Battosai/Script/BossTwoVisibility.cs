using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTwoVisibility : MonoBehaviour {
    public string makeVisibleObjectParent;
    private List<SkinnedMeshRenderer> visibilityToggleObjects = new List<SkinnedMeshRenderer>();
    private Animator anim;
    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        foreach (SkinnedMeshRenderer renderer in GameObject.Find(makeVisibleObjectParent).GetComponentsInChildren<SkinnedMeshRenderer>())
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
        anim.SetTrigger("IsAttacking");
        anim.speed += .5f;
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
        anim.ResetTrigger("IsAttacking");
        anim.speed -= .5f;
    }
}
