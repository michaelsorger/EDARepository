using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	public enum Direction {
		left,
		right,
	}

	private Direction playerFacing;

    //Members of this class
    public float walkSpeed = 3f;
    public float jumpHeight = 10f;
    public float gravity = -35f;
    public float maxHealth = 15f;
    public GameObject healthBar;
    public string horizontalCtrl = "Horizontal_p1";
    public string jumpButton = "Jump_p1";
    public string fireButton = "Fire1_p1";
    public Rigidbody2D bulletPrefab;
	public Transform bulletSpawn;
    public GameObject gameOverPanel;

    //Internals
    private float attackSpeed = 1f;
    private float cooldown = 0f;
    private CharacterController2D _controller;
    private AnimationController2D _animator;
    private float currentHealth = 0f;
    private string playerDirection = "";
    private bool playerControl = true;
    private bool shootControl = true;
	// Use this for initialization
	void Start ()
    {
        _controller = gameObject.GetComponent<CharacterController2D>();
        _animator = gameObject.GetComponent<AnimationController2D>();
        currentHealth = maxHealth;
		playerFacing = Direction.left;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (playerControl)
        {
            Vector3 velocity = playerInput(horizontalCtrl, jumpButton);
            velocity.x *= 0.90f;
            velocity.y += gravity * Time.deltaTime;

            _controller.move(velocity * Time.deltaTime);

            // Debug.Log(playerCtrl.getPlayerDirection().ToString());
            if (Time.time >= cooldown)
            {
                if (Input.GetAxis(fireButton) > 0)
                {
                    _animator.setAnimation("CrossBowShoot");
                }
            }
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

    private Vector3 playerInput(string horizontal, string jump)
    {
        Vector3 velocity = _controller.velocity;

        //velocity.x = 0;

        if (Input.GetAxis(horizontal) < 0)
        {
            velocity.x = -walkSpeed;
            if (_controller.isGrounded)
            {
                _animator.setAnimation("CrossBowRun");
            }
            _animator.setFacing("Left");
			playerFacing = Direction.left;
        }
        else if (Input.GetAxis(horizontal) > 0)
        {
            velocity.x = walkSpeed;
            if (_controller.isGrounded)
            {
                _animator.setAnimation("CrossBowRun");
            }
            _animator.setFacing("Right");
			playerFacing = Direction.right;
        }
        else if(_animator.getAnimation() == "CrossBowShoot")
        {

        }
        else
        {
            //play idle anim
            _animator.setAnimation("CrossBowIdle");
        }

        if (Input.GetAxis(jump) > 0 && _controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(2f * jumpHeight * -gravity);
            //play jump animation
            _animator.setAnimation("CrossBowIdle");
        }
        //if fire button (space or left mouse) is pressed
        return velocity;
    }

    private void playerDamage(float damage)
    {
        currentHealth -= damage;
        float normHealth = currentHealth / maxHealth; //if current health is 66/100 = .66f (normalized health for setHealth function)
        setHealthBar(normHealth);
        //GameObject.Find("Health").GetComponent<Text>().text = currentHealth.ToString();

        if(currentHealth <= 0)
        {
            playerControl = false;
            setHealthBar(0f);
            _animator.setAnimation("CrossBowIdle");
            gameOverPanel.SetActive(true);
        }
    }
 
    public void setHealthBar(float myHealth)
    {
        //myHealth needs to be value between 0-1, thats why we transformed above to use localScale
        healthBar.transform.localScale = new Vector3(myHealth, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
    }

    private void Shoot()
    {
        // Debug.Log("Started bullet a shot!");
        //if the player is facing to the right.
		if (playerFacing == Direction.right)
        {
            Rigidbody2D bPrefab = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity) as Rigidbody2D;
			bPrefab.GetComponent<Rigidbody2D> ().AddForce (Vector2.right*500, ForceMode2D.Force);

            cooldown = Time.time + attackSpeed;
        }
        else
        {
            //otherwise we are facing left
            Rigidbody2D bPrefab = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity) as Rigidbody2D;
			bPrefab.GetComponent<Rigidbody2D> ().AddForce (Vector2.left*500, ForceMode2D.Force);
            

            cooldown = Time.time + attackSpeed;
        }
   }

    public void fireCrossBow()
    {
        Debug.Log("Fire CrossBow called");
        Shoot();
    }
}
