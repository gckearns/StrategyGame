using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

public class UIMenu : MonoBehaviour {

    private static UIMenu buildMenu;
    private static BuildingType[] buildings = WorldController.buildingTypes;

    public GameObject BuildMenu;
    public ModalPanel BuildPanel;
    public RectTransform content;


    public void PopulateMenu (int bldgCategory) {
        ClearMenu ();
        BuildingType[] bldgs = BuildingManager.GetBuildingsOfCategory(bldgCategory);
        for (int i = 0; i < bldgs.Length; i++) {
            ModalPanel modalPanel = Instantiate<ModalPanel> (BuildPanel);    
            modalPanel.transform.SetParent (content);
            modalPanel.GetComponent <RectTransform> ().anchoredPosition3D = new Vector3 (0, -254 * i, 0);
            modalPanel.GetComponent <RectTransform> ().sizeDelta = new Vector2 (0, 254);
            modalPanel.BuildDialogue (GetBuildingDialogueInfo (bldgs[i]).GetStrings (), 
                modalPanel.iconImage.sprite, GetBuildAction (i), GetBuildAction (i));
        }
        content.sizeDelta = new Vector2(0, 254 * bldgs.Length);
        gameObject.SetActive (true);
    }

    static BuildingType[] GetBuildings(int type){
        
        return buildings;
    }

    void ClearMenu () {
        for (int i = 0; i < content.childCount; i++) {
            GameObject g = content.GetChild (i).gameObject;
            g.SetActive (false);
        }
    }

    UnityAction GetBuildAction (int building) {
        return delegate{OnBuildClicked(building);};
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
        InventoryItem[] yTypes = bldg.yieldTypes.ToArray ();
        float[] yNums = bldg.yieldAmounts;
        string yieldString = "Yields: ";
        for (int i = 0; i < yTypes.Length; i++) {
            yieldString += yTypes [i].itemName + ": " + yNums [i] + " ";
        }
        InventoryItem[] costTypes = bldg.costTypes.ToArray ();
        int[] costNums = bldg.costAmounts;
        string costString = "Requires: ";
        for (int i = 0; i < costTypes.Length; i++) {
            costString += "(" + costNums[i] + " " + costTypes[i].itemName + ") ";
        }
        return new DialogueTextArray (name, desc, details, yTime, yieldString, costString, "Have: (99 somthing)");
    }
}
