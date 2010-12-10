using UnityEngine;
using System.Collections;

public class Level2FlowController : MonoBehaviour {

	private IntroStoryScript introStory;
	private GameObject playerObject;
	private GameObject newton;
	private NewtonLevel2Controller newtonLevel2Controller;
	private GameController gameController;
	private Level2StoryScript level2Story;
	private ConsoleController consoleController;
	private StudentModel student;
	private L2ProblemController l2ProblemController;
	private CageController cageController;
	
	private int wolvesFreed;
	private const int maxWolvesFreed = 3;
	private int currentWolvesFreed = 0;
	private int pointsForWolf = 5;
	private int pointsForL2Quiz = 5;
	private int maxL2Quiz = 2;
	private int currL2Quiz = 0;
	
	public delegate void EventHandler(GameObject e);
	public event EventHandler FinishedEvent;
	
	// Use this for initialization
	void Start () {
		level2Story = this.GetComponent("Level2StoryScript") as Level2StoryScript;
		newtonLevel2Controller = GameObject.Find("Newton").GetComponent("NewtonLevel2Controller") as NewtonLevel2Controller;
		newtonLevel2Controller.enabled = false;
		gameController = GameObject.Find("Controller").GetComponent("GameController") as GameController;
		consoleController = GameObject.Find("console").GetComponent("ConsoleController") as ConsoleController;
		student = GameObject.FindWithTag("Player").GetComponent("StudentModel") as StudentModel;
		cageController = GameObject.Find("CageBase").GetComponent("CageController") as CageController;
		l2ProblemController = this.GetComponent("L2ProblemController") as L2ProblemController;

		cageController.CrashEvent += cageCrashListener;
		
	}
	
	void onFinished(){
		if(FinishedEvent != null){
			newtonLevel2Controller.enabled = false;
			FinishedEvent(this.gameObject);
		}
	}
	
	void cageCrashListener(GameObject g){
		StartCoroutine(wolfFreed());
	}
	
	public void startStory(){
		newtonLevel2Controller.enabled = true;
		//consoleController.setLocked();
		StartCoroutine(level2Story.startScript());
		//consoleController.unlock();
	}		
	
	public long getWolvesFreed(){
		return currentWolvesFreed; 
	}
	
	public bool needToFree() {
		return currentWolvesFreed < maxWolvesFreed;	
	}
	
	public void answeredQuiz(string quizName) {
		if(quizName == "quiz1" && currL2Quiz < maxL2Quiz ) {
			student.addPoints(pointsForL2Quiz);
			currL2Quiz++;
		}
		if( currL2Quiz == 2 ){
			onFinished();
		}
	}
	
	public IEnumerator wolfFreed() {
		print("WOLF FREEEEEEE");
		if( cageController.setFinalState == CageController.FinalState.HURT ){
			yield return new WaitForSeconds(6);	
			
			//yield return StartCoroutine(newtonLevel2Controller.playFail(1));	
			l2ProblemController.resetCurrentProblem();
		}
		if( cageController.setFinalState == CageController.FinalState.EXPLODE ){
			yield return StartCoroutine(newtonLevel2Controller.playFail(1));	
			if (currentWolvesFreed < maxWolvesFreed) {
					student.addPoints(pointsForWolf);
					//yield return StartCoroutine(newtonLevel2Controller.playSuccess(currentWolvesFreed));
					l2ProblemController.newProblem();
			}
			currentWolvesFreed++; 
			if( currentWolvesFreed == 3){
					consoleController.setLocked();
					yield return StartCoroutine(newtonLevel2Controller.playStartQuiz());
			}
		}
		if( cageController.setFinalState == CageController.FinalState.NOTHING ){
				l2ProblemController.resetCurrentProblem();
		}		
	}
	
	// Update is called once per frame
	void Update () {
		//print("level1flowcontroller");
		
	}
}
