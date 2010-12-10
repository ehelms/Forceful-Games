using UnityEngine;
using System.Collections;

public abstract class BaseToolController : MonoBehaviour { 

	protected GameObject Player;
	protected StudentModel Student;
	protected GameController Controller;

	
	//Call this in your start method
	protected void init() {
		
		Player = GameObject.FindWithTag("Player");
		Student = GameObject.FindWithTag("Player").GetComponent("StudentModel") as StudentModel;
		Controller = GameObject.Find("Controller").GetComponent("GameController") as GameController;
		
	}
	
	//This function is for notifying a tool that it's normal open button was pressed after it was opened
	// Commonly used for closing or deactivating the tool, NOTE: this may not work if tool pauses the game
	public virtual void reactivated() {
		return;
	}
	
	public abstract int getId();
	
	public abstract void showTool();
	
	public abstract void hideTool();
	public abstract bool  isUsing();
	
	
	protected void notifyClose() {
			Student.toolClosedNotifcation(getId());
	}
}
