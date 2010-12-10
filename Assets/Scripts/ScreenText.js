private var windowRect = Rect (0,0, 300, 200);

private var display = false;
private var objectName = "";
private var info = "";

function OnGUI () {
	
	if (!display) {
		return;
	}

	// Make a popup window
	windowRect.x = Screen.width - windowRect.width;
	windowRect.y = Screen.height - windowRect.height;
	windowRect = GUILayout.Window (0, windowRect, DoControlsWindow, "Controls");
}

function Awake() {
}

function Update () {
}

// Make the contents of the window
function DoControlsWindow (windowID : int) {
	// Make the window be draggable in the top 20 pixels.
	//GUI.DragWindow (Rect (0,0, System.Decimal.MaxValue, 20));
	
	GUILayout.Label (objectName);
	GUILayout.Label(info);
	if (GUILayout.Button ("Close")) {
		objectName = "";
		info = "";
		display = false;
	}
	// Let the player select the character
	
}