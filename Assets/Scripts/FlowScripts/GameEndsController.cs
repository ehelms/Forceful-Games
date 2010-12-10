using UnityEngine;
using System.Collections;

public class GameEndsController : MonoBehaviour {

	private bool Over = false;
	
	public Texture2D CongText;
	
	private bool ShowCongrats = false;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void OnGUI() {
		if (ShowCongrats) {
			GUI.DrawTexture(new Rect(20, 20, 200, 200), CongText, ScaleMode.ScaleToFit);
		}
	}
	
	public IEnumerator ShowGameEnds() {
		yield return StartCoroutine(OpenGate());
		Debug.Log("XXX");
		yield return StartCoroutine(PrintEnds());
	}
	
	IEnumerator PrintEnds() {
		while(true) {
			ShowCongrats = true;
			Debug.Log("Congs ... ");
			yield return new WaitForSeconds(2);
		}
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
