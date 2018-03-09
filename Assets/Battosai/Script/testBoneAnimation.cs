using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testBoneAnimation : MonoBehaviour {
    public Transform lowerSpine;
    void LateUpdate()
    {
        // instead of changing rotation, change my rotation var:
        float spineSpin = 0;
        spineSpin += Input.GetAxis("Mouse X") * 10;
        lowerSpine.eulerAngles = new Vector3(0, spineSpin, 0);

        // complete guess. Math to apply overall model tilt to the spine:
        lowerSpine.rotation = transform.rotation * lowerSpine.rotation;
    }
}
