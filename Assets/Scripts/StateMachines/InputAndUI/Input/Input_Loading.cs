using UnityEngine;
using System.Collections;

public class Input_Loading : InputMachine {

	public override void CheckUpdate(StateMachine checkMachine){
		//checkMachine.UpdateState (StateMaster.instance.animalRunningAway, checkMachine);
	}
	public override void InstanceUpdate(StateMachine checkMachine){
	}

	public override void ExitState(StateMachine checkMachine){
		InputMachine.instance.recticle.right.SetHand ("none");
	}
	public override void EnterState(StateMachine checkMachine){
		InputMachine.instance.recticle.SetReticle ("none");
	}

	public override void SwipeUp(GameObject obj, Vector3 point, StateMachine checkMachine){
	}
	public override void SwipeDown(GameObject obj, Vector3 point, StateMachine checkMachine){
	}
	public override void SwipeForward(GameObject obj, Vector3 point, StateMachine checkMachine){
	}
	public override void SwipeBack(GameObject obj, Vector3 point, StateMachine checkMachine){
	}
	public override void Tap(GameObject obj, Vector3 point, StateMachine checkMachine){}
	public override void Hold(GameObject obj, Vector3 point, StateMachine checkMachine){
		InputMachine.instance.recticle.SetReticle ("Default");
	}
	public override void Release(GameObject obj, Vector3 point, StateMachine checkMachine){}
}