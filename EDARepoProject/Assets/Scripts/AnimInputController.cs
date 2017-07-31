//Michael Sorger code

using Prime31;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimInputController : MonoBehaviour
{
	//Level design adjustment settings
	public float jumpHeight = 20f;
	public float gravity = -35f;
	public float maxSpeed = 3f;

    public string Left_Joystick_Axis = "";
    public string Right_Trigger_Axis = "";
    public string A_Axis = "";
    public string B_Axis = "";

    //Control settings
    //Gravity = 0, dead = 0.19, sensitivity = 1 
    private float movementAxisf; //X Axis (left joystick)
	private float attackAxisf; //3rd Axis (right trigger)
	private float jumpAxisf; //Joystick button 0	(A)		float because return value 0 or !0
	private float specialAxisf; //Joystick button 1 (B)  float because return value 0 or !0

	private Animator _anim;
	private CharacterController2D _controller;
	private string oldStateName = "";
	//Setting player direction
	private string _currentDirection = "Left";

    private float move = 0f;

    private Vector3 velocity = new Vector3(0, 0, 0);

	void Start()
	{
        //getAxisNamesFromPlayerPrefs()

		_anim = GetComponent<Animator>();
		_controller = GetComponent<CharacterController2D>();
	}
	void Update()
	{
		movementAxisf = Input.GetAxis(Left_Joystick_Axis);
		_anim.SetFloat("runF", movementAxisf);
		//Debug.Log(_anim.GetFloat("runF"));

		attackAxisf = Input.GetAxis(Right_Trigger_Axis);
		_anim.SetFloat("attackF", attackAxisf);

		jumpAxisf = Input.GetAxis(A_Axis);
        if(jumpAxisf > 0)
        {
            _anim.SetBool("isJumping", true);
        }
        else
        {
            _anim.SetBool("isJumping", false);
        }

        specialAxisf = Input.GetAxis(B_Axis);

		string statename = GetCurrentAnimatorStateName();  //function to return current state of animator
		if (statename != oldStateName)
		{
			//Debug.Log("statename = " + statename);
			oldStateName = statename;
		}
		switch (statename)
		{
			case "Idle":
				velocity.x = 0;
				velocity.y += gravity * Time.deltaTime; //add gravity
                                                        //if input is Jump while in idle
                if (_controller.collisionState.hasCollision() && _controller.collisionState.above)
				{
					velocity.y = 0;
				}

				_controller.move(velocity * Time.deltaTime); //move my guy

				break;

			case "Run":
				//Vector3 velocity = _controller.velocity; //charactercontroller2d velocity
				//Vector3 velocity = new Vector3(movementAxisf, 0, 0);
				velocity.x = movementAxisf * maxSpeed;

				if (_controller.isGrounded)
				{
					velocity.y = 0f;
				}
				
				//input is run
					//move
				move = movementAxisf; //get xBox controller input value

				_anim.SetFloat("runF", move); //set the float, runs through set conditions from animator
            
                //check player facing
                if (move < -0.5f) //left
				{
					setFacing("Left");
				}
				else if (move > 0.5f) //right
				{
				    setFacing("Right");
				}
				else
				{
					//Do nothing (I have this because controller always sending data)
				}

				//move my player
				//velocity.x *= 0.90f; //friction calc
				if (_controller.collisionState.hasCollision() && _controller.collisionState.above)
				{
					velocity.y = 0;
				}
				velocity.y += gravity * Time.deltaTime; //add gravity
				

				_controller.move(velocity * Time.deltaTime); //move my guy based on jumped/fell
				break;

			case "Attack":
                //I can move when I fire
                if (_controller.isGrounded)
                {
                    velocity.y = 0f;
                }
                velocity.x = movementAxisf * maxSpeed;

                //move
                move = movementAxisf; //get xBox controller input value

                _anim.SetFloat("runF", move); //set the float, runs through set conditions from animator

                //check player facing
                if (move < -0.5f) //left
                {
                    setFacing("Left");
                }
                else if (move > 0.5f) //right
                {
                    setFacing("Right");
                }
                else
                {
                    //Do nothing (I have this because controller always sending data)
                }

                //move my player
                //velocity.x *= 0.90f; //friction calc
                if (_controller.collisionState.hasCollision() && _controller.collisionState.above)
                {
                    velocity.y = 0;
                }


                //attack

                velocity.y += gravity * Time.deltaTime; //add gravity
				_controller.move(velocity * Time.deltaTime); //jump my guy
				break;

			case "Jump":
                
                //if input is Jump
                gameObject.GetComponent<AttackScripts>().batInactive();
                if (_controller.isGrounded)
                {
                    velocity.y = 0f;
                }
                if (jumpAxisf > 0 && _controller.isGrounded)
                {
                    //jump
                    gameObject.GetComponent<AudioSource>().Play();
                    velocity.y = jumpHeight; //Mathf.Sqrt(2f * jumpHeight * -gravity);
                    _anim.SetBool("isJumping", true);
                }
                if (_controller.collisionState.hasCollision() && _controller.collisionState.above)
                {
                    velocity.y = 0;
                }
                velocity.y += gravity * Time.deltaTime; //add gravity
                _controller.move(velocity * Time.deltaTime); //move my guy based on jumped/fell
              //  _anim.SetBool("isJumping", false); //idle animation when jump
                break;
		}
	}

	private string GetCurrentAnimatorStateName()
	{
		string sName = "";
		if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
		{
			//Do something if this particular state is palying'
			sName = "Idle";
			return sName;
		}
		else if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Run"))
		{
			//Do something if this particular state is palying
			sName = "Run";
			return sName;
		}
		else if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
		{
			//Do something if this particular state is palying
			sName = "Attack";
			return sName;
		}
        else if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            //Do something if this particular state is palying
            sName = "Jump";
            return sName;
        }
        else
		{
			return sName;
		}
	}
	public string getFacing()
	{
		return _currentDirection;
	}

	private void setFacing(string newDirection)
	{
		//Check that were' not already facing the new direction
		if (newDirection != _currentDirection)
		{
			//Flip the character sprite
			this.transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

			//Update the member variable that tracks the facing direction
			_currentDirection = newDirection;
		}
	}


}

