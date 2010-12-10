using UnityEngine;
using System.Collections;

public class RamTrigger : MonoBehaviour {

	private float ramHeight = 0;
	private long minLocation = -25;
	private long maxLocation = -42;
	
	
	public delegate void EventHandler(GameObject e);
	public event EventHandler RamKilledEvent;
	
	void Start () {
		ramHeight = transform.localScale.y;
		randomize();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void onRamKilled(){
		if(RamKilledEvent != null){
			RamKilledEvent(this.gameObject);
		}
	}
	
	void OnTriggerEnter(Collider other){
		if( other.gameObject.name == "ThrowBall" ){
				Squash();
				print("post squashed");
		}
	}
	
	public bool isSquashed() {
		return transform.localScale.y == 0;
	}
	
	void Squash(){
		if (isSquashed()) {
			return;
		}
		onRamKilled();
		transform.localScale = new Vector3(transform.localScale.x, 0, transform.localScale.z);
		print("squashed");
	}
	
	public  void reset(){ 
		print(ramHeight);
		transform.localScale = new Vector3(transform.localScale.x, ramHeight, transform.localScale.z);
		print(transform.localScale.y);
		randomize();
	}
	
	
	private void randomize() {
		float location = Random.Range(maxLocation, minLocation);
		transform.position = new Vector3(location, transform.position.y,transform.position.z);
		//GameObject marker = GameObject.Find("LaunchMarker");
		//print("Distance: " + (marker.transform.position.x - location));
	}
	
}
