using UnityEngine;
using System.Collections;

public class Plant_Fallow : StateMachine {

	public override void EnterState(StateMachine checkMachine){
		checkMachine.timerDuration = checkMachine.GetComponent<PlantMachine> ().growTime;
		checkMachine.timerStart = Time.time;
		checkMachine.phase = 0;
	}
	public override void ExitState(StateMachine checkMachine){

	}

	public override void CheckUpdate(StateMachine checkMachine){
		if (checkMachine.timerStart + checkMachine.timerDuration <= Time.time) {
			Destroy (checkMachine.gameObject);
		}
	}

	public override void InstanceInteract(GameObject obj, Vector3 point, StateMachine checkMachine){
		checkMachine.UpdateState (StateMaster.instance.plantPlanted, checkMachine);
	}
}
