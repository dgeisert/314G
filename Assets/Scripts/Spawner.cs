using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public GameObject foxPrefab;
	public float spread = 100;
	public int spawns = 100;

	// Use this for initialization
	void Start () {
		StartCoroutine ("Spawn");
	}
	public IEnumerator Spawn(){
		yield return null;
		yield return null;
		yield return null;
		yield return null;
		for (int i = 0; i < spawns; i++) {
			Vector3 newPos = transform.position + new Vector3((Random.value - 0.5f) * spread, 0.5f, (Random.value - 0.5f) * spread);
			GameObject go = (GameObject)GameObject.Instantiate (foxPrefab, newPos, Quaternion.identity);
			go.GetComponent<AnimalMachine> ().Initiate ();
			go.transform.SetParent (transform);
			InputMachine.instance.gos.Add (go);
		}
	}
}
