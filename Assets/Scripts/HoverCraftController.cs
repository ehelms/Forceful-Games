using UnityEngine;
using System.Collections;

public class HoverCraftController : MonoBehaviour {

	private bool forward = true;
	private bool activated = false;
	
	private float forwardVelocity = 8f;
	private float backVelocity = 0.7f;
	private float stopForwardThreshold = -21.61f;//-21.79f;
	private float stopReverseThreshold = -11.23f;
	
	private GameObject trackObject;
	private GameObject throwBall;
	private ConsoleController gui;
	private RamTrigger ram;
	
	private ThrowBallController ballController;
	
	public delegate void EventHandler(GameObject e);
	public event EventHandler SquashFailEvent;
	
	public void setVelocity(float vel) {
		ballController.setVelocity(-vel);
	    forwardVelocity = vel;	
	}
	
	public void activate() {
	   activated = true; 
	}
	
	public bool isActive() {
	   return activated;
	}
	
	// Use this for initialization
	void Start () {
		trackObject = GameObject.Find("track");
		throwBall = GameObject.Find("ThrowBall");
		ballController = throwBall.GetComponent("ThrowBallController") as ThrowBallController;
		gui = GameObject.Find("console").GetComponent("ConsoleController") as ConsoleController;
	    ram = GameObject.Find("ram").GetComponent("RamTrigger") as RamTrigger;	
		ballController.setState(ThrowBallController.ON_CART);
	}
	
	void onSquashFail(){
		if(SquashFailEvent != null){
			SquashFailEvent(this.gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		// Don't do anything if not 'active'
		if (!activated) {
			return;
		}
		
		float delta = forward ? forwardVelocity : -backVelocity;
		delta = delta*0.05f;
		
		transform.Translate(0, 0, delta);
		if (transform.position.x <= stopForwardThreshold && forward) {
			if (gameObject.audio != null) {
				gameObject.audio.Play();
			}
			if (trackObject != null && trackObject.audio != null) {
				trackObject.audio.Stop();
			}
			ballController.setState(ThrowBallController.RELEASED);
			forward = false;
		}
		else if(transform.position.x >= stopReverseThreshold && !forward) {
			forward = true;
			reset();
			if (gameObject.audio != null) {
				gameObject.audio.Stop();
			}
		}
	}
	
	void reset() {
		throwBall.transform.position = new Vector3(-11.0f, 2.5f, -32.73f);
		ballController.setState(ThrowBallController.ON_CART);
		activated = false;
		ballController.deactivate();
		if ( !ram.isSquashed() ) {
		    onSquashFail();
		}
	}
}
