using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOneWinder : MonoBehaviour {
    public Transform target;
    private Quaternion targetRotation;                  //Rotation to face the player
    private float str;                                  //multiplikation of rotation strength and time
    public float rotationStrength = 0.8f;               //Strength of the rotation
    private float distanceToWindingPosition;
    crankBack handle;
    private void Start()
    {
        handle = GameObject.Find("Handle").GetComponent<crankBack>();
    }

    // Update is called once per frame
    void FixedUpdate () {
        MoveEnemy();
    }
    private void MoveEnemy()
    {
        distanceToWindingPosition = Vector3.Distance(target.position, transform.position);
       // Debug.Log((target.position - transform.position) * handle.fixedUpdateDistanceDelta / 100);
        //Move to the target !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!   1 muss durch die kurbel ersetzt werden, wenn nicht gekurbelt dann 0
        transform.position += (target.position- transform.position) * handle.fixedUpdateDistanceDelta/65;
        if(distanceToWindingPosition <= 2)
        {            
            GetComponent<BossOneStateHandler>().changeState(4);
        }

        //Rotate to face the target
        targetRotation = Quaternion.LookRotation(-target.position + transform.position);
        str = Mathf.Min(rotationStrength * Time.deltaTime, 1);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, str);
    }
}
