using UnityEngine;
using System.Collections;

public class UserInput : MonoBehaviour {

	private bool dragging = false;
	private bool inViewport = false;
	private Vector3 dragOrigin;
	private Vector3 dragMovement;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		doMouse();
	}

	private void doMouse(){
		Vector3 mouseViewPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
		float zoomLevel = Camera.main.orthographicSize;
		if(dragging && Input.GetMouseButtonUp(0)){
			dragging = false;
		}
		Vector3 lastCameraPos = new Vector3();
		if(dragging){
//			Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector3 mouseDragDelta = dragOrigin - mouseViewPos;
//			float dragSpeed = 62f;
			float dragSpeed = (62f / 32f) * zoomLevel;
			Vector3 move = new Vector3(mouseDragDelta.x * dragSpeed, 0, mouseDragDelta.y * dragSpeed);
			move = Quaternion.Euler(0,45,0) * move;
			move += dragMovement / 2;
			lastCameraPos = Camera.main.transform.position;
			Camera.main.transform.Translate(move,Space.World);

		}

		inViewport = mouseViewPos.x > 0 && mouseViewPos.y > 0 && mouseViewPos.x < 1 && mouseViewPos.y < 1;
		Vector2 scroll = Input.mouseScrollDelta;
		if(inViewport && scroll.y != 0) {
			if(scroll.y == -1 && zoomLevel < 32){ //zoom out
				Camera.main.orthographicSize += zoomLevel;
			}
			if(scroll.y == 1 && zoomLevel > 1){  //zoom in
				Camera.main.orthographicSize -= Mathf.Ceil(zoomLevel / 2);
			}
		}

		if(!dragging && Input.GetMouseButtonDown(0) && inViewport){
			dragging = true;
//			dragOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			dragOrigin = mouseViewPos;
			dragMovement = new Vector3();
			lastCameraPos = Camera.main.transform.position;
		}
		dragMovement += lastCameraPos - Camera.main.transform.position;
	}

	public void TileButtonClick(){
		print("clicked tile button");
	}
}
