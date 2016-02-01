using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameTime : MonoBehaviour {

    public Text GameTimeText;

    private float myTime = 0f;
    private int elapsedGameHours = 0;
    private float timeScale = 3600f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        myTime = Time.time;
        elapsedGameHours = Mathf.FloorToInt((myTime * timeScale) / 3600);
        GameTimeText.text = elapsedGameHours.ToString ();
    }
}
