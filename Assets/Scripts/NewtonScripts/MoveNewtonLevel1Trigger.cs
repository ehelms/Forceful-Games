using UnityEngine;
using System.Collections;

public class MoveNewtonLevel1Trigger : MonoBehaviour {

	private NewtonIntroController newtonController;
	
	// Use this for initialization
	void Start () {
		newtonController = GameObject.Find("Newton").GetComponent("NewtonIntroController") as NewtonIntroController;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other) {
		if( newtonController.getPosition() == NewtonIntroController.Positions.LAB ){
			newtonController.moveNewton(NewtonIntroController.Positions.LEVEL1);
		} 
	}
}
