//Michael Sorger code

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreenManager : MonoBehaviour {

    public void MainMenu()
    {
        Debug.Log("Main menu clicked");
    }

    public void Rematch()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Rematch button clicked");
    }
}
