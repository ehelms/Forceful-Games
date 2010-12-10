using UnityEngine;
using System.Collections;

public class MeasuringScaleController : BaseToolController {

	
	private int id = 1;
	
	private int state  = NONE;
	public const int NONE = 6;
	public const int PLACING_1 = 1;
	public const int PLACING_2 = 2;
	public const int PLACED_2 = 3;

	
	public GameObject StartObject;
	public GameObject EndObject;
	

	private LayerMask mask;
	

	
	
	public override int getId() {
		return id;
	}
	
	public override void showTool() {
		state = PLACING_1;
		resetMarker();
	}
	
	public override void hideTool() {
		state = NONE;
		notifyClose();
	}
	public override bool isUsing() {
		return state != NONE;
	}
	
	void resetMarker() {
		if (state == PLACING_1) {
		//	StartObject.transform.position = Player.transform.position;
		}
		else if(state == PLACING_2) {
		//	EndObject.transform.position = Player.transform.position;
		}
	}
	
	// Use this for initialization
	void Start () {
		init();
		mask = 1 << LayerMask.NameToLayer("Measure Layer");
	}
	
	
	// Update is called once per frame
	void Update () {
		if (state == NONE || state == PLACED_2) {
			return;
		}
		
		
		
		if (Input.GetKeyUp(KeyCode.Mouse0)) {
			if (state == PLACING_1) {
				state = PLACING_2;
				resetMarker();
			}
			else if(state == PLACING_2 ){
				state = PLACED_2;
			}
			
		}
		
		
		//if(Input.GetKey(KeyCode.V)) {
		if (state == PLACED_2) {
				Vector3 Start = StartObject.transform.position;
				Vector3 End = EndObject.transform.position;
				float dist = Mathf.Sqrt((Start.x - End.x)*(Start.x - End.x) + (Start.z - End.z)*(Start.z - End.z));
				Controller.ShowMessageBox("Distance is : " + dist);
				hideTool();
			//
		}
		
		if (state == PLACING_1 || state == PLACING_2) {
			GameObject Go = state == PLACING_1 ? StartObject : EndObject;
			RaycastHit hit;
			Transform cam = Camera.main.transform;
			if(Physics.Raycast(cam.position, cam.forward, out hit, 100, mask)) {
				Vector3 pos = cam.position +cam.forward*(hit.distance+0.05f);
				Vector3 newPos = new Vector3(pos.x, 0, pos.z);
				Go.transform.position = Vector3.Slerp(Go.transform.position, newPos, Time.deltaTime*5);
				
			}
		}
	}
}
