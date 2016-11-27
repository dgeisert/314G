using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RecticleMachine : StateMachine {

	public Dictionary<string, GameObject> reticles = new Dictionary<string, GameObject>();
	public HandMachine left, right;
	public bool is_nearObjects = false, is_onGround;
	string currentRecticleValue = "none";
	float groundTimerDuration = 0.05f, groundTimerStart;

	void Start(){
		Initiate ();
	}

	public override void InstanceInitiate(StateMachine checkMachine){
		timerDuration = groundTimerDuration;
		currentRecticleValue = "none";
		for (int i = 0; i < transform.childCount; i++) {
			reticles.Add(transform.GetChild (i).name.ToLower(), transform.GetChild (i).gameObject);
		}
		groundTimerStart = Time.time;
	}

	public void SetReticle(string setValue){
		if (reticles.ContainsKey (currentRecticleValue)) {
			reticles [currentRecticleValue].SetActive (false);
		}
		if(reticles.ContainsKey(setValue.ToLower())){
			currentRecticleValue = setValue.ToLower();
			reticles [currentRecticleValue].SetActive (true);
		}
	}

	public override void InstanceUpdate(StateMachine checkMachine) {
		Vector3 targetLocation = transform.position + 2 * (transform.position - PlayerMachine.playerObject.transform.position);
		transform.LookAt (new Vector3(targetLocation.x, transform.position.y, targetLocation.z));
		if (timerStart + timerDuration < Time.time) {
			is_nearObjects = false;
		}
		if (groundTimerStart + groundTimerDuration < Time.time) {
			is_onGround = false;
		}
	}

	public void OnTriggerStay(Collider col){
		if (col.GetComponent<Ground> () == null && col.GetComponent<PlayerMachine> () == null) {
			timerStart = Time.time;
			is_nearObjects = true;
		}
		if (col.GetComponent<Ground> () != null) {
			groundTimerStart = Time.time;
			is_onGround = true;
		}
	}
}