using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using StrategyGame;

public class MouseInput : MonoBehaviour {

    private Camera mainCamera;
    private Vector3 dragWorldOrigin;
    private Vector3 dragScreenOrigin;
    private bool dragging = false;
    private float minDrag;

    void Start () {
        mainCamera = Camera.main;
        minDrag = StrategyGame.GameResources.MinDragDistance;
    }

    void OnMouseDown () {
        if (StrategyGame.GameResources.UIHovering) {
            dragging = false;
        } else {
            dragging = true;
            Vector3 cursorPoint = GetCursorPoint ();
            dragWorldOrigin = cursorPoint;
            dragScreenOrigin = mainCamera.WorldToViewportPoint (cursorPoint);
        }
    }

    void OnMouseDrag () {
        if (dragging && IsMouseOverThisGameObject ()) {
            Vector3 currentPos = GetCursorPoint ();
            Vector3 posDelta = new Vector3 () - (currentPos - dragWorldOrigin);
            Camera.main.transform.Translate (posDelta, Space.World);
        }
    }

    void OnMouseUp () {
        Vector3 cursorPoint = GetCursorPoint ();
        Vector3 currentScreenPos = mainCamera.WorldToViewportPoint (cursorPoint);
        float delta = Vector3.Distance (dragScreenOrigin, currentScreenPos);
        if (dragging && (delta < minDrag)) {
            TileMap t = (TileMap)GetComponent<TileMap> ();
            t.Click (cursorPoint);
        }
        dragging = false;
    }

    RaycastHit GetRaycastHit () {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast (ray, out hit);
        return hit;
    }

    Vector3 GetCursorPoint () {
        return GetRaycastHit ().point;
    }

    bool IsMouseOverThisGameObject () {
        Collider hitCollider = GetRaycastHit ().collider;
        if (hitCollider) {
            if (hitCollider.gameObject == this.gameObject) {
                return true;
            }
        }
        return false;
    }
}
