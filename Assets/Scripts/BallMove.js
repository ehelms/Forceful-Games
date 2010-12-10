var ballToMove:GameObject;

var speed = 2.0;


function Update () {
	var d = Time.deltaTime * speed;
	transform.Translate(0,  0,  d);	
}