using UnityEngine;
using System.Collections;

public class Plant_Growing : StateMachine {

	public override void EnterState(StateMachine checkMachine){
		checkMachine.timerDuration = checkMachine.GetComponent<PlantMachine> ().growTime;
		checkMachine.timerStart = Time.time;
	}
	public override void ExitState(StateMachine checkMachine){
		if (checkMachine.timerObject != null) {
			Destroy (checkMachine.timerObject.gameObject);
		}
	}

	public override void CheckUpdate(StateMachine checkMachine){
		if (checkMachine.timerStart + checkMachine.timerDuration <= Time.time) {
			if (checkMachine.GetComponent<PlantMachine> ().growPhases > checkMachine.phase) {
				checkMachine.phase++;
				checkMachine.UpdateState (StateMaster.instance.plantDry, checkMachine);
			} else {
				checkMachine.UpdateState (StateMaster.instance.plantGrown, checkMachine);
			}
		}
		if (checkMachine.timerObject != null) {
			checkMachine.timerObject.SetTime (checkMachine.timerStart + checkMachine.timerDuration - Time.time);
		}
	}

	public override void InstanceInteract(GameObject obj, Vector3 point, StateMachine checkMachine){
		if (checkMachine.timerObject == null) {
			GameObject timer = (GameObject)GameObject.Instantiate (StateMaster.instance.timer);
			timer.transform.parent = checkMachine.transform;
			checkMachine.timerObject = timer.GetComponent<Timer> ();
			checkMachine.timerObject.StartTimer (checkMachine.timerStart + checkMachine.timerDuration - Time.time);
		} else {
			checkMachine.timerObject.SetTime (checkMachine.timerStart + checkMachine.timerDuration - Time.time);
		}
	}
}
