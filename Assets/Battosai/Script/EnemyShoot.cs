using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour {
    public GameObject prefab;
    // Use this for initialization
    void Start () {
          
   }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject attack = Instantiate(prefab, transform.position, Quaternion.identity);
            attack.GetComponent<Rigidbody>().AddForce((GameObject.FindGameObjectWithTag("shootMe").transform.position- attack.transform.position), ForceMode.Impulse);
        }
    }
}
