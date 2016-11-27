using UnityEngine;
using System.Collections;

public class StateMaster : MonoBehaviour {

	public StateMachine plantDry, plantFallow, plantGrown, plantGrowing, plantPlanted, plantWithered;
	public StateMachine animalRoaming, animalCrying, animalFollowing, animalRunningAway;
	public InputMachine inputUI, inputTeleport, inputInteract, inputTutorial
	, inputPickUp, inputShield, inputSword, inputAxe, inputInspect
	, inputBucket, inputHammer, inputInventory, inputLeaf
	, inputPickaxe, inputRope, inputSaw, inputShop, inputShovel, inputSickle;
	public HouseMachine houseOpen;
	public GameObject number, timer;

	public static StateMaster instance;

	public void Awake(){
		StateMaster.instance = this;
		plantDry = gameObject.AddComponent<Plant_Dry>();
		plantFallow = gameObject.AddComponent<Plant_Fallow>();
		plantGrowing = gameObject.AddComponent<Plant_Growing>();
		plantGrown = gameObject.AddComponent<Plant_Grown>();
		plantPlanted = gameObject.AddComponent<Plant_Planted>();
		plantWithered = gameObject.AddComponent<Plant_Withered>();

		animalCrying = gameObject.AddComponent<Animal_Crying>();
		animalFollowing = gameObject.AddComponent<Animal_Following>();
		animalRoaming = gameObject.AddComponent<Animal_Roaming>();
		animalRunningAway = gameObject.AddComponent<Animal_RunningAway>();

		inputUI = gameObject.AddComponent<Input_UI>();
		inputTeleport = gameObject.AddComponent<Input_Teleport>();
		inputInteract = gameObject.AddComponent<Input_Interact>();
		inputSword = gameObject.AddComponent<Input_Sword>();
		inputTutorial = gameObject.AddComponent<Input_Tutorial>();
		inputShield = gameObject.AddComponent<Input_Shield>();
		inputPickUp = gameObject.AddComponent<Input_PickUp>();
		inputAxe = gameObject.AddComponent<Input_Axe>();
		inputHammer = gameObject.AddComponent<Input_Hammer>();
		inputInventory = gameObject.AddComponent<Input_Inventory>();
		inputLeaf = gameObject.AddComponent<Input_Leaf>();
		inputPickaxe = gameObject.AddComponent<Input_Pickaxe>();
		inputRope = gameObject.AddComponent<Input_Rope>();
		inputSaw = gameObject.AddComponent<Input_Saw>();
		inputShop = gameObject.AddComponent<Input_Shop>();
		inputShovel = gameObject.AddComponent<Input_Shovel>();
		inputSickle = gameObject.AddComponent<Input_Sickle>();
		inputInspect = gameObject.AddComponent<Input_Inspect>();
		inputBucket = gameObject.AddComponent<Input_Bucket>();

		houseOpen = gameObject.AddComponent<House_Open> ();
	}
}
