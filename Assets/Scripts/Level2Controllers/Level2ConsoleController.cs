using UnityEngine;
using System.Collections;

public class Level2ConsoleController : MonoBehaviour {
	
	private bool UsingController;
	private GameObject Player;
	private GameController GController;
	
	private bool NearController = false;
	private bool ShowingControllerUI = false;
	
	private GameObject CraneLRHandle;
	private GameObject ObjectHeld = null;
	private GameObject Piston2;
	private CageController cage;
	
	private L2ProblemController L2PC;
	
	enum CraneHandStates {
		STATIONARY_DOWN, MOVING_DOWN, MOVING_UP, STATIONARY_UP
	};
	
	private CraneHandStates CraneHandState = CraneHandStates.STATIONARY_UP;
	
	
	// Use this for initialization
	void Start () {
		Player = GameObject.Find("Player");
		GController = GameObject.Find("Controller").GetComponent("GameController") as GameController;
		cage = GameObject.Find("CageBase").GetComponent("CageController")  as CageController;
		L2PC = GameObject.Find("FlowControllerObject").GetComponent("L2ProblemController") as L2ProblemController;
		CraneLRHandle = GameObject.Find("L2CraneTopBlockLR");
		Piston2 = GameObject.Find("Piston2");
	}
	
	// Update is called once per frame
	void Update () {
		if (!NearController) return;
		
		
		if(Input.GetKeyUp(KeyCode.T)) {
			L2ProblemController l2Controller= GameObject.Find("FlowControllerObject").GetComponent("L2ProblemController") as L2ProblemController;
			l2Controller.flingCage();
		}
		
		if(Input.GetKeyUp(KeyCode.E)) {
			ShowingControllerUI = !ShowingControllerUI;
			if (ShowingControllerUI) {
				GController.HideInfoBox();
				GController.DisablePlayer();
				GController.ShowMousePointer();
				GController.ShowInfoBox(L2PC.GetProblemText() + "\nPress E again to release the Controller.  Press T to fling crate.");
			}
			else {
				GController.EnablePlayer();
				GController.HideMousePointer();
			}
		}
		
		if (!ShowingControllerUI)  return;
		
		if (CraneHandState == CraneHandStates.STATIONARY_UP && Input.GetKey(KeyCode.LeftArrow) && CraneLRHandle.transform.position.z < 14.0f) {
			CraneLRHandle.transform.Translate(0, 0, 2*Time.deltaTime);
			if (ObjectHeld != null) {
				ObjectHeld.transform.Translate(-2*Time.deltaTime, 0, 0);
			}
		}
		else if(CraneHandState == CraneHandStates.STATIONARY_UP && Input.GetKey(KeyCode.RightArrow) && CraneLRHandle.transform.position.z > -2.0f)  {
			CraneLRHandle.transform.Translate(0, 0, -2*Time.deltaTime);
			if (ObjectHeld != null) {
				ObjectHeld.transform.Translate(2*Time.deltaTime, 0, 0);
			}
		}
		
		if (Input.GetKeyUp(KeyCode.DownArrow)) {
			CraneHandState = CraneHandStates.MOVING_DOWN;
			//MoveCraneHandUpDown();
			StartCoroutine(MoveCraneHandUpDown());
		}
		else if(Input.GetKeyUp(KeyCode.UpArrow)) {
			CraneHandState = CraneHandStates.MOVING_UP;
			//MoveCraneHandUpDown();
			StartCoroutine(MoveCraneHandUpDown());
		}
	}
	
	IEnumerator MoveCraneHandUpDown() {
		while(CraneHandState == CraneHandStates.MOVING_UP || CraneHandState == CraneHandStates.MOVING_DOWN) {
			switch(CraneHandState) {
				case CraneHandStates.MOVING_DOWN:
					if (Piston2.transform.localPosition.y <= -22.5f) {
						CraneHandState = CraneHandStates.STATIONARY_DOWN;
					}
					else {
						Piston2.transform.Translate(0, -1*Time.deltaTime, 0);
						if (ObjectHeld != null) {
							ObjectHeld.transform.Translate(0, -1*Time.deltaTime, 0);
						}
					}
				break;
				case CraneHandStates.MOVING_UP:
					if (Piston2.transform.localPosition.y >=-16f) {
						CraneHandState = CraneHandStates.STATIONARY_UP;
					}
					else {
						Piston2.transform.Translate(0, 1*Time.deltaTime, 0);
						if (ObjectHeld != null) {
							ObjectHeld.transform.Translate(0, 1*Time.deltaTime, 0);
						}
					}
				break;
			}
			yield return 0; // executed everyframe
		}
	}
	
	void ReleaseObject() {
		ObjectHeld = null;
	}
	
	void UseController() {
		UsingController = true;
	}
	
	void ReleaseController() {
		UsingController = false;
	}
	
	// return the current object the crane is holding, null if nothing
	public GameObject GetObjectHeld() {
		return ObjectHeld;
	}
	
	public void CraneGrab(GameObject other) {
		CraneHandState = CraneHandStates.STATIONARY_DOWN;
		ObjectHeld = other;
		RecoilCrane(false);
	}
	
	// call back, when the object held by the crane hits something
	// Other - the held object that called this callback
	// HitOn - The object the "held object" hit on.
	public void HeldObjectHitSomething(GameObject Other, GameObject HitOn) {
		if (HitOn.name == "RamPlatform") { // holding object hits the floor
			RecoilCrane(true); // drop the object and recoil the crane hand
		}
		else if(HitOn.name == "CageBase") {
			RecoilCrane(true); // drop the object and recoil the crane hand
		}
		else {
			RecoilCrane(false); // hitting unknown object, recoil
		}
	}
		
	
	public void OnRamTriggerEnter(Collider other) {
		// if hittign balance, drop ram
		//if (ObjectHeld != null && other.gameObject.name == "Balance") {  RecoilCrane(); return; }
		//if (ObjectHeld != null && other.gameObject.name == "CagePlatform")  { RecoilCrane(); return; }
	}
	
	public void OnRamTriggerExit(Collider other) {
	}
	
	// crane hit some object, move it back up
	public void RecoilCrane(bool Drop) {
		CraneHandState = CraneHandStates.MOVING_UP;
		if (Drop) {
			ObjectHeld = null;
		}
	}
	
	public float GetWeight() {
		if (ObjectHeld != null) {
			L2Ram Ram = ObjectHeld.GetComponent("L2Ram") as L2Ram;
			return Ram.GetWeight();
		}
		
		return 0f;
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			NearController = true;
			GController.ShowInfoBox("Press E to use the Controller.");
		}
	}
	
	void OnTriggerExit(Collider other) {
		NearController = false;
		GController.HideInfoBox();
	}
}
