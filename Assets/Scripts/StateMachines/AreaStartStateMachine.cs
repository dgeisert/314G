using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AreaStartStateMachine : StateMachine {

	public GameObject eventSystem;
	public GameObject usedRig;
	public RoomMachine startRoom;
	public void Awake(){
		Initiate ();
	}

	public override void CheckUpdate(StateMachine checkMachine){
		
	}

	public override void ExitState(StateMachine checkMachine){}
	public override void EnterState(StateMachine checkMachine){}
	public override void InstanceInitiate(StateMachine checkMachine){
		if (eventSystem == null) {
			eventSystem = (GameObject) Resources.Load("EventSystem", typeof(GameObject));
		}
		GameObject.Instantiate (eventSystem);
		if (PlayerMachine.playerObject == null) {	
			if (usedRig == null) {
				usedRig = (GameObject) Resources.Load("UsedRig", typeof(GameObject));
			}
			Debug.Log (usedRig);
			GameObject.Instantiate (usedRig, transform.position + Vector3.up * InputMachine.playerHeight, transform.rotation);
		} else {
			PlayerMachine.playerObject.transform.position = transform.position + Vector3.up * InputMachine.playerHeight;
			PlayerMachine.playerObject.transform.rotation = transform.rotation;
			InputMachine.instance.UpdateState (InputMachine.instance.swipeForward, checkMachine);
		}
		StartCoroutine ("SetGos");	
	}
	IEnumerator SetGos(){
		yield return null;
		InputMachine.instance.gos = new List<GameObject> ();
		//actually put a list of gameobjects together that will be watched for rendering when close enough
		foreach (GameObject go in FindObjectsOfType<GameObject> ()) {
			if (go.GetComponent<Ground> () != null
				&& go.GetComponentInParent<PlayerMachine>() == null
				&& go.GetComponentInParent<AreaStartStateMachine> () == null
				&& go.GetComponentInParent<RoomMachine> () == null) {
				InputMachine.instance.grounds.Add (go);
			} else if (go.GetComponent<MovingGround> () == null
				&& go.GetComponent<Light> () == null
				&& go.GetComponentInParent<PlayerMachine>() == null
				&& go.GetComponentInParent<Ground> () == null
				&& go.GetComponentInParent<AreaStartStateMachine> () == null
				&& go.GetComponentInParent<RoomMachine> () == null) {
				InputMachine.instance.gos.Add (go);
			}
		}
		foreach (RoomMachine room in FindObjectsOfType<RoomMachine> ()) {
			InputMachine.instance.rooms.Add (room);
			room.gameObject.SetActive (false);
		}
		InputMachine.instance.SetRoom (startRoom);
	}
}