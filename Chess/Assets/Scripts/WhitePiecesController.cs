using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class WhitePiecesController : MonoBehaviour {
	
	private bool isFirstMove;
	public bool isEnPassantL, isEnPassantR;
	GameController gameController;
	GameObject gO;
	public Vector3 coordToMove;

	// Use this for initialization
	void Start () {
		coordToMove = transform.position;
		isFirstMove = true;
		isEnPassantL = false;
		isEnPassantR = false;
		gO = GameObject.FindGameObjectWithTag("GameController");
		gameController = gO.GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
		if(tag == "White"){
			Moving (coordToMove);
		}
		if (coordToMove.x != transform.position.x || coordToMove.z != transform.position.z)
			isFirstMove = false;
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
			case "WhitePawn(Clone)":
					if(x+1 < 8  && gameBoard[x+1, z] == 0){
						pieceMovements.Add(new Vector3(x+1, y, z));
						if(x == 1){
							if(gameBoard[x+2, z] == 0){
								pieceMovements.Add(new Vector3(x+2, y, z));
							}
						}
					}	
					if(z+1 < 8 && x+1 < 8 && gameBoard[x+1, z+1] == 2){
						pieceMovements.Add(new Vector3(x+1, y, z+1));
					}
					if(z-1 >= 0 && x+1 < 8 && gameBoard[x+1, z-1] == 2){
						pieceMovements.Add(new Vector3(x+1, y, z-1));
					}
					if(isEnPassantL){
						pieceMovements.Add (new Vector3(x+1, y, z-1));
					}
					if(isEnPassantR){
						pieceMovements.Add(new Vector3(x+1, y, z+1));
					}
					break;
				
			case "WhiteBishop(Clone)":
				for(int i = x+1, j = 1; i < 8 && z+j < 8; i++, j++){
					if(gameBoard[i, z+j] != 1){
						pieceMovements.Add(new Vector3(i, y, z+j));
						if(gameBoard[i, z+j] == 2){
							break;
						}
					}else{
						break;
					}
				}
				for(int i = x+1, j = 1; i < 8 && z-j >= 0; i++, j++){
					if(gameBoard[i, z-j] != 1){
						pieceMovements.Add(new Vector3(i, y, z-j));
						if(gameBoard[i, z-j] == 2){
							break;
						}
					}else{
						break;
					}
				}
				for(int i = x-1, j = 1; i >= 0 && z+j < 8; i--, j++){
					if(gameBoard[i, z+j] != 1){
						pieceMovements.Add(new Vector3(i, y, z+j));
						if(gameBoard[i, z+j] == 2){
							break;
						}
					}else{
						break;
					}
				}
				for(int i = x-1, j = 1; i >= 0 && z-j >= 0; i--, j++){
					if(gameBoard[i, z-j] != 1){
						pieceMovements.Add(new Vector3(i, y, z-j));
						if(gameBoard[i, z-j] == 2){
							break;
						}
					}else{
						break;
					}
				}
				break;
				
			case "WhiteRook(Clone)":
				for(int i = x+1; i < 8; i++){
					if(gameBoard[i, z] != 1){
						pieceMovements.Add(new Vector3(i, y, z));
						if(gameBoard[i, z] == 2){
							break;
						}
					}else{
						break;
					}
				}
				for(int i = x-1; i >= 0; i--){
					if(gameBoard[i, z] != 1){
						pieceMovements.Add(new Vector3(i, y, z));
						if(gameBoard[i, z] == 2){
							break;
						}
					}else{
						break;
					}
				}
				for(int j = z+1; j < 8; j++){
					if(gameBoard[x, j] != 1){
						pieceMovements.Add(new Vector3(x, y, j));
						if(gameBoard[x, j] == 2){
							break;
						}
					}else{
						break;
					}
				}
				for(int j = z-1; j >= 0; j--){
					if(gameBoard[x, j] != 1){
						pieceMovements.Add(new Vector3(x, y, j));	  
						if(gameBoard[x, j] == 2){
							break;
						}
					}else{
						break;
					}
				}
				break;
				
			case "WhiteQueen(Clone)":
				for(int i = x+1, j = 1; i < 8 && z+j < 8; i++, j++){
					if(gameBoard[i, z+j] != 1){
						pieceMovements.Add(new Vector3(i, y, z+j));
						if(gameBoard[i, z+j] == 2){
							break;
						}
					}else{
						break;
					}
				}
				for(int i = x+1, j = 1; i < 8 && z-j >= 0; i++, j++){
					if(gameBoard[i, z-j] != 1){
						pieceMovements.Add(new Vector3(i, y, z-j));
						if(gameBoard[i, z-j] == 2){
							break;
						}
					}else{
						break;
					}
				}
				for(int i = x-1, j = 1; i >= 0 && z+j < 8; i--, j++){
					if(gameBoard[i, z+j] != 1){
						pieceMovements.Add(new Vector3(i, y, z+j));
						if(gameBoard[i, z+j] == 2){
							break;
						}
					}else{
						break;
					}
				}
				for(int i = x-1, j = 1; i >= 0 && z-j >= 0; i--, j++){
					if(gameBoard[i, z-j] != 1){
						pieceMovements.Add(new Vector3(i, y, z-j));
						if(gameBoard[i, z-j] == 2){
							break;
						}
					}else{
						break;
					}
				}
				for(int i = x+1; i < 8; i++){
						if(gameBoard[i, z] != 1){
							pieceMovements.Add(new Vector3(i, y, z));
							if(gameBoard[i, z] == 2){
								break;
							}
						}else{
							break;
						}
					}
					for(int i = x-1; i >= 0; i--){
						if(gameBoard[i, z] != 1){
							pieceMovements.Add(new Vector3(i, y, z));
							if(gameBoard[i, z] == 2){
								break;
							}
						}else{
							break;
						}
					}
					for(int j = z+1; j < 8; j++){
						if(gameBoard[x, j] != 1){
							pieceMovements.Add(new Vector3(x, y, j));
							if(gameBoard[x, j] == 2){
								break;
							}
						}else{
							break;
						}
					}
					for(int j = z-1; j >= 0; j--){
						if(gameBoard[x, j] != 1){
							pieceMovements.Add(new Vector3(x, y, j));	  
							if(gameBoard[x, j] == 2){
								break;
							}
						}else{
							break;
						}
					}
				for(int i = x+1; i < 8; i++){
					if(gameBoard[i, z] != 1){
						pieceMovements.Add(new Vector3(i, y, z));
						if(gameBoard[i, z] == 2){
							break;
						}
					}else{
						break;
					}
				}
				for(int i = x-1; i >= 0; i--){
					if(gameBoard[i, z] != 1){
						pieceMovements.Add(new Vector3(i, y, z));
						if(gameBoard[i, z] == 2){
							break;
						}
					}else{
						break;
					}
				}
				for(int j = z+1; j < 8; j++){
					if(gameBoard[x, j] != 1){
						pieceMovements.Add(new Vector3(x, y, j));
						if(gameBoard[x, j] == 2){
							break;
						}
					}else{
						break;
					}
				}
				for(int j = z-1; j >= 0; j--){
					if(gameBoard[x, j] != 1){
						pieceMovements.Add(new Vector3(x, y, j));	  
						if(gameBoard[x, j] == 2){
							break;
						}
					}else{
						break;
					}
				}
				break;
			case "WhiteKing(Clone)":
				if(z+1 < 8 && gameBoard[x, z+1] != 1){
					pieceMovements.Add(new Vector3(x, y, z+1));
				}
				if(x+1 < 8 && z+1 < 8 && gameBoard[x+1, z+1] != 1){
					pieceMovements.Add(new Vector3(x+1, y, z+1));
				}
				if(z-1 >=0 && gameBoard[x, z-1] != 1){
					pieceMovements.Add(new Vector3(x, y, z-1));
				}
				if(z-1 >= 0 && x-1 >= 0 && gameBoard[x-1, z-1] != 1){
					pieceMovements.Add(new Vector3(x-1, y, z-1));
				}
				if(x+1 < 8 && gameBoard[x+1, z] != 1){
					pieceMovements.Add(new Vector3(x+1, y, z));
				}
				if(x-1 >= 0 && gameBoard[x-1, z] != 1){
					pieceMovements.Add(new Vector3(x-1, y, z));
				}
				if(x-1 >= 0 && z+1 < 8 && gameBoard[x-1, z+1] != 1){
					pieceMovements.Add(new Vector3(x-1, y, z+1));
				}
				if(x+1 < 8 && z-1 >= 0 && gameBoard[x+1, z-1] != 1){
					pieceMovements.Add(new Vector3(x+1, y, z-1));
				}
				if(isFirstMove){
					GameObject[] whiteRooks = gameController.GetWhiteRooks();
					for(int i = (int)transform.position.z-1; i > whiteRooks[0].transform.position.z; i--){
					if(gameBoard[(int)transform.position.x, i] != 0){
							break;
						}
						if(whiteRooks[0].GetComponent<WhitePiecesController>().isFirstMove){
							pieceMovements.Add(new Vector3(x, y, z-2));
						}
					}
					for(int i = (int)transform.position.z+1; i < whiteRooks[1].transform.position.z; i++){
						if(gameBoard[(int)transform.position.x, i] != 0){
							break;
						}
						if(whiteRooks[1].GetComponent<WhitePiecesController>().isFirstMove){
							pieceMovements.Add(new Vector3(x, y, z+2));
						}
					}
					
				}
				break;
				
			case "WhiteHorse(Clone)":
				if(x+2 < 8){
					if( z+1 < 8 && gameBoard[x+2, z+1] != 1){
						pieceMovements.Add(new Vector3(x+2, y, z+1));          
					}
					if(z-1 >= 0 && gameBoard[x+2, z-1] != 1){
						pieceMovements.Add(new Vector3(x+2, y, z-1));
					}
				}
				if(x-2 >= 0){
					if(z+1 < 8 && gameBoard[x-2, z+1] != 1){
						pieceMovements.Add(new Vector3(x-2, y, z+1));          
					}
					if(z-1 >= 0 && gameBoard[x-2, z-1] != 1){
						pieceMovements.Add(new Vector3(x-2, y, z-1));
					}
				}
				if(z+2 < 8){
					if(x+1 < 8 && gameBoard[x+1, z+2] != 1){
						pieceMovements.Add(new Vector3(x+1, y, z+2));          
					}
					if(x-1 >= 0 && gameBoard[x-1, z+2] != 1){
						pieceMovements.Add(new Vector3(x-1, y, z+2));
					}
				}
				if(z-2 >= 0){
					if(x+1 < 8 && gameBoard[x+1, z-2] != 1){
						pieceMovements.Add(new Vector3(x+1, y, z-2));          
					}
					if(x-1 >= 0 && gameBoard[x-1, z-2] != 1){
						pieceMovements.Add(new Vector3(x-1, y, z-2));
					}
				}
				break;
		}	
			return pieceMovements;
	}
}
