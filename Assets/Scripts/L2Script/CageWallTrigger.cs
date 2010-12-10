using UnityEngine;
using System.Collections;

public class CageWallTrigger : MonoBehaviour {

	private Level2ConsoleController L2Controller;
	
	// Use this for initialization
	void Start () {
		L2Controller = GameObject.Find("L2ConsoleTrigger").GetComponent("Level2ConsoleController") as Level2ConsoleController;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider other) {
		//L2Controller.CollisionSoMoveUp();
	}
}
