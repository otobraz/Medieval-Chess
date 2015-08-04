using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameController : MonoBehaviour {
	
	public GameObject blackPawn, blackRook, blackHorse, blackBishop, blackQueen, blackKing;
	
	public GameObject whitePawn, whiteRook, whiteHorse, whiteBishop, whiteQueen, whiteKing, particle;
	
	private int gameState, nBlackCaptured, nWhiteCaptured, nThreats;
	
	private int[,] gameBoard;
	
	public GameObject selectedPiece, threat;
	
	private int index;
	
	private GameObject wKing, wRookL, wRookR, bRookL, bRookR, bKing, selectionParticle;
	
	private List<GameObject> whitePieces = new List<GameObject>();
	
	private List<GameObject> blackPieces = new List<GameObject>();
	
	private List<Vector3> possibleMovements = new List<Vector3> ();
	
	List<Vector3> threatMovements = new List<Vector3>();
	
	private bool isCheck, isPromotion, isCheckMate, restart;
	
	private Texture2D wRookImage, wBishopImage, wHorseImage, wQueenImage;
	
	private Texture2D bRookImage, bBishopImage, bHorseImage, bQueenImage;
	
	private float screenWidth, screenHeight, buttonWidth, buttonHeight;
	
	private Vector3 coord, rookCoord;

	private GUIStyle fontStyle;
	
	//  Use this for initialization
	void Start () {	
		InitializeGameBoard ();
		fontStyle = new GUIStyle ();
		SetGameState (0);
		nBlackCaptured = 0;
		nWhiteCaptured = 0;
		nThreats = 0;
		isCheck = false;
		isPromotion = false;
		isCheckMate = false;
		restart = false;
		wRookImage = Resources.Load ("whiteRook") as Texture2D;
		wBishopImage = Resources.Load ("whiteBishop") as Texture2D;
		wHorseImage = Resources.Load ("whiteHorse") as Texture2D;
		wQueenImage = Resources.Load ("whiteQueen") as Texture2D;
		bRookImage = Resources.Load ("blackRook") as Texture2D;
		bBishopImage = Resources.Load ("blackBishop") as Texture2D;
		bHorseImage = Resources.Load ("blackHorse") as Texture2D;
		bQueenImage = Resources.Load ("blackQueen") as Texture2D;
		screenWidth = Screen.width;
		screenHeight = Screen.height;
		buttonWidth = screenWidth / 8;
		buttonHeight = screenHeight / 5;
		coord = new Vector3 (8, 8, 8);
		rookCoord = new Vector3(8, 8, 8);
		//selectionParticle = (GameObject)Instantiate (particle, particle.transform.position, Quaternion.Euler(-90, 0, 0));

	}
	
	// Update is called once per frame
	void Update () {
		if (selectedPiece != null){
			if(Math.Abs(coord.x - selectedPiece.transform.position.x) <= 0.5 && Math.Abs(coord.z - selectedPiece.transform.position.z) <= 0.5){
				if(selectedPiece.tag == "White"){
					foreach(GameObject gO in blackPieces){
						if(gO.transform.position.x == coord.x && gO.transform.position.z == coord.z){
							gO.tag = "CapturedPiece";
							if(nBlackCaptured < 8)
								gO.transform.position = new Vector3(nBlackCaptured, gO.transform.position.y, -1.5f);
							else
								gO.transform.position = new Vector3(nBlackCaptured - 8, gO.transform.position.y, -2.75f);
							nBlackCaptured++;	
							index = blackPieces.IndexOf(gO);
							break;
						}else if(coord.x == 5 && (gO.GetComponent<BlackPiecesController>().isEnPassantL || gO.GetComponent<BlackPiecesController>().isEnPassantR)){ 
							gO.tag = "CapturedPiece";
							if(nBlackCaptured < 8)
								gO.transform.position = new Vector3(nBlackCaptured, gO.transform.position.y, -1.5f);
							else
								gO.transform.position = new Vector3(nBlackCaptured - 8, gO.transform.position.y, -2.75f);
							nBlackCaptured++;
							index = blackPieces.IndexOf(gO);
							break;
						}
					}
					if(index >=0){
						blackPieces.RemoveAt(index);
						index = -1;
					}
				}else{
					foreach(GameObject gO in whitePieces){
						if(gO.transform.position.x == coord.x && gO.transform.position.z == coord.z){
							gO.tag = "CapturedPiece";
							if(nWhiteCaptured < 8)
								gO.transform.position = new Vector3(7 - nWhiteCaptured, gO.transform.position.y, 8.5f);
							else
								gO.transform.position = new Vector3(15 - nWhiteCaptured, gO.transform.position.y, 9.75f);
							nWhiteCaptured++;
							index = whitePieces.IndexOf(gO);	
							break;
						}else if(coord.x == 2 && (gO.GetComponent<WhitePiecesController>().isEnPassantL || gO.GetComponent<WhitePiecesController>().isEnPassantR)){ 
							gO.tag = "CapturedPiece";
							if(nWhiteCaptured < 8)
								gO.transform.position = new Vector3(7 - nWhiteCaptured, gO.transform.position.y, 8.5f);
							else
								gO.transform.position = new Vector3(15 - nWhiteCaptured, gO.transform.position.y, 9.75f);
							nWhiteCaptured++;
							index = whitePieces.IndexOf(gO);	
							break;
						}

					}
					if(index >= 0){
						whitePieces.RemoveAt(index);
						index = -1;
					}
				}
			}
			if(coord.x == selectedPiece.transform.position.x && coord.z == selectedPiece.transform.position.z) {
				if(selectedPiece.tag == "White"){
					rookCoord = new Vector3(8,8,8);				
					coord = new Vector3 (8, 8, 8);
					if(selectedPiece.name == "WhitePawn(Clone)" && selectedPiece.transform.position.x == 7){
						isPromotion = true;
						SetGameState(2);
					}
					if(gameState == 0){
						IsCheck("White", (int)wKing.transform.position.x, (int)wKing.transform.position.z);
						selectedPiece.animation.Stop ();
						selectedPiece.transform.rotation = Quaternion.Euler(0, 90, 0);
						selectedPiece = null;
						if(!isCheck){
							SetGameState(1);
						}
						IsCheck ("Black", (int)bKing.transform.position.x, (int)bKing.transform.position.z);
						if(isCheck && IsCheckMate(bKing)){		
							restart = true;
							fontStyle.normal.textColor = Color.white;
							SetGameState(2);
						}
					}
					
				}else if(selectedPiece.tag == "Black"){
					coord = new Vector3 (8, 8, 8);
					if(selectedPiece.name == "BlackPawn(Clone)" && selectedPiece.transform.position.x == 0){
						isPromotion = true;
						SetGameState(2);
					}
					if(gameState == 1){
						IsCheck("Black", (int)bKing.transform.position.x, (int)bKing.transform.position.z);
						selectedPiece.animation.Stop ();
						selectedPiece.transform.rotation = Quaternion.Euler(0, -90, 0);
						selectedPiece = null;
						if(!isCheck){
							SetGameState(0);
						}
						IsCheck ("White", (int)wKing.transform.position.x, (int)wKing.transform.position.z);
						if(isCheck && IsCheckMate(wKing)){		
							restart = true;
							fontStyle.normal.textColor = Color.black;
							SetGameState(2);
						}
					}
				}
			}
		}
		if (restart) {
			if(Input.GetKeyDown(KeyCode.R)){
				Application.LoadLevel(Application.loadedLevel);
			}
		}
	}
	
	void OnGUI(){
		if (isPromotion) {
			switch(selectedPiece.tag){
			case "White":
				if (GUI.Button(new Rect(screenWidth/4 - buttonWidth/3, buttonHeight + buttonHeight/3, buttonWidth, buttonHeight), wRookImage)){
					Promotion(GetSelectedPiece(), whiteRook);
				}
				if (GUI.Button(new Rect(screenWidth/2 - buttonWidth*1.1f, buttonHeight + buttonHeight/3, buttonWidth, buttonHeight), wHorseImage)){
					Promotion(GetSelectedPiece(), whiteHorse);
				}
				if (GUI.Button(new Rect(screenWidth/2 + buttonWidth/10, buttonHeight + buttonHeight/3, buttonWidth, buttonHeight), wBishopImage)){
					Promotion(GetSelectedPiece(), whiteBishop);
				}
				if (GUI.Button(new Rect(screenWidth/2 + buttonWidth*1.3f, buttonHeight + buttonHeight/3, buttonWidth, buttonHeight), wQueenImage)){
					Promotion(GetSelectedPiece(), whiteQueen);
				}
				break;
			case "Black":
				if (GUI.Button(new Rect(screenWidth/4 - buttonWidth/3, buttonHeight + buttonHeight/3, buttonWidth, buttonHeight), bRookImage)){
					Promotion(GetSelectedPiece(), blackRook);
				}
				if (GUI.Button(new Rect(screenWidth/2 - buttonWidth*1.1f, buttonHeight + buttonHeight/3, buttonWidth, buttonHeight), bHorseImage)){
					Promotion(GetSelectedPiece(), blackHorse);
				}
				if (GUI.Button(new Rect(screenWidth/2 + buttonWidth/10, buttonHeight + buttonHeight/3, buttonWidth, buttonHeight), bBishopImage)){
					Promotion(GetSelectedPiece(), blackBishop);
				}
				if (GUI.Button(new Rect(screenWidth/2 + buttonWidth*1.3f, buttonHeight + buttonHeight/3, buttonWidth, buttonHeight), bQueenImage)){
					Promotion(GetSelectedPiece(), blackQueen);
				}
				break;
			}
		}
		if (isCheck) {
			GUI.backgroundColor = new Color(1,1,1,0.7f);
			fontStyle.fontSize = Mathf.RoundToInt (screenWidth / 53);
			GUI.Box(new Rect(screenWidth - screenWidth/1.06f, screenHeight - screenHeight/1.03f, screenWidth/7, screenHeight/5), "");
			if(GetGameState() == 1)
				fontStyle.normal.textColor = Color.white;
			else if(GetGameState() == 0)
				fontStyle.normal.textColor = Color.black;
			if(isCheckMate){
				GUI.Label(new Rect(screenWidth - screenWidth/1.07f, screenHeight - screenHeight/1.06f, screenWidth/7, screenHeight/5), "CHECKMATE!", fontStyle);
				if(fontStyle.normal.textColor == Color.white){
					GUI.Label(new Rect(screenWidth - screenWidth/1.07f, screenHeight - screenHeight/1.17f, screenWidth/7, screenHeight/5), "WHITE WINS!!", fontStyle);
				}else{
					GUI.Label(new Rect(screenWidth - screenWidth/1.07f, screenHeight - screenHeight/1.17f, screenWidth/7, screenHeight/5), "BLACK WINS!!", fontStyle);
				}
				fontStyle.fontSize = Mathf.RoundToInt (screenWidth / 30);
				GUI.Box(new Rect(screenWidth/2 - screenWidth/6f, screenHeight - screenHeight/1.35f, screenWidth/3, screenHeight/4), "");
				GUI.Label(new Rect(screenWidth/2 - screenWidth/7.5f, screenHeight - screenHeight/1.5f, screenWidth/2, screenHeight/2), "Press R to restart", fontStyle);

				}else{
				GUI.Label(new Rect(screenWidth - screenWidth/1.1f, screenHeight - screenHeight/1.12f, screenWidth/7, screenHeight/5), "CHECK!", fontStyle);
			}
		}
	}

	
	public GameObject[] GetWhiteRooks(){
		GameObject[] whiteRooks = new GameObject[]{wRookL, wRookR};
		return whiteRooks;
	}
	
	public GameObject[] GetBlackRooks(){
		GameObject[] blackRooks = new GameObject[]{bRookL, bRookR};
		return blackRooks;
	}
	
	public List<Vector3> CheckThreatMovements(GameObject threat, GameObject king){
		threatMovements.Clear ();
		int kX = (int)king.transform.position.x;
		int kZ = (int)king.transform.position.z;
		int tX = (int)threat.transform.position.x;
		int tZ = (int)threat.transform.position.z;
		threatMovements.Add (new Vector3 (tX, threat.transform.position.y, tZ));
		if (threat.name == "WhiteRook(Clone)" || threat.name == "BlackRook(Clone)") {
			if(tX == kX){
				if(kZ < tZ){
					for(int j = kZ+1; j < tZ; j++){
						threatMovements.Add(new Vector3(tX, threat.transform.position.y, j));
					}
				}else{
					for(int j = kZ-1; j > tZ; j--){
						threatMovements.Add(new Vector3(tX, threat.transform.position.y, j));
					}
				}
			}else{
				if(kX < tX){
					for(int i = kX+1; i < tZ; i++){
						threatMovements.Add(new Vector3(i, threat.transform.position.y, tZ));
					}
				}else{
					for(int i = kZ-1; i > tZ; i--){
						threatMovements.Add(new Vector3(i, threat.transform.position.y, tZ));
					}
				}
			}
		}else if(threat.name == "WhiteBishop(Clone)" || threat.name == "BlackBishop(Clone)"){
			if(kX < tX && kZ < tZ){
				for(int i = kX+1, j = kZ+1; i < tX && j < tZ; i++, j++){
					threatMovements.Add(new Vector3(i, threat.transform.position.y, j));
				}
			}else if(kX > tX && kZ > tZ){
				for(int i = kX-1, j = kZ-1; i > tX && j > tZ; i--, j--){
					threatMovements.Add(new Vector3(i, threat.transform.position.y, j));
				}
			}else if(kX < tX && kZ > tZ){
				for(int i = kX+1, j = kZ-1; i < tX && j > tZ; i++, j--){
					threatMovements.Add(new Vector3(i, threat.transform.position.y, j));
				}
			}else if(kX > tX && kZ < tZ){
				for(int i = kX-1, j = kZ+1; i > tX && j < tZ; i--, j++){
					threatMovements.Add(new Vector3(i, threat.transform.position.y, j));
				}
			}
		}else if(threat.name == "WhiteQueen(Clone)" || threat.name == "BlackQueen(Clone)"){
			if(tX == kX){
				if(kZ < tZ){
					for(int j = kZ+1; j < tZ; j++){
						threatMovements.Add(new Vector3(tX, threat.transform.position.y, j));
					}
				}else{
					for(int j = kZ-1; j > tZ; j--){
						threatMovements.Add(new Vector3(tX, threat.transform.position.y, j));
					}
				}
			}else if(tZ == kZ){
				if(kX < tX){
					for(int i = kX+1; i < tZ; i++){
						threatMovements.Add(new Vector3(i, threat.transform.position.y, tZ));
					}
				}else{
					for(int i = kZ-1; i > tZ; i--){
						threatMovements.Add(new Vector3(i, threat.transform.position.y, tZ));
					}
				}
			}else if(kX < tX && kZ < tZ){
				for(int i = kX+1, j = kZ+1; i < tX && j < tZ; i++, j++){
					threatMovements.Add(new Vector3(i, threat.transform.position.y, j));
				}
			}else if(kX > tX && kZ > tZ){
				for(int i = kX-1, j = kZ-1; i > tX && j > tZ; i--, j--){
					threatMovements.Add(new Vector3(i, threat.transform.position.y, j));
				}
			}else if(kX < tX && kZ > tZ){
				for(int i = kX+1, j = kZ-1; i < tX && j > tZ; i++, j--){
					threatMovements.Add(new Vector3(i, threat.transform.position.y, j));
				}
			}else if(kX > tX && kZ < tZ){
				for(int i = kX-1, j = kZ+1; i > tX && j < tZ; i--, j++){
					threatMovements.Add(new Vector3(i, threat.transform.position.y, j));
				}
			}
		}
		return threatMovements;
	}
	
	public void Promotion(GameObject pawn, GameObject promotedPiece){
		switch (pawn.tag) {
		case "White":
			whitePieces.Remove(pawn);
			whitePieces.Add((GameObject)Instantiate (promotedPiece, pawn.transform.position, Quaternion.identity));
			Destroy (pawn);
			selectedPiece = promotedPiece;
			coord = promotedPiece.transform.position;
			SetGameState(0);
			break;
		case "Black":
			blackPieces.Remove(pawn);
			blackPieces.Add((GameObject)Instantiate (promotedPiece, pawn.transform.position, Quaternion.identity));
			Destroy (pawn);
			selectedPiece = promotedPiece;
			coord = promotedPiece.transform.position;
			SetGameState(1);
			break;
		}
		isPromotion = false;
	}
	
	public void EnPassant(){
		
	}
	
	public void Castling(){
		
	}
	
	public bool IsCheck(string tag, int kX, int kZ){
		possibleMovements.Clear ();
		isCheck = false;
		nThreats = 0;
		switch(tag){
		case "White":
			BlackPiecesController bPC;
			foreach(GameObject piece in blackPieces){
				bPC = piece.GetComponent<BlackPiecesController>();
				foreach(Vector3 v3 in bPC.GetMovementsList(gameBoard)){
					if(v3.x == kX && v3.z == kZ){
						threat = piece;
						nThreats++;
						Debug.Log("Check!");
						isCheck = true;
						return true;
						
					}
				}
			}
			break;

		case "Black":
			WhitePiecesController wPC;
			foreach(GameObject piece in whitePieces){		
				wPC =  piece.GetComponent<WhitePiecesController>();
				foreach(Vector3 v3 in wPC.GetMovementsList(gameBoard)){
						if(v3.x == kX && v3.z == kZ){
							threat = piece;
							nThreats++;
							Debug.Log("Check!");
							isCheck = true;
							return true;
						}
				}
			}
			break;
		}
		return false;
	}
	
	public bool IsCheckMate(GameObject king){
		bool verifier = false, checkMate = true;
		//isCheckMate = true;
		possibleMovements.Clear ();
		WhitePiecesController wPC;
		BlackPiecesController bPC;
		//Check if the King can escape moving;
			if(nThreats >= 1){
				switch(king.tag){
				case "White":
					wPC = king.GetComponent<WhitePiecesController>();
					foreach(GameObject piece in blackPieces){
						bPC = piece.GetComponent<BlackPiecesController>();
						possibleMovements.AddRange(bPC.GetMovementsList(gameBoard));
					}
					foreach(Vector3 kM in wPC.GetMovementsList(gameBoard)){
						foreach(Vector3 pM in possibleMovements){
							if(kM.x == pM.x && kM.z == pM.z){
								verifier = true;
								break;
							}
						}
						if(!verifier){
							checkMate = false;
							break;
						}
						verifier = false;
					}	
					break;

				case "Black":
				bPC = king.GetComponent<BlackPiecesController>();
				foreach(GameObject piece in whitePieces){
					wPC = piece.GetComponent<WhitePiecesController>();
					possibleMovements.AddRange(wPC.GetMovementsList(gameBoard));
				}
				foreach(Vector3 kM in bPC.GetMovementsList(gameBoard)){
					foreach(Vector3 pM in possibleMovements){
						if(kM.x == pM.x && kM.z == pM.z){
							verifier = true;
							break;
						}
					}
					if(!verifier){
						checkMate = false;
						break;
					}
				}
				break;
			}
		}
		
		//Check if some piece can block the enemy one
		if (nThreats == 1) { 
			possibleMovements.Clear();
			switch(king.tag){
			case "White":
				foreach(GameObject piece in whitePieces){
					if(piece.name != "WhiteKing(Clone)"){
						wPC = piece.GetComponent<WhitePiecesController>();
						possibleMovements.AddRange(wPC.GetMovementsList(gameBoard));
					}
				}	
				break;
			case "Black":
				foreach(GameObject piece in blackPieces){
					if(piece.name != "BlackKing(Clone)"){
						bPC = piece.GetComponent<BlackPiecesController>();
						possibleMovements.AddRange(bPC.GetMovementsList(gameBoard));
					}
				}	
				break;
			}
			foreach(Vector3 tM in CheckThreatMovements(threat, king)){
				foreach(Vector3 pM in possibleMovements){
					if((tM.x == pM.x && tM.z == pM.z)){
						checkMate = false;
					}
				}
			}
		}
		Debug.Log (checkMate);
		isCheckMate = checkMate;
		return checkMate;
	}
	
	public int GetGameState(){
		return gameState;
	}
	
	public void SetGameState(int nGS){
		gameState = nGS;
	}
	public void InitializeGameBoard(){
		gameBoard = new int[8, 8];
		for (int i = 0; i < 8; i++) {
			gameBoard[1,i] = 1;	whitePieces.Add((GameObject)Instantiate (whitePawn, new Vector3 (1, whitePawn.transform.position.y, i), Quaternion.identity));
			gameBoard[6,i] = 2;	blackPieces.Add((GameObject)Instantiate (blackPawn, new Vector3 (6, whitePawn.transform.position.y, i), Quaternion.identity));
			gameBoard[0,i] = 1;
			gameBoard[7,i] = 2;
		}
		
		wRookL = (GameObject)Instantiate (whiteRook, new Vector3 (0, whiteRook.transform.position.y, 0), Quaternion.Euler(0,-90,0));
		whitePieces.Add(wRookL);
		whitePieces.Add((GameObject)Instantiate (whiteHorse, new Vector3 (0, whiteHorse.transform.position.y, 1), Quaternion.identity));
		whitePieces.Add((GameObject)Instantiate (whiteBishop, new Vector3 (0, whiteBishop.transform.position.y, 2), whiteBishop.transform.rotation));
		whitePieces.Add((GameObject)Instantiate (whiteQueen, new Vector3 (0, whiteQueen.transform.position.y, 4), Quaternion.identity));
		wKing = (GameObject)Instantiate (whiteKing, new Vector3 (0, whiteKing.transform.position.y, 3), whiteKing.transform.rotation);
		whitePieces.Add (wKing);
		whitePieces.Add((GameObject)Instantiate (whiteBishop, new Vector3 (0, whiteBishop.transform.position.y, 5), whiteBishop.transform.rotation));
		whitePieces.Add((GameObject)Instantiate (whiteHorse, new Vector3 (0, whiteHorse.transform.position.y, 6), Quaternion.identity));
		wRookR = (GameObject)Instantiate (whiteRook, new Vector3 (0, whiteRook.transform.position.y, 7), Quaternion.Euler(0, -90, 0));
		whitePieces.Add (wRookR);
		
		bRookL = (GameObject)Instantiate (blackRook, new Vector3 (7, blackRook.transform.position.y, 0), Quaternion.identity);
		blackPieces.Add(bRookL);
		blackPieces.Add((GameObject)Instantiate (blackHorse, new Vector3 (7, blackHorse.transform.position.y, 1), Quaternion.identity));
		blackPieces.Add((GameObject)Instantiate (blackBishop, new Vector3 (7, blackBishop.transform.position.y, 2), blackBishop.transform.rotation));
		blackPieces.Add((GameObject)Instantiate (blackQueen, new Vector3 (7, blackQueen.transform.position.y, 4), Quaternion.identity));
		bKing = (GameObject)Instantiate (blackKing, new Vector3 (7, blackKing.transform.position.y, 3), blackKing.transform.rotation);
		blackPieces.Add (bKing);
		blackPieces.Add((GameObject)Instantiate (blackBishop, new Vector3 (7, blackBishop.transform.position.y, 5), blackBishop.transform.rotation));
		blackPieces.Add((GameObject)Instantiate (blackHorse, new Vector3 (7, blackHorse.transform.position.y, 6), Quaternion.identity));
		bRookR = (GameObject)Instantiate (blackRook, new Vector3 (7, blackRook.transform.position.y, 7), Quaternion.identity);
		blackPieces.Add(bRookL);
	}
	
	//Return the piece selected atm
	public GameObject GetSelectedPiece(){
		return selectedPiece;
	}
	
	//Select the piece
	public void SelectPiece(GameObject piece){
		
		Debug.Log("Animation_Selection"); //Play animation(selection)
		possibleMovements.Clear();
		if(selectedPiece != null){
			selectedPiece.animation.Stop ();
			if(gameState == 0)
				selectedPiece.transform.rotation = Quaternion.Euler(0, 90, 0);

			else
				selectedPiece.transform.rotation = Quaternion.Euler(0, -90, 0);
			selectedPiece.animation.Stop ();
		}
		selectedPiece = piece;
		selectedPiece.animation.Play ();
		/*selectionParticle.SetActive (true);
		particle.transform.position = selectedPiece.transform.position;*/
		Debug.Log (selectedPiece.name + " is selected");
	}
	
	//Move the piece selected
	public void MovePiece(Vector3 coordToMove){
		bool verifier = true;
		index = -1;	
		WhitePiecesController wPC;
		BlackPiecesController bPC;
		switch(selectedPiece.tag){
		case "White":
			wPC = selectedPiece.GetComponent<WhitePiecesController>();
			if(IsCheck("White", (int)wKing.transform.position.x, (int)wKing.transform.position.z) && !IsCheckMate(wKing)){
				if(selectedPiece.name == "WhiteKing(Clone)"){
					foreach(GameObject bPiece in blackPieces){
						bPC = bPiece.GetComponent<BlackPiecesController>();
						foreach(Vector3 v3 in bPC.GetMovementsList(gameBoard)){
							if(bPiece.name == "BlackPawn(Clone)"){
								if(v3.z != bPiece.transform.position.z){
									if(coordToMove.x == v3.x && coordToMove.z == v3.z){
										verifier = false;
										break;
									}
								}
							}else if(coordToMove.x == v3.x && coordToMove.z == v3.z){
								verifier = false;
								break;
							}
						}
					}
				}else{
					verifier = false;
					foreach(Vector3 v3 in CheckThreatMovements(threat, wKing)){
						if(coordToMove.x == v3.x && coordToMove.z == v3.z){
							verifier = true;
							break;
						}
					}
				}
			}
			if(verifier){
				foreach(Vector3 v3 in wPC.GetMovementsList(gameBoard)){
					if(v3.x == coordToMove.x && v3.z == coordToMove.z){
						coord = coordToMove;
						switch(gameBoard[(int)coordToMove.x, (int)coordToMove.z]){
						case 0:
							gameBoard[(int)selectedPiece.transform.position.x, (int)selectedPiece.transform.position.z] = 0;
							gameBoard[(int)coordToMove.x, (int)coordToMove.z] = 1;
							if(selectedPiece.name == "WhiteKing(Clone)"){
								if(IsCheck("White", (int)coordToMove.x, (int)coordToMove.z)){
									isCheck = false;
									gameBoard[(int)selectedPiece.transform.position.x, (int)selectedPiece.transform.position.z] = 1;
									gameBoard[(int)coordToMove.x, (int)coordToMove.z] = 0;
									break;
								}
							}else{
								if(IsCheck("White", (int)wKing.transform.position.x, (int)wKing.transform.position.z)){
									isCheck = false;
									gameBoard[(int)selectedPiece.transform.position.x, (int)selectedPiece.transform.position.z] = 1;
									gameBoard[(int)coordToMove.x, (int)coordToMove.z] = 0;
									break;
								}
							}
							if(selectedPiece.name == "WhitePawn(Clone)" && coordToMove.x - selectedPiece.transform.position.x == 2){
								if((coordToMove.z-1 >= 0 && gameBoard[(int)coordToMove.x, (int)coordToMove.z-1] == 2) || (coordToMove.z+1 <=7 && gameBoard[(int)coordToMove.x, (int)coordToMove.z+1] == 2)){	
									foreach(GameObject bPiece in blackPieces){
										if(bPiece.transform.position.x == coordToMove.x && bPiece.transform.position.z == coordToMove.z-1){
											bPiece.GetComponent<BlackPiecesController>().isEnPassantR = true;
											wPC.isEnPassantR = true;
										}
										if(bPiece.transform.position.x == coordToMove.x && bPiece.transform.position.z == coordToMove.z+1){
											bPiece.GetComponent<BlackPiecesController>().isEnPassantL = true;
											wPC.isEnPassantL = true;
										}
										
									}
								}
							}
							wPC.coordToMove = coordToMove;	
							if(selectedPiece.name == "WhiteKing(Clone)"){
								if(selectedPiece.transform.position.z - coordToMove.z == 2){
									rookCoord = new Vector3(wRookL.transform.position.x, wRookL.transform.position.y, coordToMove.z+1);
									wRookL.GetComponent<WhitePiecesController>().coordToMove = rookCoord;
									gameBoard[(int)wRookL.transform.position.x, (int)wRookL.transform.position.z] = 0;
									gameBoard[(int)rookCoord.x, (int)rookCoord.z] = 1;
								}
								else if(selectedPiece.transform.position.z - coordToMove.z == -2){
									rookCoord = new Vector3(wRookL.transform.position.x, wRookL.transform.position.y, coordToMove.z-1);
									wRookR.GetComponent<WhitePiecesController>().coordToMove = rookCoord; 
									gameBoard[(int)wRookR.transform.position.x, (int)wRookR.transform.position.z] = 0;
									gameBoard[(int)rookCoord.x, (int)rookCoord.z] = 1;
								}
							}
							break;
							
						case 2:
							gameBoard[(int)selectedPiece.transform.position.x, (int)selectedPiece.transform.position.z] = 0;
							gameBoard[(int)coordToMove.x, (int)coordToMove.z] = 1;
							GameObject piece = null;
							foreach(GameObject gO in blackPieces){
								if(gO.transform.position.x == coord.x && gO.transform.position.z == coord.z){
									piece = gO;
									blackPieces.Remove(gO);
									break;
								}
							}
							if(selectedPiece.name == "WhiteKing(Clone)"){
								if(IsCheck("White", (int)coordToMove.x, (int)coordToMove.z)){
									isCheck = false;
									gameBoard[(int)selectedPiece.transform.position.x, (int)selectedPiece.transform.position.z] = 1;
									gameBoard[(int)coordToMove.x, (int)coordToMove.z] = 0;
									break;
								}
							}else{
								if(IsCheck("White", (int)wKing.transform.position.x, (int)wKing.transform.position.z)){
									isCheck = false;
									gameBoard[(int)selectedPiece.transform.position.x, (int)selectedPiece.transform.position.z] = 1;
									gameBoard[(int)coordToMove.x, (int)coordToMove.z] = 0;
									break;
								}
							}
							blackPieces.Add(piece);
							Debug.Log ("Animation_eating"); //Play animation(eating)
							wPC.coordToMove = coordToMove;
							break;
						}
						break;
					}
				}
			}
			break;
		case "Black":	
			bPC = selectedPiece.GetComponent<BlackPiecesController>();
			if(IsCheck("Black", (int)bKing.transform.position.x, (int)bKing.transform.position.z) && !IsCheckMate(bKing)){
				if(selectedPiece.name == "BlackKing(Clone)"){
					foreach(GameObject wPiece in whitePieces){
						wPC = wPiece.GetComponent<WhitePiecesController>();
						foreach(Vector3 v3 in wPC.GetMovementsList(gameBoard)){
							if(wPiece.name == "WhitePawn(Clone)"){
								if(v3.z != wPiece.transform.position.z){
									if(coordToMove.x == v3.x && coordToMove.z == v3.z){
										verifier = false;
										break;
									}
								}
							}else if(coordToMove.x == v3.x && coordToMove.z == v3.z){
								verifier = false;
								break;
							}
						}
					}
				}else{
					verifier = false;
					foreach(Vector3 v3 in CheckThreatMovements(threat, bKing)){
						if(coordToMove.x == v3.x && coordToMove.z == v3.z){
							verifier = true;
							break;
						}
					}
				}
			}
			if(verifier){
				foreach(Vector3 v3 in bPC.GetMovementsList(gameBoard)){	
					if(v3.x == coordToMove.x && v3.z == coordToMove.z){	
						coord = coordToMove;
						switch(gameBoard[(int)coordToMove.x, (int)coordToMove.z]){
						case 0:
							gameBoard[(int)selectedPiece.transform.position.x, (int)selectedPiece.transform.position.z] = 0;
							gameBoard[(int)coordToMove.x, (int)coordToMove.z] = 2;
							if(selectedPiece.name == "BlackKing(Clone)"){
								if(IsCheck("Black", (int)coordToMove.x, (int)coordToMove.z)){
									gameBoard[(int)selectedPiece.transform.position.x, (int)selectedPiece.transform.position.z] = 2;
									gameBoard[(int)coordToMove.x, (int)coordToMove.z] = 0;
									break;
								}
							}else{
								if(IsCheck("Black", (int)bKing.transform.position.x, (int)bKing.transform.position.z)){
									gameBoard[(int)selectedPiece.transform.position.x, (int)selectedPiece.transform.position.z] = 2;
									gameBoard[(int)coordToMove.x, (int)coordToMove.z] = 0;
									break;
								}
							}
							if(selectedPiece.name == "BlackPawn(Clone)" && coordToMove.x - selectedPiece.transform.position.x == -2){
								if((coordToMove.z-1 >= 0 && gameBoard[(int)coordToMove.x, (int)coordToMove.z-1] == 1) || (coordToMove.z+1 <=7 && gameBoard[(int)coordToMove.x, (int)coordToMove.z+1] == 1)){	
									foreach(GameObject wPiece in whitePieces){
										if(wPiece.transform.position.x == coordToMove.x && wPiece.transform.position.z == coordToMove.z-1){
											wPiece.GetComponent<WhitePiecesController>().isEnPassantR = true;
											bPC.isEnPassantR = true;	
										}
										if(wPiece.transform.position.x == coordToMove.x && wPiece.transform.position.z == coordToMove.z+1){
											wPiece.GetComponent<WhitePiecesController>().isEnPassantL = true;
											bPC.isEnPassantL = true;
										}
										
									}
								}
							}
							bPC.coordToMove = coordToMove;
							if(selectedPiece.name == "BlackKing(Clone)"){
								if(selectedPiece.transform.position.z - coordToMove.z == 2){
									bRookL.GetComponent<BlackPiecesController>().coordToMove = new Vector3(bRookL.transform.position.x, bRookL.transform.position.y, coordToMove.z+1);
									gameBoard[(int)bRookL.transform.position.x, (int)bRookL.transform.position.z] = 0;
									gameBoard[(int)rookCoord.x, (int)rookCoord.z] = 2;
								}
								else if(selectedPiece.transform.position.z - coordToMove.z == -2){
									bRookR.GetComponent<BlackPiecesController>().coordToMove = new Vector3(bRookL.transform.position.x, bRookL.transform.position.y, coordToMove.z-1);
									gameBoard[(int)bRookR.transform.position.x, (int)bRookR.transform.position.z] = 0;
									gameBoard[(int)rookCoord.x, (int)rookCoord.z] = 2;
								}	
							}
							break;
							
						case 1:			
							gameBoard[(int)selectedPiece.transform.position.x, (int)selectedPiece.transform.position.z] = 0;
							gameBoard[(int)coordToMove.x, (int)coordToMove.z] = 2;
							GameObject piece = null;
							foreach(GameObject gO in whitePieces){
								if(gO.transform.position.x == coord.x && gO.transform.position.z == coord.z){
									piece = gO;
									whitePieces.Remove(gO);
									break;
								}
							}
							if(selectedPiece.name == "BlackKing(Clone)"){
								if(IsCheck("Black", (int)coordToMove.x, (int)coordToMove.z)){
									gameBoard[(int)selectedPiece.transform.position.x, (int)selectedPiece.transform.position.z] = 2;
									gameBoard[(int)coordToMove.x, (int)coordToMove.z] = 0;
									break;
								}
							}else{
								if(IsCheck("Black", (int)bKing.transform.position.x, (int)bKing.transform.position.z)){
									gameBoard[(int)selectedPiece.transform.position.x, (int)selectedPiece.transform.position.z] = 2;
									gameBoard[(int)coordToMove.x, (int)coordToMove.z] = 0;
									break;
								}
							}
							whitePieces.Add(piece);
							Debug.Log ("Animation_eating"); //Play animation(eating) 
							bPC.coordToMove = coordToMove;
							break;
						}
						break;
					}
				}
			}
			break;
		}
	}
}
