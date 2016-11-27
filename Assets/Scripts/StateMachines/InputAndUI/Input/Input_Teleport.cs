using UnityEngine;
using System.Collections;

public class Input_Teleport : InputMachine {

	public override void CheckUpdate(StateMachine checkMachine){
	}
	public override void InstanceUpdate(StateMachine checkMachine){
		if (!InputMachine.instance.is_holding) {
			InputMachine.instance.recticle.SetReticle ("none");
			InputMachine.instance.recticle.right.SetHand ("Teleport");
		}
	}

	public override void ExitState(StateMachine checkMachine){
		InputMachine.instance.recticle.right.SetHand ("none");
	}
	public override void EnterState(StateMachine checkMachine){
		InputMachine.instance.timerDuration = 0.3f;
		InputMachine.instance.timerStart = Time.time;
		InputMachine.instance.recticle.SetReticle ("none");
		InputMachine.instance.recticle.right.SetHand ("Teleport");
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
	public override void Tap(GameObject obj, Vector3 point, StateMachine checkMachine){
		
	}
	public override void Hold(GameObject obj, Vector3 point, StateMachine checkMachine){
		if (obj == null) {
			InputMachine.instance.recticle.SetReticle ("NoTeleport");
			InputMachine.instance.recticle.right.SetHand ("Teleport");
		}
		else if (obj.GetComponent<Blocked>() != null) {
			InputMachine.instance.recticle.SetReticle ("NoTeleport");
			InputMachine.instance.recticle.right.SetHand ("Teleport");
		}
		else if (obj.GetComponent<Platform>() != null) {
			InputMachine.instance.recticle.SetReticle ("Teleport");
			InputMachine.instance.recticle.right.SetHand ("none");
		}
		else if (InputMachine.instance.recticle.is_nearObjects
			|| !InputMachine.instance.recticle.is_onGround) {
			InputMachine.instance.recticle.SetReticle ("NoTeleport");
			InputMachine.instance.recticle.right.SetHand ("Teleport");
		} else {
			InputMachine.instance.recticle.SetReticle ("Teleport");
			InputMachine.instance.recticle.right.SetHand ("none");
		}
	}
	public override void Release(GameObject obj, Vector3 point, StateMachine checkMachine){
		if (obj == null) {
			return;
		}
		if (obj.GetComponent<Blocked>() != null) {
			return;
		}
		if (InputMachine.instance.timerStart + InputMachine.instance.timerDuration > Time.time) {
			return;
		}
		else if (obj.GetComponent<Platform> ()) {
			PlayerMachine.playerObject.transform.position = point + transform.up;
		}
		else if (!InputMachine.instance.recticle.is_nearObjects
			&& InputMachine.instance.recticle.is_onGround) {
			PlayerMachine.playerObject.transform.position = point 
				+ PlayerMachine.playerObject.transform.up * InputMachine.playerHeight;
		}
	}
}
