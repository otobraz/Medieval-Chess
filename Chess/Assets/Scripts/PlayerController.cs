using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private Camera camera;
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

		// On Left Click
		if(Input.GetMouseButtonDown(0))
		{
			_ray = camera.ScreenPointToRay(Input.mousePosition); // Specify the ray to be casted from the position of the mouse click
			
			// Raycast and verify that it collided
			if(Physics.Raycast (_ray,out _hitInfo))
			{
				// Select the piece if the collider has a piece Tag
				if(_hitInfo.collider.gameObject.tag == ("White"))
				{
					gameController.SelectedPiece(_hitInfo.collider.gameObject);
				}else if(_hitInfo.collider.gameObject.tag == "Cube" && gameController.getSelectedPiece() != null){ //if the collider has a cube Tag && if a piece is selected, it moves the piece to the cube position
					Vector3 selectedCoord = new Vector3(_hitInfo.collider.gameObject.transform.position.x, 0.8f, _hitInfo.collider.gameObject.transform.position.z); //get the position of the click.
					gameController.MovePiece(selectedCoord);
				}
			}
		}
	}
}
