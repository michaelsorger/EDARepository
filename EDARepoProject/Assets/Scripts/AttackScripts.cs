//Michael Sorger code

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScripts : MonoBehaviour {

	public GameObject bulletPrefab;
    public Rigidbody2D bulletTrigger;
    public Rigidbody2D bulletPlatform;
	public Transform bulletSpawn;
    public BoxCollider2D batCollider;
    public AudioSource audio1;
    public AudioSource audio2;
    public AudioSource audio3;

    //   private float attackSpeed = 1f;
    private AnimInputController _inputController;
    private Vector2 originalBatSize;
    private Vector2 originalBatOffset;
    private GameObject bPrefab;
    private Rigidbody2D triggerPrefab;
    private Rigidbody2D platformPrefab;
    

    // Use this for initialization
    void Start ()
	{
        _inputController = GetComponent<AnimInputController>();
        originalBatSize = batCollider.GetComponent<BoxCollider2D>().size;
        originalBatOffset = batCollider.GetComponent<BoxCollider2D>().offset;
        batCollider.enabled = false;

        AudioSource[] audioSources = GetComponents<AudioSource>();
        audio1 = audioSources[0];
        audio2 = audioSources[1];
        audio3 = audioSources[2];
    }
	
	// Update is called once per frame
	void Update ()
	{

    }


	private void Shoot()
	{
        // Debug.Log("Started bullet a shot!");
        Debug.Log("Input Facing" + _inputController.getFacing());
        //if the player is facing to the right.
        if (_inputController.getFacing() == "Right")
		{
			bPrefab = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
            triggerPrefab = Instantiate(bulletTrigger, bulletSpawn.position, Quaternion.identity) as Rigidbody2D;
            triggerPrefab.transform.parent = bPrefab.transform;

            platformPrefab = Instantiate(bulletPlatform, bulletSpawn.position, Quaternion.identity) as Rigidbody2D;
            platformPrefab.transform.parent = bPrefab.transform;

            triggerPrefab.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 500, ForceMode2D.Force);
            platformPrefab.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 500, ForceMode2D.Force);

            //cooldown = Time.time + attackSpeed;
        }
		else
		{
            //otherwise we are facing left
            bPrefab = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity); //instantiate parent does not have a sprite component attatched

            triggerPrefab = Instantiate(bulletTrigger, bulletSpawn.position, Quaternion.identity) as Rigidbody2D; //instantiate child 1? 
            triggerPrefab.transform.parent = bPrefab.transform;

            platformPrefab = Instantiate(bulletPlatform, bulletSpawn.position, Quaternion.identity) as Rigidbody2D; //instanitate child 2
            platformPrefab.transform.parent = bPrefab.transform;

            triggerPrefab.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 500, ForceMode2D.Force); //add force to child trigger component
            platformPrefab.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 500, ForceMode2D.Force); //add force to other child platform component 
        }
        
    }

	public void fireCrossBow()
	{
		Debug.Log("Fire CrossBow called22");
		Shoot();
	}


    public void swingBaseBallBat()
    {
        //change shape collider
        batCollider.GetComponent<BoxCollider2D>().size = new Vector2(originalBatSize.x + .35f, originalBatSize.y - .3f);
        batCollider.GetComponent<BoxCollider2D>().offset = new Vector2(originalBatOffset.x - .35f, originalBatOffset.y - .15f);
        // Debug.Log("boxCollider x = " + batCollider.GetComponent<BoxCollider2D>().size.x + " BoxCollider y = " + batCollider.GetComponent<BoxCollider2D>().size.y);
        //what it hitsnsfo
        audio2.Play();

    }

    public void batInactive()
    {
        batCollider.enabled = false;
    }

    public void batActive()
    {
        batCollider.GetComponent<BoxCollider2D>().size = new Vector2(originalBatSize.x, originalBatSize.y);
        batCollider.GetComponent<BoxCollider2D>().offset = new Vector2(originalBatOffset.x, originalBatOffset.y);
       // Debug.Log("boxCollider x = " + batCollider.GetComponent<BoxCollider2D>().size.x + " BoxCollider y = " + batCollider.GetComponent<BoxCollider2D>().size.y);
        batCollider.enabled = true;
        
    }

    public void fireMachineGun()
    {
        audio3.Play();
        Shoot();
    }

    public void fireBow()
    {
        //audio3.Play();
        Shoot();
    }

}
