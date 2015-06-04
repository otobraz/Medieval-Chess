using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {


	private GameObject selectedPiece;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//Return the piece selected atm
	public GameObject getSelectedPiece(){
		return selectedPiece;
	}

	//Select the piece
	public void SelectedPiece(GameObject piece){

		if (getSelectedPiece()) {
			Debug.Log("Animation_Selection"); //Play animation(selection)

		}
		selectedPiece = piece;
		Debug.Log (selectedPiece.name + " is selected");
	}
	
	//Move the piece selected
	public void MovePiece(Vector3 coordToMove){

		Vector3 currentPosition = selectedPiece.transform.position;
		switch(selectedPiece.name){
			case "Pawn":
				if(currentPosition == coordToMove){
					break;
				}else if(currentPosition.x == coordToMove.x && (coordToMove.z - currentPosition.z) < 3 && (coordToMove.z - currentPosition.z) > 0){
					selectedPiece.transform.position = coordToMove;
					Debug.Log ("Animation_moving"); //Play animation(moving)
					selectedPiece = null;
				}
				break;

			case "Bishop":
				if(currentPosition == coordToMove){
					break;
				}else if((coordToMove.x - currentPosition.x) == (coordToMove.z - currentPosition.z) || (coordToMove.x + coordToMove.z) == (currentPosition.x + currentPosition.z)){
					selectedPiece.transform.position = coordToMove;
					Debug.Log ("Animation_moving"); //Play animation(moving)
					selectedPiece = null;
				}
				break;

			case "Rook":
				if(currentPosition == coordToMove){
					break;
				}else if((coordToMove.x != currentPosition.x && coordToMove.z == currentPosition.z) || (coordToMove.x == currentPosition.x && currentPosition.z != coordToMove.z)){
					selectedPiece.transform.position = coordToMove;
					Debug.Log ("Animation_moving"); //Play animation(moving)
					selectedPiece = null;
				}
				break;

			case "Queen":
				if(currentPosition == coordToMove){
					break;
				}else if(((coordToMove.x != currentPosition.x && coordToMove.z == currentPosition.z) || (coordToMove.x == currentPosition.x && currentPosition.z != coordToMove.z))
			    || ((coordToMove.x - currentPosition.x) == (coordToMove.z - currentPosition.z) || (coordToMove.x + coordToMove.z) == (currentPosition.x + currentPosition.z))){
					selectedPiece.transform.position = coordToMove;
					Debug.Log ("Animation_moving"); //Play animation(moving)
					selectedPiece = null;
				}
				break;

			case "King":
				if(currentPosition == coordToMove){
					break;
				}else if(Mathf.Abs(coordToMove.x - currentPosition.x) <= 1 && Mathf.Abs(coordToMove.z - currentPosition.z) <= 1 
			         && Mathf.Abs(coordToMove.z - currentPosition.z) >= 0 && Mathf.Abs(coordToMove.x - currentPosition.x) >= 0) {
					selectedPiece.transform.position = coordToMove;
					Debug.Log ("Animation_moving"); //Play animation(moving)
					selectedPiece = null;
				}
				break;

			case "Horse":
				if(currentPosition == coordToMove){
					break;
				}else if(Mathf.Abs(coordToMove.x - currentPosition.x) == 2){
					if(Mathf.Abs(coordToMove.z - currentPosition.z) == 1){
						selectedPiece.transform.position = coordToMove;
						Debug.Log ("Animation_moving"); //Play animation(moving)
						selectedPiece = null;
					}
				}else if(Mathf.Abs(coordToMove.z - currentPosition.z) == 2){
					if(Mathf.Abs(coordToMove.x - currentPosition.x) == 1){
						selectedPiece.transform.position = coordToMove;
						Debug.Log ("Animation_moving"); //Play animation(moving)
						selectedPiece = null;
					}
				}	
				break;
		}						
	}

	/*
	// for example, this would go on the tile to handle snapping for initial placement:
	// you need to have a reference of the unit being placed
	void OnMouseOver()
	{
		if (tileIsPasable)
		{
			Unit.GetComponent<unitScript>().snapping = true;
			Unit.transform.position = this.transform.position;
		}
	}
	void OnMouseExit()
	{
		if (tileIsPasable)
		{
			Unit.GetComponent<unitScript>().snapping = false;
		}
	}
	void OnMouseDown()
	{
		if (tileIsPasable)
		{
			Unit.GetComponent<unitScript>().snapping = true;
			Unit.transform.position = this.transform.position;
			Unit.GetComponent<unitScript>().state = UnitScript.State.idle;  
		}
	}
	
	// then this bit would be on the unit, 
	//it attaches the unit to the mouse when it is not being snapped to he grid
	if(snapping == false){
		Vector3 screenPos = Input.mousePosition;
		screenPos.z = 40f;
		Vector3 worldPos = Camera.mainCamera.ScreenToWorldPoint(screenPos);
		transform.position = worldPos;
	}*/
}
