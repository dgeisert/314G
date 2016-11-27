using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.VR;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InputMachine: StateMachine {

	public Camera thisCamera;
	public RoomMachine currentRoom;
	public float renderDistance = 50;
	public float baseRenderDistance = 50;
	float lastRendererCheck;
	float lastSwipeTime;
	bool is_swipe = false;
	float backgroundMult = 0.032f;
	public float deltaTime;
	public TextMesh fpsText;
	public RecticleMachine recticle;
	public RoomMachine myRoom;
	public GameObject mainUI;
	public GameObject loadingUI;
	public GameObject blackout;
	public float maxDistance = 100f;
	public static float playerHeight = 4.5f;
	public float canvasDistance = 3f;
	public float canvasWidth = 3f;
	public float holdStart;
	public bool is_holding = false;
	public float holdingTimeSwipeNegation = 0.3f;
	public float waitToSpawnUI = 1;
	public static InputMachine instance;
	public InputMachine swipeUp, swipeForward, swipeBack, swipeDown;
	public List<GameObject> gos;
	public List<GameObject> grounds;
	public List<RoomMachine> rooms;

	void Start(){
		if (GetComponent<Camera> () != null) {
			Initiate ();
		}
	}

	public override void InstanceInitiate(StateMachine checkMachine){
		InputMachine.instance = this;
		if (thisCamera == null) {
			thisCamera = GetComponent<Camera> ();
		}
		OVRTouchpad.Create ();
		OVRTouchpad.TouchHandler += HandleTouchHandler;
		swipeUp = StateMaster.instance.inputUI;
		swipeDown = StateMaster.instance.inputInteract;
		swipeForward = StateMaster.instance.inputTeleport;
		swipeBack = StateMaster.instance.inputInteract;
		UpdateState (StateMaster.instance.inputTeleport, this);
		EventSystem.current.sendNavigationEvents = false;
		thisCamera.layerCullSpherical = true;
		Application.targetFrameRate = 240;
	}

	InputMachine GetCurrentState(){
		if (currentState == swipeUp) {
			return swipeUp;
		}
		if (currentState == swipeDown) {
			return swipeDown;
		}
		if (currentState == swipeForward) {
			return swipeForward;
		}
		if (currentState == swipeBack) {
			return swipeBack;
		}
		return StateMaster.instance.inputTutorial;
	}

	void HandleTouchHandler (object sender, System.EventArgs e)
	{
		OVRTouchpad.TouchArgs touchArgs = (OVRTouchpad.TouchArgs)e;
		if(touchArgs.TouchType == OVRTouchpad.TouchEvent.Up) {
			if (holdStart + holdingTimeSwipeNegation > Time.time) {
				GetCurrentState ().SwipeUp (GetSightedObject (), GetSightedPoint (), this);
				is_swipe = true;
				lastSwipeTime = Time.time;
			}
		}
		if(touchArgs.TouchType == OVRTouchpad.TouchEvent.Down) {
			if (holdStart + holdingTimeSwipeNegation > Time.time) {
				GetCurrentState().SwipeDown (GetSightedObject(), GetSightedPoint(), this);
				is_swipe = true;
				lastSwipeTime = Time.time;
			}
		}
		if(touchArgs.TouchType == OVRTouchpad.TouchEvent.Left) {
			if (holdStart + holdingTimeSwipeNegation > Time.time) {
				GetCurrentState().SwipeForward (GetSightedObject(), GetSightedPoint(), this);
				is_swipe = true;
				lastSwipeTime = Time.time;
			}
		}
		if(touchArgs.TouchType == OVRTouchpad.TouchEvent.Right) {
			if (holdStart + holdingTimeSwipeNegation > Time.time) {
				GetCurrentState().SwipeBack (GetSightedObject(), GetSightedPoint(), this);
				is_swipe = true;
				lastSwipeTime = Time.time;
			}
		}
	}

	public virtual void SwipeUp(GameObject obj, Vector3 point, StateMachine checkMachine){}
	public virtual void SwipeDown(GameObject obj, Vector3 point, StateMachine checkMachine){}
	public virtual void SwipeForward(GameObject obj, Vector3 point, StateMachine checkMachine){}
	public virtual void SwipeBack(GameObject obj, Vector3 point, StateMachine checkMachine){}
	public virtual void Tap(GameObject obj, Vector3 point, StateMachine checkMachine){}
	public virtual void Release(GameObject obj, Vector3 point, StateMachine checkMachine){}
	public virtual void Hold(GameObject obj, Vector3 point, StateMachine checkMachine){}


	Vector3 GetSightedPoint(){
		RaycastHit hit;
		Ray inputRay;
		if (VRDevice.isPresent) {
			inputRay = new Ray(transform.position, transform.forward);
		} else {
			inputRay = thisCamera.ScreenPointToRay (Input.mousePosition);
		}
		if (Physics.Raycast (inputRay, out hit, maxDistance)) {
			if (hit.transform != null) {
				return hit.point;
			} else {
				return transform.position + transform.forward * maxDistance;
			}
		} else {
			return transform.position + transform.forward * maxDistance;
		}
	}
	GameObject GetSightedObject(){
		RaycastHit hit;
		Ray inputRay;
		if (VRDevice.isPresent) {
			inputRay = new Ray(transform.position, transform.forward);
		} else {
			inputRay = thisCamera.ScreenPointToRay (Input.mousePosition);
		}
		Debug.Log (Physics.Raycast (inputRay, out hit, maxDistance));
		if (Physics.Raycast (inputRay, out hit, maxDistance)) {
			Debug.Log (hit);
			Debug.Log (hit.transform);
			if (hit.transform != null) {
				return hit.transform.gameObject;
			}
		}
		return null;
	}
	void Gaze(Vector3 point){
		recticle.transform.position = point;
	}

	public override void InstanceUpdate(StateMachine checkMachine){
		if (lastSwipeTime + 0.1f < Time.time) {
			is_swipe = false;
		}
		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			GetCurrentState().SwipeUp (GetSightedObject(), GetSightedPoint(), this);
		}
		if (Input.GetKeyDown(KeyCode.DownArrow)) {
			GetCurrentState().SwipeDown (GetSightedObject(), GetSightedPoint(), this);
		}
		if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			GetCurrentState().SwipeBack (GetSightedObject(), GetSightedPoint(), this);
		}
		if (Input.GetKeyDown(KeyCode.RightArrow)) {
			GetCurrentState().SwipeForward (GetSightedObject(), GetSightedPoint(), this);
		}
		if (Input.GetMouseButtonDown(0) && !is_swipe) {
			is_holding = true;
			holdStart = Time.time;
			GetCurrentState().Tap (GetSightedObject(), GetSightedPoint(), this);
		}
		if (Input.GetMouseButtonUp(0)) {
			is_holding = false;
			if (!is_swipe) {
				GetCurrentState ().Release (GetSightedObject (), GetSightedPoint (), this);
			}
		}
		if (is_holding) {
			GetCurrentState().Hold (GetSightedObject(), GetSightedPoint(), this);
		}
		if(Input.GetKeyDown(KeyCode.A)){
			PlayerMachine.playerObject.transform.Rotate (0, -45, 0);
		}
		if(Input.GetKeyDown(KeyCode.D)){
			PlayerMachine.playerObject.transform.Rotate (0, 45, 0);
		}
		Gaze (GetSightedPoint ());
		currentState.InstanceUpdate (this);
		if (InputMachine.instance != this) {
			return;
		}
		if (lastRendererCheck + 0.5f < Time.time) {
			CheckObjects();
		}
		deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
		float fps = 1.0f / deltaTime;
		fpsText.text = string.Format("{0:0.} fps", fps);;
	}
	public void SpawnUI(GameObject prefab){
		Quaternion quat = new Quaternion ();
		quat.eulerAngles = new Vector3 (0, transform.rotation.eulerAngles.y, 0);
	}

	public void CheckObjects(){
		if (currentRoom == null) {
			lastRendererCheck = Time.time;
			foreach (GameObject go in gos) {
				float magnitude = Vector3.Magnitude (go.transform.lossyScale);
				if (go.GetComponent<MeshRenderer> () != null) {
					magnitude *= Vector3.Magnitude (go.GetComponent<MeshRenderer> ().bounds.size);
				}
				if ((renderDistance + magnitude) < Vector3.Distance (transform.position, go.transform.position)) {
					go.SetActive (false);
				} else {
					go.SetActive (true);
				}
			}
			foreach (GameObject go in grounds) {
				go.SetActive (true);
			}
		}
	}

	public void SetRoom(RoomMachine room){
		if (currentRoom != null) {
			currentRoom.gameObject.SetActive (false);
		}
		currentRoom = room;
		if (currentRoom == null) {
			CheckObjects ();
			blackout.SetActive (false);
		} else {
			foreach (GameObject go in gos) {
				go.SetActive (false);
			}
			foreach (GameObject go in grounds) {
				go.SetActive (false);
			}
			blackout.SetActive (true);
			currentRoom.gameObject.SetActive (true);
			Transform tr = currentRoom.transform;
			while(tr.parent != null){
				tr = tr.parent;
				tr.gameObject.SetActive (true);
			}
		}
	}

	public void SetFarClip(float maxDistance){
		renderDistance = maxDistance - 15;
		thisCamera.farClipPlane = maxDistance * 20;
		CheckObjects ();
	}
}
