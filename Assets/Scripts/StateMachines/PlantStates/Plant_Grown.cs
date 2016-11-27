using UnityEngine;
using System.Collections;

public class Plant_Grown : StateMachine {

	public override void EnterState(StateMachine checkMachine){
		
	}
	public override void ExitState(StateMachine checkMachine){

	}

	public override void CheckUpdate(StateMachine checkMachine){
		if (checkMachine.timerStart + checkMachine.timerDuration <= Time.time) {

		}
	}

	public override void InstanceInteract(GameObject obj, Vector3 point, StateMachine checkMachine){
		checkMachine.UpdateState (StateMaster.instance.plantFallow, checkMachine);
	}
}
