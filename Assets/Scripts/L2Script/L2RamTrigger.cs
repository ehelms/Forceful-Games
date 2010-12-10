using UnityEngine;
using System.Collections;

public class L2RamTrigger : MonoBehaviour {

	private Level2ConsoleController L2Controller;
	
	// Use this for initialization
	void Start () {
		L2Controller = GameObject.Find("L2ConsoleTrigger").GetComponent("Level2ConsoleController") as Level2ConsoleController;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	/**
	We are interested in Ram hitting two things
	
	1. platform - when ram is lifted and dropped
	2. when it is dropped and picked up from cage.
	*/
	
	void OnTriggerEnter(Collider other) {
		// ok ram is hitting something, because it is being moved by the crane, inform crane about this
		if (L2Controller.GetObjectHeld() == GameObject.Find("L2Ram" + gameObject.name)) {
			L2Controller.HeldObjectHitSomething(gameObject, other.gameObject);
		}
	}
	
	void OnTriggerExit(Collider other) {
		L2Controller.OnRamTriggerExit(other);
	}
}
