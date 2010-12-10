using UnityEngine;
using System.Collections;

public class FormulaController : BaseToolController {

	int id = 3;
	
	private enum Formula : byte {
		NONE = 0,
    	F_eq_MA ,
    	V_eq_D_div_T
    }
    
    public bool Display = false;
    private Formula currentFormula = Formula.NONE;

	public GUISkin CustomSkin;

	private int WINDOW_ID = 8347385;

	private string num1 = "";
	private string num2 = "";
	private string num3 = "";
	private bool error = false; 
	
	
	public override int getId() {
		return id;
	}
	
	public override void showTool() {
		Display = true;
		Controller.PauseGame();
	}
	
	public override void hideTool() {
		notifyClose();
		Controller.unpause();
		Display = false;	
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
			GUILayout.Window(WINDOW_ID, new Rect(100, 100, 300, 400), DoWindow, "");
			//DoWindow(1);
		}
	}
	
	
	
	
	void DoWindow(int id) {
		
		//GUILayout.BeginArea(new Rect(100, 100, 300, 400));
		if (error) {
				GUILayout.Label("ERROR");
		}
		else {
				GUILayout.Label("");
		}
		switch (currentFormula) {
			case Formula.NONE:
				drawFormulaList();
				break;
			case Formula.F_eq_MA:
				drawFeqMA();
				break;
			case Formula.V_eq_D_div_T:
				drawVDT();
				break;
					
			
		}

		GUILayout.Space (20);	
		if (GUILayout.Button("Cancel")) {
			resetValues();
			if (currentFormula != Formula.NONE) {
				currentFormula = Formula.NONE;	
			}
			else {
				hideTool();
			}
		}
	}
	
	void drawFormulaList() {
		GUILayout.Space (20);
		GUILayout.BeginHorizontal();
		if (GUILayout.Button("Force = Mass X Acceleration", GUILayout.Width (200), GUILayout.Height (25))) {
			currentFormula = Formula.F_eq_MA;
		}
		GUILayout.EndHorizontal();

		GUILayout.Space (20);
		GUILayout.BeginHorizontal();		
		if (GUILayout.Button("Velocity = Distance / Time", GUILayout.Width (200), GUILayout.Height (25))) {
			currentFormula = Formula.V_eq_D_div_T;
		}
		GUILayout.EndHorizontal();				
	}
	
	void drawFeqMA() {
		GUILayout.BeginHorizontal();
		GUILayout.Space(70);		
		GUILayout.Label("Force (");
		num1 = GUILayout.TextArea(num1, GUILayout.Width(50));
		GUILayout.Label(") =");
		GUILayout.Space(70);		

		GUILayout.EndHorizontal();

		GUILayout.Space(20);		
		GUILayout.BeginHorizontal();	
		GUILayout.Space(5);		
		GUILayout.Label("Mass (");
		num2 = GUILayout.TextArea(num2, GUILayout.Width(50));
		GUILayout.Label(") X Acceleration (");
		num3 = GUILayout.TextArea(num3, GUILayout.Width(50));
		GUILayout.Label(")");
		GUILayout.Space(5);		
		GUILayout.EndHorizontal();
		
		GUILayout.Space(20);
		
		
		GUILayout.BeginHorizontal();
		GUILayout.Space(20);		
		if (GUILayout.Button("Clear")) {
			resetValues();
		}
		GUILayout.Space(20);

		if (GUILayout.Button("Calculate")) {
			calcFeqMA();
		}
		GUILayout.Space(20);		
		GUILayout.EndHorizontal();	
		GUILayout.Space(20);		
	}
	
	void calcFeqMA() {
		int valid = 0;
		string[] nums = new string[]{ num1, num2, num3};
		foreach (string num in nums) {
			if (isValid(num)) {
				valid++;
			}
		}
		if (valid != 2) {
			error = true;
			return;
		}
		
		if (isValid(num1) && isValid(num2)) {
			num3 = round(float.Parse(num1)/float.Parse(num2)) + "";
		}
		else if(isValid(num2) && isValid(num3)) {
			num1 = round(float.Parse(num2)*float.Parse(num3)) + "";
		}
		else { //num1 & num3 is valid
			num2 = round(float.Parse(num1)/float.Parse(num3)) + "";
		}
		
	}
	
	float round(float val) {
		return Mathf.Round(val*100)/100;
	}
	
	void drawVDT() {
		GUILayout.BeginHorizontal();
		GUILayout.Space(70);		
		GUILayout.Label("Velocity (");
		num1 = GUILayout.TextArea(num1, GUILayout.Width(50));
		GUILayout.Label(") =");
		GUILayout.Space(70);		

		GUILayout.EndHorizontal();

		GUILayout.Space(20);		
		GUILayout.BeginHorizontal();	
		GUILayout.Space(5);		
		GUILayout.Label("Distance (");
		num2 = GUILayout.TextArea(num2, GUILayout.Width(50));
		GUILayout.Label(") / Time (");
		num3 = GUILayout.TextArea(num3, GUILayout.Width(50));
		GUILayout.Label(")");
		GUILayout.Space(5);		
		GUILayout.EndHorizontal();
		
		GUILayout.Space(20);
		
		
		GUILayout.BeginHorizontal();
		GUILayout.Space(20);		
		if (GUILayout.Button("Clear")) {
			resetValues();
		}
		GUILayout.Space(20);

		if (GUILayout.Button("Calculate")) {
			calcVDT();
		}
		GUILayout.Space(20);		
		GUILayout.EndHorizontal();	
		GUILayout.Space(20);		

	}
	
	void calcVDT(){
				int valid = 0;
		string[] nums = new string[]{ num1, num2, num3};
		foreach (string num in nums) {
			if (isValid(num)) {
				valid++;
			}
		}
		if (valid != 2) {
			error = true;
			return;
		}
		
		if (isValid(num1) && isValid(num2)) {
			num3 = round(float.Parse(num2)/float.Parse(num1)) + "";
		}
		else if(isValid(num2) && isValid(num3)) {
			num1 = round(float.Parse(num2)/float.Parse(num3)) + "";
		}
		else { //num1 & num3 is valid
			num2 = round(float.Parse(num3)*float.Parse(num1)) + "";
		}
		
	}
	
	bool isValid(string val) {
		if (val == "") {
			return false;
		}
		try {
			float.Parse(val);
			return true;
		} 
		finally {
		}
	}
	
	void resetValues() {
			num1 = "";
			num2 = "";
			num3 = "";	
			error = false;	
	}
	
	
}
