using UnityEngine;
using System.Collections;

public class GameEndsController : MonoBehaviour {

	private bool Over = false;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (!Over) {
			ShowGameEnds();
			Over = true;
		}
	}
	
	public IEnumerator ShowGameEnds() {
		yield return StartCoroutine(OpenGate());
		yield return StartCoroutine(PrintEnds());
	}
	
	IEnumerator PrintEnds() {
		while(true) {
			Debug.Log("Ending ...");
			yield return new WaitForSeconds(2);
		}
	}

	IEnumerator OpenGate() {
		while(true) {
			yield return 0;
		}
	}
}
