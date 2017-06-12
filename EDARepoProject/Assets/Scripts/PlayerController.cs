using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 3;
    public float jumpHeight = 10;
    public float gravity = -35;
    public int playerHealth = 15;

    private CharacterController2D _controller;
    private AnimationController2D _animator;
    private int currentHealth = 0;

    private bool playerControl = true;
	// Use this for initialization
	void Start ()
    {
        _controller = gameObject.GetComponent<CharacterController2D>();
        _animator = gameObject.GetComponent<AnimationController2D>();
        currentHealth = playerHealth;

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (playerControl)
        {
            Vector3 velocity = playerInput();
            velocity.x *= 0.90f;
            velocity.y += gravity * Time.deltaTime;

            _controller.move(velocity * Time.deltaTime);
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

    private Vector3 playerInput()
    {
        Vector3 velocity = _controller.velocity;

        //velocity.x = 0;

        if (Input.GetAxis("Horizontal") < 0)
        {
            velocity.x = -walkSpeed;
            if (_controller.isGrounded)
            {
                _animator.setAnimation("playerRun");
            }
            _animator.setFacing("Left");
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            velocity.x = walkSpeed;
            if (_controller.isGrounded)
            {
                _animator.setAnimation("playerRun");
            }
            _animator.setFacing("Right");
        }
        else
        {
            //play idle anim
            _animator.setAnimation("playerIdle");
        }

        if (Input.GetAxis("Jump") > 0 && _controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(2f * jumpHeight * -gravity);
            //play jump animation
            _animator.setAnimation("playerJump");
        }
        return velocity;
    }

    private void playerDamage(int damage)
    {
        currentHealth -= damage;
        GameObject.Find("Health").GetComponent<Text>().text = currentHealth.ToString();

        if(currentHealth <= 0)
        {
            playerControl = false;
            _animator.setAnimation("playerDeath");
        }

    }
}
