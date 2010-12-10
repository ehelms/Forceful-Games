using UnityEngine;
using System.Collections;

public class Level1FlowController : MonoBehaviour {

	private IntroStoryScript introStory;
	private GameObject playerObject;
	private GameObject newton;
	private NewtonLevel1Controller newtonLevel1Controller;
	private GameController gameController;
	private Level1StoryScript level1Story;
	private ConsoleController consoleController;
	private StudentModel student;
	private RamTrigger ramTrigger;
	private HoverCraftController hovercraftController;
	
	private int ramsKilled;
	private const int maxRamsKilled = 3;
	private int currentRamsKilled = 0;
	private int pointsForRamL1 = 5;
	private int pointsForL1Quiz = 5;
	private int maxL1Quiz = 2;
	private int currL1Quiz = 0;
	
	public delegate void EventHandler(GameObject e);
	public event EventHandler FinishedEvent;
	
	// Use this for initialization
	void Start () {
		level1Story = this.GetComponent("Level1StoryScript") as Level1StoryScript;
		newtonLevel1Controller = GameObject.Find("Newton").GetComponent("NewtonLevel1Controller") as NewtonLevel1Controller;
		newtonLevel1Controller.enabled = false;
		gameController = GameObject.Find("Controller").GetComponent("GameController") as GameController;
		consoleController = GameObject.Find("console").GetComponent("ConsoleController") as ConsoleController;
		student = GameObject.FindWithTag("Player").GetComponent("StudentModel") as StudentModel;
		ramTrigger = GameObject.Find("ram").GetComponent("RamTrigger") as RamTrigger;
		hovercraftController = GameObject.Find("hovercraft").GetComponent("HoverCraftController") as HoverCraftController;
		
		ramTrigger.RamKilledEvent += ramKilledListener;
		hovercraftController.SquashFailEvent += squashFailListener;

	}
	
	void onFinished(){
		if(FinishedEvent != null){
			newtonLevel1Controller.enabled = false;
			FinishedEvent(this.gameObject);
		}
	}
	
	void ramKilledListener(GameObject g){
		StartCoroutine(killedRam());
	}

	void squashFailListener(GameObject g){
		StartCoroutine(newtonLevel1Controller.playFail(1));
		consoleController.unlock();
	}
	
	public void startStory(){
		newtonLevel1Controller.enabled = true;
		consoleController.setLocked();
		StartCoroutine(level1Story.startScript());
		consoleController.unlock();
	}		
	
	public long getRamsKilled(){
		return currentRamsKilled; 
	}
	
	public bool needToKill() {
		return currentRamsKilled < maxRamsKilled;	
	}
	
	public void answeredQuiz(string quizName) {
		if(quizName == "quiz1" && currL1Quiz < maxL1Quiz ) {
			student.addPoints(pointsForL1Quiz);
			currL1Quiz++;
		}
		if( currL1Quiz == 2 ){
			onFinished();
		}
	}
	
	public IEnumerator killedRam() {
		if (currentRamsKilled < maxRamsKilled) {
			student.addPoints(pointsForRamL1);
			consoleController.setLocked();
			yield return StartCoroutine(newtonLevel1Controller.playSuccess(currentRamsKilled));
			consoleController.unlock();
			ramTrigger.reset();
		}
		currentRamsKilled++; 
		if( currentRamsKilled == 3){
			consoleController.setLocked();
			yield return StartCoroutine(newtonLevel1Controller.playStartQuiz());
		}
	}
	
	// Update is called once per frame
	void Update () {
		//print("level1flowcontroller");
		
	}
}
