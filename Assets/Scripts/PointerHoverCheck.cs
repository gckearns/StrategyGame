using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using StrategyGame;

public class PointerHoverCheck : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public void OnPointerEnter (PointerEventData eventData){
        StrategyGame.GameResources.UIHovering = true;
	}

    public void OnPointerExit (PointerEventData eventData){
        StrategyGame.GameResources.UIHovering = false;
	}
}
