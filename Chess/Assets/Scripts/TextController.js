function OnMouseEnter()
{
	// change the color of the text
	GetComponent.<Renderer>().material.color = Color.gray;
}

function OnMouseExit()
{
	// change the color of the text
	GetComponent.<Renderer>().material.color = Color.white;
}

function OnMouseUp()
{
	Application.LoadLevel(1);
}