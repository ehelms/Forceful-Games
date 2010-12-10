using UnityEngine;
using System.Collections;

public class FlowController : MonoBehaviour {
	
	private GameController gameController;
	private IntroFlowController introFlowController;
	private Level1FlowController level1FlowController;
	private Level2FlowController level2FlowController;
	private GameEndsController EndController;
	
	// Use this for initialization
	void Start () {
		
		gameController = GameObject.Find("Controller").GetComponent("GameController") as GameController;
		introFlowController = this.GetComponent("IntroFlowController") as IntroFlowController;
		introFlowController.enabled = false;
		level1FlowController = this.GetComponent("Level1FlowController") as Level1FlowController;
		level1FlowController.enabled = false;
		level2FlowController = this.GetComponent("Level2FlowController") as Level2FlowController;
		level2FlowController.enabled = false;
		
		introFlowController.FinishedEvent += introFlowControllerListener;
		level1FlowController.FinishedEvent += level1FlowControllerListener;
		level2FlowController.FinishedEvent += level2FlowControllerListener;
		
		if( !gameController.isCheating() ){
			if( gameController.startingLevel() == 0){
				startStory();
			} else if( gameController.startingLevel() == 1){
				introFlowControllerListener(null);
			} else if( gameController.startingLevel() == 2){
				level1FlowControllerListener(null);
			}
		} 
	}
	
	void startStory(){
		introFlowController.enabled = true;
		introFlowController.startStory();
	}
	
	void introFlowControllerListener(GameObject g){
		introFlowController.enabled = false;
		level1FlowController.enabled = true;
		level1FlowController.startStory();		
	}
	
	void level1FlowControllerListener(GameObject g){
		level1FlowController.enabled = false;
		level2FlowController.enabled = true;
		level2FlowController.startStory();
	}
	
	void level2FlowControllerListener(GameObject g){
		level2FlowController.enabled = false;
		EndController.enabled = true;
		EndController.ShowGameEnds();
	}
	
	// Update is called once per frame
	void Update () {

	}
}
