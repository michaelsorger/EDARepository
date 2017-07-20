//Michael Sorger code

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarScript : MonoBehaviour {

 //   public GameObject health_bar;
    private Transform barTransform;
    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {

    }

    public void setHealthBar(float myHealth, GameObject green_bar)
    {
        //myHealth needs to be value between 0-1, thats why we transformed above to use localScale
        green_bar.transform.localScale = new Vector3(myHealth, green_bar.transform.localScale.y, green_bar.transform.localScale.z);
    }

}
