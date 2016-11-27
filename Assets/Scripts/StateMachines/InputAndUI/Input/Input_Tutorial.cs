using UnityEngine;
using System.Collections;

public class Input_Tutorial : InputMachine {

	public override void CheckUpdate(StateMachine checkMachine){
		//checkMachine.UpdateState (StateMaster.instance.animalRunningAway, checkMachine);
	}/*
	public override void InstanceUpdate(StateMachine checkMachine){
		switch (checkMachine.GetComponent<InputMachine> ().canvasInstance.GetComponent<TutorialMachine> ().currentTutorialStep) {
		case 1:
		case 2:
			if (!checkMachine.GetComponent<InputMachine> ().is_holding) {
				checkMachine.GetComponent<InputMachine> ().recticle.SetReticle ("none");
				checkMachine.GetComponent<InputMachine> ().recticle.right.SetHand ("Interact");
			}
			break;
		case 3:
			if (!checkMachine.GetComponent<InputMachine> ().is_holding) {
				checkMachine.GetComponent<InputMachine> ().recticle.SetReticle ("none");
				checkMachine.GetComponent<InputMachine> ().recticle.right.SetHand ("Teleport");
			}
			break;
		default:
			break;
		}
	}

	public override void ExitState(StateMachine checkMachine){
		checkMachine.GetComponent<InputMachine> ().recticle.SetReticle ("none");
		checkMachine.GetComponent<InputMachine> ().recticle.right.SetHand ("none");
		Destroy (checkMachine.GetComponent<InputMachine> ().canvasInstance);
		checkMachine.GetComponent<InputMachine> ().SetFarClip (checkMachine.GetComponent<InputMachine> ().baseRenderDistance);
	}
	public override void EnterState(StateMachine checkMachine){
		checkMachine.GetComponent<InputMachine> ().timerDuration = 0.3f;
		checkMachine.GetComponent<InputMachine> ().timerStart = Time.time;
		checkMachine.GetComponent<InputMachine> ().recticle.SetReticle ("none");
		checkMachine.GetComponent<InputMachine> ().recticle.right.SetHand ("none");
		checkMachine.GetComponent<InputMachine>().SpawnUI (checkMachine.GetComponent<InputMachine>().tutorialUI);
		checkMachine.GetComponent<InputMachine> ().SetFarClip (10);
	}

	public override void SwipeUp(GameObject obj, Vector3 point, StateMachine checkMachine){
		switch (checkMachine.GetComponent<InputMachine> ().canvasInstance.GetComponent<TutorialMachine> ().currentTutorialStep) {
		case 4:
			if (!checkMachine.GetComponent<InputMachine> ().canvasInstance.GetComponent<TutorialMachine> ().NextTutorialStep ()) {
				checkMachine.UpdateState (StateMaster.instance.inputTeleport, checkMachine);
			} else {
				checkMachine.GetComponent<InputMachine> ().recticle.SetReticle ("none");
				checkMachine.GetComponent<InputMachine> ().recticle.right.SetHand ("Interact");
			}
			break;
		default:
			break;
		}
	}
	public override void SwipeDown(GameObject obj, Vector3 point, StateMachine checkMachine){
		switch (checkMachine.GetComponent<InputMachine> ().canvasInstance.GetComponent<TutorialMachine> ().currentTutorialStep) {
		case 0:
			if (!checkMachine.GetComponent<InputMachine> ().canvasInstance.GetComponent<TutorialMachine> ().NextTutorialStep ()) {
				checkMachine.UpdateState (StateMaster.instance.inputTeleport, checkMachine);
			} else {
				checkMachine.GetComponent<InputMachine> ().timerDuration = 0.3f;
				checkMachine.GetComponent<InputMachine> ().timerStart = Time.time;
				checkMachine.GetComponent<InputMachine> ().recticle.SetReticle ("none");
				checkMachine.GetComponent<InputMachine> ().recticle.right.SetHand ("Interact");
			}
			break;
		default:
			break;
		}
	}
	public override void SwipeForward(GameObject obj, Vector3 point, StateMachine checkMachine){
		switch (checkMachine.GetComponent<InputMachine> ().canvasInstance.GetComponent<TutorialMachine> ().currentTutorialStep) {
		case 2:
			if (!checkMachine.GetComponent<InputMachine> ().canvasInstance.GetComponent<TutorialMachine> ().NextTutorialStep ()) {
				checkMachine.UpdateState (StateMaster.instance.inputTeleport, checkMachine);
			} else {
				checkMachine.GetComponent<InputMachine> ().timerDuration = 0.3f;
				checkMachine.GetComponent<InputMachine> ().timerStart = Time.time;
				checkMachine.GetComponent<InputMachine> ().recticle.SetReticle ("none");
				checkMachine.GetComponent<InputMachine> ().recticle.right.SetHand ("Interact");
			}
			break;
		default:
			break;
		}
	}
	public override void SwipeBack(GameObject obj, Vector3 point, StateMachine checkMachine){
		if (checkMachine.GetComponent<InputMachine> ().canvasInstance.GetComponent<TutorialMachine> ().currentTutorialStep == 1) {

		}
	}
	public override void Tap(GameObject obj, Vector3 point, StateMachine checkMachine){}
	public override void Hold(GameObject obj, Vector3 point, StateMachine checkMachine){
		if (obj == null) {
			return;
		}
		switch (checkMachine.GetComponent<InputMachine> ().canvasInstance.GetComponent<TutorialMachine> ().currentTutorialStep) {
		case 1:
		case 2:
			if (obj.GetComponentInParent<TutorialMachine> () != null) {
				checkMachine.GetComponent<InputMachine> ().recticle.SetReticle ("Interact");
				checkMachine.GetComponent<InputMachine> ().recticle.right.SetHand ("none");
				return;
			}
			else {
				checkMachine.GetComponent<InputMachine> ().recticle.SetReticle ("NoInteract");
				checkMachine.GetComponent<InputMachine> ().recticle.right.SetHand ("Interact");
				return;
			}
			break;
		case 3:
			if (obj.GetComponentInParent<Ground> () != null) {
				if (obj.GetComponentInParent<Ground> ().phase == 100) {
					checkMachine.GetComponent<InputMachine> ().recticle.SetReticle ("Teleport");
					checkMachine.GetComponent<InputMachine> ().recticle.right.SetHand ("none");
				} else {
					checkMachine.GetComponent<InputMachine> ().recticle.SetReticle ("NoTeleport");
					checkMachine.GetComponent<InputMachine> ().recticle.right.SetHand ("Teleport");
				}
			} else {
				checkMachine.GetComponent<InputMachine> ().recticle.SetReticle ("NoTeleport");
				checkMachine.GetComponent<InputMachine> ().recticle.right.SetHand ("Teleport");
			}
			break;
		default:
			checkMachine.GetComponent<InputMachine> ().recticle.SetReticle ("none");
			checkMachine.GetComponent<InputMachine> ().recticle.right.SetHand ("none");
			break;
		}
	}
	public override void Release(GameObject obj, Vector3 point, StateMachine checkMachine){
		if (checkMachine.GetComponent<InputMachine> ().timerStart + checkMachine.GetComponent<InputMachine> ().timerDuration > Time.time) {
			return;
		}
		switch (checkMachine.GetComponent<InputMachine> ().canvasInstance.GetComponent<TutorialMachine> ().currentTutorialStep) {
		case 1:
			if (obj.GetComponentInParent<TutorialMachine> () != null) {
				if (!checkMachine.GetComponent<InputMachine> ().canvasInstance.GetComponent<TutorialMachine> ().NextTutorialStep ()) {
					checkMachine.UpdateState (StateMaster.instance.inputTeleport, checkMachine);
				}
				checkMachine.GetComponent<InputMachine> ().recticle.SetReticle ("none");
				checkMachine.GetComponent<InputMachine> ().recticle.right.SetHand ("none");
			}
			break;
		case 3:
			if (obj.GetComponentInParent<Ground> () != null) {
				if (obj.GetComponentInParent<Ground> ().phase == 100) {
					if (!checkMachine.GetComponent<InputMachine> ().canvasInstance.GetComponent<TutorialMachine> ().NextTutorialStep ()) {
						checkMachine.UpdateState (StateMaster.instance.inputTeleport, checkMachine);
					}
					PlayerMachine.playerObject.transform.position = point + transform.up * InputMachine.playerHeight;
					checkMachine.GetComponent<InputMachine> ().canvasInstance.GetComponent<TutorialMachine> ().transform.position 
					= checkMachine.transform.position + new Vector3(checkMachine.transform.forward.x, 0, checkMachine.transform.forward.z).normalized * checkMachine.GetComponent<InputMachine> ().canvasDistance;
					checkMachine.GetComponent<InputMachine> ().canvasInstance.transform.eulerAngles = new Vector3 (0, transform.rotation.eulerAngles.y - 180, 0);
					checkMachine.GetComponent<InputMachine> ().recticle.SetReticle ("none");
					checkMachine.GetComponent<InputMachine> ().recticle.right.SetHand ("none");
				}
			}
			break;
		default:
			break;
		}
	}
	*/
}
