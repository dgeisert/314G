using UnityEngine;
using System.Collections;

public class Input_Interact : InputMachine {

	public override void CheckUpdate(StateMachine checkMachine){
		//checkMachine.UpdateState (StateMaster.instance.animalRunningAway, checkMachine);
	}
	public override void InstanceUpdate(StateMachine checkMachine){
		if (!InputMachine.instance.is_holding) {
			InputMachine.instance.recticle.SetReticle ("none");
			InputMachine.instance.recticle.right.SetHand ("Interact");
		}
	}

	public override void ExitState(StateMachine checkMachine){
		InputMachine.instance.recticle.right.SetHand ("none");
	}
	public override void EnterState(StateMachine checkMachine){
		InputMachine.instance.timerDuration = 0.3f;
		InputMachine.instance.timerStart = Time.time;
		InputMachine.instance.recticle.SetReticle ("none");
		InputMachine.instance.recticle.right.SetHand ("Interact");
	}

	public override void SwipeUp(GameObject obj, Vector3 point, StateMachine checkMachine){
		checkMachine.UpdateState (InputMachine.instance.swipeUp, checkMachine);
	}
	public override void SwipeDown(GameObject obj, Vector3 point, StateMachine checkMachine){
		checkMachine.UpdateState (InputMachine.instance.swipeDown, checkMachine);
	}
	public override void SwipeForward(GameObject obj, Vector3 point, StateMachine checkMachine){
		checkMachine.UpdateState (InputMachine.instance.swipeForward, checkMachine);
	}
	public override void SwipeBack(GameObject obj, Vector3 point, StateMachine checkMachine){
		checkMachine.UpdateState (InputMachine.instance.swipeBack, checkMachine);
	}
	public override void Tap(GameObject obj, Vector3 point, StateMachine checkMachine){}
	public override void Hold(GameObject obj, Vector3 point, StateMachine checkMachine){
		if (obj == null) {
			InputMachine.instance.recticle.SetReticle ("NoInteract");
			InputMachine.instance.recticle.right.SetHand ("Interact");
			return;
		}
		if (obj.GetComponentInParent<Ground> () != null) {
			InputMachine.instance.recticle.SetReticle ("NoInteract");
			InputMachine.instance.recticle.right.SetHand ("Interact");
			return;
		}
		if (obj.GetComponentInParent<StateMachine> () == null) {
			InputMachine.instance.recticle.SetReticle ("NoInteract");
			InputMachine.instance.recticle.right.SetHand ("Interact");
			return;
		}
		InputMachine.instance.recticle.SetReticle ("Interact");
		InputMachine.instance.recticle.right.SetHand ("none");
	}
	public override void Release(GameObject obj, Vector3 point, StateMachine checkMachine){
		if (InputMachine.instance.timerStart + InputMachine.instance.timerDuration > Time.time) {
			return;
		}
		if (obj == null) {
			InputMachine.instance.recticle.SetReticle ("NoInteract");
			InputMachine.instance.recticle.right.SetHand ("Interact");
			return;
		}
		if (obj.GetComponent<Ground>() != null) {
			return;
		} else if (obj.transform.parent.GetComponent<Ground> () != null) {
			InputMachine.instance.recticle.SetReticle ("NoInteract");
			InputMachine.instance.recticle.right.SetHand ("Interact");
			return;
		}
		if (obj.GetComponent<StateMachine> () == null) {
			if (obj.GetComponentInParent<StateMachine> () != null) {
				obj.GetComponentInParent<StateMachine> ().Interact (obj, point);
			}
			return;
		}
		obj.GetComponent<StateMachine> ().Interact (obj, point);
	}
}
