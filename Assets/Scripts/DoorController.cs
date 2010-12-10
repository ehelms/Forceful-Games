using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour {

	public bool isLocked;
	
	private MeshRenderer meshRenderer;
	
	public Material DoorOpen;
	public Material DoorClose;
	
	private const int CLOSED = 0;
	private const int OPENING = 1;
	private const int CLOSING = 2;
	private const int OPENED = 3;
	
	private int state = CLOSED;
	
	public float OpenZ;
	public float CloseZ;
	
	// Use this for initialization
	void Start () {
		GameController Controller = GameObject.Find("Controller").GetComponent("GameController") as GameController;
		
		isLocked = !Controller.isCheating();
		state = CLOSED;
		meshRenderer = GetComponent("MeshRenderer") as MeshRenderer;
	}
	
	// Update is called once per frame
	void Update () {
		float vel = Time.deltaTime*1.5f;
		switch(state) {
			case OPENING:
				transform.Translate(0, 0, vel);
				if (transform.position.z >= OpenZ) {
					state = OPENED;
				}
			break;
			case CLOSING:
				transform.Translate(0, 0, -vel);
				if (transform.position.z <= CloseZ) {
					state = CLOSED;
				}
			break;
		}
	}
	
	public bool Open() {
		return state == OPENED;
	}
	
	public bool Locked() {
		return isLocked;
	}
	
	public void LockDoor() {
		isLocked = false;
		meshRenderer.material = DoorClose;
	}
	
	public void UnlockDoor() {
		isLocked = false;
		meshRenderer.sharedMaterial = DoorOpen;
	}
	
	public void OpenDoor() {
		state = OPENING;
	}
	
	public bool isClosing() {
		return state == CLOSING;
	}
	
	public void CloseDoor() {
		state = CLOSING;
	}

}
