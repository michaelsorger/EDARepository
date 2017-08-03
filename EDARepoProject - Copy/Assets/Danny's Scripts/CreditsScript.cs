using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsScript : MonoBehaviour {

    public GameObject canvas;
    public int speed = 1;
    public string level;
	
	// Update is called once per frame
	void Update () {

        canvas.transform.Translate(Vector3.down * Time.deltaTime * speed);
	}

    IEnumerator waitFor()
    {
        yield return new WaitForSeconds(20);
        Application.LoadLevel(level);
    }
}
