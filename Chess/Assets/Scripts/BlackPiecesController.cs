using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class BlackPiecesController : MonoBehaviour {
	
	private bool isFirstMove;
	GameController gameController;
	GameObject gO;
	public Vector3 coordToMove;
	public bool isEnPassantL, isEnPassantR;

	// Use this for initialization
	void Start () {
		isFirstMove = true;
		coordToMove = transform.position;
		isEnPassantL = false;
		isEnPassantR = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(tag == "Black")
			Moving (coordToMove);
	}
	
	public void Moving(Vector3 coordToMove){
		transform.position = Vector3.MoveTowards (transform.position, new Vector3(coordToMove.x, transform.position.y, coordToMove.z), 7 * Time.deltaTime);
		
	}
	
	public List<Vector3> GetMovementsList(int[,] gameBoard){
		int x = (int)gameObject.transform.position.x;
		int z = (int)gameObject.transform.position.z;
		int y = (int)gameObject.transform.localScale.y;
		List<Vector3> pieceMovements = new List<Vector3>();
		switch(gameObject.name){
		case "BlackPawn(Clone)":
			if(x-1 >=0 && gameBoard[x-1, z] == 0){
				pieceMovements.Add(new Vector3(x-1, y, z));
				if(x == 6){
					if(gameBoard[x-2, z] == 0){
						pieceMovements.Add(new Vector3(x-2, y, z));
					}
				}
			}
			if(z+1 < 8 && x-1 >= 0 && gameBoard[x-1, z+1] == 1){
				pieceMovements.Add(new Vector3(x-1, y, z+1));
			}
			if(z-1 >= 0 && x-1 >= 0 && gameBoard[x-1, z-1] == 1){
				pieceMovements.Add(new Vector3(x-1, y, z-1));
			}
			if(isEnPassantL){
				pieceMovements.Add (new Vector3(x-1, y, z-1));Debug.Log ("e");}
			if(isEnPassantR){
				pieceMovements.Add(new Vector3(x-1, y, z+1));Debug.Log ("f");}
			break;
			
		case "BlackBishop(Clone)":
			for(int i = x+1, j = 1; i < 8 && z+j < 8; i++, j++){
				if(gameBoard[i, z+j] != 2){
					pieceMovements.Add(new Vector3(i, y, z+j));
					if(gameBoard[i, z+j] == 1){
						break;
					}
				}else{
					break;
				}
			}
			for(int i = x+1, j = 1; i < 8 && z-j >= 0; i++, j++){
				if(gameBoard[i, z-j] != 2){
					pieceMovements.Add(new Vector3(i, y, z-j));
					if(gameBoard[i, z-j] == 1){
						break;
					}
				}else{
					break;
				}
			}
			for(int i = x-1, j = 1; i >= 0 && z+j < 8; i--, j++){
				if(gameBoard[i, z+j] != 2){
					pieceMovements.Add(new Vector3(i, y, z+j));
					if(gameBoard[i, z+j] == 1){
						break;
					}
				}else{
					break;
				}
			}
			for(int i = x-1, j = 1; i >= 0 && z-j >= 0; i--, j++){
				if(gameBoard[i, z-j] != 2){
					pieceMovements.Add(new Vector3(i, y, z-j));
					if(gameBoard[i, z-j] == 1){
						break;
					}
				}else{
					break;
				}
			}
			break;
			
		case "BlackRook(Clone)":
			for(int i = x+1; i < 8; i++){
				if(gameBoard[i, z] != 2){
					pieceMovements.Add(new Vector3(i, y, z));
					if(gameBoard[i, z] == 1){
						break;
					}
				}else{
					break;
				}
			}
			for(int i = x-1; i >= 0; i--){
				if(gameBoard[i, z] != 2){
					pieceMovements.Add(new Vector3(i, y, z));
					if(gameBoard[i, z] == 1){
						break;
					}
				}else{
					break;
				}
			}
			for(int j = z+1; j < 8; j++){
				if(gameBoard[x, j] != 2){
					pieceMovements.Add(new Vector3(x, y, j));
					if(gameBoard[x, j] == 1){
						break;
					}
				}else{
					break;
				}
			}
			for(int j = z-1; j >= 0; j--){
				if(gameBoard[x, j] != 2){
					pieceMovements.Add(new Vector3(x, y, j));	  
					if(gameBoard[x, j] == 1){
						break;
					}
				}else{
					break;
				}
			}
			break;
			
		case "BlackQueen(Clone)":
			for(int i = x+1, j = 1; i < 8 && z+j < 8; i++, j++){
				if(gameBoard[i, z+j] != 2){
					pieceMovements.Add(new Vector3(i, y, z+j));
					if(gameBoard[i, z+j] == 1){
						break;
					}
				}else{
					break;
				}
			}
			for(int i = x+1, j = 1; i < 8 && z-j >= 0; i++, j++){
				if(gameBoard[i, z-j] != 2){
					pieceMovements.Add(new Vector3(i, y, z-j));
					if(gameBoard[i, z-j] == 1){
						break;
					}
				}else{
					break;
				}
			}
			for(int i = x-1, j = 1; i >= 0 && z+j < 8; i--, j++){
				if(gameBoard[i, z+j] != 2){
					pieceMovements.Add(new Vector3(i, y, z+j));
					if(gameBoard[i, z+j] == 1){
						break;
					}
				}else{
					break;
				}
			}
			for(int i = x-1, j = 1; i >= 0 && z-j >= 0; i--, j++){
				if(gameBoard[i, z-j] != 2){
					pieceMovements.Add(new Vector3(i, y, z-j));
					if(gameBoard[i, z-j] == 1){
						break;
					}
				}else{
					break;
				}
			}
			for(int i = x+1; i < 8; i++){
				if(gameBoard[i, z] != 2){
					pieceMovements.Add(new Vector3(i, y, z));
					if(gameBoard[i, z] == 1){
						break;
					}
				}else{
					break;
				}
			}
			for(int i = x-1; i >= 0; i--){
				if(gameBoard[i, z] != 2){
					pieceMovements.Add(new Vector3(i, y, z));
					if(gameBoard[i, z] == 1){
						break;
					}
				}else{
					break;
				}
			}
			for(int j = z+1; j < 8; j++){
				if(gameBoard[x, j] != 2){
					pieceMovements.Add(new Vector3(x, y, j));
					if(gameBoard[x, j] == 1){
						break;
					}
				}else{
					break;
				}
			}
			for(int j = z-1; j >= 0; j--){
				if(gameBoard[x, j] != 2){
					pieceMovements.Add(new Vector3(x, y, j));	  
					if(gameBoard[x, j] == 1){
						break;
					}
				}else{
					break;
				}
			}
			for(int i = x+1; i < 8; i++){
				if(gameBoard[i, z] != 2){
					pieceMovements.Add(new Vector3(i, y, z));
					if(gameBoard[i, z] == 1){
						break;
					}
				}else{
					break;
				}
			}
			for(int i = x-1; i >= 0; i--){
				if(gameBoard[i, z] != 2){
					pieceMovements.Add(new Vector3(i, y, z));
					if(gameBoard[i, z] == 1){
						break;
					}
				}else{
					break;
				}
			}
			for(int j = z+1; j < 8; j++){
				if(gameBoard[x, j] != 2){
					pieceMovements.Add(new Vector3(x, y, j));
					if(gameBoard[x, j] == 1){
						break;
					}
				}else{
					break;
				}
			}
			for(int j = z-1; j >= 0; j--){
				if(gameBoard[x, j] != 2){
					pieceMovements.Add(new Vector3(x, y, j));	  
					if(gameBoard[x, j] == 1){
						break;
					}
				}else{
					break;
				}
			}
			break;
		case "BlackKing(Clone)":
			if(z+1 < 8 && gameBoard[x, z+1] != 2){
				pieceMovements.Add(new Vector3(x, y, z+1));
			}
			if(x+1 < 8 && z+1 < 8 && gameBoard[x+1, z+1] != 2){
				pieceMovements.Add(new Vector3(x+1, y, z+1));
			}
			if(z-1 >=0 && gameBoard[x, z-1] != 2){
				pieceMovements.Add(new Vector3(x, y, z-1));
			}
			if(z-1 >= 0 && x-1 >= 0 && gameBoard[x-1, z-1] != 2){
				pieceMovements.Add(new Vector3(x-1, y, z-1));
			}
			if(x+1 < 8 && gameBoard[x+1, z] != 2){
				pieceMovements.Add(new Vector3(x+1, y, z));
			}
			if(x-1 >= 0 && gameBoard[x-1, z] != 2){
				pieceMovements.Add(new Vector3(x-1, y, z));
			}
			if(x-1 >= 0 && z+1 < 8 && gameBoard[x-1, z+1] != 2){
				pieceMovements.Add(new Vector3(x-1, y, z+1));
			}
			if(x+1 < 8 && z-1 >= 0 && gameBoard[x+1, z-1] != 2){
				pieceMovements.Add(new Vector3(x+1, y, z-1));
			}
			if(isFirstMove){
				gO = GameObject.FindGameObjectWithTag("GameController");
				gameController = gO.GetComponent<GameController>();
				GameObject[] blackRooks = gameController.GetBlackRooks();
				for(int i = (int)transform.position.z-1; i > blackRooks[0].transform.position.z; i--){
					if(gameBoard[(int)transform.position.x, i] != 0){
						break;
					}
					if(blackRooks[0].GetComponent<BlackPiecesController>().isFirstMove){
						pieceMovements.Add(new Vector3(x, y, z-2));
					}
				}
				for(int i = (int)transform.position.z+1; i < blackRooks[1].transform.position.z; i++){
					if(gameBoard[(int)transform.position.x, i] != 0){
						break;
					}
					if(blackRooks[1].GetComponent<BlackPiecesController>().isFirstMove){
						pieceMovements.Add(new Vector3(x, y, z+2));
					}
				}
			}
			break;
			
		case "BlackHorse(Clone)":
			if(x+2 < 8){
				if( z+1 < 8 && gameBoard[x+2, z+1] != 2){
					pieceMovements.Add(new Vector3(x+2, y, z+1));          
				}
				if(z-1 >= 0 && gameBoard[x+2, z-1] != 2){
					pieceMovements.Add(new Vector3(x+2, y, z-1));
				}
			}
			if(x-2 >= 0){
				if(z+1 < 8 && gameBoard[x-2, z+1] != 2){
					pieceMovements.Add(new Vector3(x-2, y, z+1));          
				}
				if(z-1 >= 0 && gameBoard[x-2, z-1] != 2){
					pieceMovements.Add(new Vector3(x-2, y, z-1));
				}
			}
			if(z+2 < 8){
				if(x+1 < 8 && gameBoard[x+1, z+2] != 2){
					pieceMovements.Add(new Vector3(x+1, y, z+2));          
				}
				if(x-1 >= 0 && gameBoard[x-1, z+2] != 2){
					pieceMovements.Add(new Vector3(x-1, y, z+2));
				}
			}
			if(z-2 >= 0){
				if(x+1 < 8 && gameBoard[x+1, z-2] != 2){
					pieceMovements.Add(new Vector3(x+1, y, z-2));          
				}
				if(x-1 >= 0 && gameBoard[x-1, z-2] != 2){
					pieceMovements.Add(new Vector3(x-1, y, z-2));
				}
			}
			break;
		}	
		return pieceMovements;
	}
}
