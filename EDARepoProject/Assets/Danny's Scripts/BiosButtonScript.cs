﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BiosButtonScript : MonoBehaviour 
{
	public void NewGameButton(string newGameLevel)
	{
        UnityEngine.SceneManagement.SceneManager.LoadScene(newGameLevel);
    }


}
