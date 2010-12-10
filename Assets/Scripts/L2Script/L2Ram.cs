using UnityEngine;
using System.Collections;

public class L2Ram : MonoBehaviour {

	private float Weight = 10f;
	
	private TextMesh fltText;
	private int floatDist = 5;
	GameObject Player;	
	Vector3 position;
	Vector3 rotation;
	
	// Use this for initialization
	void Start () {		
		Player = GameObject.Find("Player");
		position = this.transform.position;
		rotation = this.transform.eulerAngles;
		fltText = this.transform.Find("text").gameObject.GetComponent("TextMesh") as TextMesh;
		hideText();
		//showText();
	}
	
	// Update is called once per frame
	void Update () {
		fltText.transform.LookAt(new Vector3(Player.transform.position.x, 1.238406f, Player.transform.position.z));
		fltText.transform.Rotate(0,180,0);
	}
	

	public void reset() {
		if (position.x != 0) {
			this.transform.position = position;
			this.transform.eulerAngles = rotation;
		}
	}
	
	public void showText() {
		fltText.text = Weight + "";
	}
	
	public void hideText() {
		fltText.text = "";	
	}
	
	public float GetWeight()
	{
			return Weight;
	}
	
	public void SetWeight(float wt) {
		Weight = wt;
	}
	
	public void OnTriggerEnter(Collider other) {
			print(other);
	}
}
