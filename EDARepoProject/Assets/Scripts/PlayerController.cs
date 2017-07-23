//Michael Sorger Code

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
    //private AttackScripts _attackScripts;
    public float currentHealth = 0f;
	public float maxHealth = 15f;
    private bool playerControl = true;
    //public GameObject HealthBar; //this is for the entire health bar (border, red, green)
    private GameObject greenBar; //this is for just for scaling the GREEN portion of the health bar down)
    //private Vector2 knockRight = new Vector2(1, 1);
    //private Vector2 knockLeft = new Vector2(-1, 1);
    //private GameObject _health = null;
	// Use this for initialization
    private Transform initialTransform;
    private Vector3 initialPlayerSpawn;
    private CharacterController2D pController;

	void Awake ()
    {
        _healthBarScript = gameObject.GetComponent<HealthBarScript>();
        currentHealth = maxHealth;
        greenBar = GetComponentInChildren<Canvas>().transform.Find("Border").Find("Bar").gameObject;
       // HealthBar = GetComponentInChildren<Canvas>().gameObject;
        initialTransform = gameObject.GetComponent<Transform>();
        initialPlayerSpawn = new Vector3(initialTransform.position.x, initialTransform.position.y, initialTransform.position.z);
        pController = gameObject.GetComponent<CharacterController2D>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //GameObject.Find("Health").GetComponent<Text>().text = currentHealth.ToString();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
       // string bulletTriggerChild = GameObject.Find("FullGunnerBullet").transform.Find("GunnerBulletTrigger").tag;
        string bTriggerChild = gameObject.GetComponent<AttackScripts>().bulletTrigger.tag;
        //runs into spike
        if(col.tag == "Damaging")
        {
            playerDamage(3);            
        }
        else if(col.tag == "Red Brute Bat")
        {
            playerDamage(5);
        }
        else if(col.tag == "Blue Brute Bat")
        {
            playerDamage(5);
        }
        else if (col.tag == bTriggerChild)
        {
            playerDamage(10);
        }
    }

    private void OnCollisionEnter2D(Collision2D bullet)
    {
        Debug.Log("bullet collided with me!");
        if(bullet.transform.tag == "GunnerBullet")
        {
            Destroy(bullet.collider.gameObject);
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
            GameMaster.killPlayer(this.gameObject, gameObject.tag ,initialPlayerSpawn);
        }
    }

    public void playerInit()
    {
        playerDamage(0);
    }
}
