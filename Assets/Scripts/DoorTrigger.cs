using UnityEngine;
using System.Collections;


public class DoorTrigger : MonoBehaviour {

	private DoorController doorController;
	private bool startLevel1 = true;
	
	public string whichDoor;
	
	// Use this for initialization
	void Start () {
		doorController = GameObject.Find(whichDoor).GetComponent("DoorController") as DoorController;
	}
	

	void Update () {
	
	}
	
		
	void OnTriggerEnter(Collider other) {
		if ( !doorController.Locked() && whichDoor == "Door2" ) {
			doorController.OpenDoor();
			if( startLevel1 ){
				startLevel1 = false;
			}
		} else if ( !doorController.Locked() && whichDoor == "Door1" ) {
			doorController.OpenDoor();
		} else if ( !doorController.Locked()  && whichDoor == "Door3" ) {
			doorController.OpenDoor();
		} else {
			audio.Play();
		}
	}
	
	void OnTriggerStay(Collider other) {
		if (!doorController.Locked() && doorController.isClosing() ) {
			doorController.OpenDoor();
		}
	}
	
	void OnTriggerExit(Collider other) {
		StartCoroutine("closeDoor");
	}
	
	IEnumerator  closeDoor () {
		yield  return new WaitForSeconds(3);
		doorController.CloseDoor();
	}
	
}
