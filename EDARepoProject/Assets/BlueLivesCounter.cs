//Michael Sorger Code

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class BlueLivesCounter : MonoBehaviour
{

    private Text livesText;
    // Use this for initialization
    void Awake()
    {
        livesText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        livesText.text = "BLUE: " + GameMaster.getBlueTeamLives().ToString();
    }
}
