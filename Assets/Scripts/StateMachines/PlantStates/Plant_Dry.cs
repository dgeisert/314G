using UnityEngine;
using System.Collections;

public class Plant_Dry : StateMachine {

	public override void EnterState(StateMachine checkMachine){
		checkMachine.timerDuration = checkMachine.GetComponent<PlantMachine> ().growTime * 2;
		checkMachine.timerStart = Time.time;
	}
	public override void ExitState(StateMachine checkMachine){

	}

	public override void CheckUpdate(StateMachine checkMachine){
		if (checkMachine.timerStart + checkMachine.timerDuration <= Time.time) {
			checkMachine.UpdateState (StateMaster.instance.plantWithered, checkMachine);
		}
	}

	public override void InstanceInteract(GameObject obj, Vector3 point, StateMachine checkMachine){
		checkMachine.UpdateState (StateMaster.instance.plantGrowing, checkMachine);
	}
}