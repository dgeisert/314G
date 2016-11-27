using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

	public GameObject number;
	public GameObject cube1, cube2;

	void Initiate(){
		cube1 = (GameObject)GameObject.CreatePrimitive (PrimitiveType.Cube);
		cube1.transform.parent = transform;
		cube1.transform.localPosition = new Vector3 (0, 0.12f, 0.02f);
		cube1.transform.localScale = Vector3.one * 0.04f;
		cube2 = (GameObject)GameObject.CreatePrimitive (PrimitiveType.Cube);
		cube2.transform.parent = transform;
		cube2.transform.localPosition = new Vector3 (0, 0.04f, 0.02f);
		cube2.transform.localScale = Vector3.one * 0.04f;
	}
	public void SetTime(float setTime){
		if (setTime > 3600) {

		} else if (setTime <= 0) {
			TurnOff ();
		} else {
			
		}
	}
	public void TurnOff(){
		cube1.gameObject.SetActive (false);
		cube2.gameObject.SetActive (false);
	}
	public void StartTimer(float setTime){
		cube1.gameObject.SetActive (true);
		cube2.gameObject.SetActive (true);
		SetTime (setTime);
	}
}
