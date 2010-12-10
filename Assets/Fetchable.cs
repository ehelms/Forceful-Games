using UnityEngine;
using System.Collections;

public class Fetchable : MonoBehaviour {

	public string ToolID;
	public string InfoMessage;
	
	private GameController Controller;
	
	// Use this for initialization
	void Start () {
		Controller = GameObject.Find("Controller").GetComponent("GameController") as GameController;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(transform.up * Time.deltaTime*20, Space.World);
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.name == "Player") {
			Controller.FoundTool(ToolID, InfoMessage);
			Destroy(this.gameObject);
			Destroy(GetComponent("Fetchable"));
		}
	}
	
}
