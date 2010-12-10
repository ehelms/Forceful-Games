using UnityEngine;
using System.Collections;

public class CalcController : BaseToolController {

	int id = 0;
	
	bool Display = false;	

	public GUISkin CustomSkin;
	
	public Texture ButtonTexture;
	
	private string Num1 = "0"; 
	private string Num2 = "0";
	private string Result = "Enter Input and Perform Math.";

	
	public override int getId() {
		return id;
	}
	
	public override void showTool() {
		Display = true;
		Controller.PauseGame();
	}
	
	public override void hideTool() {
		Display = false;
		notifyClose();
		Controller.unpause();
	}

	public override bool isUsing() {
		return Display ==  true;
	}
	
	
	void Start () {
		init();
	}
	// Update is called once per frame
	void Update () {
	}
	
	void OnGUI() {
		if (Display) {
			GUI.skin = CustomSkin;	
			GUILayout.Window(0, new Rect((Screen.width - 300)/2, (Screen.height - 400)/2, 300, 400), 
				DoWindow, "");
		}
	}
	
	void DoWindow(int id) {
		//try 
		{
			GUILayout.Label("First Number: ");
			Num1 = GUILayout.TextArea(Num1);
			GUILayout.Label("Second Number: ");
			Num2 = GUILayout.TextArea(Num2);
			GUILayout.BeginHorizontal();
			if (GUILayout.Button("+")) {
				float op1 = (float) float.Parse(Num1);
				float op2 = (float) float.Parse(Num2);
				Result = Num1 + " + " + Num2 + " = " + (op1+op2);
			}
			GUILayout.Space (20);
			if (GUILayout.Button("-")) {
				float op1 = (float) float.Parse(Num1);
				float op2 = (float) float.Parse(Num2);
				Result = Num1 + " - " + Num2 + " = " + (op1-op2);
			}
			GUILayout.Space (20);
			if (GUILayout.Button("X")) {
				float op1 = (float) float.Parse(Num1);
				float op2 = (float) float.Parse(Num2);
				Result = Num1 + " X " + Num2 + " = " + (op1*op2);
			}
			GUILayout.Space (20);
			if (GUILayout.Button("/")) {
				float op1 = (float) float.Parse(Num1);
				float op2 = (float) float.Parse(Num2);
				Result = Num1 + " / " + Num2 + " = " + (op1/op2);
			}
			GUILayout.EndHorizontal();
			GUILayout.Space(40);
			GUILayout.Label(Result);
			
			GUILayout.FlexibleSpace ();
			if (GUILayout.Button("Close Calculator")) {
				hideTool();
			}
		}
		//catch(Exception e) {}
	}
}
