using UnityEngine;
using System.Collections;

public class GameEndsController : MonoBehaviour {

	private bool Over = false;
	private GameController gameController;
	
	public Texture2D CongText;
	public Texture2D YouWon;
	
	private bool ShowCongrats = false;
	
	string str = "You Won !!";
	
	int width = Screen.width;
	float ang = 0f;
	int cx = 10, cy = 400; // centerx , y
	int rad = 50;
	int delta = 5;
	Rect r = new Rect(0, 0, 50, 50);
	
	// Use this for initialization
	void Start () {
		gameController = GameObject.Find("Controller").GetComponent("GameController") as GameController;
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void OnGUI() {
		if (ShowCongrats) {
			GUI.DrawTexture(new Rect((Screen.width - YouWon.width)/2, 100, YouWon.width, YouWon.height), YouWon);
			GUI.DrawTexture(new Rect((Screen.width - 200)/2, 300, 200, 200), CongText);
		}
	}
	
	public IEnumerator ShowGameEnds() {
		gameController.DisablePlayer();
		yield return StartCoroutine(OpenGate());
		StartCoroutine(PrintEnds());
	}
	
	IEnumerator PrintEnds() {
		ShowCongrats = true;
		yield return new WaitForSeconds(20);
		ShowCongrats = false;
		Application.Quit();
	}

	IEnumerator OpenGate() {
		GameObject Gate = GameObject.Find("Gate");
		Vector3 Target = new Vector3(Gate.transform.position.x, -7.75f, Gate.transform.position.z);
		while(Gate.transform.position.y >= -5.75f) {
			Gate.transform.position = Vector3.Slerp(Gate.transform.position, Target, Time.deltaTime);
			yield return 0;
		}
	}
}
