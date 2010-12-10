using UnityEngine;
using System.Collections;

public class BarrierTrigger : MonoBehaviour {

	
	private CageController cage;
	
	// Use this for initialization
	void Start () {
		cage = GameObject.Find("CageBase").GetComponent("CageController")  as CageController;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	public void OnTriggerEnter(Collider other) {
		if (other.tag == "Level2Barrier") {	
			cage.crash();
		}
	}

}
