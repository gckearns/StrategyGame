using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class ModalMenuButton : MonoBehaviour {

    public Text menuButtonText;
    public Button menuButton;

    // Yes/No/Cancel: A string, a Yes event, a No event and Cancel event
    public void BuildMenus (string buttonText, UnityAction buttonEvent) {
        this.menuButtonText.text = buttonText;

        menuButton.onClick.RemoveAllListeners();
        menuButton.onClick.AddListener (buttonEvent);
        menuButton.onClick.AddListener (ClosePanel);

        gameObject.SetActive (true);
    }

    void ClosePanel () {
        transform.parent.gameObject.SetActive (false);
    }
}