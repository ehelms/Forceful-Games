using UnityEngine;
using System.Collections;

public class L2ProblemController : MonoBehaviour {
	
	private class Problem {
		public float[] ramValues { get; set; }
		public float cageWeight {get; set;}
		public float accel {get; set; }
		public float finalForce  {get; set;}
		public static Problem deserialize(Hashtable hashQuest) {
			Problem prob = new Problem();
			
			prob.cageWeight = (float.Parse((string) hashQuest["cageWeight"]));
			prob.accel = (float.Parse((string) hashQuest["acceleration"]));
			prob.finalForce = (float.Parse((string) hashQuest["force"])); 
			
			prob.ramValues = new float[5];
			for(int i= 0; i < 5; i++) {
				prob.ramValues[i] = float.Parse(((string[]) hashQuest["rams"])[i]);
				
			}
			return prob;
		}
	}
	
	
	L2Ram[] rams = new L2Ram[5];
	private ArrayList problemList;
	Problem currentProblem;
	
	CageController cage;
	
	// Use this for initialization
	void Start () {
		
		cage = GameObject.Find("CageBase").GetComponent("CageController") as CageController;
		
		for (int i =0; i < 5; i++) {
			rams[i] = GameObject.Find("L2Ram" + (i+1)).GetComponent("L2Ram") as L2Ram;
		}
		problemList = new ArrayList();
		loadProblems();		
		newProblem();
	}
	
	void loadProblems() {
		FPSInputController	json = GameObject.Find("Player").GetComponent("FPSInputController") as FPSInputController;	
		TextAsset textFile = (TextAsset)Resources.Load("Level2Problems", typeof(TextAsset));
		Hashtable[] parsed = (Hashtable[]) json.parse(textFile.text);
		print(parsed.Length);
		foreach (Hashtable hash in parsed) {
			problemList.Add(Problem.deserialize(hash));
		}
	}
	
	public void flingCage() {
		float Total = cage.GetTotalWeight();
		print(Total + "," + currentProblem.accel + "," + currentProblem.finalForce);
		if (approx(Total*currentProblem.accel, currentProblem.finalForce)) {
			cage.flingAndExplode();
		}
		else if (Total*currentProblem.accel < currentProblem.finalForce) {
			cage.fling();
		}
		else {
			cage.flingAndHurt();
		}
		
	}
	
	public bool approx(float a, float b) {
			return a/b > 0.99 && a/b < 1.01;		
	}
	
	void resetRams() {
		foreach (L2Ram ram in rams) {
			ram.reset();
		}
	}
	
	public void resetCurrentProblem() {
		cage.reset();
		resetRams();
	}
	
	public void newProblem() {
		cage.reset();
		resetRams();
		int Qindx = (int) Random.Range(0, problemList.Count);				
		currentProblem = (Problem) problemList[Qindx];
		cage.setWeight(currentProblem.cageWeight);
		for(int i = 0; i < 5; i++) {
			rams[i].SetWeight(currentProblem.ramValues[i]);
		}
	}
	
	public string GetProblemText() {
		if (currentProblem == null) return null;
		string txt = "";
		txt = "Cage of weight " + currentProblem.cageWeight + "g needs " +
			currentProblem.finalForce + "N (force) to break it. Cage will accelerate at a rate of " + currentProblem.accel + "m/s/s." + 
			" Load the cage with the Ram(s) to make it heavier, so that it experience enough force to break, when it hits the block.";
		return txt;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
