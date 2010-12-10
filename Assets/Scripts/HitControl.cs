using UnityEngine;
using System.Collections;

public class HitControl : MonoBehaviour {

	private Level2ConsoleController L2Controller;
	
	// Use this for initialization
	void Start () {
		L2Controller = GameObject.Find("L2ConsoleTrigger").GetComponent("Level2ConsoleController") as Level2ConsoleController;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other) {
		if (gameObject.name == "Balance") {
			if (other.gameObject.name == "CraneHand") {
				
			}
		}
	}
}
