using UnityEngine;
using System.Collections;

public class WolfController : MonoBehaviour {

	private TextMesh fltText;
	GameObject Player;
	
	// Use this for initialization
	void Start () {
		Player = GameObject.Find("Player");

		fltText = this.transform.Find("WolfText").gameObject.GetComponent("TextMesh") as TextMesh;
	}
	
	// Update is called once per frame
	void Update () {
		fltText.transform.LookAt(new Vector3(Player.transform.position.x, 1.238406f, Player.transform.position.z));
		fltText.transform.Rotate(0,180,0);
	}
}
