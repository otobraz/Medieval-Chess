using UnityEngine;
using System.Collections;

public class WhitePiecesController : MonoBehaviour {

	public Vector3 coordToMove;
	public bool isFirstMove;
	// Use this for initialization
	void Start () {
		isFirstMove = true;
		coordToMove = new Vector3 (transform.position.x, 0.25f, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		//Moving (coordToMove);
	}

	/*public void Moving(Vector3 coordToMove){
		transform.position = Vector3.MoveTowards (transform.position, coordToMove, 10 * Time.deltaTime);

	}*/

	public bool IsMoveValid(Vector3 coordToMove){
		Vector3 currentPosition = gameObject.transform.position;
		switch(gameObject.name){
			case "WhitePawn(Clone)":
				if(currentPosition == coordToMove){
					return false;
				}else if(currentPosition.z == coordToMove.z && (coordToMove.x - currentPosition.x) <= 2 && (coordToMove.x - currentPosition.x) > 0 && isFirstMove){
					isFirstMove = false;
					return true;
				}else if(currentPosition.z == coordToMove.z && (coordToMove.x - currentPosition.x) == 1){
					return true;
				}
				break;
				
			case "WhiteBishop(Clone)":
				if(currentPosition == coordToMove){
					return false;
				}else if((coordToMove.z - currentPosition.z) == (coordToMove.x - currentPosition.x) || (coordToMove.z + coordToMove.x) == (currentPosition.z + currentPosition.x)){
					return true;
				}
				break;
				
			case "WhiteRook(Clone)":
				if(currentPosition == coordToMove){
					return false;
				}else if((coordToMove.z != currentPosition.z && coordToMove.x == currentPosition.x) || (coordToMove.z == currentPosition.z && currentPosition.x != coordToMove.x)){
					return true;
				}
				break;
				
			case "WhiteQueen(Clone)":
				if(currentPosition == coordToMove){
					return false;
				}else if(((coordToMove.z != currentPosition.z && coordToMove.x == currentPosition.x) || (coordToMove.z == currentPosition.z && currentPosition.x != coordToMove.x))
				         || ((coordToMove.z - currentPosition.z) == (coordToMove.x - currentPosition.x) || (coordToMove.z + coordToMove.x) == (currentPosition.z + currentPosition.x))){
					return true;
				}
				break;
				
			case "WhiteKing(Clone)":
				if(currentPosition == coordToMove){
					return false;
				}else if(Mathf.Abs(coordToMove.z - currentPosition.z) <= 1 && Mathf.Abs(coordToMove.x - currentPosition.x) <= 1 
				         && Mathf.Abs(coordToMove.x - currentPosition.x) >= 0 && Mathf.Abs(coordToMove.z - currentPosition.z) >= 0) {
					return true;
				}
				break;
				
			case "WhiteHorse(Clone)":
				if(currentPosition == coordToMove){
					return false;
				}else if(Mathf.Abs(coordToMove.z - currentPosition.z) == 2){
					if(Mathf.Abs(coordToMove.x - currentPosition.x) == 1){
						return true;
					}
				}else if(Mathf.Abs(coordToMove.x - currentPosition.x) == 2){
					if(Mathf.Abs(coordToMove.z - currentPosition.z) == 1){
						return true;
					}
				}	
				break;
		}	
		return false;
	}
}
