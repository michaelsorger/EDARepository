
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Internals
    //private float cooldown = 0f;
    private HealthBarScript _healthBarScript; //reference to the health bar script
    private float currentHealth = 0f;
	private float maxHealth = 15f;
    private bool playerControl = true;
    private GameObject healthBar; //this is for the entire health bar (border, red, green)
    private GameObject greenBar; //this is for just for scaling the GREEN portion of the health bar down)
    //private GameObject _health = null;
	// Use this for initialization
	void Start ()
    {
        _healthBarScript = gameObject.GetComponent<HealthBarScript>();
        currentHealth = maxHealth;
        greenBar = GetComponentInChildren<Canvas>().transform.Find("Border").Find("Bar").gameObject;
        healthBar = GetComponentInChildren<Canvas>().gameObject;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (playerControl == false)
        {
			Debug.Log("What should happen here");
        }
        
        //GameObject.Find("Health").GetComponent<Text>().text = currentHealth.ToString();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Damaging")
        {
            playerDamage(5);            
        }	
    }

    private void playerDamage(float damage)
    {
        currentHealth -= damage;
        float normHealth = currentHealth / maxHealth; //if current health is 66/100 = .66f (normalized health for setHealth function)
        _healthBarScript.setHealthBar(normHealth, greenBar);
        //GameObject.Find("Health").GetComponent<Text>().text = currentHealth.ToString();

        if(currentHealth <= 0)
        {
            playerControl = false;
            _healthBarScript.setHealthBar(0f, greenBar);
        }
    }
}
