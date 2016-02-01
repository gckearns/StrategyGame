using UnityEngine;
using System.Collections;
using StrategyGame;

public class CameraZoom : MonoBehaviour {
    public float minZoomLevel;
    public float maxZoomLevel;
    private bool overUi;

	// Update is called once per frame
	void Update () {
        MouseActivity ();
	}

    void MouseActivity(){
        bool inViewport = false;
        overUi = GameResources.UIHovering;
        Vector3 viewportPos = Camera.main.ScreenToViewportPoint (Input.mousePosition);
        if (viewportPos.x > 0 && viewportPos.x < 1 && viewportPos.y > 0 && viewportPos.y < 1) {
            inViewport = true;
        }
            
        float scrollDelta = Input.mouseScrollDelta.y;
        if (inViewport && !overUi && scrollDelta != 0) {
            float zoomLevel = Camera.main.orthographicSize;
            switch ((int) scrollDelta) {
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
