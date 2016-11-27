using UnityEngine;
using System.Collections;

public class FoxTutorial : MonoBehaviour {

	public AnimalMachine fox;
	public WaypointHolder wph;
	public SceneLoadMachine gem;
	bool is_disappearing = false;

	public void StopCrying(){
		wph.Next ();
		fox.UpdateState (StateMaster.instance.animalFollowing, fox);
	}

	void Update(){
		if (gem.is_spinUp && !is_disappearing) {
			StartCoroutine ("Disappear");
			is_disappearing = true;
		}
	}

	IEnumerator Disappear(){
		yield return new WaitForSeconds (2);
		Destroy (gameObject);
	}
}
