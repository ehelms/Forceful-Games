using UnityEngine;
using System.Collections;

public class Level1StoryScript : MonoBehaviour {

	private Camera playerCamera;
	private Camera storyCamera;
	private Vector3 newtonPosition;
	private Vector3 newtonRotation;
	
	// Use this for initialization
	void Start () {
		playerCamera = GameObject.Find("Player").GetComponentInChildren<Camera>(); 
		storyCamera = GameObject.Find("StoryCamera").GetComponent<Camera>();
		newtonPosition = new Vector3(-34.37271f, 1.996695f, -26.36013f);
		newtonRotation = new Vector3(0f, 270.5855f, 0f);
	}
	
	public IEnumerator startScript(){
		yield return new WaitForSeconds(1);
		playerCamera.enabled = false;
		storyCamera.enabled = true;
		yield return StartCoroutine("play");
		storyCamera.enabled = false;
		playerCamera.enabled = true;
	}
	
	IEnumerator play(){
		yield return StartCoroutine(showNewton(15f));
		yield return StartCoroutine(showCartBall(4f));
		yield return StartCoroutine(showEndOfTrack(4f));
		yield return StartCoroutine(showNewton(12f));
		yield return StartCoroutine(showConsole(7f));
		yield return StartCoroutine(showEndOfTrack(5f));
		yield return StartCoroutine(showNewton(2f));
	}
	
	IEnumerator showEndOfTrack(float waitTime){
		storyCamera.transform.position = new Vector3(-23.44763f, 2.881423f, -26.78935f);
		storyCamera.transform.eulerAngles = new Vector3(0f, 160.2778f, 0f);
		yield return new WaitForSeconds(waitTime);
	}
	
	IEnumerator showCartBall(float waitTime){
		storyCamera.transform.position = new Vector3(-13.4041f, 2.881423f, -31.99579f);
		storyCamera.transform.eulerAngles = new Vector3(0f, 112.2304f,0f);
		yield return new WaitForSeconds(waitTime);
	}
	
	IEnumerator showNewton(float waitTime){
		storyCamera.transform.position = newtonPosition;
		storyCamera.transform.eulerAngles = newtonRotation;
		yield return new WaitForSeconds(waitTime);
	}
	
	IEnumerator showConsole(float waitTime){
		storyCamera.transform.position = new Vector3(-40.06734f, 3.92204f, -24.09014f);
		storyCamera.transform.eulerAngles = new Vector3(38.0025f, 158.5931f, 359.8258f);
		yield return new WaitForSeconds(waitTime);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
