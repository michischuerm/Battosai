using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOneWinder : MonoBehaviour {
    public Transform target;
    private Vector3 originalPosition;
    private Quaternion targetRotation;                  //Rotation to face the player
    private float str;                                  //multiplikation of rotation strength and time
    public float rotationStrength = 0.8f;               //Strength of the rotation

    private void Start()
    {
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update () {
        MoveEnemy();
    }
    private void MoveEnemy()
    {
        Debug.Log(Vector3.Distance(target.position, transform.position));
        //Move to the target !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!   1 muss durch die kurbel ersetzt werden, wenn nicht gekurbelt dann 0
        transform.position += (target.position- transform.position) * 0.01f;
        if(Vector3.Distance(target.position, transform.position) <= 2)
        {            
            GetComponent<BossOneStateHandler>().changeState(4);
        }

        //Rotate to face the target
        targetRotation = Quaternion.LookRotation(-target.position + transform.position);
        str = Mathf.Min(rotationStrength * Time.deltaTime, 1);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, str);
    }
}
