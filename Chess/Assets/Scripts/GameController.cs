using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	private int[,] gameBoard;

	public GameObject blackPawn, blackRook, blackHorse, blackBishop, blackQueen, blackKing;

	public GameObject whitePawn, whiteRook, whiteHorse, whiteBishop, whiteQueen, whiteKing;

	private GameObject selectedPiece;

	// Use this for initialization
	void Start () {	
		initializeGameBoard ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void initializeGameBoard(){
		gameBoard = new int[8, 8];
		for (int i = 0; i < 8; i++) {
			gameBoard[1,i] = 1;	Instantiate (whitePawn, new Vector3 (1, whitePawn.transform.position.y, i), Quaternion.identity);
			gameBoard[6,i] = 2;	Instantiate (blackPawn, new Vector3 (6, whitePawn.transform.position.y, i), Quaternion.identity);
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

		Instantiate (whiteRook, new Vector3 (0, whiteRook.transform.position.y, 0), Quaternion.identity);
		Instantiate (whiteHorse, new Vector3 (0, whiteHorse.transform.position.y, 1), Quaternion.identity);
		Instantiate (whiteBishop, new Vector3 (0, whiteBishop.transform.position.y, 2), Quaternion.identity);
		Instantiate (whiteQueen, new Vector3 (0, whiteQueen.transform.position.y, 3), Quaternion.identity);
		Instantiate (whiteKing, new Vector3 (0, whiteKing.transform.position.y, 4), whiteKing.transform.rotation);
		Instantiate (whiteBishop, new Vector3 (0, whiteBishop.transform.position.y, 5), Quaternion.identity);
		Instantiate (whiteHorse, new Vector3 (0, whiteHorse.transform.position.y, 6), Quaternion.identity);
		Instantiate (whiteRook, new Vector3 (0, whiteRook.transform.position.y, 7), Quaternion.identity);
		
		Instantiate (blackRook, new Vector3 (7, blackRook.transform.position.y, 0), Quaternion.identity);
		Instantiate (blackHorse, new Vector3 (7, blackHorse.transform.position.y, 1), Quaternion.identity);
		Instantiate (blackBishop, new Vector3 (7, blackBishop.transform.position.y, 2), Quaternion.identity);
		Instantiate (blackQueen, new Vector3 (7, blackQueen.transform.position.y, 3), Quaternion.identity);
		Instantiate (blackKing, new Vector3 (7, blackKing.transform.position.y, 4), blackKing.transform.rotation);
		Instantiate (blackBishop, new Vector3 (7, blackBishop.transform.position.y, 5), Quaternion.identity);
		Instantiate (blackHorse, new Vector3 (7, blackHorse.transform.position.y, 6), Quaternion.identity);
		Instantiate (blackRook, new Vector3 (7, blackRook.transform.position.y, 7), Quaternion.identity);

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
		switch(getSelectedPiece().tag){
			case "White":
				WhitePiecesController whitePieceController = selectedPiece.GetComponent<WhitePiecesController> ();
				if(whitePieceController.IsMoveValid(coordToMove)){
					switch(gameBoard[(int)coordToMove.x, (int)coordToMove.z]){
						case 0:
							gameBoard[(int)selectedPiece.transform.position.x, (int)selectedPiece.transform.position.z] = 0;
							gameBoard[(int)coordToMove.x, (int)coordToMove.z] = 1;
							selectedPiece.transform.position = coordToMove;
							Debug.Log ("Animation_moving"); //Play animation(moving)
							selectedPiece = null;
							break;

						case 1:
							break;

						case 2:
							gameBoard[(int)selectedPiece.transform.position.x, (int)selectedPiece.transform.position.z] = 0;
							gameBoard[(int)coordToMove.x, (int)coordToMove.z] = 1;
							selectedPiece.transform.position = coordToMove;
							Debug.Log ("Animation_moving"); //Play animation(moving)
							Debug.Log ("Animation_eating"); //Play animation(eating)
							selectedPiece = null;
							break;
					}
				}
				break;
			case "Black":	
				BlackPiecesController blackPieceController = selectedPiece.GetComponent<BlackPiecesController> ();
				if(blackPieceController.IsMoveValid(coordToMove)){
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
							selectedPiece.transform.position = coordToMove;
							Debug.Log ("Animation_moving"); //Play animation(moving)
							Debug.Log ("Animation_eating"); //Play animation(eating)
							selectedPiece = null;
							break;

						case 2:
							break;
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
