using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneLoadMachine : StateMachine {

	public string scene;
	public bool is_active = true;
	public bool is_spinUp = false;

	void Start(){
		Initiate ();
	}

	public override void InstanceInitiate(StateMachine checkMachine){
		if (SceneManager.GetActiveScene ().name == "PreLoad") {
			StartCoroutine ("SetFirstScene");
		}
	}
	public override void InstanceUpdate(StateMachine checkMachine){
		if (is_spinUp) {
			transform.Rotate (0, (Time.time - timerStart + Time.deltaTime)/2, 0);
			transform.position += new Vector3(0, (Time.time - timerStart + Time.deltaTime)/8000, 0);
			if (timerStart + timerDuration < Time.time) {
				is_active = true;
				is_spinUp = false;
			}
		}
		else if (is_active) {
			transform.Rotate (0, 300 * Time.deltaTime, 0);
		}
	}
	public override void InstanceInteract(GameObject obj, Vector3 point, StateMachine checkMachine){
		if (!is_active && !is_spinUp) {
			timerStart = Time.time;
			timerDuration = 10;
			StartSpin ();
		}
		if (is_active && !is_spinUp) {
			InputMachine.instance.recticle.SetReticle ("Default");
			InputMachine.instance.SetRoom (InputMachine.instance.myRoom);
			InputMachine.instance.mainUI.SetActive (false);
			InputMachine.instance.loadingUI.SetActive (true);
			SceneManager.LoadSceneAsync (scene);
		}
	}

	public void StartSpin(){
		is_spinUp = true;
	}

	IEnumerator SetFirstScene(){
		yield return null;
		InputMachine.instance.recticle.SetReticle ("Default");
		InputMachine.instance.SetRoom (InputMachine.instance.myRoom);
		InputMachine.instance.mainUI.SetActive (false);
		InputMachine.instance.loadingUI.SetActive (true);
		SceneManager.LoadSceneAsync (scene);
	}
}
