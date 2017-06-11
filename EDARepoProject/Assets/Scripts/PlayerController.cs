using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 3;
    public float jumpHeight = 10;
    public float gravity = -35;

    private CharacterController2D _controller;

	// Use this for initialization
	void Start ()
    {
        _controller = gameObject.GetComponent<CharacterController2D>();

    }
	
	// Update is called once per frame
	void Update ()
    {

        Vector3 velocity = _controller.velocity;

        //velocity.x = 0;

        if (Input.GetAxis("Horizontal") < 0)
        {
            velocity.x = -walkSpeed;
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            velocity.x = walkSpeed;
        }

        if (Input.GetAxis("Jump") > 0 && _controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(2f * jumpHeight * -gravity);
        }

        velocity.x *= 0.90f;
        velocity.y += gravity * Time.deltaTime;

        _controller.move(velocity * Time.deltaTime);
    }
}
