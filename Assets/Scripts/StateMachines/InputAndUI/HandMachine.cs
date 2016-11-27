using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HandMachine : StateMachine {

	public Dictionary<string, GameObject> hands = new Dictionary<string, GameObject>();
	public bool is_nearObjects = false, is_onGround;
	string currentHandValue = "none";

	void Start(){
		Initiate ();
	}

	public override void InstanceInitiate(StateMachine checkMachine){
		currentHandValue = "none";
		for (int i = 0; i < transform.childCount; i++) {
			hands.Add(transform.GetChild (i).name.ToLower(), transform.GetChild (i).gameObject);
		}
	}

	public void SetHand(string setValue){
		if (hands.ContainsKey (currentHandValue)) {
			hands [currentHandValue].SetActive (false);
		}
		if(hands.ContainsKey(setValue.ToLower())){
			currentHandValue = setValue.ToLower();
			hands [currentHandValue].SetActive (true);
		}
	}

	public override void InstanceUpdate(StateMachine checkMachine) {
	}

	public void OnTriggerStay(Collider col){
	}
}
