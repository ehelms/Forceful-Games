using UnityEngine;
using System.Collections;


public class GameController : MonoBehaviour {

	private GameObject playerObject;
	private AudioSource playerAudio;
	private SoundPlayer soundPlayer;
	private StudentModel Student;
	private NewtonController newtonController;
	private FPSInputController FPSInput; 
	
	private bool paused; // true when other game component requests pause 
	private bool UserPaused; // true when user requests pause by pressing esc
	private bool DisableMouseLook = false; // prevent player form usign mouse to look around
	private bool DisablePlayerMove = false; // prevent player from moving
	private bool MousePtrVisible = false; // set true to request visible mouse pointer
	
	private int GameState = 0;

	private bool ShowMessage = false;
	private bool NonModalMessage = false;
	private string Message = "";
	private string InfoMsg = "";
	private int MsgIdx = 0;
	
	private bool CHEAT = true;
	private int startLevel = 0;
	
	public Texture MessageBackGroundTexture;
	public Texture OkTexture;
	public Texture CloseTexture;
	public Texture GamePausedTexture;
	public Texture InfoMsgTexture;
	public GUISkin CustomSkin;
	
	private const long maxRamsKilled = 3;
	private long currentRamsKilled = 0;
	private int pointsForRamL1 = 5;
	private int pointsForL1Quiz = 5;
	private int maxL1Quiz = 2;
	private int currL1Quiz = 0;
	
	private int ObjectiveIdx = 0;
	
	private string[] Objectives = {
@"Find all the tools. Once you find all the tools, 
door for Level 1 will unlock. You can walk into the 
Door once you unlock it.",
@"Walk to level 1 and complete the following tasks to unlock the next level.
	* You must crush the Ram 3 times.
	* Play quiz and get 2 question right.",
@"Walk to level 2 and complete the following tasks to escape from UNC Physics Lab.
	* You must break the cage to free the Wolf.
	* Play quiz and get 2 question right.",
};
	
	public void answeredQuiz(string quizName) {
		if(quizName == "quiz1" && currL1Quiz < maxL1Quiz ) {
			Student.addPoints(pointsForL1Quiz);
			currL1Quiz++;
		}
		
	}
	
	// Use this for initialization
	void Start () {
		Screen.showCursor = false;
		playerObject = GameObject.FindWithTag("Player");
		playerAudio = playerObject.GetComponent("AudioSource") as AudioSource;
		soundPlayer = GetComponent("SoundPlayer") as SoundPlayer;
		Student = playerObject.GetComponent("StudentModel") as StudentModel;
		newtonController = GameObject.Find("Newton").GetComponent("NewtonController") as NewtonController;
		FPSInput = playerObject.GetComponent("FPSInputController") as FPSInputController;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp(KeyCode.Escape) && !ShowMessage) {
			if (paused && UserPaused) {  //unpause
				paused = false;
				UserPaused = false;	
				DisableMouseLook = true;

			} 
			else if (!paused && !UserPaused) { //pause
				paused = true;
				UserPaused = true;
				DisableMouseLook = true;
			}			 	
		}
		StartCoroutine(Pause());
	}
	
	void OnGUI() {
		
		GUI.skin = CustomSkin;		
		if(UserPaused) { // if user paused, show pause menu, otherwise some game component has request game pause
			GUI.DrawTexture(new Rect((Screen.width - GamePausedTexture.width)/2, 50, GamePausedTexture.width, GamePausedTexture.height), GamePausedTexture);
			GUILayout.Window(0, new Rect((Screen.width - MessageBackGroundTexture.width)/2, (Screen.height - MessageBackGroundTexture.height)/2, MessageBackGroundTexture.width,
				MessageBackGroundTexture.height), DoMenuWindow, "");
			
			return;
		}
		
		if (ShowMessage)  {
			GUILayout.Window(0, new Rect((Screen.width - MessageBackGroundTexture.width)/2, (Screen.height - MessageBackGroundTexture.height)/2, MessageBackGroundTexture.width,
				MessageBackGroundTexture.height), DoWindow, "");
		}
		
		if (NonModalMessage) {
			GUILayout.Window(0, new Rect((Screen.width - MessageBackGroundTexture.width)/2, (Screen.height - 100 - 10), MessageBackGroundTexture.width,
				100), DoInfoWindow, "");
		}
	}
	
	void DoInfoWindow(int wid) {
		GUILayout.BeginHorizontal();
		GUILayout.TextArea(InfoMsg);
		GUILayout.EndHorizontal();
	}
	
	void DoMenuWindow(int wid) {
		GUILayout.Label("Current Objective: ");
		GUILayout.BeginHorizontal ();
		if (ObjectiveIdx >=0 && ObjectiveIdx <= Objectives.Length) {
			GUILayout.TextArea(Objectives[ObjectiveIdx]);
		}
		else {
			GUILayout.TextArea("You don't have any objective now.");
		}
		GUILayout.EndHorizontal();
	}
	
	void DoWindow(int wid) {
		GUILayout.TextArea(Message.Substring(0, MsgIdx/2));
		if (MsgIdx/2 < Message.Length) MsgIdx++;
		GUILayout.FlexibleSpace();
		if (GUILayout.Button(OkTexture)) {
			HideMessage();
		}
	}
	
	public void PauseGame()
	{
		paused = true;
	}
	
	public void unpause() {
	   paused = false;
	}	
	
	public bool isPaused()
	{
		return paused;
	}
	
	public bool IsMouseDisabled()
	{
		return DisableMouseLook;
	}

	public void DisableMouse() 
	{
		DisableMouseLook = false;
	}
	
	public void EnableMouse()
	{
		DisableMouseLook = true;
	}
	
	public void DisablePlayer() {
		DisablePlayerMove = true;
		FPSInput.disableMovement();
	}
	
	public void EnablePlayer() {
		DisablePlayerMove = false;
		FPSInput.enableMovement();
	}
	
	public bool IsPlayerDisabled() {
		return DisablePlayerMove;
	}
	
	IEnumerator Pause()
	{
		Time.timeScale = (paused ? 0 : 1);
		DisableMouseLook = paused;
		//(GameObject.FindWithTag("MainCamera").GetComponent("MouseLook") as MouseLook).enable = paused;
		//Debug.Log(paused);
		Screen.lockCursor = !(MousePtrVisible || paused);
		Screen.showCursor = (MousePtrVisible || paused);
		yield return 0;
	}
	
	public void PlayWelcomeMessage() {
		soundPlayer.playWelcome(playerAudio);
	}
	
	public void ShowMessageBox(string msg) {
		ShowIntroMessage(msg);
	}
	public void ShowIntroMessage(string msg) {
		ShowMessage = true;
		Message = msg;
		MsgIdx = 0;
		paused = true;
	}
	
	// show small non-model message box
	public void ShowInfoBox(string msg) {
		InfoMsg = msg;
		NonModalMessage = true;
	}
	
	public void HideInfoBox() {
		InfoMsg = "";
		NonModalMessage = false;
	}
	
	public void HideMessage() {
		ShowMessage = false;
		paused = false;
	}
	
	public void ShowMousePointer() {
		MousePtrVisible = true;
	}
	
	public void HideMousePointer() {
		MousePtrVisible = false;
	}
	
	public void FoundTool(string ToolId, string InfoMsg) {
		Student.FoundTool(ToolId);
		ShowIntroMessage(InfoMsg);
	}
	
	public bool isCheating() {
		return CHEAT;
	}
	
	public int startingLevel(){
		return startLevel;
	}
	
	public void IncrementObjective() {
		ObjectiveIdx++;
	}
}