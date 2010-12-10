using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;



public class PlayQuiz : MonoBehaviour {
	
	
	public class Question {
		public bool gotCorrect = false;
		private int misses = 0;
		
		public string question;
		public string[] answers;
		public string correctAnswer;
		public string[] hints;
		
		private string[] randomizedAnswers;
		public void randomizeAnswers() {
				misses = 0;
				
				randomizedAnswers = new string[answers.Length];
				
				ArrayList tmpList = new ArrayList();
				foreach (string answer in answers) {
					tmpList.Add(answer);	
				}
				
				for (int i = 0; i < answers.Length; i++) {
					int j = (int) Random.Range(0, tmpList.Count);
					randomizedAnswers[i] = (string) tmpList[j];
					tmpList.RemoveAt(j);
				}
		}
		
		public void missed() {
			if (misses < hints.Length) {
				misses++;	
			}	
		}
		public int getMisses() {
			return misses;	
		}
		public string[] getRandomizedAnswers() {
			return randomizedAnswers;
		}
		
	}
	

	public string QuizName;

	
	private GameController Controller;

	
	public GUISkin QuizSkin;
	private bool Display;
	private ArrayList questionList;
	private Question currentQuestion;

	
	// Use this for initialization
	void Start () {
		questionList = new ArrayList();
		Controller = GameObject.Find("Controller").GetComponent("GameController") as GameController;
		FPSInputController	json = GameObject.Find("Player").GetComponent("FPSInputController") as FPSInputController;	
		TextAsset textFile = (TextAsset)Resources.Load(QuizName, typeof(TextAsset));
		Hashtable[] parsed = (Hashtable[]) json.parse(textFile.text);
		foreach (Hashtable hashQuest in parsed) {
			Question q = new Question();
			q.question = (string) hashQuest["question"];
			q.answers = (string[]) hashQuest["answers"];
			q.correctAnswer = (string) hashQuest["correct"];
			q.hints = (string[]) hashQuest["hints"];
			questionList.Add(q);
		}
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.up*100*Time.deltaTime);
	}
	
	void OnGUI() {
		if (Display) {
			GUI.skin = QuizSkin;
				GUILayout.Window(0, new Rect((Screen.width - 700)/2, (Screen.height - 500)/2, 700, 500), 
				DoWindow, "");
		}
	}
	
	void DoWindow(int id) {
		//try 
		{
			GUILayout.BeginHorizontal();
			//GUILayout.Space(250);
			GUILayout.Label("Quiz");
			GUILayout.EndHorizontal();
			GUILayout.Space(5);
			GUILayout.Label("Choose the correct Answer: "); GUILayout.Space(20);

			GUILayout.TextArea(currentQuestion.question); 
			
			GUILayout.Space(30);
			
			string[] randomizedAnswers = currentQuestion.getRandomizedAnswers();
			for (int i = 0; i < randomizedAnswers.Length; i++) {

			   GUILayout.BeginHorizontal();
			   string answer = randomizedAnswers[i];
			   if (GUILayout.Button(answer)) {
			   		if (answer == currentQuestion.correctAnswer) {
						close();
						Controller.answeredQuiz(QuizName);			   			
			   			currentQuestion.gotCorrect = true;
						playSound("rightAnswer");
			   			
			   			print("RIGHT");
			   		}
			   		else {
			   			currentQuestion.missed();
						playSound("wrongAnswer");
			   			print("WRONG");
			   		}
			      
			   }
			   //GUILayout.Space(500);
			   GUILayout.EndHorizontal();
			   GUILayout.Space(30);
			}
			
			GUILayout.BeginHorizontal();
			if (currentQuestion.getMisses() > 0) {
				
				GUILayout.Label("Hint: " + currentQuestion.hints[currentQuestion.getMisses() -1]); 
			}
			else {
				GUILayout.Space(30);
			}
			GUILayout.EndHorizontal();
			
			
			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			
			GUILayout.Space(200);
			if (GUILayout.Button("Close Quiz")) {
				Display = false;
				Controller.unpause();
			}
			GUILayout.Space(200);
			GUILayout.EndHorizontal();
		}
		//catch(Exception e) {}
	}
	
	void close() {
						Display = false;
				Controller.unpause();
	}
	
	bool isCorrect(int index, string ans) {
		string realAnswer = ((string[])questionList[index])[5];
		if (realAnswer == ans) {
			return true;
		}		
		return false;
	}
	
	void OnTriggerEnter(Collider Other) {
		Controller.PauseGame();
		Display = true;
		int Qindx = (int) Random.Range(0, questionList.Count);		
		currentQuestion = (Question) questionList[Qindx];
		currentQuestion.randomizeAnswers();
	}
	
	void playSound(string clipName) {
		AudioClip clip = (AudioClip)Resources.Load(clipName, typeof(AudioClip));
		audio.clip = clip;
		audio.Play();	
	}
	
	
}
