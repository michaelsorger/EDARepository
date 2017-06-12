using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerandHealth : MonoBehaviour
{
    public GameObject player;
    public GameObject healthBar;
    public Vector3 offset;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        healthBar.transform.localPosition = player.transform.localPosition + offset;
	}
}
