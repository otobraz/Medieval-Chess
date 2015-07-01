using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameController : MonoBehaviour {
	
	private int gameState, nBlackCaptured, nWhiteCaptured, nThreats;
	
	private int[,] gameBoard;
	
	private GameObject selectedPiece, threat;
	
	public GameObject blackPawn, blackRook, blackHorse, blackBishop, blackQueen, blackKing;
	
	public GameObject whitePawn, whiteRook, whiteHorse, whiteBishop, whiteQueen, whiteKing;
	
	private GameObject wKing, bKing;
	
	private List<GameObject> whitePieces = new List<GameObject>();
	private List<GameObject> blackPieces = new List<GameObject>();
	
	private List<Vector3> possibleMovements = new List<Vector3> ();

	List<Vector3> threatMovements = new List<Vector3>();

	private bool isCheck, isPromotion;

	private Texture2D wRookImage, wBishopImage, wHorseImage, wQueenImage;

	private Texture2D bRookImage, bBishopImage, bHorseImage, bQueenImage;

	private float screenWidth, screenHeight, buttonWidth, buttonHeight;
		
	//  Use this for initialization
	void Start () {	
		InitializeGameBoard ();
		SetGameState (0);
		nBlackCaptured = 0;
		nWhiteCaptured = 0;
		nThreats = 0;
		isCheck = false;
		isPromotion = false;
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
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI(){
		if (isPromotion) {
			switch(GetSelectedPiece().tag){
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
	}
	
	public List<Vector3> CheckPawnMovements(GameObject sPiece, int x, int z){
		List<Vector3> pieceMovements = new List<Vector3>();
		switch (sPiece.tag) {
		case "White":
			if(x+1 < 8  && gameBoard[x+1, z] == 0){
				pieceMovements.Add(new Vector3(x+1, sPiece.transform.position.y, z));
				if(x == 1){
					if(gameBoard[x+2, z] == 0){
						pieceMovements.Add(new Vector3(x+2, sPiece.transform.position.y, z));
					}
				}
			}	
			if(z+1 < 8 && x+1 < 8 && gameBoard[x+1, z+1] == 2){
				pieceMovements.Add(new Vector3(x+1, sPiece.transform.position.y, z+1));
			}
			if(z-1 >= 0 && x+1 < 8 && gameBoard[x+1, z-1] == 2){
				pieceMovements.Add(new Vector3(x+1, sPiece.transform.position.y, z-1));
			}
			break;
			
		case "Black":
			if(x-1 >=0 && gameBoard[x-1, z] == 0){
				pieceMovements.Add(new Vector3(x-1, sPiece.transform.position.y, z));
				if(x == 6){
					if(gameBoard[x-2, z] == 0){
						pieceMovements.Add(new Vector3(x-2, sPiece.transform.position.y, z));
					}
				}
			}
			if(z+1 < 8 && x-1 >= 0 && gameBoard[x-1, z+1] == 1){
				pieceMovements.Add(new Vector3(x-1, sPiece.transform.position.y, z+1));
			}
			if(z-1 >= 0 && x-1 >= 0 && gameBoard[x-1, z-1] == 1){
				pieceMovements.Add(new Vector3(x-1, sPiece.transform.position.y, z-1));
			}
			break;
		}
		return pieceMovements;
	}
	
	public List<Vector3> CheckBishopMovements(GameObject sPiece, int x, int z){
		List<Vector3> pieceMovements = new List<Vector3>();
		switch(sPiece.tag){
		case "White":
			for(int i = x+1, j = 1; i < 8 && z+j < 8; i++, j++){
				if(gameBoard[i, z+j] != 1){
					pieceMovements.Add(new Vector3(i, sPiece.transform.position.y, z+j));
					if(gameBoard[i, z+j] == 2){
						break;
					}
				}else{
					break;
				}
			}
			for(int i = x+1, j = 1; i < 8 && z-j >= 0; i++, j++){
				if(gameBoard[i, z-j] != 1){
					pieceMovements.Add(new Vector3(i, sPiece.transform.position.y, z-j));
					if(gameBoard[i, z-j] == 2){
						break;
					}
				}else{
					break;
				}
			}
			for(int i = x-1, j = 1; i >= 0 && z+j < 8; i--, j++){
				if(gameBoard[i, z+j] != 1){
					pieceMovements.Add(new Vector3(i, sPiece.transform.position.y, z+j));
					if(gameBoard[i, z+j] == 2){
						break;
					}
				}else{
					break;
				}
			}
			for(int i = x-1, j = 1; i >= 0 && z-j >= 0; i--, j++){
				if(gameBoard[i, z-j] != 1){
					pieceMovements.Add(new Vector3(i, sPiece.transform.position.y, z-j));
					if(gameBoard[i, z-j] == 2){
						break;
					}
				}else{
					break;
				}
			}
			break;
		case "Black":
			for(int i = x+1, j = 1; i < 8 && z+j < 8; i++, j++){
				if(gameBoard[i, z+j] != 2){
					pieceMovements.Add(new Vector3(i, sPiece.transform.position.y, z+j));
					if(gameBoard[i, z+j] == 1){
						break;
					}
				}else{
					break;
				}
			}
			for(int i = x+1, j = 1; i < 8 && z-j >= 0; i++, j++){
				if(gameBoard[i, z-j] != 2){
					pieceMovements.Add(new Vector3(i, sPiece.transform.position.y, z-j));
					if(gameBoard[i, z-j] == 1){
						break;
					}
				}else{
					break;
				}
			}
			for(int i = x-1, j = 1; i >= 0 && z+j < 8; i--, j++){
				if(gameBoard[i, z+j] != 2){
					pieceMovements.Add(new Vector3(i, sPiece.transform.position.y, z+j));
					if(gameBoard[i, z+j] == 1){
						break;
					}
				}else{
					break;
				}
			}
			for(int i = x-1, j = 1; i >= 0 && z-j >= 0; i--, j++){
				if(gameBoard[i, z-j] != 2){
					pieceMovements.Add(new Vector3(i, sPiece.transform.position.y, z-j));	
					if(gameBoard[i, z-j] == 1){
						break;
					}
				}else{
					break;
				}
			}
			break;
		}
		return pieceMovements;
	}
	
	public List<Vector3> CheckRookMovements(GameObject sPiece, int x, int z){
		List<Vector3> pieceMovements = new List<Vector3>();
		switch(sPiece.tag){
		case "White":
			for(int i = x+1; i < 8; i++){
				if(gameBoard[i, z] != 1){
					pieceMovements.Add(new Vector3(i, sPiece.transform.position.y, z));
					if(gameBoard[i, z] == 2){
						break;
					}
				}else{
					break;
				}
			}
			for(int i = x-1; i >= 0; i--){
				if(gameBoard[i, z] != 1){
					pieceMovements.Add(new Vector3(i, sPiece.transform.position.y, z));
					if(gameBoard[i, z] == 2){
						break;
					}
				}else{
					break;
				}
			}
			for(int j = z+1; j < 8; j++){
				if(gameBoard[x, j] != 1){
					pieceMovements.Add(new Vector3(x, sPiece.transform.position.y, j));
					if(gameBoard[x, j] == 2){
						break;
					}
				}else{
					break;
				}
			}
			for(int j = z-1; j >= 0; j--){
				if(gameBoard[x, j] != 1){
					pieceMovements.Add(new Vector3(x, sPiece.transform.position.y, j));	  
					if(gameBoard[x, j] == 2){
						break;
					}
				}else{
					break;
				}
			}
			break;
		case "Black":
			for(int i = x+1; i < 8; i++){
				if(gameBoard[i, z] != 2){
					pieceMovements.Add(new Vector3(i, sPiece.transform.position.y, z));
					if(gameBoard[i, z] == 1){
						break;
					}
				}else{
					break;
				}
			}
			for(int i = x-1; i >= 0; i--){
				if(gameBoard[i, z] != 2){
					pieceMovements.Add(new Vector3(i, sPiece.transform.position.y, z));
					if(gameBoard[i, z] == 1){
						break;
					}
				}else{
					break;
				}
			}
			for(int j = z+1; j < 8; j++){
				if(gameBoard[x, j] != 2){
					pieceMovements.Add(new Vector3(x, sPiece.transform.position.y, j));
					if(gameBoard[x, j] == 1){
						break;
					}
				}else{
					break;
				}
			}
			for(int j = z-1; j >= 0; j--){
				if(gameBoard[x, j] != 2){
					pieceMovements.Add(new Vector3(x, sPiece.transform.position.y, j));	
					if(gameBoard[x, j] == 1){
						break;
					}
				}else{
					break;
				}
			}
			break;
		}
		return pieceMovements;
	}
	
	public List<Vector3> CheckKingMovements(GameObject sPiece, int x, int z){
		List<Vector3> pieceMovements = new List<Vector3>();
		if (sPiece.tag == "White") {
			if(z+1 < 8 && gameBoard[x, z+1] != 1){
				pieceMovements.Add(new Vector3(x, sPiece.transform.position.y, z+1));
			}
			if(x+1 < 8 && z+1 < 8 && gameBoard[x+1, z+1] != 1){
				pieceMovements.Add(new Vector3(x+1, sPiece.transform.position.y, z+1));
			}
			if(z-1 >=0 && gameBoard[x, z-1] != 1){
				pieceMovements.Add(new Vector3(x, sPiece.transform.position.y, z-1));
			}
			if(z-1 >= 0 && x-1 >= 0 && gameBoard[x-1, z-1] != 1){
				pieceMovements.Add(new Vector3(x-1, sPiece.transform.position.y, z-1));
			}
			if(x+1 < 8 && gameBoard[x+1, z] != 1){
				pieceMovements.Add(new Vector3(x+1, sPiece.transform.position.y, z));
			}
			if(x-1 >= 0 && gameBoard[x-1, z] != 1){
				pieceMovements.Add(new Vector3(x-1, sPiece.transform.position.y, z));
			}
			if(x-1 >= 0 && z+1 < 8 && gameBoard[x-1, z+1] != 1){
				pieceMovements.Add(new Vector3(x-1, sPiece.transform.position.y, z+1));
			}
			if(x+1 < 8 && z-1 >= 0 && gameBoard[x+1, z-1] != 1){
				pieceMovements.Add(new Vector3(x+1, sPiece.transform.position.y, z-1));
			}

		}else if(sPiece.tag == "Black"){
			if(z+1 < 8 && gameBoard[x, z+1] != 2){
				pieceMovements.Add(new Vector3(x, sPiece.transform.position.y, z+1));
			}
			if(x+1 < 8 && z+1 < 8 && gameBoard[x+1, z+1] != 2){
				pieceMovements.Add(new Vector3(x+1, sPiece.transform.position.y, z+1));
			}
			if(z-1 >=0 && gameBoard[x, z-1] != 2){
				pieceMovements.Add(new Vector3(x, sPiece.transform.position.y, z-1));
			}
			if(z-1 >= 0 && x-1 >= 0 && gameBoard[x-1, z-1] != 2){
				pieceMovements.Add(new Vector3(x-1, sPiece.transform.position.y, z-1));
			}
			if(x+1 < 8 && gameBoard[x+1, z] != 2){
				pieceMovements.Add(new Vector3(x+1, sPiece.transform.position.y, z));
			}
			if(x-1 >= 0 && gameBoard[x-1, z] != 2){
				pieceMovements.Add(new Vector3(x-1, sPiece.transform.position.y, z));
			}
			if(x-1 >= 0 && z+1 < 8 && gameBoard[x-1, z+1] != 2){
				pieceMovements.Add(new Vector3(x-1, sPiece.transform.position.y, z+1));
			}
			if(x+1 < 8 && z-1 >= 0 && gameBoard[x+1, z-1] != 2){
				pieceMovements.Add(new Vector3(x+1, sPiece.transform.position.y, z-1));
			}
		}
		return pieceMovements;
	}
	
	public List<Vector3> CheckHorseMovements(GameObject sPiece, int x, int z){
		List<Vector3> pieceMovements = new List<Vector3>();
		if (sPiece.tag == "White") {
			if(x+2 < 8){
				if( z+1 < 8 && gameBoard[x+2, z+1] != 1){
					pieceMovements.Add(new Vector3(x+2, sPiece.transform.position.y, z+1));          
				}
				if(z-1 >= 0 && gameBoard[x+2, z-1] != 1){
					pieceMovements.Add(new Vector3(x+2, sPiece.transform.position.y, z-1));
				}
			}
			if(x-2 >= 0){
				if(z+1 < 8 && gameBoard[x-2, z+1] != 1){
					pieceMovements.Add(new Vector3(x-2, sPiece.transform.position.y, z+1));          
				}
				if(z-1 >= 0 && gameBoard[x-2, z-1] != 1){
					pieceMovements.Add(new Vector3(x-2, sPiece.transform.position.y, z-1));
				}
			}
			if(z+2 < 8){
				if(x+1 < 8 && gameBoard[x+1, z+2] != 1){
					pieceMovements.Add(new Vector3(x+1, sPiece.transform.position.y, z+2));          
				}
				if(x-1 >= 0 && gameBoard[x-1, z+2] != 1){
					pieceMovements.Add(new Vector3(x-1, sPiece.transform.position.y, z+2));
				}
			}
			if(z-2 >= 0){
				if(x+1 < 8 && gameBoard[x+1, z-2] != 1){
					pieceMovements.Add(new Vector3(x+1, sPiece.transform.position.y, z-2));          
				}
				if(x-1 >= 0 && gameBoard[x-1, z-2] != 1){
					pieceMovements.Add(new Vector3(x-1, sPiece.transform.position.y, z-2));
				}
			}
		}else if(sPiece.tag == "Black"){
			if(x+2 < 8){
				if(z+1 < 8 && gameBoard[x+2, z+1] != 2){
					pieceMovements.Add(new Vector3(x+2, sPiece.transform.position.y, z+1));          
				}
				if(z-1 >= 0 && gameBoard[x+2, z-1] != 2){
					pieceMovements.Add(new Vector3(x+2, sPiece.transform.position.y, z-1));
				}
			}
			if(x-2 >= 0){
				if(z+1 < 8 && gameBoard[x-2, z+1] != 2){
					pieceMovements.Add(new Vector3(x-2, sPiece.transform.position.y, z+1));          
				}
				if(z-1 >= 0 && gameBoard[x-2, z-1] != 2){
					pieceMovements.Add(new Vector3(x-2, sPiece.transform.position.y, z-1));
				}
			}
			if(z+2 < 8){
				if(x+1 < 8 && gameBoard[x+1, z+2] != 2){
					pieceMovements.Add(new Vector3(x+1, sPiece.transform.position.y, z+2));          
				}
				if(x-1 >= 0 && gameBoard[x-1, z+2] != 2){
					pieceMovements.Add(new Vector3(x-1, sPiece.transform.position.y, z+2));
				}
			}
			if(z-2 >= 0){
				if(x+1 < 8 && gameBoard[x+1, z-2] != 2){
					pieceMovements.Add(new Vector3(x+1, sPiece.transform.position.y, z-2));          
				}
				if(x-1 >= 0 && gameBoard[x-1, z-2] != 2){
					pieceMovements.Add(new Vector3(x-1, sPiece.transform.position.y, z-2));
				}
			}
		}	
		return pieceMovements;
	}
	
	public List<Vector3> CheckThreatMovements(GameObject threat, GameObject king){
		threatMovements.Clear ();
		int kX = (int)king.transform.position.x;
		int kZ = (int)king.transform.position.z;
		int tX = (int)threat.transform.position.x;
		int tZ = (int)threat.transform.position.z;
		threatMovements.Add (new Vector3(tX, threat.transform.position.y, tZ));
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
		foreach (Vector3 v3 in threatMovements)
			Debug.Log (v3);
		return threatMovements;
	}

	public void Promotion(GameObject pawn, GameObject promotedPiece){
		switch (pawn.tag) {
			case "White":
				selectedPiece = null;
				whitePieces.Remove(pawn);
				whitePieces.Add((GameObject)Instantiate (promotedPiece, pawn.transform.position, Quaternion.identity));
				Destroy (pawn);
				IsCheck("White", (int)wKing.transform.position.x, (int)wKing.transform.position.z);
				if(!isCheck)
					SetGameState(1);
				IsCheck ("Black", (int)bKing.transform.position.x, (int)bKing.transform.position.z);
				if(isCheck)
					IsCheckMate(bKing);
				break;
			case "Black":
				blackPieces.Remove(pawn);
				blackPieces.Add((GameObject)Instantiate (promotedPiece, pawn.transform.position, Quaternion.identity));
				Destroy (pawn);
				break;
		}
		isPromotion = false;
	}

	public void EnPassant(){

	}

	public void Castling(){

	}

	public List<Vector3> CheckPossibleMovements(GameObject sPiece){
		int x = (int)sPiece.transform.position.x;
		int z = (int)sPiece.transform.position.z;
		
		if(sPiece.name == "WhitePawn(Clone)" || sPiece.name == "BlackPawn(Clone)"){
			return CheckPawnMovements(sPiece, x, z);
		}
		
		if(sPiece.name == "WhiteBishop(Clone)" || sPiece.name == "BlackBishop(Clone)"){
			return CheckBishopMovements(sPiece, x, z);
		}
		
		if(sPiece.name == "WhiteRook(Clone)" || sPiece.name == "BlackRook(Clone)"){
			return CheckRookMovements(sPiece, x, z);
		}
		
		if (sPiece.name == "WhiteQueen(Clone)" || sPiece.name == "BlackQueen(Clone)") {
			List<Vector3> temp = new List<Vector3>();
			temp.AddRange(CheckBishopMovements(sPiece, x, z));
			temp.AddRange(CheckRookMovements(sPiece, x, z));
			/*foreach(Vector3 v3 in temp)
				Debug.Log (v3);*/
			return temp;
		}
		
		if (sPiece.name == "WhiteKing(Clone)" || sPiece.name == "BlackKing(Clone)") {
			return CheckKingMovements(sPiece, x, z);
		}
		
		if (sPiece.name == "WhiteHorse(Clone)" || sPiece.name == "BlackHorse(Clone)") {
			return CheckHorseMovements(sPiece, x, z);
		}
		
		return null;
	}
	
	public bool IsCheck(string tag, int kX, int kZ){
		possibleMovements.Clear ();
		isCheck = false;
		nThreats = 0;
		switch(tag){
		case "White":
			foreach(GameObject piece in blackPieces){		
				foreach(Vector3 v3 in CheckPossibleMovements(piece)){
					if(v3.x == kX && v3.z == kZ){
						threat = piece;
						nThreats++;
						Debug.Log(nThreats);
						Debug.Log("Check!");
						isCheck = true;
						return true;

					}
				}
			}
			break;
		case "Black":
			foreach(GameObject piece in whitePieces){		
				foreach(Vector3 v3 in CheckPossibleMovements(piece)){
					if(v3.x == bKing.transform.position.x && v3.z == bKing.transform.position.z){
						threat = piece;
						nThreats++;
						Debug.Log(nThreats);
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
		
		bool verifier = false;
		possibleMovements.Clear ();
		//Check if the King can escape moving;
		if(nThreats >= 1){
			switch(king.tag){
				case "White":
					foreach(GameObject piece in blackPieces){
						possibleMovements.AddRange(CheckPossibleMovements(piece));
					}
					break;
				case "Black":
					foreach(GameObject piece in whitePieces){
						possibleMovements.AddRange(CheckPossibleMovements(piece));
					}
					break;
			}
			foreach(Vector3 kM in CheckKingMovements(king, (int)king.transform.position.x, (int)king.transform.position.z)){
				foreach(Vector3 pM in possibleMovements){
					if(kM.x == pM.x && kM.z == pM.z){
						verifier = true;
						break;
					}else{
						verifier = false;
					}
				}	
			}
		}
		
		//Check if some piece can block the enemy one
		if (nThreats == 1) { 
			possibleMovements.Clear();
			possibleMovements.Add(threat.transform.position);
			switch(king.tag){
				case "White":
					foreach(GameObject piece in whitePieces){
						if(piece.name != "WhiteKing(Clone)")
							possibleMovements.AddRange(CheckPossibleMovements(piece));
					}	
					break;
				case "Black":
					foreach(GameObject piece in blackPieces){
						if(piece.name != "BlackKing(Clone)")
							possibleMovements.AddRange(CheckPossibleMovements(piece));
					}	
					break;
			}
			foreach(Vector3 tM in CheckThreatMovements(threat, king)){
				foreach(Vector3 pM in possibleMovements){
					if((tM.x == pM.x && tM.z == pM.z)){
						return false;
					}
				}
			}
			Debug.Log ("Checkmate!!");
			return true;
		}
		Debug.Log (verifier);
		return verifier;
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
		
		whitePieces.Add((GameObject)Instantiate (whiteRook, new Vector3 (0, whiteRook.transform.position.y, 0), Quaternion.Euler(0,-90,0)));
		whitePieces.Add((GameObject)Instantiate (whiteHorse, new Vector3 (0, whiteHorse.transform.position.y, 1), Quaternion.identity));
		whitePieces.Add((GameObject)Instantiate (whiteBishop, new Vector3 (0, whiteBishop.transform.position.y, 2), Quaternion.identity));
		whitePieces.Add((GameObject)Instantiate (whiteQueen, new Vector3 (0, whiteQueen.transform.position.y, 3), Quaternion.identity));
		wKing = (GameObject)Instantiate (whiteKing, new Vector3 (0, whiteKing.transform.position.y, 4), whiteKing.transform.rotation);
		whitePieces.Add (wKing);
		whitePieces.Add((GameObject)Instantiate (whiteBishop, new Vector3 (0, whiteBishop.transform.position.y, 5), Quaternion.identity));
		whitePieces.Add((GameObject)Instantiate (whiteHorse, new Vector3 (0, whiteHorse.transform.position.y, 6), Quaternion.identity));
		whitePieces.Add((GameObject)Instantiate (whiteRook, new Vector3 (0, whiteRook.transform.position.y, 7), Quaternion.Euler(0, -90, 0)));
		
		blackPieces.Add((GameObject)Instantiate (blackRook, new Vector3 (7, blackRook.transform.position.y, 0), Quaternion.identity));
		blackPieces.Add((GameObject)Instantiate (blackHorse, new Vector3 (7, blackHorse.transform.position.y, 1), Quaternion.identity));
		blackPieces.Add((GameObject)Instantiate (blackBishop, new Vector3 (7, blackBishop.transform.position.y, 2), Quaternion.identity));
		blackPieces.Add((GameObject)Instantiate (blackQueen, new Vector3 (7, blackQueen.transform.position.y, 3), Quaternion.identity));
		bKing = (GameObject)Instantiate (blackKing, new Vector3 (7, blackKing.transform.position.y, 4), blackKing.transform.rotation);
		blackPieces.Add (bKing);
		blackPieces.Add((GameObject)Instantiate (blackBishop, new Vector3 (7, blackBishop.transform.position.y, 5), Quaternion.identity));
		blackPieces.Add((GameObject)Instantiate (blackHorse, new Vector3 (7, blackHorse.transform.position.y, 6), Quaternion.identity));
		blackPieces.Add((GameObject)Instantiate (blackRook, new Vector3 (7, blackRook.transform.position.y, 7), Quaternion.identity));
		
	}
	
	//Return the piece selected atm
	public GameObject GetSelectedPiece(){
		return selectedPiece;
	}
	
	//Select the piece
	public void SelectPiece(GameObject piece){

		Debug.Log("Animation_Selection"); //Play animation(selection)
		possibleMovements.Clear();
		selectedPiece = piece;
		Debug.Log (selectedPiece.name + " is selected");
	}
	
	//Move the piece selected
	public void MovePiece(Vector3 coordToMove){
		bool verifier = true;
		switch(GetSelectedPiece().tag){
		case "White":
			if(IsCheck("White", (int)wKing.transform.position.x, (int)wKing.transform.position.z) && !IsCheckMate(wKing)){
				if(GetSelectedPiece().name == "WhiteKing(Clone)"){
					foreach(GameObject bPiece in blackPieces){
						foreach(Vector3 v3 in CheckPossibleMovements(bPiece)){
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
						Debug.Log(v3);
						if(coordToMove.x == v3.x && coordToMove.z == v3.z){
							verifier = true;
							break;
						}
					}
				}
			}
			if(verifier){	
				foreach(Vector3 v3 in CheckPossibleMovements(GetSelectedPiece())){
					if(v3.x == coordToMove.x && v3.z == coordToMove.z){
						switch(gameBoard[(int)coordToMove.x, (int)coordToMove.z]){
							case 0:
								gameBoard[(int)selectedPiece.transform.position.x, (int)selectedPiece.transform.position.z] = 0;
								gameBoard[(int)coordToMove.x, (int)coordToMove.z] = 1;
								if(GetSelectedPiece().name == "WhiteKing(Clone)"){
									if(IsCheck("White", (int)coordToMove.x, (int)coordToMove.z)){
										gameBoard[(int)selectedPiece.transform.position.x, (int)selectedPiece.transform.position.z] = 1;
										gameBoard[(int)coordToMove.x, (int)coordToMove.z] = 0;
										break;
									}
								}else{
									if(IsCheck("White", (int)wKing.transform.position.x, (int)wKing.transform.position.z)){
										gameBoard[(int)selectedPiece.transform.position.x, (int)selectedPiece.transform.position.z] = 1;
										gameBoard[(int)coordToMove.x, (int)coordToMove.z] = 0;
										break;
									}
								}
								GetSelectedPiece().transform.position = coordToMove;
								//WhitePiecesController wPC = GetSelectedPiece().GetComponent<WhitePiecesController>();
								//wPC.coordToMove = coordToMove;
								Debug.Log ("Animation_moving"); //Play animation(moving)
								if(GetSelectedPiece().name == "WhitePawn(Clone)" && GetSelectedPiece().transform.position.x == 7){
									isPromotion = true;
									SetGameState(2);
									return;
								}
								IsCheck("White", (int)wKing.transform.position.x, (int)wKing.transform.position.z);
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
								if(GetSelectedPiece().name == "WhitePawn(Clone)" && GetSelectedPiece().transform.position.x == 7){
									isPromotion = true;
									SetGameState(2);
									return;
								}
								IsCheck("White", (int)wKing.transform.position.x, (int)wKing.transform.position.z);
								selectedPiece = null;
								break;
						}
						//Debug.Log(IsCheck("Black"));
						//Debug.Log (IsCheckMate(wKing));
							if(!isCheck)
								SetGameState(1);
							IsCheck ("Black", (int)bKing.transform.position.x, (int)bKing.transform.position.z);
							if(isCheck)
								IsCheckMate(bKing);
							break;
					}
				}
			}
			break;
		case "Black":	
			//Debug.Log(IsCheck("White", (int)wKing.transform.position.x, (int)wKing.transform.position.z));
			if(IsCheck("Black", (int)bKing.transform.position.x, (int)bKing.transform.position.z) && !IsCheckMate(bKing)){
				if(GetSelectedPiece().name == "BlackKing(Clone)"){
					foreach(GameObject wPiece in whitePieces){
						foreach(Vector3 v3 in CheckPossibleMovements(wPiece)){
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
				foreach(Vector3 v3 in CheckPossibleMovements(GetSelectedPiece())){				
					if(v3.x == coordToMove.x && v3.z == coordToMove.z){	
						switch(gameBoard[(int)coordToMove.x, (int)coordToMove.z]){
						case 0:
							gameBoard[(int)selectedPiece.transform.position.x, (int)selectedPiece.transform.position.z] = 0;
							gameBoard[(int)coordToMove.x, (int)coordToMove.z] = 2;
							if(GetSelectedPiece().name == "BlackKing(Clone)"){
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
							selectedPiece.transform.position = coordToMove;
							Debug.Log ("Animation_moving"); //Play animation(moving)
							if(GetSelectedPiece().name == "BlackPawn(Clone)" && GetSelectedPiece().transform.position.x == 0){
								isPromotion = true;
								SetGameState(2);
								return;
							}
							IsCheck("Black", (int)bKing.transform.position.x, (int)bKing.transform.position.z);
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
							if(GetSelectedPiece().name == "BlackPawn(Clone)" && GetSelectedPiece().transform.position.x == 0){
								isPromotion = true;
								SetGameState(2);
								return;
							}
							IsCheck("Black", (int)bKing.transform.position.x, (int)bKing.transform.position.z);
							selectedPiece = null;
							break;
						}
						//Debug.Log (IsCheckMate(bKing));
						if(!isCheck)
							SetGameState(0);
						IsCheck ("White", (int)wKing.transform.position.x, (int)wKing.transform.position.z);
						if(isCheck)
							IsCheckMate(wKing);
						break;
					}
				}
			}
			break;
		}
	}
}
