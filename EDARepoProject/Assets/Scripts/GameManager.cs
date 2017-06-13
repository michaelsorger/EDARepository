using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void restartLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void exitLevel()
    {

    }
}
