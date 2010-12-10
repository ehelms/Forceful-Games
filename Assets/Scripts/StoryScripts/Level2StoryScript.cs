using UnityEngine;
using System.Collections;

public class Level2StoryScript : MonoBehaviour {

	private Camera playerCamera;
	private Camera storyCamera;
	private Vector3 newtonPosition;
	private Vector3 newtonRotation;

	// Use this for initialization
	void Start () {
		playerCamera = GameObject.Find("Player").GetComponentInChildren<Camera>(); 
		storyCamera = GameObject.Find("StoryCamera").GetComponent<Camera>();
		newtonPosition = new Vector3(-75.87531f, 2.849679f, 1.456935f);
		newtonRotation = new Vector3(11.14955f, 271.6407f, 0f);
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
	}

	IEnumerator showCrane(float waitTime){
		storyCamera.transform.position = new Vector3(-73.97161f, 4.304578f, 3.025819f);
		storyCamera.transform.eulerAngles = new Vector3(3.126648f, 88.4872f, 0.2114105f);
		yield return new WaitForSeconds(waitTime);
	}

	IEnumerator showRams(float waitTime){
		storyCamera.transform.position = new Vector3(-69.468f, 1.972466f, 5.348705f);
		storyCamera.transform.eulerAngles = new Vector3(24.36166f, 311.7668f, -2.566132f);
		yield return new WaitForSeconds(waitTime);
	}

	IEnumerator showNewton(float waitTime){
		storyCamera.transform.position = newtonPosition;
		storyCamera.transform.eulerAngles = newtonRotation;
		yield return new WaitForSeconds(waitTime);
	}

	IEnumerator showConsole(float waitTime){
		storyCamera.transform.position = new Vector3(-79.37403f, 2.744502f, 6.237887f);
		storyCamera.transform.eulerAngles = new Vector3(25.33296f, 460.752f, 0.3505096f);
		yield return new WaitForSeconds(waitTime);
	}

	IEnumerator showWolf(float waitTime){
		storyCamera.transform.position = new Vector3(-72.19843f, 3.324285f, 10.50946f);
		storyCamera.transform.eulerAngles = new Vector3(27.05788f, 361.5032f, 0.2438202f);
		yield return new WaitForSeconds(waitTime);
	}

	IEnumerator showSelectedRam(float waitTime){
		storyCamera.transform.position = new Vector3(-73.22144f, 1.975909f, 3.5688f);
		storyCamera.transform.eulerAngles = new Vector3(25.33296f, 460.752f, 0.3505096f);
		yield return new WaitForSeconds(waitTime);
	}

	IEnumerator showEntirePlatform(float waitTime){
		storyCamera.transform.position = new Vector3(-80.6381f, 7.176246f, 6.822087f);
		storyCamera.transform.eulerAngles = new Vector3(24.79182f, 447.2465f, -5.392395f);
		yield return new WaitForSeconds(waitTime);
	}

	IEnumerator showCrashSite(float waitTime){
		storyCamera.transform.position = new Vector3(-48.56134f, 5.416394f, 6.766066f);
		storyCamera.transform.eulerAngles = new Vector3(11.4353f, 414.1694f, 2.397736f);
		yield return new WaitForSeconds(waitTime);
	}

	// Update is called once per frame
	void Update () {

	}
}