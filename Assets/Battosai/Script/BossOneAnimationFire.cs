using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOneAnimationFire : MonoBehaviour {
	public Animator animator;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("space"))
		{
			animator.SetTrigger("IsAttacking");
		}
		if (Input.GetKeyDown("1"))
		{
			animator.SetTrigger("IsTransitioning");
		}

		if (Input.GetKeyDown("2"))
		{
           animator.SetTrigger("IsTaunting");
		}
			if (Input.GetKeyDown("3"))
		{
           animator.SetTrigger("IsDying");
		}
	}
}
