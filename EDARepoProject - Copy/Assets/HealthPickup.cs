using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour {

    public float respawnTick = 25f; //in seconds 
    public float timer = 0;
    private bool isDestroyed = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (isDestroyed)
        {            
            if (timer == 0)
            {
                GameObject newHealthPickup = Instantiate(this.gameObject);
            }
            timer -= Time.fixedDeltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        isDestroyed = true;
        timer = respawnTick;
        Destroy(gameObject);
    }
}
