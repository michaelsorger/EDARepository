using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScripts : MonoBehaviour {

	public Rigidbody2D bulletPrefab;
	public Transform bulletSpawn;
    public BoxCollider2D batCollider;

    private float attackSpeed = 1f;
	private AnimInputController _inputController;
    private Vector2 originalBatSize;
    private Vector2 originalBatOffset;

    // Use this for initialization
    void Start ()
	{
		_inputController = GetComponent<AnimInputController>();
        originalBatSize = batCollider.GetComponent<BoxCollider2D>().size;
        originalBatOffset = batCollider.GetComponent<BoxCollider2D>().offset;
        batCollider.enabled = false;
    }
	
	// Update is called once per frame
	void Update ()
	{
        
    }

	private void Shoot()
	{
		// Debug.Log("Started bullet a shot!");
		//if the player is facing to the right.
		if (_inputController.getFacing() == "Right")
		{
			Rigidbody2D bPrefab = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity) as Rigidbody2D;
			bPrefab.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 500, ForceMode2D.Force);

			//cooldown = Time.time + attackSpeed;
		}
		else
		{
			//otherwise we are facing left
			Rigidbody2D bPrefab = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity) as Rigidbody2D;
			bPrefab.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 500, ForceMode2D.Force);

			//cooldown = Time.time + attackSpeed;
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
        Debug.Log("boxCollider x = " + batCollider.GetComponent<BoxCollider2D>().size.x + " BoxCollider y = " + batCollider.GetComponent<BoxCollider2D>().size.y);
       //what it hitsnsfo
    }

    public void batInactive()
    {
        batCollider.enabled = false;
    }

    public void batActive()
    {
        batCollider.GetComponent<BoxCollider2D>().size = new Vector2(originalBatSize.x, originalBatSize.y);
        batCollider.GetComponent<BoxCollider2D>().offset = new Vector2(originalBatOffset.x, originalBatOffset.y);
        Debug.Log("boxCollider x = " + batCollider.GetComponent<BoxCollider2D>().size.x + " BoxCollider y = " + batCollider.GetComponent<BoxCollider2D>().size.y);
        batCollider.enabled = true;
    }

}
