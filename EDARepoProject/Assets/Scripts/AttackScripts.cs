using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScripts : MonoBehaviour {

	public Rigidbody2D bulletPrefab;
	public Transform bulletSpawn;

	private float attackSpeed = 1f;
	private AnimInputController _inputController;

	// Use this for initialization
	void Start ()
	{
		_inputController = GetComponent<AnimInputController>();
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
}
