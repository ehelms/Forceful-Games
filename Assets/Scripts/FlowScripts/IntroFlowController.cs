using UnityEngine;
using System.Collections;

public class IntroFlowController : MonoBehaviour {

	private IntroStoryScript introStory;
	private NewtonIntroController newtonIntroController;
	private GameObject playerObject;
	private GameObject newton;
	
	public delegate void EventHandler(GameObject e);
	public event EventHandler FinishedEvent;
	
	// Use this for initialization
	void Start () {
		playerObject = GameObject.FindWithTag("Player");
		newton = GameObject.Find("Newton");
		introStory = this.GetComponent("IntroStoryScript") as IntroStoryScript;
		newtonIntroController = GameObject.Find("Newton").GetComponent("NewtonIntroController") as NewtonIntroController;
		newtonIntroController.enabled = false;
	}
	
	void onFinished(){
		if(FinishedEvent != null){
			newtonIntroController.enabled = false;
			FinishedEvent(this.gameObject);
		}
	}
	
	public void startStory(){
		newtonIntroController.enabled = true;
		StartCoroutine(introStory.startScript());
		StartCoroutine(newtonIntroController.playIntro());
	}		
	
	// Update is called once per frame
	void Update () {
		if( newtonIntroController.enabled == true ){
			if( newtonIntroController.getState() == NewtonIntroController.States.POSTINTRO
								&& newtonIntroController.getPosition() == NewtonIntroController.Positions.LEVEL1 ){
				float distance = Vector3.Distance(playerObject.transform.position, newton.transform.position);
				if( distance < 7.5 ){
					onFinished();
				}
			}
		}
	}
}
