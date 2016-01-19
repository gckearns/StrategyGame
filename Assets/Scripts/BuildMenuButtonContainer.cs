using UnityEngine;
using System.Collections;

public class BuildMenuButtonContainer : MonoBehaviour {

    public GameObject menuButtonContainer;

    private static BuildMenuButtonContainer menuButtons;

    public static BuildMenuButtonContainer Instance () {
        if (!menuButtons) {
            menuButtons = FindObjectOfType(typeof (BuildMenuButtonContainer)) as BuildMenuButtonContainer;
            if (!menuButtons)
                Debug.LogError ("There needs to be one active BuildMenuButtonContainer script on a GameObject in your scene.");
        }

        return menuButtons;
    }

    public void Activate () {
//        print ("activate menu");
        menuButtonContainer.SetActive (true);
    }

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
