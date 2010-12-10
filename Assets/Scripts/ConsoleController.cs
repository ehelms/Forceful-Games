using UnityEngine;
using System.Collections;

public class ConsoleController : MonoBehaviour {

	private float BallMass = 1.0f;
	private float TargetDistance = 2.0f;
	
	public Texture BoardTexture;
	public Texture InfoTipTexture;
	
	private bool Enabled = false;
	private bool ShowController = false;
	private Rect TextRect;
	private string Text = "";
	// Use this for initialization
	
	private HoverCraftController hovercraft;
	private ThrowBallController ball;
	private GameObject trackObject;	
	private ConsoleTrigger trigger;

	private bool locked;
	public GUISkin CustomSkin;
	private int WINDOW_ID = 124873;

	
	private GameController Controller;
	
	void Start () {
		locked = false;
	    hovercraft = GameObject.Find("hovercraft").GetComponent("HoverCraftController") as HoverCraftController;
		ball = GameObject.Find("ThrowBall").GetComponent("ThrowBallController") as ThrowBallController;
		trackObject = GameObject.Find("track");		
		trigger = GameObject.Find("console").GetComponent("ConsoleTrigger") as ConsoleTrigger;
		TextRect = new Rect((Screen.width - BoardTexture.width)/2, (Screen.height - BoardTexture.height)/2, BoardTexture.width, BoardTexture.height);
		Controller = GameObject.Find("Controller").GetComponent("GameController") as GameController;
	}
	
	// Update is called once per frame
	void Update () {
		Screen.showCursor = ShowController;
		if (Enabled) {
			//if (Input.GetKey(KeyCode.E)) {
				ShowController = true;
				Controller.PauseGame();
			//}
		}
	}
	
	void OnGUI() {
		if (Enabled && !ShowController) {
			GUI.Label (new Rect ((Screen.width-InfoTipTexture.width)/2, (Screen.height-InfoTipTexture.height)/2, InfoTipTexture.width, InfoTipTexture.height),InfoTipTexture);
			GUI.Label(new Rect(Screen.width/2 - 150, Screen.height/2 - 10, 300, 20), "Press E to activate Hover Craft Controller");
			return;
		}
		
		if (!ShowController) return;
		TextRect.x = (Screen.width - 590)/2+100;
		TextRect.y = (Screen.height - 415)/2;
		TextRect.width = 590;
		TextRect.height = 415;
		GUI.skin = CustomSkin;	
		GUILayout.Window(WINDOW_ID, TextRect, DoWindow, "");
	}
	
	void DoWindow(int id) {

		GUILayout.BeginHorizontal();
		GUILayout.Label( "Use the formula sheet (G) to choose an appropriate formula to calculate the velocity at which to fire the ball and cart.  The ball takes 2.6 seconds to hit the ground.  Press start after you enter a velocity to fire the cart and ball.\n\n   Enter Velocity: ");
		GUILayout.EndHorizontal();
		
		GUILayout.Space (20);
        
        GUILayout.BeginHorizontal();
		Text = GUILayout.TextField(Text, GUILayout.Width(50));
		GUILayout.EndHorizontal();GUILayout.Space (20);
		
		GUILayout.BeginHorizontal();
		if (GUILayout.Button("Enter")) {
			
			float vel = (float)float.Parse(Text);
			Controller.unpause();
			Enabled = false;
			ShowController = false;
			locked = true; //lock the trigger, so the gui can't open again until the cart is reset
			
			startCart(vel);
		}
		GUILayout.Space (20);
		if (GUILayout.Button("Cancel")) {
			ShowController = false;	
			Enabled = false;
			Controller.unpause();
		}

		GUILayout.EndHorizontal();
		
	}
	
	
	void startCart(float vel) {
		if (hovercraft.isActive()) {
		    //Already activated, just return
			return;
		}
		hovercraft.setVelocity(vel);
		hovercraft.activate();
		ball.activate();
		if (trackObject != null && trackObject.audio != null) {
				trackObject.audio.Play();
		}
	}
	
	public void activate() {
		Enabled = true;
	}

	// will be called from hovercraft controller
	void GenerateSetupForNextTrial() {
		
	}
	
	public bool isLocked() {
	   return locked;
	}
	
	public void setLocked() {
		locked = true;	
	}
	
	public void unlock() {
		locked = false;	
	}	
	
}
