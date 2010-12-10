using UnityEngine;
using System.Collections;

public class ThrowBallController : MonoBehaviour {

	private float velocity = 0;
	private float gravity;
	private float totTime = 0;
	private float height;
	private float timeFactor;
	
	private bool activated = false;
	
	public int state = 0;
	
	public const int YET_TO_START = 0;
	public const int ON_CART = 1;
	public const int RELEASED = 2;
	
	private long dropTime = 0;
	private long hitFloorTime = 0;
	
	void Start() {
		float origGravity = -9.8F;
		float factor = 2.8f;
		gravity = origGravity * factor;
		transform.position = new Vector3(-11.0f, 2.5f, -32.73f);
		height = transform.position.y;
		timeFactor = 1.5f;
	}
	
	long getTime(){
		return (long) (System.DateTime.UtcNow - new System.DateTime(1970, 1, 1)).TotalMilliseconds;
	}
	
	public void setState(int state) {
		this.state = state;
		if (state == RELEASED) {
			dropTime = getTime();	
		}
		if (state == ON_CART) {
		  dropTime = 0;
		  hitFloorTime = 0;	
		}
	}
	
	public void setVelocity(float v) {
		velocity = v;
	}
	
	
	public void activate() {
	   activated = true;
	}
	
	public void deactivate() {
	   activated = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!activated) {
			return;
		}
		
		float v = (velocity*0.05f);
		totTime = Time.deltaTime + totTime;

		float y = (0.05f*0.05f) * gravity * 0.5F;
		
		switch(state) {
			case ON_CART:
				transform.Translate(v, 0, 0);
				break;
			case RELEASED:
				transform.Translate(v, y, 0);
				if (transform.position.y <= 0 && hitFloorTime == 0) {
				    hitFloorTime = getTime();	
					print("Fall Time (ms): " + (hitFloorTime - dropTime));
				}
				break;
		}
	}
}
