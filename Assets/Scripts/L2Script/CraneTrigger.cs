using UnityEngine;
using System.Collections;

public class CraneTrigger : MonoBehaviour {

	private Level2ConsoleController L2Controller;
	
	// Use this for initialization
	void Start () {
		L2Controller = GameObject.Find("L2ConsoleTrigger").GetComponent("Level2ConsoleController") as Level2ConsoleController;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
		// ignore collision detection ignore_crane tagged objectes
		if (other.gameObject.CompareTag("Ignore_Crane")) return;
		
		// same trigger attached more than one object. but check if the current instance is for CraneHandInternal
		if (gameObject.name == "CraneHandInternal") {
			string name = (other.gameObject.name);
			if (name[0] >= '1' && name[0] <= '5') { // are we hitting a ram?
				// yes
				GameObject RamObject = GameObject.Find("L2Ram" + other.gameObject.name);
				L2Controller.CraneGrab(RamObject);	// ask crane to grab the ram
			}
		}
	}
}
