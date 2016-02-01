using UnityEngine;
using System.Collections;

public class DialogueTextArray {

    private string buildingName;
    private string buildingDescription;
    private string buildingTextOne;
    private string buildingTextTwo;
    private string buildingTextThree;
    private string buildingRequires;
    private string buildingHasMaterials;

    public DialogueTextArray (string buildingName, string buildingDescription, string buildingTextOne, 
        string buildingTextTwo, string buildingTextThree, string buildingRequires, string buildingHasMaterials) 
    {
        this.buildingName = buildingName;    
        this.buildingDescription = buildingDescription;
        this.buildingTextOne = buildingTextOne;
        this.buildingTextTwo = buildingTextTwo;
        this.buildingTextThree = buildingTextThree;
        this.buildingRequires = buildingRequires;
        this.buildingHasMaterials = buildingHasMaterials;
    }

    public string[] GetStrings() {
        return new string[] {this.buildingName, this.buildingDescription, this.buildingTextOne, this.buildingTextTwo, 
            this.buildingTextThree, this.buildingRequires, this.buildingHasMaterials
        };
    }
}
