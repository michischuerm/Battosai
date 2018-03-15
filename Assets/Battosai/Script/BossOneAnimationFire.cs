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
			animator.SetTrigger("Attacking");
		}
		if (Input.GetKeyDown("1"))
		{
			animator.Play("Fly");
		}
		if (Input.GetKeyDown("2"))
		{
			animator.Play("Hover");
		}		
		if (Input.GetKeyDown("3"))
		{
			animator.Play("Death");
		}
		if (Input.GetKeyDown("4"))
		{
			animator.Play("RightWing");
		}
	}
}
