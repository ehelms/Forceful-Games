using UnityEngine;
using System.Collections;

public class MoveNewtonLevel2Trigger : MonoBehaviour {

	private NewtonLevel1Controller newtonLevel1Controller;
	
	// Use this for initialization
	void Start () {
		newtonLevel1Controller = GameObject.Find("Newton").GetComponent("NewtonLevel1Controller") as NewtonLevel1Controller;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other) {
		if( newtonLevel1Controller.getPosition() == NewtonLevel1Controller.Positions.LEVEL1 ){
			newtonLevel1Controller.moveNewton(NewtonLevel1Controller.Positions.LEVEL2);
		} 
	}
}