using UnityEngine;
using System.Collections;

public class IntroStoryScript : MonoBehaviour {

	private Camera playerCamera;
	private Camera storyCamera;
	private Vector3 newtonPosition;
	private Vector3 newtonRotation;
	
	// Use this for initialization
	void Start () {
		playerCamera = GameObject.Find("Player").GetComponentInChildren<Camera>(); 
		storyCamera = GameObject.Find("StoryCamera").GetComponent<Camera>();
		newtonPosition = new Vector3(-4.207006f, 2.044167f, -6.957426f);
		newtonRotation = new Vector3(4.48104f, 212.3029f, 9.099158e-06f);
	}
	
	public IEnumerator startScript(){
		yield return new WaitForSeconds(3);
		playerCamera.enabled = false;
		storyCamera.enabled = true;
		yield return StartCoroutine("play");
		storyCamera.enabled = false;
		playerCamera.enabled = true;
	}
	
	IEnumerator play(){
		StartCoroutine(showNewton(1f));
		yield return StartCoroutine(showNewton(17f));
		yield return StartCoroutine(showDoors(12f));
		yield return StartCoroutine(showNewton(18f));
		yield return StartCoroutine(showMeasuringTape(2f));
		yield return StartCoroutine(showCalculator(2f));
	}
	
	IEnumerator showMeasuringTape(float waitTime){
		storyCamera.transform.position = new Vector3(-1.316253f,  2.903968f, -4.610877f);
		storyCamera.transform.eulerAngles = new Vector3(0f,  5.37825f,  0f);
		yield return new WaitForSeconds(waitTime);
	}
	
	IEnumerator showCalculator(float waitTime){
		storyCamera.transform.position = new Vector3(-3.888951f, 2.626106f, -7.348285f);
		storyCamera.transform.eulerAngles = new Vector3(0, -196.0125f, 0);
		yield return new WaitForSeconds(waitTime);
	}
	
	IEnumerator showNewton(float waitTime){
		storyCamera.transform.position = newtonPosition;
		storyCamera.transform.eulerAngles = newtonRotation;
		yield return new WaitForSeconds(waitTime);
	}
	
	IEnumerator showDoors(float waitTime){
		storyCamera.transform.position = new Vector3(-5.009148f, 3.198845f, -12.18156f);
		storyCamera.transform.eulerAngles = new Vector3(17.38409f, 270f, 0f);
		yield return new WaitForSeconds(waitTime);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
