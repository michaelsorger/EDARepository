//Michael Sorger code

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class RedLivesCounter : MonoBehaviour {

    private Text livesTextRed;
	// Use this for initialization
	void Awake ()
    {
        livesTextRed = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        livesTextRed.text = "RED: " + GameMaster.getRedTeamLives().ToString();
	}
}
