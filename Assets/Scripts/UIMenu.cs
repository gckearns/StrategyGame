using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

public class UIMenu : MonoBehaviour {

    private static UIMenu buildMenu;

    public GameObject BuildMenu;
    public GameObject BuildMenuItem;
    public ModalPanel BuildPanel;
    public RectTransform content;

    public void PopulateMenu (int menu) {
        gameObject.SetActive (true);
        switch (menu) {
        default:
            break;
        }
        WorldController wc = WorldController.Instance ();
        BuildingType bld = wc.buildingTypes [0];
        UnityAction buildAction = new UnityAction (delegate{OnBuildClicked(1);});
        UnityAction cancelAction = new UnityAction (delegate{OnBuildClicked(2);});
        UnityAction buildAction2 = new UnityAction (delegate{OnBuildClicked(3);});
        UnityAction cancelAction2 = new UnityAction (delegate{OnBuildClicked(4);});
        ModalPanel modalPanel = Instantiate<ModalPanel> (BuildPanel);
        modalPanel.transform.SetParent (content);
        modalPanel.GetComponent <RectTransform> ().anchoredPosition3D = new Vector3 (0, 0, 0);
        modalPanel.GetComponent <RectTransform> ().sizeDelta = new Vector2 (0, 254);
        modalPanel.BuildDialogue (GetBuildingDialogueInfo (bld).GetStrings (), modalPanel.iconImage.sprite, buildAction, cancelAction);
        ModalPanel modalPanel2 = Instantiate<ModalPanel> (BuildPanel);
        modalPanel2.transform.SetParent (content);
        modalPanel2.GetComponent <RectTransform> ().anchoredPosition3D = new Vector3 (0, -254, 0);
        modalPanel2.GetComponent <RectTransform> ().sizeDelta = new Vector2 (0, 254);
        modalPanel2.BuildDialogue (GetBuildingDialogueInfo (bld).GetStrings (), modalPanel2.iconImage.sprite, buildAction2, cancelAction2);
        GetComponent<ScrollRect> ().Rebuild (CanvasUpdate.Layout);
        content.sizeDelta = new Vector2(0, 254 * 2);
    }

    public void OnBuildClicked(int id){
        print (id); // Close the UI Menu
        gameObject.SetActive (false);
    }

    public DialogueTextArray GetBuildingDialogueInfo(BuildingType bldg) {
        string name = bldg.buildingType;
        Sprite icon = bldg.icon;
        string desc = bldg.description;
        int size = bldg.size;
        int pwr = bldg.powerRequirement;
        int jobs = bldg.population;
        string details = "Size: " + size + "x" + size + "  Pwr: " + pwr + "  Jobs: " + jobs;
        string yTime = "Yield time: " + bldg.yieldFrequency;
        InventoryItem[] yTypes = bldg.yieldTypes;
        float[] yNums = bldg.yieldAmounts;
        string yieldString = "Yields: ";
        for (int i = 0; i < yTypes.Length; i++) {
            yieldString += yTypes [i].itemName;
//                + ": " + yNums[i] + " " ;
        }
        InventoryItem[] costTypes = bldg.costTypes;
        int[] costNums = bldg.costAmounts;
        string costString = "Requires: ";
        for (int i = 0; i < costTypes.Length; i++) {
            costString += "(" + costNums[i] + " " + costTypes[i].itemName + ") ";
        }
        return new DialogueTextArray (name, desc, details, yTime, yieldString, costString, "Have: (99 somthing)");
    }
}
