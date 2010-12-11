using UnityEngine;
using System.Collections;

public class MoveNewtonLevel2Trigger : MonoBehaviour {

	private NewtonLevel2Controller newtonLevel1Controller;
	
	// Use this for initialization
	void Start () {
		newtonLevel1Controller = GameObject.Find("Newton").GetComponent("NewtonLevel2Controller") as NewtonLevel2Controller;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other) {
		if( newtonLevel1Controller.getPosition() == NewtonLevel2Controller.Positions.LEVEL1 ){
			newtonLevel1Controller.moveNewton(NewtonLevel2Controller.Positions.LEVEL2);
		} 
	}
}