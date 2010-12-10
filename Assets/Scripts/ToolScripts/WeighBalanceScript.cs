using UnityEngine;
using System.Collections;

public class WeighBalanceScript : BaseToolController {

	
	private int id = 2;
	private bool Using =false;
	private StudentModel Stud;
	private int state = 0;
	
	public GameObject ToolObject;
	
	private Vector3 HiddenPos;
	
	public override int getId() {
		return id;
	}
	
	public override void showTool() {
		print("SHOW");
		Using = true;
	}
	
	public override void hideTool() {
		print("HIDE");
		Using = false;
		notifyClose();
	}

	public override bool isUsing() {
		return Using ==  true;
	}
	
	public override void reactivated() {
		hideTool();
	}
	
	// Use this for initialization
	void Start () {
		init();
		Stud = gameObject.GetComponent("StudentModel") as StudentModel;
		HiddenPos = new Vector3(ToolObject.transform.position.x, 0, ToolObject.transform.position.z); 
	}
	
	// Update is called once per frame
	void Update () {		
	//	print(Using);
		if (Using) {
			RaycastHit hit;
			Transform cam = Camera.main.transform;
			if(Physics.Raycast(cam.position, cam.forward, out hit, 100)) {
				Vector3 pos = cam.position +cam.forward*(hit.distance+0.05f);
				if (pos.z > 0.65f && pos.z < 9.4f) { // restrict movement of the balance
					Vector3 newPos = new Vector3(HiddenPos.x, 0.8f, pos.z); //.8 - bring the balance above ground
					ToolObject.transform.position = Vector3.Slerp(ToolObject.transform.position, newPos, Time.deltaTime*5);
				}
			}
		}
		else {
			ToolObject.transform.position = HiddenPos;
		}
	}

}
