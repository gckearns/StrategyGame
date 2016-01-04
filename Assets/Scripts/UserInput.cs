using UnityEngine;
using System.Collections;

public class UserInput : MonoBehaviour {

    private bool dragging = false;
    private Vector3 dragOrigin;
    public float minZoomLevel = 0.5f;
    public float maxZoomLevel = 32f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        MouseActivity ();
	}

    void MouseActivity(){
        if (dragging && Input.GetMouseButtonUp (0)) {
            dragging = false;
        }

        if (dragging) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast (ray, out hit);
            if (hit.collider) {
                Vector3 cursorPos = hit.point;
                Vector3 posDelta = cursorPos - dragOrigin;
                posDelta = new Vector3 () - posDelta;
                Camera.main.transform.Translate (posDelta, Space.World);
            }
        }

        if (Input.GetMouseButtonDown (0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast (ray, out hit);
            if (hit.collider) {
                dragging = true;
                dragOrigin = hit.point;
            }
        }
        bool inViewport = false;
        Vector3 viewportPos = Camera.main.ScreenToViewportPoint (Input.mousePosition);
        if (viewportPos.x > 0 && viewportPos.x < 1 && viewportPos.y > 0 && viewportPos.y < 1) {
            inViewport = true;
        }
        if (inViewport == true && Input.mouseScrollDelta.y != 0) {
            float zoomLevel = Camera.main.orthographicSize;
            switch ((int) Input.mouseScrollDelta.y) {
            case 1: // Scrolled up, zoom in
                if (zoomLevel > minZoomLevel) {
                    Camera.main.orthographicSize = zoomLevel / 2f;
                }
                break;
            case -1: // Scrolled down, zoom out
                if (zoomLevel < maxZoomLevel) {
                    Camera.main.orthographicSize = zoomLevel * 2f;
                }
                break;
            default:
                break;
            };
        }
    }
}
