using UnityEngine;
using System.Collections;

public class WhitePiecesController : MonoBehaviour {

	private bool isFirstMove;
	// Use this for initialization
	void Start () {
		isFirstMove = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool IsMoveValid(Vector3 coordToMove){
		Vector3 currentPosition = gameObject.transform.position;
		switch(gameObject.name){
			case "WhitePawn":
				if(currentPosition == coordToMove){
					return false;
				}else if(currentPosition.x == coordToMove.x && (coordToMove.z - currentPosition.z) <= 2 && (coordToMove.z - currentPosition.z) > 0 && isFirstMove){
					isFirstMove = false;
					return true;
				}else if(currentPosition.x == coordToMove.x && (coordToMove.z - currentPosition.z) == 1){
					return true;
				}
				break;
				
			case "WhiteBishop":
				if(currentPosition == coordToMove){
					return false;
				}else if((coordToMove.x - currentPosition.x) == (coordToMove.z - currentPosition.z) || (coordToMove.x + coordToMove.z) == (currentPosition.x + currentPosition.z)){
					return true;
				}
				break;
				
			case "WhiteRook":
				if(currentPosition == coordToMove){
					return false;
				}else if((coordToMove.x != currentPosition.x && coordToMove.z == currentPosition.z) || (coordToMove.x == currentPosition.x && currentPosition.z != coordToMove.z)){
					return true;
				}
				break;
				
			case "WhiteQueen":
				if(currentPosition == coordToMove){
					return false;
				}else if(((coordToMove.x != currentPosition.x && coordToMove.z == currentPosition.z) || (coordToMove.x == currentPosition.x && currentPosition.z != coordToMove.z))
				         || ((coordToMove.x - currentPosition.x) == (coordToMove.z - currentPosition.z) || (coordToMove.x + coordToMove.z) == (currentPosition.x + currentPosition.z))){
					return true;
				}
				break;
				
			case "WhiteKing":
				if(currentPosition == coordToMove){
					return false;
				}else if(Mathf.Abs(coordToMove.x - currentPosition.x) <= 1 && Mathf.Abs(coordToMove.z - currentPosition.z) <= 1 
				         && Mathf.Abs(coordToMove.z - currentPosition.z) >= 0 && Mathf.Abs(coordToMove.x - currentPosition.x) >= 0) {
					return true;
				}
				break;
				
			case "WhiteHorse":
				if(currentPosition == coordToMove){
					return false;
				}else if(Mathf.Abs(coordToMove.x - currentPosition.x) == 2){
					if(Mathf.Abs(coordToMove.z - currentPosition.z) == 1){
						return true;
					}
				}else if(Mathf.Abs(coordToMove.z - currentPosition.z) == 2){
					if(Mathf.Abs(coordToMove.x - currentPosition.x) == 1){
						return true;
					}
				}	
				break;
		}	
		return false;
	}
}
