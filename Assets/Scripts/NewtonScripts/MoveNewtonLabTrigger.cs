using UnityEngine;
using System.Collections;

public class MoveNewtonLabTrigger : MonoBehaviour {

	private NewtonIntroController newtonIntroController;
	
	// Use this for initialization
	void Start () {
		newtonIntroController = GameObject.Find("Newton").GetComponent("NewtonIntroController") as NewtonIntroController;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other) {
		if( newtonIntroController.getPosition() == NewtonIntroController.Positions.LEVEL1 ){
			newtonIntroController.moveNewton(NewtonIntroController.Positions.LAB);
		}
	}
}
