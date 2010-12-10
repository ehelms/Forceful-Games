using UnityEngine;
using System.Collections;

public class ConsoleTrigger : MonoBehaviour {


	private ConsoleController gui;

	
	// Use this for initialization
	void Start () {
		gui = GameObject.Find("console").GetComponent("ConsoleController") as ConsoleController;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay (Collider other) {
		if (other.tag == "Player") {
			if (Input.GetKeyUp ("e")) {
				if (!gui.isLocked()) {
			 	 gui.activate();
				}
			}
			
		}
	}
	

	


}
