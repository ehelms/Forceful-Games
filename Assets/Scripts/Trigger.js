function OnTriggerEnter(other : Collider) {
	bm = other.gameObject.GetComponent("BallMove");
	if (bm)
	{
		bm.speed = -bm.speed;
	}
}

function Update () {
	
}

