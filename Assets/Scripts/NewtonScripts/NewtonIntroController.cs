using UnityEngine;
using System.Collections;

public class NewtonIntroController : MonoBehaviour {
	
	private int positionState = 0;
	private States state;
	private Positions newtonPosition; 
	private ConsoleController consoleController;
	private GameController gameController;
	private RamTrigger ramTrigger;
	private bool newtonEnabled = true;
	private GameObject Player;
	private FPSInputController fpsInput;
	
	public enum States { INTRO=1, POSTINTRO };
	public enum Positions { LAB=1, LEVEL1, LEVEL2, LEVEL3 }; // return "Level1", "Level2", Level3 or Main for lobby
	
	void Start(){
		state = States.INTRO;
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
		StartCoroutine(playClip("Audio/Intro/NewtonIntro", true));
		yield return new WaitForSeconds(60);
		state = States.POSTINTRO;
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
	
	public States getState(){
		return state;
	}
	
	public Positions getPosition(){
		return newtonPosition;
	}
	
}