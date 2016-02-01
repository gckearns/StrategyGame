using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

//  This script will be updated in Part 2 of this 2 part series.
public class ModalPanel : MonoBehaviour {

    public Text buildingName;
    public Image iconImage;
    public Text buildingInfo;
    public Text textOne;
    public Text textTwo;
    public Text textThree;
    public Text textRequires;
    public Text textHas;
    public Button yesButton;
    public Button cancelButton;

    // Yes/No/Cancel: A string, a Yes event, a No event and Cancel event
    public void BuildDialogue (string[] dialogueStrings, Sprite icon, UnityAction buildEvent, UnityAction cancelEvent) {
        this.buildingName.text = dialogueStrings [0];
        this.buildingInfo.text = dialogueStrings [1];
        this.textOne.text = dialogueStrings [2];
        this.textTwo.text = dialogueStrings [3];
        this.textThree.text = dialogueStrings [4];
        this.textRequires.text = dialogueStrings [5];
        this.textHas.text = dialogueStrings [6];

        this.iconImage.sprite = icon;

        yesButton.onClick.RemoveAllListeners();
        yesButton.onClick.AddListener (buildEvent);
        yesButton.onClick.AddListener (ClosePanel);

        cancelButton.onClick.RemoveAllListeners();
        cancelButton.onClick.AddListener (cancelEvent);
        cancelButton.onClick.AddListener (ClosePanel);

        gameObject.SetActive (true);
    }

    void ClosePanel () {
        gameObject.SetActive (false);
    }
}