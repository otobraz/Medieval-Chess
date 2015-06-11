using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameController : MonoBehaviour {

	private int gameState, nBlackCaptured, nWhiteCaptured;

	private int[,] gameBoard;

	private GameObject selectedPiece;

	public GameObject blackPawn, blackRook, blackHorse, blackBishop, blackQueen, blackKing;

	public GameObject whitePawn, whiteRook, whiteHorse, whiteBishop, whiteQueen, whiteKing;

	private List<GameObject> whitePieces = new List<GameObject>();
	private List<GameObject> blackPieces = new List<GameObject>();

	private List<Vector3> possibleMovements = new List<Vector3> ();



	//  Use this for initialization
	void Start () {	
		initializeGameBoard ();
		setGameState (0);
		nBlackCaptured = 0;
		nWhiteCaptured = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void CheckPawnMovements(GameObject sPiece, int x, int z){
		switch (sPiece.tag) {
		case "White":
			if(x+1 < 8  && gameBoard[x+1, z] != 1){
				possibleMovements.Add(new Vector3(x+1, sPiece.transform.position.y, z));
				if(x == 1){
					if(gameBoard[x+2, z] != 1){
						possibleMovements.Add(new Vector3(x+2, sPiece.transform.position.y, z));
					}
				}
				if(z+1 < 8 && gameBoard[x+1, z+1] == 2){
					possibleMovements.Add (new Vector3(x+1, sPiece.transform.position.y, z+1));
				}
				if(z-1 >= 0 && gameBoard[x+1, z-1] == 2){
					possibleMovements.Add(new Vector3(x+1, sPiece.transform.position.y, z-1));
				}
			}
			break;
			
		case "Black":
			if(x-1 >=0 && gameBoard[x-1, z] != 2){
				possibleMovements.Add(new Vector3(x-1, sPiece.transform.position.y, z));
				if(x == 6){
					if(gameBoard[x-2, z] != 2){
						possibleMovements.Add(new Vector3(x-2, sPiece.transform.position.y, z));
					}
				}
				if(z+1 < 8 && gameBoard[x-1, z+1] == 1){
					possibleMovements.Add (new Vector3(x-1, sPiece.transform.position.y, z+1));
				}
				if(z-1 >= 0 && gameBoard[x-1, z-1] == 1){
					possibleMovements.Add(new Vector3(x-1, sPiece.transform.position.y, z-1));
				}
			}
			break;
		}
	}

	public void CheckBishopMovements(GameObject sPiece, int x, int z){
		switch(sPiece.tag){
			case "White":
				for(int i = x+1, j = 1; i < 8 && z+j < 8; i++, j++){
					if(gameBoard[i, z+j] != 1){
						possibleMovements.Add(new Vector3(i, sPiece.transform.position.y, z+j));
					}else{
						break;
					}
				}
				for(int i = x+1, j = 1; i < 8 && z-j >= 0; i++, j++){
					if(gameBoard[i, z-j] != 1){
						possibleMovements.Add(new Vector3(i, sPiece.transform.position.y, z-j));
					}else{
						break;
					}
				}
				for(int i = x-1, j = 1; i >= 0 && z+j < 8; i--, j++){
					if(gameBoard[i, z+j] != 1){
						possibleMovements.Add(new Vector3(i, sPiece.transform.position.y, z+j));
					}else{
						break;
					}
				}
				for(int i = x-1, j = 1; i >= 0 && z-j >= 0; i--, j++){
					if(gameBoard[i, z-j] != 1){
						possibleMovements.Add(new Vector3(i, sPiece.transform.position.y, z-j));	  
					}else{
						break;
					}
				}
				break;
			case "Black":
				for(int i = x+1, j = 1; i < 8 && z+j < 8; i++, j++){
					if(gameBoard[i, z+j] != 2){
						possibleMovements.Add(new Vector3(i, sPiece.transform.position.y, z+j));
					}else{
						break;
					}
				}
				for(int i = x+1, j = 1; i < 8 && z-j >= 0; i++, j++){
					if(gameBoard[i, z-j] != 2){
						possibleMovements.Add(new Vector3(i, sPiece.transform.position.y, z-j));
					}else{
						break;
					}
				}
				for(int i = x-1, j = 1; i >= 0 && z+j < 8; i--, j++){
					if(gameBoard[i, z+j] != 2){
						possibleMovements.Add(new Vector3(i, sPiece.transform.position.y, z+j));
					}else{
						break;
					}
				}
				for(int i = x-1, j = 1; i >= 0 && z-j >= 0; i--, j++){
					if(gameBoard[i, z-j] != 2){
						possibleMovements.Add(new Vector3(i, sPiece.transform.position.y, z-j));	  
					}else{
						break;
					}
				}
				break;
		}
	}

	public void CheckRookMovements(GameObject sPiece, int x, int z){
		switch(sPiece.tag){
			case "White":
				for(int i = x+1; i < 8; i++){
					if(gameBoard[i, z] != 1){
						possibleMovements.Add(new Vector3(i, sPiece.transform.position.y, z));
					}else{
						break;
					}
				}
				for(int i = x-1; i >= 0; i--){
					if(gameBoard[i, z] != 1){
						possibleMovements.Add(new Vector3(i, sPiece.transform.position.y, z));
					}else{
						break;
					}
				}
				for(int j = z+1; j < 8; j++){
					if(gameBoard[x, j] != 1){
						possibleMovements.Add(new Vector3(x, sPiece.transform.position.y, j));
					}else{
						break;
					}
				}
				for(int j = z-1; j >= 0; j--){
					if(gameBoard[x, j] != 1){
						possibleMovements.Add(new Vector3(x, sPiece.transform.position.y, j));	  
					}else{
						break;
					}
				}
				break;
			case "Black":
				for(int i = x+1; i < 8; i++){
					if(gameBoard[i, z] != 2){
						possibleMovements.Add(new Vector3(i, sPiece.transform.position.y, z));
					}else{
						break;
					}
				}
				for(int i = x-1; i >= 0; i--){
					if(gameBoard[i, z] != 2){
						possibleMovements.Add(new Vector3(i, sPiece.transform.position.y, z));
					}else{
						break;
					}
				}
				for(int j = z+1; j < 8; j++){
					if(gameBoard[x, j] != 2){
						possibleMovements.Add(new Vector3(x, sPiece.transform.position.y, j));
					}else{
						break;
					}
				}
				for(int j = z-1; j >= 0; j--){
					if(gameBoard[x, j] != 2){
						possibleMovements.Add(new Vector3(x, sPiece.transform.position.y, j));	  
					}else{
						break;
					}
				}
				break;
			}
	}

	public void CheckKingMovements(GameObject sPiece, int x, int z){
		if (sPiece.tag == "White") {
			if(z+1 < 8 && gameBoard[x, z+1] != 1){
				possibleMovements.Add (new Vector3(x, sPiece.transform.position.y, z+1));
			}
			if(x+1 < 8 && z+1 < 8 && gameBoard[x+1, z+1] != 1){
				possibleMovements.Add (new Vector3(x+1, sPiece.transform.position.y, z+1));
			}
			if(z-1 >=0 && gameBoard[x, z-1] != 1){
				possibleMovements.Add (new Vector3(x, sPiece.transform.position.y, z-1));
			}
			if(z-1 >= 0 && x-1 >= 0 && gameBoard[x-1, z-1] != 1){
				possibleMovements.Add (new Vector3(x-1, sPiece.transform.position.y, z-1));
			}
			if(x+1 < 8 && gameBoard[x+1, z] != 1){
				possibleMovements.Add (new Vector3(x+1, sPiece.transform.position.y, z));
			}
			if(x-1 >= 0 && gameBoard[x-1, z] != 1){
				possibleMovements.Add (new Vector3(x-1, sPiece.transform.position.y, z));
			}
			if(x-1 >= 0 && z+1 < 8 && gameBoard[x-1, z+1] != 1){
				possibleMovements.Add (new Vector3(x-1, sPiece.transform.position.y, z+1));
			}
			if(x+1 < 8 && z-1 >= 0 && gameBoard[x+1, z-1] != 1){
				possibleMovements.Add (new Vector3(x+1, sPiece.transform.position.y, z-1));
			}
		}else if(sPiece.tag == "Black"){
			if(z+1 < 8 && gameBoard[x, z+1] != 2){
				possibleMovements.Add (new Vector3(x, sPiece.transform.position.y, z+1));
			}
			if(x+1 < 8 && z+1 < 8 && gameBoard[x+1, z+1] != 2){
				possibleMovements.Add (new Vector3(x+1, sPiece.transform.position.y, z+1));
			}
			if(z-1 >=0 && gameBoard[x, z-1] != 2){
				possibleMovements.Add (new Vector3(x, sPiece.transform.position.y, z-1));
			}
			if(z-1 >= 0 && x-1 >= 0 && gameBoard[x-1, z-1] != 2){
				possibleMovements.Add (new Vector3(x-1, sPiece.transform.position.y, z-1));
			}
			if(x+1 < 8 && gameBoard[x+1, z] != 2){
				possibleMovements.Add (new Vector3(x+1, sPiece.transform.position.y, z));
			}
			if(x-1 >= 0 && gameBoard[x-1, z] != 2){
				possibleMovements.Add (new Vector3(x-1, sPiece.transform.position.y, z));
			}
			if(x-1 >= 0 && z+1 < 8 && gameBoard[x-1, z+1] != 2){
				possibleMovements.Add (new Vector3(x-1, sPiece.transform.position.y, z+1));
			}
			if(x+1 < 8 && z-1 >= 0 && gameBoard[x+1, z-1] != 2){
				possibleMovements.Add (new Vector3(x+1, sPiece.transform.position.y, z-1));
			}
		}	
	}

	public void CheckHorseMovements(GameObject sPiece, int x, int z){
		if (sPiece.tag == "White") {
			if(x+2 < 8){
				if( z+1 < 8 && gameBoard[x+2, z+1] != 1){
					possibleMovements.Add (new Vector3(x+2, sPiece.transform.position.y, z+1));          
				}
				if(z-1 >= 0 && gameBoard[x+2, z-1] != 1){
					possibleMovements.Add (new Vector3(x+2, sPiece.transform.position.y, z-1));
				}
			}
			if(x-2 >= 0){
				if(z+1 < 8 && gameBoard[x-2, z+1] != 1){
					possibleMovements.Add (new Vector3(x-2, sPiece.transform.position.y, z+1));          
				}
				if(z-1 >= 0 && gameBoard[x-2, z-1] != 1){
					possibleMovements.Add (new Vector3(x-2, sPiece.transform.position.y, z-1));
				}
			}
			if(z+2 < 8){
				if(x+1 < 8 && gameBoard[x+1, z+2] != 1){
					possibleMovements.Add (new Vector3(x+1, sPiece.transform.position.y, z+2));          
				}
				if(x-1 >= 0 && gameBoard[x-1, z+2] != 1){
					possibleMovements.Add (new Vector3(x-1, sPiece.transform.position.y, z+2));
				}
			}
			if(z-2 >= 0){
				if(x+1 < 8 && gameBoard[x+1, z-2] != 1){
					possibleMovements.Add (new Vector3(x+1, sPiece.transform.position.y, z-2));          
				}
				if(x-1 >= 0 && gameBoard[x-1, z-2] != 1){
					possibleMovements.Add (new Vector3(x-1, sPiece.transform.position.y, z-2));
				}
			}
		}else if(sPiece.tag == "Black"){
			if(x+2 < 8){
				if(z+1 < 8 && gameBoard[x+2, z+1] != 2){
					possibleMovements.Add (new Vector3(x+2, sPiece.transform.position.y, z+1));          
				}
				if(z-1 >= 0 && gameBoard[x+2, z-1] != 2){
					possibleMovements.Add (new Vector3(x+2, sPiece.transform.position.y, z-1));
				}
			}
			if(x-2 >= 0){
				if(z+1 < 8 && gameBoard[x-2, z+1] != 2){
					possibleMovements.Add (new Vector3(x-2, sPiece.transform.position.y, z+1));          
				}
				if(z-1 >= 0 && gameBoard[x-2, z-1] != 2){
					possibleMovements.Add (new Vector3(x-2, sPiece.transform.position.y, z-1));
				}
			}
			if(z+2 < 8){
				if(x+1 < 8 && gameBoard[x+1, z+2] != 2){
					possibleMovements.Add (new Vector3(x+1, sPiece.transform.position.y, z+2));          
				}
				if(x-1 >= 0 && gameBoard[x-1, z+2] != 2){
					possibleMovements.Add (new Vector3(x-1, sPiece.transform.position.y, z+2));
				}
			}
			if(z-2 >= 0){
				if(x+1 < 8 && gameBoard[x+1, z-2] != 2){
					possibleMovements.Add (new Vector3(x+1, sPiece.transform.position.y, z-2));          
				}
				if(x-1 >= 0 && gameBoard[x-1, z-2] != 2){
					possibleMovements.Add (new Vector3(x-1, sPiece.transform.position.y, z-2));
				}
			}
		}	
	}

	public void CheckPossibleMovements(GameObject sPiece){
		int x = (int)sPiece.transform.position.x;
		int z = (int)sPiece.transform.position.z;

		if(sPiece.name == "WhitePawn(Clone)" || sPiece.name == "BlackPawn(Clone)"){
			CheckPawnMovements(sPiece, x, z);
		}

		if(sPiece.name == "WhiteBishop(Clone)" || sPiece.name == "BlackBishop(Clone)"){
			CheckBishopMovements(sPiece, x, z);
		}

		if(sPiece.name == "WhiteRook(Clone)" || sPiece.name == "BlackRook(Clone)"){
			CheckRookMovements(sPiece, x, z);
		}
				
		if (sPiece.name == "WhiteQueen(Clone)" || sPiece.name == "BlackQueen(Clone)") {
			CheckBishopMovements(sPiece, x, z);
			CheckRookMovements(sPiece, x, z);
		}
				
		if (sPiece.name == "WhiteKing(Clone)" || sPiece.name == "BlackKing(Clone)") {
			CheckKingMovements(sPiece, x, z);
		}

		if (sPiece.name == "WhiteHorse(Clone)" || sPiece.name == "BlackHorse(Clone)") {
			CheckHorseMovements(sPiece, x, z);
		}

		foreach(Vector3 v in  possibleMovements){
			Debug.Log(v);
		}
	}

	public int getGameState(){
		return gameState;
	}

	public void setGameState(int nGS){
		gameState = nGS;
	}
	public void initializeGameBoard(){
		gameBoard = new int[8, 8];
		for (int i = 0; i < 8; i++) {
			gameBoard[1,i] = 1;	whitePieces.Add((GameObject)Instantiate (whitePawn, new Vector3 (1, whitePawn.transform.position.y, i), Quaternion.identity));
			gameBoard[6,i] = 2;	blackPieces.Add((GameObject)Instantiate (blackPawn, new Vector3 (6, whitePawn.transform.position.y, i), Quaternion.identity));
			gameBoard[0,i] = 1;
			gameBoard[7,i] = 2;
		}

		/*gameBoard [0, 0] = "wR";	Instantiate (whiteRook, new Vector3 (0, whiteRook.transform.position.y, 0), Quaternion.identity);
		gameBoard [0, 1] = "wH";	Instantiate (whiteHorse, new Vector3 (0, whiteHorse.transform.position.y, 1), Quaternion.identity);
		gameBoard [0, 2] = "wB";	Instantiate (whiteBishop, new Vector3 (0, whiteBishop.transform.position.y, 2), Quaternion.identity);
		gameBoard [0, 3] = "wQ";	Instantiate (whiteQueen, new Vector3 (0, whiteQueen.transform.position.y, 3), Quaternion.identity);
		gameBoard [0, 4] = "wK";	Instantiate (whiteKing, new Vector3 (0, whiteKing.transform.position.y, 4), whiteKing.transform.rotation);
		gameBoard [0, 5] = "wB";	Instantiate (whiteBishop, new Vector3 (0, whiteBishop.transform.position.y, 5), Quaternion.identity);
		gameBoard [0, 6] = "wH";	Instantiate (whiteHorse, new Vector3 (0, whiteHorse.transform.position.y, 6), Quaternion.identity);
		gameBoard [0, 7] = "wR";	Instantiate (whiteRook, new Vector3 (0, whiteRook.transform.position.y, 7), Quaternion.identity);

		gameBoard [7, 0] = "bR";	Instantiate (blackRook, new Vector3 (7, blackRook.transform.position.y, 0), Quaternion.identity);
		gameBoard [7, 1] = "bH";	Instantiate (blackHorse, new Vector3 (7, blackHorse.transform.position.y, 1), Quaternion.identity);
		gameBoard [7, 2] = "bB";	Instantiate (blackBishop, new Vector3 (7, blackBishop.transform.position.y, 2), Quaternion.identity);
		gameBoard [7, 3] = "bQ";	Instantiate (blackQueen, new Vector3 (7, blackQueen.transform.position.y, 3), Quaternion.identity);
		gameBoard [7, 4] = "bK";	Instantiate (blackKing, new Vector3 (7, blackKing.transform.position.y, 4), blackKing.transform.rotation);
		gameBoard [7, 5] = "bB";	Instantiate (blackBishop, new Vector3 (7, blackBishop.transform.position.y, 5), Quaternion.identity);
		gameBoard [7, 6] = "bH";	Instantiate (blackHorse, new Vector3 (7, blackHorse.transform.position.y, 6), Quaternion.identity);
		gameBoard [7, 7] = "bR";	Instantiate (blackRook, new Vector3 (7, blackRook.transform.position.y, 7), Quaternion.identity);*/

		whitePieces.Add((GameObject)Instantiate (whiteRook, new Vector3 (0, whiteRook.transform.position.y, 0), Quaternion.identity));
        whitePieces.Add((GameObject)Instantiate (whiteHorse, new Vector3 (0, whiteHorse.transform.position.y, 1), Quaternion.identity));
        whitePieces.Add((GameObject)Instantiate (whiteBishop, new Vector3 (0, whiteBishop.transform.position.y, 2), Quaternion.identity));
        whitePieces.Add((GameObject)Instantiate (whiteQueen, new Vector3 (0, whiteQueen.transform.position.y, 3), Quaternion.identity));
        whitePieces.Add((GameObject)Instantiate (whiteKing, new Vector3 (0, whiteKing.transform.position.y, 4), whiteKing.transform.rotation));
        whitePieces.Add((GameObject)Instantiate (whiteBishop, new Vector3 (0, whiteBishop.transform.position.y, 5), Quaternion.identity));
        whitePieces.Add((GameObject)Instantiate (whiteHorse, new Vector3 (0, whiteHorse.transform.position.y, 6), Quaternion.identity));
        whitePieces.Add((GameObject)Instantiate (whiteRook, new Vector3 (0, whiteRook.transform.position.y, 7), Quaternion.identity));
		
		blackPieces.Add((GameObject)Instantiate (blackRook, new Vector3 (7, blackRook.transform.position.y, 0), Quaternion.identity));
		blackPieces.Add((GameObject)Instantiate (blackHorse, new Vector3 (7, blackHorse.transform.position.y, 1), Quaternion.identity));
		blackPieces.Add((GameObject)Instantiate (blackBishop, new Vector3 (7, blackBishop.transform.position.y, 2), Quaternion.identity));
		blackPieces.Add((GameObject)Instantiate (blackQueen, new Vector3 (7, blackQueen.transform.position.y, 3), Quaternion.identity));
		blackPieces.Add((GameObject)Instantiate (blackKing, new Vector3 (7, blackKing.transform.position.y, 4), blackKing.transform.rotation));
		blackPieces.Add((GameObject)Instantiate (blackBishop, new Vector3 (7, blackBishop.transform.position.y, 5), Quaternion.identity));
		blackPieces.Add((GameObject)Instantiate (blackHorse, new Vector3 (7, blackHorse.transform.position.y, 6), Quaternion.identity));
		blackPieces.Add((GameObject)Instantiate (blackRook, new Vector3 (7, blackRook.transform.position.y, 7), Quaternion.identity));

	}

	//Return the piece selected atm
	public GameObject getSelectedPiece(){
		return selectedPiece;
	}

	//Select the piece
	public void SelectPiece(GameObject piece){

		if (getSelectedPiece()) {
			Debug.Log("Animation_Selection"); //Play animation(selection)

		}
		possibleMovements.Clear();
		selectedPiece = piece;
		CheckPossibleMovements (piece);
		Debug.Log (selectedPiece.name + " is selected");
	}
	
	//Move the piece selected
	public void MovePiece(Vector3 coordToMove){
		switch(getSelectedPiece().tag){
			case "White":
				WhitePiecesController whitePieceController = selectedPiece.GetComponent<WhitePiecesController> ();
					foreach(Vector3 v3 in possibleMovements){
						if(v3.x == coordToMove.x && v3.z == coordToMove.z){
							switch(gameBoard[(int)coordToMove.x, (int)coordToMove.z]){
								case 0:
									gameBoard[(int)selectedPiece.transform.position.x, (int)selectedPiece.transform.position.z] = 0;
									gameBoard[(int)coordToMove.x, (int)coordToMove.z] = 1;
									selectedPiece.transform.position = coordToMove;
									Debug.Log ("Animation_moving"); //Play animation(moving)
									selectedPiece = null;
									break;

								case 2:
									gameBoard[(int)selectedPiece.transform.position.x, (int)selectedPiece.transform.position.z] = 0;
									gameBoard[(int)coordToMove.x, (int)coordToMove.z] = 1;
									Debug.Log ("Animation_moving"); //Play animation(moving)
									Debug.Log ("Animation_eating"); //Play animation(eating)
									int index = 0; 
									selectedPiece.transform.position = coordToMove;
									foreach(GameObject gO in blackPieces){
										if(gO.transform.position.x == coordToMove.x && gO.transform.position.z == coordToMove.z){
											if(nBlackCaptured < 8)
												gO.transform.position = new Vector3(nBlackCaptured, gO.transform.position.y, -1.5f);
											else
												gO.transform.position = new Vector3(nBlackCaptured-8, gO.transform.position.y, -2.75f);
											nBlackCaptured++;
											index = blackPieces.IndexOf(gO);
											break;
										}
									}
									blackPieces.RemoveAt(index);
									selectedPiece = null;
									break;
							}
							setGameState(1);
						}
					}
					break;
			case "Black":	
				BlackPiecesController blackPieceController = selectedPiece.GetComponent<BlackPiecesController> ();
				foreach(Vector3 v3 in possibleMovements){
					if(v3.x == coordToMove.x && v3.z == coordToMove.z){
						switch(gameBoard[(int)coordToMove.x, (int)coordToMove.z]){
							case 0:
								gameBoard[(int)selectedPiece.transform.position.x, (int)selectedPiece.transform.position.z] = 0;
								gameBoard[(int)coordToMove.x, (int)coordToMove.z] = 2;
								selectedPiece.transform.position = coordToMove;
								Debug.Log ("Animation_moving"); //Play animation(moving)
								selectedPiece = null;
								break;
								
							case 1:
								gameBoard[(int)selectedPiece.transform.position.x, (int)selectedPiece.transform.position.z] = 0;
								gameBoard[(int)coordToMove.x, (int)coordToMove.z] = 2;
								Debug.Log ("Animation_moving"); //Play animation(moving)
								Debug.Log ("Animation_eating"); //Play animation(eating)
								int index = 0; 
								selectedPiece.transform.position = coordToMove;
								foreach(GameObject gO in whitePieces){
									if(gO.transform.position.x == coordToMove.x && gO.transform.position.z == coordToMove.z){
										if(nWhiteCaptured < 8)
											gO.transform.position = new Vector3(7 - nWhiteCaptured, gO.transform.position.y, 8.5f);
										else
											gO.transform.position = new Vector3(15 - nWhiteCaptured, gO.transform.position.y, 9.75f);
										nWhiteCaptured++;
										index = whitePieces.IndexOf(gO);									
									}
								}
								whitePieces.RemoveAt(index);
								selectedPiece = null;
								break;
						}
						setGameState(0);
					}
				}
				break;
		}
	}
}
