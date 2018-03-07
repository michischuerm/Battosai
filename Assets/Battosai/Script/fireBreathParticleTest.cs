using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireBreathParticleTest : MonoBehaviour
{
	ParticleSystem ps;
	// these lists are used to contain the particles which match
	// the trigger conditions each frame.
	List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
	List<ParticleSystem.Particle> inside = new List<ParticleSystem.Particle>();
	List<ParticleSystem.Particle> outside = new List<ParticleSystem.Particle>();
	List<ParticleSystem.Particle> exit = new List<ParticleSystem.Particle>();

	void OnEnable()
	{
		ps = GetComponent<ParticleSystem>();
	}

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	void OnParticleTrigger()
	{
		int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
		Debug.Log("numEnter " + numEnter);
		int numInside = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Inside, inside);
		Debug.Log("numInside " + numInside);
		int numExit = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);
		Debug.Log("numExit " + numExit);
		int numOutside = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Outside, outside);
		Debug.Log("numOutside " + numOutside);
	}
}
