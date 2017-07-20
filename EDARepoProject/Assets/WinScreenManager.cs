//Michael Sorger code

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinScreenManager : MonoBehaviour {

    public void MainMenu()
    {
        Debug.Log("Main menu clicked");
    }

    public void Rematch()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Rematch button clicked");
    }
}
