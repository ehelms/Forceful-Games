using UnityEngine;
using System.Collections;

public class MeasureWeightScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other) {
		string idx = other.gameObject.name;
		if (idx[0] >= '1' && idx[0] <= '5') { 
			// if weight balance hits any of the ram , just show its weight
			L2Ram ram = GameObject.Find("L2Ram" + idx).GetComponent("L2Ram") as L2Ram;
			ram.showText();
		}
	}
	
	void OnTriggerExit(Collider other) {
	}
}
