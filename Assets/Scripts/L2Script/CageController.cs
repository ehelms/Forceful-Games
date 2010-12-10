using UnityEngine;
using System.Collections;

public class CageController : MonoBehaviour {

	private float TotalWeight = 0f;
	private float totTime = 0;
	
	private float crashBackMovement = -3;

	private State currentState;
	private float velocity;
	private enum State { INITIAL=1, MOVING, CRASHED};
	
	public enum FinalState {EXPLODE =1, HURT, NOTHING};
	public FinalState setFinalState = FinalState.NOTHING;
	
	
	private GameObject cage;
	
	private GameObject leftSide;
	private GameObject rightSide;
	private GameObject frontSide;
	private GameObject backSide;
	GameObject Player;	
	
	private TextMesh fltText;
	
	private ArrayList otherItems;
	
	private Vector3[,] cageLoc = new Vector3[5,2];
	private ArrayList otherPos = new ArrayList();
	private ArrayList otherRot = new ArrayList();
	
	private float weight = 0;

	public delegate void EventHandler(GameObject e);
	public event EventHandler CrashEvent;
	GameObject wolf;
	
	// Use this for initialization
	void Start () {
		velocity = 10f;
		currentState = State.INITIAL;
		cage = GameObject.Find("Cage");
		leftSide = GameObject.Find("CageL");
		rightSide = GameObject.Find("CageR");
		frontSide = GameObject.Find("CageF");
		backSide = GameObject.Find("CageB");
		saveCageLocation();
		
		Player = GameObject.Find("Player");
		initText();
		fltText.text = weight + "";
		
		otherItems = new ArrayList();	
		GameObject wolf	 = GameObject.Find("Wolf");
		addItem(wolf);
		setWolfText("");
		
						//wolf.transform.Find(".020").gameObject.transform.Rotate(180,0,0, Space.Self);


	}
	

	
	void saveCageLocation() {
		GameObject[] list =  {cage, leftSide, rightSide, frontSide, backSide };
		for(int i = 0; i < list.Length; i++) {
			cageLoc[i,0] = list[i].transform.position;
			cageLoc[i,1] = list[i].transform.eulerAngles;
		}
	}
	
	void restoreCageLocation() {
		if (cage == null) {
			return;	
		}		
		GameObject[] list =  {cage, leftSide, rightSide, frontSide, backSide };
		for(int i = 0; i < list.Length; i++) {
			list[i].transform.position = cageLoc[i,0];
			list[i].transform.eulerAngles = cageLoc[i,1];
		}
	}	

    public void addItem(GameObject item) {		
    	otherItems.Add(item);
    	otherPos.Add(item.transform.position);
		otherRot.Add(item.transform.eulerAngles);
		Debug.Log("Adding to cage:" + item.name);
    }
    
	public void removeItem(GameObject item) {
		otherItems.Remove(item);
		otherPos.Remove(item.transform.position);
		otherRot.Remove(item.transform.eulerAngles);
		Debug.Log("Removing From Cage:" + item.name);
	}

    public void restoreOtherItems() {
		for(int i = 0; i < otherItems.Count; i++) {
			((GameObject)otherItems[i]).transform.position = (Vector3) otherPos[i];
			((GameObject)otherItems[i]).transform.eulerAngles = (Vector3) otherRot[i];
		}
    }

	
	// Update is called once per frame
	void Update () {
		fltText.transform.LookAt(new Vector3(Player.transform.position.x, 1.238406f, Player.transform.position.z));
		fltText.transform.Rotate(0,180,0);		
		
		if (currentState == State.MOVING) {
				float v = (velocity*0.05f);
				totTime = Time.deltaTime + totTime;
				cage.transform.Translate(v, 0, 0);
				
				foreach (GameObject obj in otherItems) {
				    obj.transform.Translate(v, 0,0, this.transform);	
				}
		}
	}
	
	
	public float GetTotalWeight() {
		float totalWeight = weight;
		foreach (GameObject obj in otherItems) {
			if (obj.GetComponent("L2Ram") != null) {
				L2Ram tmpRam = obj.GetComponent("L2Ram") as L2Ram;
				totalWeight += tmpRam.GetWeight();
			}
		}
		return totalWeight;
	}
	
	
	public void fling() {
		setFinalState = FinalState.NOTHING;
		move();
	}
	
	public void flingAndExplode() {
		setFinalState = FinalState.EXPLODE;
		move();
	}
	
	
	public void flingAndHurt() {
		setFinalState  = FinalState.HURT;
		move();		
	}
	
	void move() {
		/*if (currentState == State.CRASHED) {
			reset();
			currentState = State.INITIAL;	
		}
		else */ if (currentState == State.INITIAL) { 
			currentState = State.MOVING;
		}
	}
	
	
	public void crash() {
		currentState = State.CRASHED;
		moveBack();
		if (setFinalState == FinalState.EXPLODE) {
				print("EXPLODE");
				explode();
		}
		if (setFinalState == FinalState.HURT) {
				print("EXPLODE AND HURT");
				explode();
				//qwolf.transform.Rotate(0,0,180);
				setWolfText("!@$% :(");
		}
		if (setFinalState == FinalState.NOTHING) {
				print("NOTHING");
		}
		onCrashEvent();
	}
	
	void onCrashEvent(){
		if(CrashEvent != null){
			CrashEvent(this.gameObject);
		}
	}
	void setWolfText(string txt) {
			TextMesh wlfTxt = GameObject.Find("WolfText").GetComponent("TextMesh") as TextMesh;
			wlfTxt.text = txt;
	}
	
	
	void moveBack() {
		cage.transform.Translate(crashBackMovement, 0, 0);
		foreach (GameObject obj in otherItems) {
		    obj.transform.Translate(crashBackMovement, 0,0, this.transform);	
		}		
	}
	
	void explode() {
		leftSide.transform.Rotate(90, 0, 0);
		rightSide.transform.Rotate(-90, 0, 0);
		rightSide.transform.Translate(0f,0f,.25f);		
		frontSide.transform.Rotate(90,0,0);
		backSide.transform.Translate(0f,.25f,-.5f);
		backSide.transform.Rotate(-90,0,0);
	}
	
		
	public  void reset() {
		currentState = State.INITIAL;
		restoreCageLocation();
		otherItems = new ArrayList();		
		addItem(GameObject.Find("Wolf"));
		restoreOtherItems();
		
		setWolfText("");
	}
	
	public void setVelocity(int vel) {
		velocity = vel;
	}

	public float getWeight() {
			return weight;
	}
	
	//makes sure the txt is initiazled
	void initText(){
		if (fltText == null) {
			 fltText = GameObject.Find("CageText").gameObject.GetComponent("TextMesh") as TextMesh;
		}
	}
	
	
	public void setWeight(float wIn) {
			initText();
			fltText.text =  wIn + "";
			weight = wIn;
	}
	
	
	// when ram is dropped
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.name[0] >= '1' && other.gameObject.name[0] <= '5') {
			string RamName = "L2Ram" + other.gameObject.name;
			GameObject RamGO = GameObject.Find(RamName);
			TotalWeight += (RamGO.GetComponent("L2Ram") as L2Ram).GetWeight();
			addItem(RamGO);
		}
	}
	
	// when ram is picked up
	void OnTriggerExit(Collider other) {
		if (other.gameObject.name[0] >= '1' && other.gameObject.name[0] <= '5') {
			string RamName = "L2Ram" + other.gameObject.name;
			GameObject RamGO = GameObject.Find(RamName);
			TotalWeight -= (RamGO.GetComponent("L2Ram") as L2Ram).GetWeight();
			removeItem(RamGO);
		}
	}
}
