using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private Camera camera;
	public Transform target;
	private GameController gameController;
	
	// Use this for initialization
	void Start () {
		camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>(); // Find the Camera's GameObject from its tag
		gameController = gameObject.GetComponent<GameController>();
		if (gameController == null) {
			Debug.Log("It was not possible to get GameController script");		
		}
	}
	
	// Update is called once per frame
	void Update() {
		GetInput();
	}

	void GetInput(){

		Ray _ray;	
		RaycastHit _hitInfo;

		if (gameController.GetGameState() == 0) {
			if(camera.transform.eulerAngles.y > 90.1f){
				camera.transform.RotateAround (target.position,new Vector3(0.0f,1.0f,0.0f),-20 * Time.deltaTime * 10);
			}else{
				camera.transform.rotation = Quaternion.Euler(50, 90, 0);
				camera.transform.position = new Vector3(-3f, 5.8f, 3.5f);
			}
			if(Input.GetMouseButtonDown(0))	{
				_ray = camera.ScreenPointToRay(Input.mousePosition); // Specify the ray to be casted from the position of the mouse click	
				if(Physics.Raycast (_ray,out _hitInfo)){ 	// Raycast and verify that it collided
					if(_hitInfo.collider.gameObject.tag == ("White")){ // Select the piece if the collider has a piece Tag
						gameController.SelectPiece(_hitInfo.collider.gameObject);
					}else if((_hitInfo.collider.gameObject.tag == "Cube" || _hitInfo.collider.gameObject.tag == "Black") && gameController.GetSelectedPiece() != null){ //if the collider has a cube Tag && if a piece is selected, it moves the piece to the cube position
						Vector3 selectedCoord = new Vector3(_hitInfo.collider.gameObject.transform.position.x, 0.8f, _hitInfo.collider.gameObject.transform.position.z); //get the position of the click.
						gameController.MovePiece(selectedCoord);
					}
				}
			}
		}else if(gameController.GetGameState() == 1){
			if(camera.transform.eulerAngles.y < 270){
				camera.transform.RotateAround (target.position,new Vector3(0.0f,1.0f,0.0f),20 * Time.deltaTime * 10);
			}else{
				camera.transform.rotation = Quaternion.Euler(50, 270, 0);
				camera.transform.position = new Vector3(10f, 5.8f, 3.5f);
			}
			if(Input.GetMouseButtonDown(0))	{
				_ray = camera.ScreenPointToRay(Input.mousePosition); // Specify the ray to be casted from the position of the mouse click	
				if(Physics.Raycast (_ray,out _hitInfo)){ // Raycast and verify that it collided
					if(_hitInfo.collider.gameObject.tag == ("Black")){ // Select the piece if the collider has a piece Tag
						gameController.SelectPiece(_hitInfo.collider.gameObject);
					}else if((_hitInfo.collider.gameObject.tag == "Cube" || _hitInfo.collider.gameObject.tag == "White") && gameController.GetSelectedPiece() != null){ //if the collider has a cube Tag && if a piece is selected, it moves the piece to the cube position
						Vector3 selectedCoord = new Vector3(_hitInfo.collider.gameObject.transform.position.x, 0.8f, _hitInfo.collider.gameObject.transform.position.z); //get the position of the click.
						gameController.MovePiece(selectedCoord);
					}
				}
			}
		}

	}
}
