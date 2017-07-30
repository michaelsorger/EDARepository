using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuControllerInput : MonoBehaviour {

    private GameObject currentButton;
    private AxisEventData currentAxis;

    //timer
    public float timeBetweenInputs = 0.15f; //in seconds
    public float deadZone = 0.9f;
    private float timer = 0;

     void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0)
        {
            currentAxis = new AxisEventData(EventSystem.current);
            currentButton = EventSystem.current.currentSelectedGameObject;

            if (Input.GetAxis("VerticalDpad") > deadZone) // move up
            {
                //Debug.Log("vertical val is = " + Input.GetAxis("VerticalDpad"));
                currentAxis.moveDir = MoveDirection.Up;
                ExecuteEvents.Execute(currentButton, currentAxis, ExecuteEvents.moveHandler);
            }
            else if (Input.GetAxis("VerticalDpad") < -deadZone) // move down
            {
                currentAxis.moveDir = MoveDirection.Down;
                ExecuteEvents.Execute(currentButton, currentAxis, ExecuteEvents.moveHandler);
            }
            else if (Input.GetAxis("HorizontalDpad") > deadZone) // move right
            {
                currentAxis.moveDir = MoveDirection.Right;
                ExecuteEvents.Execute(currentButton, currentAxis, ExecuteEvents.moveHandler);
            }
            else if (Input.GetAxis("HorizontalDpad") < -deadZone) // move left
            {
                currentAxis.moveDir = MoveDirection.Left;
                ExecuteEvents.Execute(currentButton, currentAxis, ExecuteEvents.moveHandler);
            }
            timer = timeBetweenInputs;
        }

        //timer counting down
        timer -= Time.fixedDeltaTime;
    }
}
