using UnityEngine;
using System.Collections;

public class NewtonLevel1Controller : MonoBehaviour {
	
	private int positionState = 0;
	private States state;
	private Positions newtonPosition; 
	private ConsoleController consoleController;
	private GameController gameController;
	private RamTrigger ramTrigger;
	private bool newtonEnabled = true;
	private GameObject Player;
	private FPSInputController fpsInput;
	
	public enum States { LEVEL1, HINT1, HINT2, HINT3 };
	public enum Positions { LAB=1, LEVEL1, LEVEL2, LEVEL3 }; // return "Level1", "Level2", Level3 or Main for lobby
	
	void Start(){
		state = States.LEVEL1;
		newtonPosition = Positions.LAB;
		fpsInput = GameObject.Find("Player").GetComponent("FPSInputController") as FPSInputController;
		consoleController = GameObject.Find("console").GetComponent("ConsoleController") as ConsoleController;
		gameController = GameObject.Find("Controller").GetComponent("GameController") as GameController;
		ramTrigger = GameObject.Find("ram").GetComponent("RamTrigger") as RamTrigger;
		Player = GameObject.Find("Player");
	}

	void Update () {
		transform.LookAt(new Vector3(Player.transform.position.x, 1.238406f, Player.transform.position.z));
	}

	public void moveNewton(Positions newPosition){
		if( newPosition == Positions.LAB ){
			transform.position = new Vector3(-4.748125f, 1.238406f, -8.060616f);
		} else if ( newPosition == Positions.LEVEL1 ){
			transform.position = new Vector3(-35.4f, 1.238406f, -26.3f);
		}
		newtonPosition = newPosition;
	}
	
	public IEnumerator playIntro(){
		yield return new WaitForSeconds(1);
		yield return StartCoroutine(playClip("Audio/Level1/NewtonL1Intro", true));
		state = States.LEVEL1;
	}
	
	public IEnumerator playSuccess(int successNumber){
		if( successNumber == 0 ){
			yield return StartCoroutine(playClip("Audio/Level1/NewtonL1-FirstProblemComplete", true));
		} else if( successNumber == 1 ){
			yield return StartCoroutine(playClip("Audio/Level1/NewtonL1-SecondProblemComplete", true));
		} else if( successNumber == 2 ){
			yield return StartCoroutine(playClip("Audio/Level1/NewtonL1-ThirdProblemComplete", true));
		}
	}
	
	public IEnumerator playFail(int failNumber){
		yield return StartCoroutine(playClip("NewtonFail", true));
	}
	
	public IEnumerator playStartQuiz(){
		yield return StartCoroutine(playClip("Audio/Level1/NewtonStartQuiz", true));
	}
	
	IEnumerator playClip(string clipName, bool lockMovement){
		
		if(lockMovement && !gameController.isCheating()) {
			fpsInput.disableMovement();
			gameController.DisableMouse();
		}
		newtonEnabled = false;
		AudioClip clip = (AudioClip)Resources.Load(clipName, typeof(AudioClip));
		audio.clip = clip;
		audio.Play();
		yield return new WaitForSeconds(audio.clip.length);
		newtonEnabled = true;
		if (lockMovement && !gameController.isCheating()) {
			fpsInput.enableMovement();
			gameController.EnableMouse();
		}
		
	}
	
	void OnTriggerStay(Collider other){
		if( other.tag == "Player" ){
			if( Input.GetKeyUp("e")  && newtonEnabled ){
				print(state);
				StartCoroutine(performAction());
			}
		}
	}
	
	IEnumerator performAction(){
		if( state == States.HINT1 ){
			yield return StartCoroutine(playClip("Audio/Level1/Level1Hint1", false));
			state = States.HINT2;
		} else if( state == States.HINT2 ){
			yield return StartCoroutine(playClip("Audio/Level1/Level1Hint2", false));
			state = States.HINT3;
		} else if( state == States.HINT3 ){
			yield return StartCoroutine(playClip("Audio/Level1/Level1Hint3", false));
		} 
	}
	
	public States getState(){
		return state;
	}
	
	public Positions getPosition(){
		return newtonPosition;
	}
	
}