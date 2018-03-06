using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject prefab;
    public float bulletSpeed = 0.8f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject attack = Instantiate(prefab, transform.position, Quaternion.identity);
            attack.GetComponent<Rigidbody>().AddForce(bulletSpeed * (GameObject.Find("Camera (head)").transform.position - attack.transform.position), ForceMode.Impulse);
        }
    }
}
