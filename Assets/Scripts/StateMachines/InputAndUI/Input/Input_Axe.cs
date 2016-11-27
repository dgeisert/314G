using UnityEngine;
using System.Collections;

public class Input_Axe : InputMachine {

	public override void CheckUpdate(StateMachine checkMachine){
		//checkMachine.UpdateState (StateMaster.instance.animalRunningAway, checkMachine);
	}
	public override void InstanceUpdate(StateMachine checkMachine){
		if (!checkMachine.GetComponent<InputMachine>().is_holding) {
			checkMachine.GetComponent<InputMachine> ().recticle.SetReticle ("none");
			checkMachine.GetComponent<InputMachine> ().recticle.right.SetHand ("Axe");
		}
	}

	public override void ExitState(StateMachine checkMachine){
		checkMachine.GetComponent<InputMachine> ().recticle.right.SetHand ("none");
	}
	public override void EnterState(StateMachine checkMachine){
		checkMachine.GetComponent<InputMachine> ().timerDuration = 0.3f;
		checkMachine.GetComponent<InputMachine> ().timerStart = Time.time;
	}

	public override void SwipeUp(GameObject obj, Vector3 point, StateMachine checkMachine){
		checkMachine.UpdateState (checkMachine.GetComponent<InputMachine> ().swipeUp, checkMachine);
	}
	public override void SwipeDown(GameObject obj, Vector3 point, StateMachine checkMachine){
		checkMachine.UpdateState (checkMachine.GetComponent<InputMachine> ().swipeDown, checkMachine);
	}
	public override void SwipeForward(GameObject obj, Vector3 point, StateMachine checkMachine){
		checkMachine.UpdateState (checkMachine.GetComponent<InputMachine> ().swipeForward, checkMachine);
	}
	public override void SwipeBack(GameObject obj, Vector3 point, StateMachine checkMachine){
		checkMachine.UpdateState (checkMachine.GetComponent<InputMachine> ().swipeBack, checkMachine);
	}
	public override void Tap(GameObject obj, Vector3 point, StateMachine checkMachine){}
	public override void Hold(GameObject obj, Vector3 point, StateMachine checkMachine){
		if (obj == null) {
			checkMachine.GetComponent<InputMachine> ().recticle.SetReticle ("NoInteract");
			checkMachine.GetComponent<InputMachine> ().recticle.right.SetHand ("Axe");
			return;
		}
		if (obj.GetComponentInParent<Ground> () != null) {
			checkMachine.GetComponent<InputMachine> ().recticle.SetReticle ("NoInteract");
			checkMachine.GetComponent<InputMachine> ().recticle.right.SetHand ("Axe");
			return;
		}
		if (obj.GetComponentInParent<StateMachine> () == null) {
			checkMachine.GetComponent<InputMachine> ().recticle.SetReticle ("NoInteract");
			checkMachine.GetComponent<InputMachine> ().recticle.right.SetHand ("Axe");
			return;
		}
		checkMachine.GetComponent<InputMachine> ().recticle.SetReticle ("Axe");
		checkMachine.GetComponent<InputMachine> ().recticle.right.SetHand ("none");
	}
	public override void Release(GameObject obj, Vector3 point, StateMachine checkMachine){}
}