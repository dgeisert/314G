using UnityEngine;
using System.Collections;

public class PlayerMachine : StateMachine {

	public static GameObject playerObject;

	void Awake () {
		PlayerMachine.playerObject = gameObject;
		DontDestroyOnLoad (gameObject);
	}
}
