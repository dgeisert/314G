using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovingGround : MonoBehaviour {

	public Vector2 layout = new Vector2 (2, 2);
	public Dictionary<Vector2, Transform> ground = new Dictionary<Vector2, Transform>();
	public float squareScale = 100;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < layout.x*layout.y; i++) {
			Transform child = transform.GetChild (i);
			if (child != null) {
				ground.Add(new Vector2(Mathf.RoundToInt(child.position.x/squareScale),Mathf.RoundToInt(child.position.z/squareScale)), child);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (PlayerMachine.playerObject != null) {
			foreach (KeyValuePair<Vector2, Transform> kvp in ground) {
				Transform tile = kvp.Value;
				if (PlayerMachine.playerObject.transform.position.x - tile.position.x > squareScale) {
					tile.position += new Vector3 (squareScale * 2, 0, 0);
				}
				if (PlayerMachine.playerObject.transform.position.z - tile.position.z > squareScale) {
					tile.position += new Vector3 (0, 0, squareScale * 2);
				}
				if (PlayerMachine.playerObject.transform.position.x - tile.position.x < -squareScale) {
					tile.position -= new Vector3 (squareScale * 2, 0, 0);
				}
				if (PlayerMachine.playerObject.transform.position.z - tile.position.z < -squareScale) {
					tile.position -= new Vector3 (0, 0, squareScale * 2);
				}
			}
		}
	}
}
