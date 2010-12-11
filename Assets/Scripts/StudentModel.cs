using UnityEngine;
using System.Collections;

public class StudentModel : MonoBehaviour {

	public Texture KnowledgeTexture;
	public float KnowledgePoints;
	public int MaxKnowledgePoints = 100;
	
	public float ptsForTool = 25/3f;
	
	private Rect KbarRect;
	
	public BaseToolController Calculator ;
	public BaseToolController MeasuringTape;
	public BaseToolController FormulaSheet;
	public BaseToolController ScaleTool ;
	
	
	private bool[] ToolsAvail = {false, false, false, false};
	private int activeTool = -1;
	private string[] ToolsID = {"CALC", "MSR_TAPE", "WGH_BAL", "FORM"};
	private string[] ToolsMessage = { "You have not found a calculator yet. You should probably look for it in the lobby!",
		"You have not found a Measuring Tape yet. You should probably look for it in the lobby! If the it is on the table, you can use <SPACE> to jump and grab it !!",
		"You have don't have a weighing Balance. Last time I saw it lying around somewhere near the Crane in Level 2.",
		"You must to find Formula tool, before you can use it! Keep looking for it!! Good Luck!"
	};
	private BaseToolController[] Tools;
	private Texture[] ToolsTexture;
	
	public Texture CalculatorTexture;
	public Texture MsrTapeTexture;
	public Texture WgBalTexture;
	public Texture FormBoardTexture;
	public Texture HighlightTexture;
	public Texture KnowledgeFillTexture;
	private Rect ToolBarRect;
	
	private GameController Controller;
	private ConsoleController level1ConsoleController;
	
	// Use this for initialization
	void Start () {
		if (!KnowledgeTexture) {
			return;
		}
		Controller = GameObject.Find("Controller").GetComponent("GameController") as GameController;

			
		Calculator =  GameObject.Find("Player").GetComponent("CalcController") as BaseToolController;
		MeasuringTape = GameObject.Find("Player").GetComponent("MeasuringScaleController") as BaseToolController;
		FormulaSheet = GameObject.Find("Player").GetComponent("FormulaController") as BaseToolController;
		ScaleTool  = GameObject.Find("Player").GetComponent("WeighBalanceScript") as BaseToolController;
		level1ConsoleController = GameObject.Find("console").GetComponent("ConsoleController") as ConsoleController;
	
		
		
		KnowledgePoints = 0f;
		KbarRect = new Rect(5, 5, 400, 15);
		ToolBarRect = new Rect(500, 10, 23, 30);
		ToolsTexture = new Texture[]{CalculatorTexture, MsrTapeTexture, WgBalTexture, FormBoardTexture};
		Tools = new BaseToolController[]{Calculator, MeasuringTape, ScaleTool, FormulaSheet};
		
	}
	
	
	
		// Update is called once per frame
	void Update () {
		//HandleChangeTool();  //Don't think we are going to use this
		
		
		int toolId = -1;
		if (Input.GetKeyUp(KeyCode.Alpha1)) {
			toolId = 0;
		}
		else if (Input.GetKeyUp(KeyCode.Alpha2)) {
			toolId = 1;
		}
		else if (Input.GetKeyUp(KeyCode.Alpha3)) {
			toolId = 2;
		}
		else if (Input.GetKeyUp(KeyCode.Alpha4)) {
			toolId = 3;
		}
		
		
		if (toolId >= 0 && activeTool < 0) {
			if (Controller.isCheating() || ToolsAvail[toolId] ) { //if you are cheating or the tool is avail
				if( toolId != 3 ){
					if( !level1ConsoleController.isEnabled() ){
						activeTool = toolId;
						Tools[toolId].showTool();
					} 
				} else {
					activeTool = toolId;
					Tools[toolId].showTool();
				}
			}
			else {
				//Controller.ShowMessageBox(ToolsMessage[toolId]);
			}
		}
		else if (toolId > 0 && toolId == activeTool) {
			Tools[toolId].reactivated();
		}
		
	}
	
	
	public void toolClosedNotifcation(int toolId) {
			if (activeTool < 0) {
				print("ERROR: No Tool active, but " + ToolsID[toolId] + " Tried to close");
				return;
			}
			else if (toolId != activeTool) {
				print("ERROR: ActiveTool " + ToolsID[activeTool] + " but tool " + ToolsID[toolId] + "tried to close.");
				return ;
			}
			else {
				activeTool = -1;
			}
	}
	
	
	public void addPoints(float points) {
		 KnowledgePoints = KnowledgePoints + points;
		print(KnowledgePoints);
		if (KnowledgePoints % 25 == 0) {
			Controller.IncrementObjective();
		}
		if (KnowledgePoints == 25) {
			findDoor("2").UnlockDoor();
		}
		else if(close(KnowledgePoints,50)) {
			findDoor("1").UnlockDoor();
		}
		else if(close(KnowledgePoints,75)) {
			findDoor("3").UnlockDoor();
		}
		
	}
	
	//function to used to tell if two floats are close enough together (within 1)
	 bool close(float a, float b) {
		float c = a - b;
		if (c < 1  && c >= 0 ) {
			return true;
		}
		if (-c < 1 && -c >= 0 ) {
			return true;
		}
		return false;
	}
	
	DoorController findDoor(string num) {
		return GameObject.Find("Door" + num).GetComponent("DoorController") as DoorController;
	}
	

	
	void OnGUI() {
		//KbarRect.width = 700*(((float)KnowledgePoints)/((float) MaxKnowledgePoints));
		KbarRect.width = 200;
		GUI.DrawTexture(new Rect(KbarRect.x + 3, KbarRect.y + 3, (KbarRect.width-6)*(KnowledgePoints/100), KbarRect.height-6), 
					KnowledgeFillTexture);		
		GUI.DrawTexture(KbarRect, KnowledgeTexture);

		Color c = GUI.color;
		DrawTools();
		GUI.color = c;
	}
	
	void DrawTools() {
		if (!CalculatorTexture) {
		    return;
		}
		
		ToolBarRect.x = Screen.width - 180;
		for(int i = 0; i < ToolsAvail.Length; i++) {
			if (!ToolsAvail[i]) {
				continue;
			}
			ToolBarRect.width = ToolsTexture[i].width;
			ToolBarRect.height = ToolsTexture[i].height;
			//GUI.color = CurrentTool == i ? Color.white : Color.gray;
			GUI.DrawTexture(ToolBarRect, ToolsTexture[i]);
			if (activeTool == i) {
				GUI.DrawTexture(new Rect(ToolBarRect.x-8, ToolBarRect.y-8, 50, 50), HighlightTexture);
			}
			//GUI.color = CurrentTool == MSR_TAPE ? Color.white : Color.gray;
			ToolBarRect.x += 40;
		}
	}
	
	void HandleChangeTool() {
		if (activeTool > 0 && (Input.GetKeyUp(",") || Input.GetKeyUp("<"))) {
			activeTool--;
		}
		else if(activeTool <2 && (Input.GetKeyUp(".") || Input.GetKeyUp(">"))) {
			activeTool++;
		}
	}
	
	public int GetCurrentTool() {
		return activeTool;
	}
	
	public void FoundTool(string ToolID) {
		addPoints(ptsForTool);
		for(int i =0; i < ToolsID.Length; i++) {
			if(ToolsID[i] == ToolID) {
				ToolsAvail[i] = true;
			}
		}
	}
	
	public bool HasCalc() {
		return ToolsAvail[0];
	}
	
	public bool HasMsrTape() {
		return ToolsAvail[1];
	}
	
	public bool HasBalance() {
		return ToolsAvail[2];
	}
}
